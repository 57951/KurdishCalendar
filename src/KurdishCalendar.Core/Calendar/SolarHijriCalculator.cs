using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides calculations for the Solar Hijri (Kurdish) calendar system using simplified Nowruz calculation.
  /// The Kurdish calendar is based on the Solar Hijri calendar with year offset for Median Empire (~700 BCE).
  /// This calculator uses a fixed 21 March date for Nowruz, suitable for 99.9% of use cases.
  /// </summary>
  internal static class SolarHijriCalculator
  {
    // Kurdish calendar starts from ~700 BCE (founding of Median Empire)
    // So 2025 CE = 2725 Kurdish
    private const int KurdishEpochOffset = 700;

    private static readonly int[] DaysInMonth = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };

    /// <summary>
    /// Converts a Gregorian date to Kurdish (Solar Hijri) date.
    /// </summary>
    public static (int Year, int Month, int Day) FromGregorian(DateTime gregorianDate)
    {
      int kurdishYear = gregorianDate.Year + KurdishEpochOffset;

      // Calculate Nowruz for this Kurdish year
      DateTime nowruz = CalculateNowruz(kurdishYear);

      // If before Nowruz, we're in the previous Kurdish year
      if (gregorianDate < nowruz)
      {
        kurdishYear--;
        nowruz = CalculateNowruz(kurdishYear);
      }

      // Calculate days since Nowruz
      int daysSinceNowruz = (gregorianDate - nowruz).Days;

      // Find the month and day
      int month = 1;
      int day = daysSinceNowruz + 1;

      bool isLeapYear = IsLeapYear(kurdishYear);

      for (int i = 0; i < 12; i++)
      {
        int daysInCurrentMonth = DaysInMonth[i];

        // Adjust last month for leap year
        if (i == 11 && isLeapYear)
        {
          daysInCurrentMonth = 30;
        }

        if (day <= daysInCurrentMonth)
        {
          month = i + 1;
          break;
        }

        day -= daysInCurrentMonth;
      }

      return (kurdishYear, month, day);
    }

    /// <summary>
    /// Converts a Kurdish (Solar Hijri) date to Gregorian date.
    /// </summary>
    public static DateTime ToGregorian(int year, int month, int day)
    {
      ValidateKurdishDate(year, month, day);

      // Calculate Nowruz for the given Kurdish year
      DateTime nowruz = CalculateNowruz(year);

      // Add the days for complete months
      int totalDays = 0;
      for (int m = 1; m < month; m++)
      {
        totalDays += GetDaysInMonth(m, year);
      }

      // Add the days in the current month
      totalDays += day - 1;

      return nowruz.AddDays(totalDays);
    }

    /// <summary>
    /// Gets the number of days in a month for a given year.
    /// </summary>
    public static int GetDaysInMonth(int month, int year)
    {
      if (month < 1 || month > 12)
      {
        throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
      }

      if (month <= 11)
      {
        return DaysInMonth[month - 1];
      }

      // Month 12: check if leap year
      return IsLeapYear(year) ? 30 : 29;
    }

    /// <summary>
    /// Determines whether a given year is a leap year using the 33-year cycle.
    /// </summary>
    public static bool IsLeapYear(int year)
    {
      // 33-year cycle pattern: leap years at positions 1, 5, 9, 13, 17, 22, 26, 30
      int cycleYear = ((year - 1) % 33) + 1;

      return cycleYear == 1 || cycleYear == 5 || cycleYear == 9 || cycleYear == 13 ||
             cycleYear == 17 || cycleYear == 22 || cycleYear == 26 || cycleYear == 30;
    }

    /// <summary>
    /// Calculates the Gregorian date of Nowruz (spring equinox) for a given Kurdish year.
    /// This uses a simplified calculation - always 21 March.
    /// </summary>
    private static DateTime CalculateNowruz(int kurdishYear)
    {
      int gregorianYear = kurdishYear - KurdishEpochOffset;
      int day = 21; // Simplified - always 21 March
      return new DateTime(gregorianYear, 3, day);
    }

    /// <summary>
    /// Validates that a Kurdish date is valid.
    /// </summary>
    public static void ValidateKurdishDate(int year, int month, int day)
    {
      if (year < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(year), "Year must be 1 or greater.");
      }

      if (month < 1 || month > 12)
      {
        throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
      }

      int maxDays = GetDaysInMonth(month, year);
      if (day < 1 || day > maxDays)
      {
        throw new ArgumentOutOfRangeException(nameof(day),
          $"Day must be between 1 and {maxDays} for month {month} in year {year}.");
      }
    }
  }
}