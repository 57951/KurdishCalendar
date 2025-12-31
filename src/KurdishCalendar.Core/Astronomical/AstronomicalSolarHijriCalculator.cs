using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides astronomical calculations for the Solar Hijri (Kurdish) calendar system.
  /// Uses precise spring equinox calculation for each year.
  /// </summary>
  internal static class AstronomicalSolarHijriCalculator
  {
    // Kurdish calendar starts from ~700 BCE (founding of Median Empire)
    private const int KurdishEpochOffset = 700;

    private static readonly int[] DaysInMonth = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };

    /// <summary>
    /// Converts a Gregorian date to astronomical Kurdish date.
    /// </summary>
    public static (int Year, int Month, int Day) FromGregorian(DateTime gregorianDate, double longitudeDegrees)
    {
      int kurdishYear = gregorianDate.Year + KurdishEpochOffset;

      // Calculate astronomical Nowruz for this Kurdish year
      DateTime nowruz = CalculateNowruz(kurdishYear, longitudeDegrees);

      // If before Nowruz, we're in the previous Kurdish year
      if (gregorianDate.Date < nowruz.Date)
      {
        kurdishYear--;
        nowruz = CalculateNowruz(kurdishYear, longitudeDegrees);
      }

      // Calculate days since Nowruz
      int daysSinceNowruz = (gregorianDate.Date - nowruz.Date).Days;

      // Find the month and day
      int month = 1;
      int day = daysSinceNowruz + 1;

      bool isLeapYear = IsLeapYear(kurdishYear, longitudeDegrees);

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
    /// Converts an astronomical Kurdish date to Gregorian date.
    /// </summary>
    public static DateTime ToGregorian(int year, int month, int day, double longitudeDegrees)
    {
      ValidateKurdishDate(year, month, day, longitudeDegrees);

      // Calculate astronomical Nowruz for the given Kurdish year
      DateTime nowruz = CalculateNowruz(year, longitudeDegrees);

      // Add the days for complete months
      int totalDays = 0;
      for (int m = 1; m < month; m++)
      {
        totalDays += GetDaysInMonth(year, m, longitudeDegrees);
      }

      // Add the days in the current month
      totalDays += day - 1;

      return nowruz.AddDays(totalDays);
    }

    /// <summary>
    /// Gets the number of days in a month for a given year.
    /// </summary>
    public static int GetDaysInMonth(int year, int month, double longitudeDegrees)
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
      return IsLeapYear(year, longitudeDegrees) ? 30 : 29;
    }

    /// <summary>
    /// Determines whether a given year is a leap year using astronomical calculation.
    /// A year is a leap year if it has 366 days between consecutive Nowruz dates.
    /// </summary>
    public static bool IsLeapYear(int year, double longitudeDegrees)
    {
      DateTime thisNowruz = CalculateNowruz(year, longitudeDegrees);
      DateTime nextNowruz = CalculateNowruz(year + 1, longitudeDegrees);

      int daysInYear = (nextNowruz.Date - thisNowruz.Date).Days;

      return daysInYear == 366;
    }

    /// <summary>
    /// Calculates the astronomical Nowruz date for a given Kurdish year.
    /// </summary>
    private static DateTime CalculateNowruz(int kurdishYear, double longitudeDegrees)
    {
      int gregorianYear = kurdishYear - KurdishEpochOffset;

      // Get the precise astronomical equinox date for this location
      DateTime nowruzDate = AstronomicalEquinoxCalculator.GetNowruzDate(gregorianYear, longitudeDegrees);

      return nowruzDate;
    }

    /// <summary>
    /// Validates that an astronomical Kurdish date is valid.
    /// </summary>
    public static void ValidateKurdishDate(int year, int month, int day, double longitudeDegrees)
    {
      if (year < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(year), "Year must be 1 or greater.");
      }

      if (month < 1 || month > 12)
      {
        throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
      }

      int maxDays = GetDaysInMonth(year, month, longitudeDegrees);
      if (day < 1 || day > maxDays)
      {
        throw new ArgumentOutOfRangeException(nameof(day),
          $"Day must be between 1 and {maxDays} for month {month} in year {year}.");
      }
    }
  }
}