using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core.Tests.Fixtures
{
  /// <summary>
  /// Authoritative spring equinox reference data for validation.
  /// Source: Fred Espenak, www.Astropixels.com
  /// Based on: "Astronomical Algorithms" by Jean Meeus (Willmann-Bell, Inc., Richmond, 1998)
  /// 
  /// These times are in UTC (Greenwich Mean Time).
  /// Accuracy: Within ±1 minute for years 1800-2200.
  /// </summary>
  public static class AstronomicalReferenceData
  {
    /// <summary>
    /// Spring equinox dates and times in UTC for years 2000-2030.
    /// Format: (Year, Month, Day, Hour, Minute)
    /// Source: Fred Espenak's Solstice and Equinox Tables
    /// URL: https://www.astropixels.com/ephemeris/soleq2001.html
    /// </summary>
    public static readonly Dictionary<int, DateTime> SpringEquinoxDatesUtc = new Dictionary<int, DateTime>
    {
      // Year 2000-2010
      { 2000, new DateTime(2000, 3, 20, 7, 35, 0, DateTimeKind.Utc) },
      { 2001, new DateTime(2001, 3, 20, 13, 31, 0, DateTimeKind.Utc) },
      { 2002, new DateTime(2002, 3, 20, 19, 16, 0, DateTimeKind.Utc) },
      { 2003, new DateTime(2003, 3, 21, 1, 0, 0, DateTimeKind.Utc) },
      { 2004, new DateTime(2004, 3, 20, 6, 49, 0, DateTimeKind.Utc) },
      { 2005, new DateTime(2005, 3, 20, 12, 34, 0, DateTimeKind.Utc) },
      { 2006, new DateTime(2006, 3, 20, 18, 25, 0, DateTimeKind.Utc) },
      { 2007, new DateTime(2007, 3, 21, 0, 7, 0, DateTimeKind.Utc) },
      { 2008, new DateTime(2008, 3, 20, 5, 49, 0, DateTimeKind.Utc) },
      { 2009, new DateTime(2009, 3, 20, 11, 44, 0, DateTimeKind.Utc) },
      { 2010, new DateTime(2010, 3, 20, 17, 32, 0, DateTimeKind.Utc) },

      // Year 2011-2020
      { 2011, new DateTime(2011, 3, 20, 23, 21, 0, DateTimeKind.Utc) },
      { 2012, new DateTime(2012, 3, 20, 5, 15, 0, DateTimeKind.Utc) },
      { 2013, new DateTime(2013, 3, 20, 11, 2, 0, DateTimeKind.Utc) },
      { 2014, new DateTime(2014, 3, 20, 16, 57, 0, DateTimeKind.Utc) },
      { 2015, new DateTime(2015, 3, 20, 22, 45, 0, DateTimeKind.Utc) },
      { 2016, new DateTime(2016, 3, 20, 4, 31, 0, DateTimeKind.Utc) },
      { 2017, new DateTime(2017, 3, 20, 10, 29, 0, DateTimeKind.Utc) },
      { 2018, new DateTime(2018, 3, 20, 16, 15, 0, DateTimeKind.Utc) },
      { 2019, new DateTime(2019, 3, 20, 21, 58, 0, DateTimeKind.Utc) },
      { 2020, new DateTime(2020, 3, 20, 3, 50, 0, DateTimeKind.Utc) },

      // Year 2021-2030
      { 2021, new DateTime(2021, 3, 20, 9, 37, 0, DateTimeKind.Utc) },
      { 2022, new DateTime(2022, 3, 20, 15, 33, 0, DateTimeKind.Utc) },
      { 2023, new DateTime(2023, 3, 20, 21, 24, 0, DateTimeKind.Utc) },
      { 2024, new DateTime(2024, 3, 20, 3, 7, 0, DateTimeKind.Utc) },
      { 2025, new DateTime(2025, 3, 20, 9, 2, 0, DateTimeKind.Utc) },
      { 2026, new DateTime(2026, 3, 20, 14, 46, 0, DateTimeKind.Utc) },
      { 2027, new DateTime(2027, 3, 20, 20, 25, 0, DateTimeKind.Utc) },
      { 2028, new DateTime(2028, 3, 20, 2, 17, 0, DateTimeKind.Utc) },
      { 2029, new DateTime(2029, 3, 20, 8, 2, 0, DateTimeKind.Utc) },
      { 2030, new DateTime(2030, 3, 20, 13, 52, 0, DateTimeKind.Utc) }
    };

    /// <summary>
    /// Gets the expected Nowruz date for a given Gregorian year at a specific longitude.
    /// This accounts for timezone differences.
    /// </summary>
    /// <param name="gregorianYear">The Gregorian year.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east (e.g., 44.0 for Erbil).</param>
    /// <returns>The date (local) when Nowruz occurs at that longitude.</returns>
    public static DateTime GetExpectedNowruzDate(int gregorianYear, double longitudeDegrees)
    {
      if (!SpringEquinoxDatesUtc.TryGetValue(gregorianYear, out DateTime equinoxUtc))
      {
        throw new ArgumentException($"No reference data available for year {gregorianYear}");
      }

      // Convert UTC to local time for the given longitude
      // Each 15 degrees = 1 hour (360 degrees / 24 hours)
      double hoursOffset = longitudeDegrees / 15.0;
      DateTime localTime = equinoxUtc.AddHours(hoursOffset);

      // Return the date (midnight) of Nowruz
      return localTime.Date;
    }

    /// <summary>
    /// Gets the expected Kurdish year number for a given Gregorian year.
    /// Kurdish calendar epoch is approximately 700 BCE (founding of Median Empire).
    /// </summary>
    public static int GetExpectedKurdishYear(int gregorianYear)
    {
      return gregorianYear + 700;
    }

    /// <summary>
    /// Known historical Nowruz dates for cross-validation.
    /// These are documented dates when Nowruz was observed.
    /// </summary>
    public static readonly Dictionary<int, (int Month, int Day)> HistoricalNowruzDates = new Dictionary<int, (int, int)>
    {
      // Most Nowruz dates fall on 21 March
      // Source: Historical Iranian calendar records and UNESCO documentation
      { 2020, (3, 20) }, // Documented: 20 March 2020
      { 2021, (3, 20) }, // Documented: 20 March 2021
      { 2022, (3, 20) }, // Documented: 20 March 2021
      { 2023, (3, 20) }, // Documented: 20 March 2023
      { 2024, (3, 20) }, // Documented: 20 March 2024
      { 2025, (3, 20) }  // Expected: 20 March 2025
    };

    /// <summary>
    /// Known leap years in the Solar Hijri calendar.
    /// These are years where the year has 366 days (last month has 30 days instead of 29).
    /// </summary>
    public static readonly HashSet<int> KnownLeapYears = new HashSet<int>
    {
      // Source: Calculated based on astronomical observations
      // A year is leap if the interval between consecutive Nowruz dates is 366 days
      2000, 2004, 2008, 2012, 2016, 2020, 2024, 2028
      // Pattern: Generally every 4 years, but with exceptions in the 33-year cycle
    };

    /// <summary>
    /// Tolerance for equinox time comparisons (in minutes).
    /// Jean Meeus algorithm is accurate to within ±1 minute for years 1800-2200.
    /// </summary>
    public const int EquinoxToleranceMinutes = 2;

    /// <summary>
    /// Reference longitudes for major Kurdish cities.
    /// </summary>
    public static class Longitudes
    {
      /// <summary>Erbil, Kurdistan Region of Iraq</summary>
      public const double Erbil = 44.009167;

      /// <summary>Sulaymaniyah, Kurdistan Region of Iraq</summary>
      public const double Sulaymaniyah = 45.4375;

      /// <summary>Duhok, Kurdistan Region of Iraq</summary>
      public const double Duhok = 42.9386;

      /// <summary>Tehran, Iran (used for Iranian calendar calculations)</summary>
      public const double Tehran = 52.5;

      /// <summary>UTC reference (0° longitude)</summary>
      public const double Utc = 0.0;
    }
  }
}