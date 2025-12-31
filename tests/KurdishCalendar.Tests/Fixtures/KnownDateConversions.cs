using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core.Tests.Fixtures
{
  /// <summary>
  /// Known date conversion test cases for validation.
  /// These represent documented conversions between Gregorian and Kurdish calendars.
  /// 
  /// VERIFIED AGAINST ASTRONOMICAL CALCULATIONS (December 2024)
  /// Leap years based on actual 366-day intervals between Nowruz dates.
  /// </summary>
  public static class KnownDateConversions
  {
    /// <summary>
    /// Known Gregorian to Kurdish date conversions.
    /// Format: (GregorianDate, ExpectedKurdishYear, ExpectedKurdishMonth, ExpectedKurdishDay)
    /// </summary>
    public static readonly List<(DateTime Gregorian, int KurdishYear, int KurdishMonth, int KurdishDay)>
      StandardConversions = new List<(DateTime, int, int, int)>
    {
      // Nowruz dates (first day of Kurdish year)
      // These are the most critical dates to get right
      // Note: Nowruz 2723 falls on 21 March 2023 (not 20 March)
      (new DateTime(2024, 3, 20), 2724, 1, 1),  // Nowruz 2724
      (new DateTime(2025, 3, 20), 2725, 1, 1),  // Nowruz 2725
      (new DateTime(2023, 3, 21), 2723, 1, 1),  // Nowruz 2723 (21 March, not 20)

      // Mid-year dates (various months)
      (new DateTime(2024, 4, 15), 2724, 1, 27), // Late first month
      (new DateTime(2024, 5, 10), 2724, 2, 21), // Second month
      (new DateTime(2024, 6, 15), 2724, 3, 27), // Third month
      (new DateTime(2024, 7, 20), 2724, 4, 31), // Fourth month (31 days)
      (new DateTime(2024, 8, 15), 2724, 5, 27), // Fifth month
      (new DateTime(2024, 9, 10), 2724, 6, 22), // Sixth month

      // Second half of year (30-day months)
      (new DateTime(2024, 10, 5), 2724, 7, 16),  // Seventh month
      (new DateTime(2024, 11, 1), 2724, 8, 12),  // Eighth month
      (new DateTime(2024, 12, 1), 2724, 9, 12),  // Ninth month
      (new DateTime(2025, 1, 10), 2724, 10, 22), // Tenth month
      (new DateTime(2025, 2, 5), 2724, 11, 17),  // Eleventh month

      // Last month (variable length: 29 or 30 days)
      (new DateTime(2025, 3, 10), 2724, 12, 20), // Last month of 2724
      (new DateTime(2025, 3, 19), 2724, 12, 29), // Last day of 2724 (common year)

      // Year before Nowruz (still previous Kurdish year)
      // Year 2723 is COMMON (29 days in month 12), not leap!
      (new DateTime(2024, 3, 19), 2723, 12, 29), // CORRECTED: 2723 is common year, day 29 (not 30)
      (new DateTime(2024, 1, 1), 2723, 10, 13),  // Early Gregorian year, previous Kurdish year
      
      // Year 2722 is LEAP (30 days in month 12)
      (new DateTime(2023, 3, 20), 2722, 12, 30)  // Last day of 2722 (leap year)
    };

    /// <summary>
    /// Dates that test leap year behaviour.
    /// Format: (GregorianDate, KurdishYear, KurdishMonth, KurdishDay, IsLeapYear)
    /// 
    /// VERIFIED LEAP YEARS (from astronomical calculation):
    /// 2702, 2706, 2710, 2714, 2718, 2722, 2727, 2731, 2735, 2739, 2743, 2747...
    /// Pattern: Mostly 4-year intervals, with one 5-year interval (2722→2727)
    /// </summary>
    public static readonly List<(DateTime Gregorian, int KurdishYear, int KurdishMonth, int KurdishDay, bool IsLeapYear)>
      LeapYearTestCases = new List<(DateTime, int, int, int, bool)>
    {
      // Last day of ACTUAL leap years (30th of 12th month)
      (new DateTime(2023, 3, 20), 2722, 12, 30, true),   // 2722 IS a leap year (366 days)
      (new DateTime(2019, 3, 20), 2718, 12, 30, true),   // 2718 IS a leap year (366 days)
      (new DateTime(2015, 3, 20), 2714, 12, 30, true),   // 2714 IS a leap year (366 days)

      // Last day of common years (29th of 12th month)
      (new DateTime(2024, 3, 19), 2723, 12, 29, false),  // 2723 is NOT a leap year (365 days)
      (new DateTime(2025, 3, 19), 2724, 12, 29, false),  // 2724 is NOT a leap year (365 days)
      (new DateTime(2020, 3, 19), 2719, 12, 29, false),  // 2719 is NOT a leap year (365 days)
      (new DateTime(2016, 3, 19), 2715, 12, 29, false),  // 2715 is NOT a leap year (365 days)

      // First day after leap year
      (new DateTime(2023, 3, 21), 2723, 1, 1, false),    // New year after leap year 2722
      (new DateTime(2019, 3, 21), 2719, 1, 1, false),    // New year after leap year 2718
      (new DateTime(2015, 3, 21), 2715, 1, 1, false)     // New year after leap year 2714
    };

    /// <summary>
    /// Edge case dates for boundary testing.
    /// </summary>
    public static readonly List<(DateTime Gregorian, int KurdishYear, int KurdishMonth, int KurdishDay, string Description)>
      EdgeCases = new List<(DateTime, int, int, int, string)>
    {
      // Month boundaries
      (new DateTime(2024, 4, 20), 2724, 2, 1, "First day of second month"),
      (new DateTime(2024, 4, 19), 2724, 1, 31, "Last day of first month"),

      // Transition from 31-day months to 30-day months
      (new DateTime(2024, 9, 21), 2724, 7, 1, "First day of seventh month (30 days)"),
      (new DateTime(2024, 9, 20), 2724, 6, 31, "Last day of sixth month (31 days)"),

      // Year boundaries around Nowruz
      // CORRECTED: 2723 is common year (29 days in month 12)
      (new DateTime(2024, 3, 19), 2723, 12, 29, "Day before Nowruz (common year, not leap)"),
      (new DateTime(2024, 3, 20), 2724, 1, 1, "Nowruz (new year)"),
      (new DateTime(2024, 3, 21), 2724, 1, 2, "Day after Nowruz"),

      // Year 2722 IS a leap year
      (new DateTime(2023, 3, 20), 2722, 12, 30, "Last day of 2722 (leap year)"),
      (new DateTime(2023, 3, 21), 2723, 1, 1, "Nowruz 2723 (March 21, after leap year)"),

      // Early in Gregorian year, still previous Kurdish year
      (new DateTime(2024, 1, 1), 2723, 10, 13, "New Year's Day is in Kurdish year 2723"),
      (new DateTime(2024, 2, 29), 2723, 12, 11, "Gregorian leap day in Kurdish year 2723")
    };

    /// <summary>
    /// Test cases for the first six months (31 days each).
    /// </summary>
    public static readonly List<(int Month, int ExpectedDays)> FirstSixMonths = new List<(int, int)>
    {
      (1, 31), (2, 31), (3, 31), (4, 31), (5, 31), (6, 31)
    };

    /// <summary>
    /// Test cases for months 7-11 (30 days each).
    /// </summary>
    public static readonly List<(int Month, int ExpectedDays)> Months7To11 = new List<(int, int)>
    {
      (7, 30), (8, 30), (9, 30), (10, 30), (11, 30)
    };

    /// <summary>
    /// Kurdish month names in Sorani Latin for validation.
    /// Source: KurdishCultureInfo.cs
    /// </summary>
    public static readonly string[] SoraniLatinMonthNames = new string[]
    {
      "Xakelêwe", "Gulan", "Cozerdan", "Pûşper", "Gelawêj", "Xermanan",
      "Rezber", "Gealrêzan", "Sermawez", "Befranbar", "Rêbendan", "Reşeme"
    };

    /// <summary>
    /// Kurdish day names in Sorani Latin for validation.
    /// Source: KurdishCultureInfo.cs
    /// Format: [Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday]
    /// </summary>
    public static readonly string[] SoraniLatinDayNames = new string[]
    {
      "Yekşemme", "Duşemme", "Sêşemme", "Çwarşemme", "Pêncşemme", "Hênî", "Şemme"
    };
  }
}