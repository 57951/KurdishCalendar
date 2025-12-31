using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Represents a date in the Kurdish calendar using simplified Nowruz calculation (21 March).
  /// For astronomical precision based on actual spring equinox, use <see cref="KurdishAstronomicalDate"/>.
  /// The Kurdish calendar is based on the Solar Hijri system with Nowruz (spring equinox) as the start of the year.
  /// </summary>
  public readonly struct KurdishDate : IKurdishDate, IComparable, IComparable<KurdishDate>, IEquatable<KurdishDate>
  {
    /// <summary>
    /// Gets the year component of the date.
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Gets the month component of the date (1-12).
    /// </summary>
    public int Month { get; }

    /// <summary>
    /// Gets the day component of the date.
    /// </summary>
    public int Day { get; }

    /// <summary>
    /// Initialises a new instance of the <see cref="KurdishDate"/> struct.
    /// </summary>
    /// <param name="year">The year (must be 1 or greater).</param>
    /// <param name="month">The month (1-12).</param>
    /// <param name="day">The day.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any component is out of valid range.</exception>
    public KurdishDate(int year, int month, int day)
    {
      SolarHijriCalculator.ValidateKurdishDate(year, month, day);

      Year = year;
      Month = month;
      Day = day;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="KurdishDate"/> struct from a Gregorian DateTime.
    /// </summary>
    /// <param name="gregorianDate">The Gregorian date to convert.</param>
    public KurdishDate(DateTime gregorianDate)
    {
      (int year, int month, int day) = SolarHijriCalculator.FromGregorian(gregorianDate);
      Year = year;
      Month = month;
      Day = day;
    }

    /// <summary>
    /// Gets the Kurdish date for today (based on UTC).
    /// </summary>
    public static KurdishDate Today
    {
      get { return new KurdishDate(DateTime.UtcNow.Date); }
    }

    /// <summary>
    /// Gets the Kurdish date for the current moment (based on UTC).
    /// </summary>
    public static KurdishDate Now
    {
      get { return new KurdishDate(DateTime.UtcNow); }
    }

    /// <summary>
    /// Gets the day of the week for this Kurdish date.
    /// </summary>
    public DayOfWeek DayOfWeek
    {
      get { return ToDateTime().DayOfWeek; }
    }

    /// <summary>
    /// Gets the day of the year (1-365 or 1-366 in leap years).
    /// </summary>
    public int DayOfYear
    {
      get
      {
        int dayOfYear = 0;
        for (int m = 1; m < Month; m++)
        {
          dayOfYear += SolarHijriCalculator.GetDaysInMonth(m, Year);
        }
        dayOfYear += Day;
        return dayOfYear;
      }
    }

    /// <summary>
    /// Determines whether the year of this Kurdish date is a leap year.
    /// </summary>
    public bool IsLeapYear
    {
      get { return SolarHijriCalculator.IsLeapYear(Year); }
    }

    /// <summary>
    /// Creates a <see cref="KurdishDate"/> from a Gregorian DateTime.
    /// </summary>
    /// <param name="gregorianDate">The Gregorian date.</param>
    /// <returns>The corresponding Kurdish date.</returns>
    public static KurdishDate FromDateTime(DateTime gregorianDate)
    {
      return new KurdishDate(gregorianDate);
    }

    /// <summary>
    /// Converts this Kurdish date to Gregorian DateTime.
    /// </summary>
    /// <returns>The corresponding Gregorian DateTime.</returns>
    public DateTime ToDateTime()
    {
      return SolarHijriCalculator.ToGregorian(Year, Month, Day);
    }

    /// <summary>
    /// Converts to <see cref="KurdishAstronomicalDate"/> preserving year, month, and day values.
    /// This is a lossless conversion that only changes the type.
    /// Use <see cref="ToAstronomicalRecalculated"/> if you need to account for 
    /// differences in Nowruz dates between standard and astronomical calculations.
    /// </summary>
    public KurdishAstronomicalDate ToAstronomical()
    {
      return KurdishAstronomicalDate.FromErbil(Year, Month, Day);
    }

    /// <summary>
    /// Converts to <see cref="KurdishAstronomicalDate"/> by recalculating through Gregorian.
    /// This accounts for the fact that simplified dates use fixed 21 March for Nowruz,
    /// while astronomical dates use the actual spring equinox, which can vary by 1-2 days.
    /// </summary>
    public KurdishAstronomicalDate ToAstronomicalRecalculated()
    {
      DateTime gregorian = ToDateTime();
      return KurdishAstronomicalDate.FromDateTime(gregorian);
    }

    /// <summary>
    /// Converts to <see cref="KurdishAstronomicalDate"/> using a specific longitude.
    /// </summary>
    public KurdishAstronomicalDate ToAstronomical(double longitudeDegrees)
    {
      return KurdishAstronomicalDate.FromLongitude(Year, Month, Day, longitudeDegrees);
    }

    /// <summary>
    /// Adds the specified number of days to this date.
    /// </summary>
    /// <param name="days">The number of days to add (can be negative).</param>
    /// <returns>A new <see cref="KurdishDate"/> representing the result.</returns>
    public KurdishDate AddDays(int days)
    {
      DateTime gregorian = ToDateTime().AddDays(days);
      return new KurdishDate(gregorian);
    }

    /// <summary>
    /// Adds the specified number of months to this date.
    /// </summary>
    /// <param name="months">The number of months to add (can be negative).</param>
    /// <returns>A new <see cref="KurdishDate"/> representing the result.</returns>
    public KurdishDate AddMonths(int months)
    {
      int newYear = Year;
      int newMonth = Month + months;

      while (newMonth > 12)
      {
        newMonth -= 12;
        newYear++;
      }

      while (newMonth < 1)
      {
        newMonth += 12;
        newYear--;
      }

      int maxDays = SolarHijriCalculator.GetDaysInMonth(newMonth, newYear);
      int newDay = Day > maxDays ? maxDays : Day;

      return new KurdishDate(newYear, newMonth, newDay);
    }

    /// <summary>
    /// Adds the specified number of years to this date.
    /// </summary>
    /// <param name="years">The number of years to add (can be negative).</param>
    /// <returns>A new <see cref="KurdishDate"/> representing the result.</returns>
    public KurdishDate AddYears(int years)
    {
      int newYear = Year + years;
      int maxDays = SolarHijriCalculator.GetDaysInMonth(Month, newYear);
      int newDay = Day > maxDays ? maxDays : Day;

      return new KurdishDate(newYear, Month, newDay);
    }

    /// <summary>
    /// Calculates the number of days between this date and another.
    /// </summary>
    /// <param name="other">The other date to compare to.</param>
    /// <returns>The number of days difference (can be negative).</returns>
    public int DaysDifference(KurdishDate other)
    {
      return (ToDateTime() - other.ToDateTime()).Days;
    }

    /// <summary>
    /// Returns a string representation of this date in short format with Sorani Latin.
    /// </summary>
    public override string ToString()
    {
      return KurdishDateFormatter.Format(this, null, KurdishDialect.SoraniLatin, null);
    }

    /// <summary>
    /// Returns a string representation with the specified format.
    /// </summary>
    public string ToString(string? format)
    {
      return KurdishDateFormatter.Format(this, format, KurdishDialect.SoraniLatin, null);
    }

    /// <summary>
    /// Returns a string representation with the specified format and dialect.
    /// </summary>
    public string ToString(string? format, KurdishDialect dialect)
    {
      return KurdishDateFormatter.Format(this, format, dialect, null);
    }

    /// <summary>
    /// Returns a string representation with custom format, dialect, and text direction.
    /// </summary>
    public string ToString(string? format, KurdishDialect dialect, KurdishTextDirection? textDirection)
    {
      return KurdishDateFormatter.Format(this, format, dialect, textDirection);
    }

    // IKurdishDate implementation
    int IComparable<IKurdishDate>.CompareTo(IKurdishDate? other)
    {
      if (other == null) return 1;
      return ToDateTime().CompareTo(other.ToDateTime());
    }

    bool IEquatable<IKurdishDate>.Equals(IKurdishDate? other)
    {
      if (other == null) return false;
      return ToDateTime() == other.ToDateTime();
    }

    // Comparison operators

    /// <summary>
    /// Compares this instance to another <see cref="KurdishDate"/>.
    /// </summary>
    /// <param name="other">The date to compare with.</param>
    /// <returns>A value indicating the relative order of the dates.</returns>
    public int CompareTo(KurdishDate other)
    {
      if (Year != other.Year) return Year.CompareTo(other.Year);
      if (Month != other.Month) return Month.CompareTo(other.Month);
      return Day.CompareTo(other.Day);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
      if (obj == null) return 1;
      if (obj is KurdishDate other) return CompareTo(other);
      throw new ArgumentException("Object must be of type KurdishDate.", nameof(obj));
    }

    /// <inheritdoc/>
    public bool Equals(KurdishDate other)
    {
      return Year == other.Year && Month == other.Month && Day == other.Day;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
      return obj is KurdishDate other && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(Year, Month, Day);
    }

    /// <summary>
    /// Determines whether two <see cref="KurdishDate"/> instances are equal.
    /// </summary>
    public static bool operator ==(KurdishDate left, KurdishDate right)
    {
      return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="KurdishDate"/> instances are not equal.
    /// </summary>
    public static bool operator !=(KurdishDate left, KurdishDate right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishDate"/> is less than another.
    /// </summary>
    public static bool operator <(KurdishDate left, KurdishDate right)
    {
      return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishDate"/> is greater than another.
    /// </summary>
    public static bool operator >(KurdishDate left, KurdishDate right)
    {
      return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishDate"/> is less than or equal to another.
    /// </summary>
    public static bool operator <=(KurdishDate left, KurdishDate right)
    {
      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishDate"/> is greater than or equal to another.
    /// </summary>
    public static bool operator >=(KurdishDate left, KurdishDate right)
    {
      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Implicit conversion from DateTime to KurdishDate.
    /// </summary>
    public static implicit operator KurdishDate(DateTime dateTime)
    {
      return new KurdishDate(dateTime);
    }

    /// <summary>
    /// Explicit conversion from KurdishDate to DateTime.
    /// </summary>
    public static explicit operator DateTime(KurdishDate kurdishDate)
    {
      return kurdishDate.ToDateTime();
    }

    /// <summary>
    /// Parses a string representation of a Kurdish date.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <returns>A KurdishDate representing the parsed date.</returns>
    /// <exception cref="FormatException">Thrown when the input string is not in a valid format.</exception>
    /// <example>
    /// <code>
    /// // Parse Latin script dates
    /// KurdishDate date1 = KurdishDate.Parse("15 Xakelêwe 2725", KurdishDialect.KurmanjiLatin);
    /// KurdishDate date2 = KurdishDate.Parse("15/01/2725", KurdishDialect.SoraniLatin);
    /// 
    /// // Parse Arabic script dates
    /// KurdishDate date3 = KurdishDate.Parse("١٥ خاکەلێوە ٢٧٢٥", KurdishDialect.SoraniArabic);
    /// KurdishDate date4 = KurdishDate.Parse("٢٧٢٥/٠١/١٥", KurdishDialect.SoraniArabic); // RTL format
    /// </code>
    /// </example>
    public static KurdishDate Parse(string input, KurdishDialect dialect)
    {
      return KurdishDateParser.Parse(input, dialect);
    }

    /// <summary>
    /// Attempts to parse a string representation of a Kurdish date.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="result">When this method returns, contains the parsed KurdishDate if parsing succeeded, or default if it failed.</param>
    /// <returns>True if the string was parsed successfully; otherwise, false.</returns>
    /// <example>
    /// <code>
    /// if (KurdishDate.TryParse("15 Xakelêwe 2725", KurdishDialect.KurmanjiLatin, out KurdishDate date))
    /// {
    ///   Console.WriteLine($"Parsed successfully: {date}");
    /// }
    /// else
    /// {
    ///   Console.WriteLine("Failed to parse date");
    /// }
    /// </code>
    /// </example>
    public static bool TryParse(string input, KurdishDialect dialect, out KurdishDate result)
    {
      return KurdishDateParser.TryParse(input, dialect, out result);
    }
  }
}