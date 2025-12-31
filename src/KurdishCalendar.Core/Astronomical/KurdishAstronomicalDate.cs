using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Represents a date in the Kurdish calendar using astronomical spring equinox calculation.
  /// Unlike <see cref="KurdishDate"/> which uses a simplified March 20/21 assumption,
  /// this type calculates the precise equinox for each year based on astronomical algorithms.
  /// The Kurdish calendar is based on the Solar Hijri system with Nowruz (spring equinox) as the start of the year.
  /// </summary>
  public readonly struct KurdishAstronomicalDate : IKurdishDate
  {
    /// <summary>
    /// Default longitude for Erbil, capital of Kurdistan Region (44.0Ã‚Â°E).
    /// </summary>
    public const double DefaultLongitude = 44.0;

    /// <summary>
    /// Longitude for Sulaymaniyah, Kurdistan Region (45.0Ã‚Â°E).
    /// </summary>
    public const double SulaymaniyahLongitude = 45.0;

    /// <summary>
    /// Longitude for Tehran meridian, used in Iranian calendar (52.5Ã‚Â°E).
    /// </summary>
    public const double TehranLongitude = 52.5;

    private readonly double _longitude;

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
    /// Gets the longitude used for equinox calculation.
    /// </summary>
    public double Longitude => _longitude;

    /// <summary>
    /// Initialises a new instance of the <see cref="KurdishAstronomicalDate"/> struct.
    /// Uses Erbil (44.0Ã‚Â°E) as the default reference location.
    /// </summary>
    /// <param name="year">The Kurdish year (must be 1 or greater).</param>
    /// <param name="month">The Kurdish month (1-12).</param>
    /// <param name="day">The day of the month.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any component is out of valid range.</exception>
    public KurdishAstronomicalDate(int year, int month, int day)
      : this(year, month, day, DefaultLongitude)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="KurdishAstronomicalDate"/> struct
    /// from a Gregorian date using Erbil (44.0Ã‚Â°E) as the reference location.
    /// </summary>
    /// <param name="gregorianDate">The Gregorian date to convert.</param>
    public KurdishAstronomicalDate(DateTime gregorianDate)
      : this(gregorianDate, DefaultLongitude)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="KurdishAstronomicalDate"/> struct
    /// with a custom longitude for equinox calculation.
    /// </summary>
    /// <param name="year">The Kurdish year (must be 1 or greater).</param>
    /// <param name="month">The Kurdish month (1-12).</param>
    /// <param name="day">The day of the month.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east for equinox calculation.</param>
    private KurdishAstronomicalDate(int year, int month, int day, double longitudeDegrees)
    {
      AstronomicalSolarHijriCalculator.ValidateKurdishDate(year, month, day, longitudeDegrees);

      Year = year;
      Month = month;
      Day = day;
      _longitude = longitudeDegrees;
    }

    /// <summary>
    /// Initialises a new instance from a Gregorian date with custom longitude.
    /// </summary>
    private KurdishAstronomicalDate(DateTime gregorianDate, double longitudeDegrees)
    {
      (int year, int month, int day) = AstronomicalSolarHijriCalculator.FromGregorian(
        gregorianDate, longitudeDegrees);

      Year = year;
      Month = month;
      Day = day;
      _longitude = longitudeDegrees;
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> using Erbil (44.0Ã‚Â°E) as reference.
    /// </summary>
    public static KurdishAstronomicalDate FromErbil(int year, int month, int day)
    {
      return new KurdishAstronomicalDate(year, month, day, DefaultLongitude);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> using Sulaymaniyah (45.0Ã‚Â°E) as reference.
    /// </summary>
    public static KurdishAstronomicalDate FromSulaymaniyah(int year, int month, int day)
    {
      return new KurdishAstronomicalDate(year, month, day, SulaymaniyahLongitude);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> using Tehran meridian (52.5Ã‚Â°E) as reference.
    /// This matches the official Iranian Solar Hijri calendar.
    /// </summary>
    public static KurdishAstronomicalDate FromTehran(int year, int month, int day)
    {
      return new KurdishAstronomicalDate(year, month, day, TehranLongitude);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> using UTC (0.0Ã‚Â°) as reference.
    /// </summary>
    public static KurdishAstronomicalDate FromUtc(int year, int month, int day)
    {
      return new KurdishAstronomicalDate(year, month, day, 0.0);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> using a custom longitude.
    /// </summary>
    /// <param name="year">The Kurdish year.</param>
    /// <param name="month">The Kurdish month (1-12).</param>
    /// <param name="day">The day of the month.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east.</param>
    public static KurdishAstronomicalDate FromLongitude(int year, int month, int day, double longitudeDegrees)
    {
      return new KurdishAstronomicalDate(year, month, day, longitudeDegrees);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> from a Gregorian DateTime.
    /// Uses Erbil (44.0Ã‚Â°E) as the default reference location.
    /// </summary>
    public static KurdishAstronomicalDate FromDateTime(DateTime gregorianDate)
    {
      return new KurdishAstronomicalDate(gregorianDate, DefaultLongitude);
    }

    /// <summary>
    /// Creates a <see cref="KurdishAstronomicalDate"/> from a Gregorian DateTime
    /// using a custom longitude for equinox calculation.
    /// </summary>
    public static KurdishAstronomicalDate FromDateTime(DateTime gregorianDate, double longitudeDegrees)
    {
      return new KurdishAstronomicalDate(gregorianDate, longitudeDegrees);
    }

    /// <summary>
    /// Gets the current date in astronomical Kurdish calendar (UTC, Erbil reference).
    /// </summary>
    public static KurdishAstronomicalDate Today
    {
      get { return new KurdishAstronomicalDate(DateTime.UtcNow.Date, DefaultLongitude); }
    }

    /// <summary>
    /// Gets the current date and time in astronomical Kurdish calendar (UTC, Erbil reference).
    /// </summary>
    public static KurdishAstronomicalDate Now
    {
      get { return new KurdishAstronomicalDate(DateTime.UtcNow, DefaultLongitude); }
    }

    /// <summary>
    /// Gets the day of the week for this date.
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
        int dayOfYear = Day;
        for (int m = 1; m < Month; m++)
        {
          dayOfYear += AstronomicalSolarHijriCalculator.GetDaysInMonth(Year, m, _longitude);
        }
        return dayOfYear;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this year is a leap year.
    /// </summary>
    public bool IsLeapYear
    {
      get { return AstronomicalSolarHijriCalculator.IsLeapYear(Year, _longitude); }
    }

    /// <summary>
    /// Converts this astronomical Kurdish date to Gregorian DateTime.
    /// </summary>
    public DateTime ToDateTime()
    {
      return AstronomicalSolarHijriCalculator.ToGregorian(Year, Month, Day, _longitude);
    }

    /// <summary>
    /// Converts to standard <see cref="KurdishDate"/> preserving year, month, and day values.
    /// This is a lossless conversion that only changes the type.
    /// Note: The resulting date may differ by 1-2 days when converted back to Gregorian
    /// because standard dates use fixed March 20/21 while astronomical dates use precise equinox.
    /// </summary>
    public KurdishDate ToSimplifiedDate()
    {
      return new KurdishDate(Year, Month, Day);
    }

    /// <summary>
    /// Converts to standard <see cref="KurdishDate"/> by recalculating through Gregorian.
    /// This accounts for differences in Nowruz dates between astronomical and standard calculations.
    /// </summary>
    public KurdishDate ToStandardDateRecalculated()
    {
      DateTime gregorian = ToDateTime();
      return KurdishDate.FromDateTime(gregorian);
    }

    /// <summary>
    /// Adds the specified number of days to this date.
    /// </summary>
    public KurdishAstronomicalDate AddDays(int days)
    {
      DateTime gregorian = ToDateTime().AddDays(days);
      return new KurdishAstronomicalDate(gregorian, _longitude);
    }

    /// <summary>
    /// Adds the specified number of months to this date.
    /// </summary>
    public KurdishAstronomicalDate AddMonths(int months)
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

      int maxDays = AstronomicalSolarHijriCalculator.GetDaysInMonth(newYear, newMonth, _longitude);
      int newDay = Day > maxDays ? maxDays : Day;

      return new KurdishAstronomicalDate(newYear, newMonth, newDay, _longitude);
    }

    /// <summary>
    /// Adds the specified number of years to this date.
    /// </summary>
    public KurdishAstronomicalDate AddYears(int years)
    {
      int newYear = Year + years;
      int maxDays = AstronomicalSolarHijriCalculator.GetDaysInMonth(newYear, Month, _longitude);
      int newDay = Day > maxDays ? maxDays : Day;

      return new KurdishAstronomicalDate(newYear, Month, newDay, _longitude);
    }

    /// <summary>
    /// Calculates the number of days between this date and another.
    /// </summary>
    public int DaysDifference(KurdishAstronomicalDate other)
    {
      return (ToDateTime() - other.ToDateTime()).Days;
    }

    /// <summary>
    /// Returns a string representation of this date in short format.
    /// </summary>
    public override string ToString()
    {
      return KurdishDateFormatter.Format(this, null, KurdishDialect.SoraniLatin, null);
    }

    /// <summary>
    /// Returns a string representation of this date with custom format.
    /// </summary>
    public string ToString(string? format)
    {
      return KurdishDateFormatter.Format(this, format, KurdishDialect.SoraniLatin, null);
    }

    /// <summary>
    /// Returns a string representation of this date with custom format and dialect.
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
    /// Determines whether two <see cref="KurdishAstronomicalDate"/> instances are equal.
    /// </summary>
    public static bool operator ==(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return left.Year == right.Year && left.Month == right.Month && left.Day == right.Day
             && Math.Abs(left._longitude - right._longitude) < 0.01;
    }

    /// <summary>
    /// Determines whether two <see cref="KurdishAstronomicalDate"/> instances are not equal.
    /// </summary>
    public static bool operator !=(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishAstronomicalDate"/> is less than another.
    /// </summary>
    public static bool operator <(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return left.ToDateTime() < right.ToDateTime();
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishAstronomicalDate"/> is greater than another.
    /// </summary>
    public static bool operator >(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return left.ToDateTime() > right.ToDateTime();
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishAstronomicalDate"/> is less than or equal to another.
    /// </summary>
    public static bool operator <=(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return left.ToDateTime() <= right.ToDateTime();
    }

    /// <summary>
    /// Determines whether one <see cref="KurdishAstronomicalDate"/> is greater than or equal to another.
    /// </summary>
    public static bool operator >=(KurdishAstronomicalDate left, KurdishAstronomicalDate right)
    {
      return left.ToDateTime() >= right.ToDateTime();
    }

    /// <summary>
    /// Determines whether an object is equal to another.
    /// </summary>
    public override bool Equals(object? obj)
    {
      return obj is KurdishAstronomicalDate other && this == other;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return HashCode.Combine(Year, Month, Day, _longitude);
    }

    /// <summary>
    /// Implicit conversion from DateTime to KurdishAstronomicalDate.
    /// </summary>
    public static implicit operator KurdishAstronomicalDate(DateTime dateTime)
    {
      return new KurdishAstronomicalDate(dateTime);
    }

    /// <summary>
    /// Explicit conversion from KurdishAstronomicalDate to DateTime.
    /// </summary>
    public static explicit operator DateTime(KurdishAstronomicalDate kurdishDate)
    {
      return kurdishDate.ToDateTime();
    }

    /// <summary>
    /// Clears the astronomical equinox calculation cache.
    /// Useful for testing or long-running applications.
    /// </summary>
    public static void ClearEquinoxCache()
    {
      AstronomicalEquinoxCalculator.ClearCache();
    }

    /// <summary>
    /// Clears the cached equinox for a specific year.
    /// </summary>
    public static void ClearEquinoxCache(int year)
    {
      AstronomicalEquinoxCalculator.ClearCache(year);
    }

    /// <summary>
    /// Gets the exact moment of the spring equinox (Nowruz) for a given Kurdish year at a specific location.
    /// This returns the actual time when the equinox occurs, in local time for that longitude.
    /// </summary>
    /// <param name="kurdishYear">The Kurdish calendar year.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east (default: 44.0° for Erbil).</param>
    /// <returns>The DateTime of the equinox in local time at that longitude.
    /// DateTimeKind is Unspecified because this represents a local time, not UTC.</returns>
    /// <example>
    /// <code>
    /// // Get the equinox moment in Erbil for year 2725
    /// DateTime equinoxErbil = KurdishAstronomicalDate.GetEquinoxMoment(2725, 44.0);
    /// // Returns approximately: 2025-03-20 12:00:49 (local time at 44°E)
    /// 
    /// // Get the equinox moment in Tehran for year 2725
    /// DateTime equinoxTehran = KurdishAstronomicalDate.GetEquinoxMoment(2725, 52.5);
    /// // Returns approximately: 2025-03-20 12:37:06 (local time at 52.5°E)
    /// </code>
    /// </example>
    public static DateTime GetEquinoxMoment(int kurdishYear, double longitudeDegrees = DefaultLongitude)
    {
      int gregorianYear = kurdishYear - 700; // KurdishEpochOffset from AstronomicalSolarHijriCalculator
      return AstronomicalEquinoxCalculator.GetEquinoxMomentLocal(gregorianYear, longitudeDegrees);
    }

    /// <summary>
    /// Gets the exact moment of the spring equinox (Nowruz) in UTC for a given Kurdish year.
    /// </summary>
    /// <param name="kurdishYear">The Kurdish calendar year.</param>
    /// <returns>The DateTime of the equinox in UTC.</returns>
    /// <example>
    /// <code>
    /// // Get the UTC time of the equinox for year 2725
    /// DateTime equinoxUtc = KurdishAstronomicalDate.GetEquinoxMomentUtc(2725);
    /// // Returns approximately: 2025-03-20 09:07:06 UTC
    /// </code>
    /// </example>
    public static DateTime GetEquinoxMomentUtc(int kurdishYear)
    {
      int gregorianYear = kurdishYear - 700;
      return AstronomicalEquinoxCalculator.CalculateSpringEquinox(gregorianYear);
    }

    /// <summary>
    /// Parses a string representation of a Kurdish astronomical date.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="longitude">The longitude for astronomical calculations (default: 44.0 for Erbil).</param>
    /// <returns>A KurdishAstronomicalDate representing the parsed date.</returns>
    /// <exception cref="FormatException">Thrown when the input string is not in a valid format.</exception>
    /// <example>
    /// <code>
    /// // Parse with default Erbil longitude (44.0Ã‚Â°E)
    /// KurdishAstronomicalDate date1 = KurdishAstronomicalDate.Parse(
    ///   "15 XakelÃƒÂªwe 2725", 
    ///   KurdishDialect.KurmanjiLatin
    /// );
    /// 
    /// // Parse with custom longitude (Sulaymaniyah: 45.4375Ã‚Â°E)
    /// KurdishAstronomicalDate date2 = KurdishAstronomicalDate.Parse(
    ///   "15 XakelÃƒÂªwe 2725", 
    ///   KurdishDialect.KurmanjiLatin,
    ///   45.4375
    /// );
    /// 
    /// // Parse Arabic script dates
    /// KurdishAstronomicalDate date3 = KurdishAstronomicalDate.Parse(
    ///   "Ã™Â¡Ã™Â¥ Ã˜Â®Ã˜Â§ÃšÂ©Ã›â€¢Ã™â€žÃ›Å½Ã™Ë†Ã›â€¢ Ã™Â¢Ã™Â§Ã™Â¢Ã™Â¥", 
    ///   KurdishDialect.SoraniArabic
    /// );
    /// </code>
    /// </example>
    public static KurdishAstronomicalDate Parse(string input, KurdishDialect dialect, double longitude = 44.0)
    {
      return KurdishDateParser.ParseAstronomical(input, dialect, longitude);
    }

    /// <summary>
    /// Attempts to parse a string representation of a Kurdish astronomical date.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="longitude">The longitude for astronomical calculations (default: 44.0 for Erbil).</param>
    /// <param name="result">When this method returns, contains the parsed KurdishAstronomicalDate if parsing succeeded, or default if it failed.</param>
    /// <returns>True if the string was parsed successfully; otherwise, false.</returns>
    /// <example>
    /// <code>
    /// if (KurdishAstronomicalDate.TryParse(
    ///   "15 XakelÃƒÂªwe 2725", 
    ///   KurdishDialect.KurmanjiLatin,
    ///   44.0,
    ///   out KurdishAstronomicalDate date))
    /// {
    ///   Console.WriteLine($"Parsed successfully: {date}");
    /// }
    /// else
    /// {
    ///   Console.WriteLine("Failed to parse date");
    /// }
    /// </code>
    /// </example>
    public static bool TryParse(string input, KurdishDialect dialect, double longitude, out KurdishAstronomicalDate result)
    {
      return KurdishDateParser.TryParseAstronomical(input, dialect, longitude, out result);
    }

    /// <summary>
    /// Attempts to parse a string representation of a Kurdish astronomical date using default Erbil longitude.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="result">When this method returns, contains the parsed KurdishAstronomicalDate if parsing succeeded, or default if it failed.</param>
    /// <returns>True if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string input, KurdishDialect dialect, out KurdishAstronomicalDate result)
    {
      return TryParse(input, dialect, 44.0, out result);
    }
  }
}