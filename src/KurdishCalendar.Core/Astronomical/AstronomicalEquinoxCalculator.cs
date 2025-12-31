using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Calculates the precise spring equinox using astronomical algorithms.
  /// Based on Jean Meeus' "Astronomical Algorithms" (1991/1998).
  /// Uses all 24 periodic terms for maximum accuracy (Â±1 minute for years 1800-2200).
  /// </summary>
  internal static class AstronomicalEquinoxCalculator
  {
    // Cache for calculated equinoxes to improve performance
    // Key: Gregorian year
    // Value: DateTime of equinox in UTC
    private static readonly Dictionary<int, DateTime> _equinoxCache = new Dictionary<int, DateTime>();

    /// <summary>
    /// Clears the entire equinox cache.
    /// Useful for testing or long-running applications with memory concerns.
    /// </summary>
    public static void ClearCache()
    {
      _equinoxCache.Clear();
    }

    /// <summary>
    /// Clears cached equinox for a specific year.
    /// </summary>
    /// <param name="year">The Gregorian year.</param>
    public static void ClearCache(int year)
    {
      _equinoxCache.Remove(year);
    }

    /// <summary>
    /// Calculates the spring equinox for a given year.
    /// The equinox occurs at the same instant globally (in UTC).
    /// Use GetNowruzDate to determine the local date at a specific longitude.
    /// </summary>
    /// <param name="gregorianYear">The Gregorian year.</param>
    /// <returns>The DateTime of the spring equinox in UTC.</returns>
    public static DateTime CalculateSpringEquinox(int gregorianYear)
    {
      if (_equinoxCache.TryGetValue(gregorianYear, out DateTime cachedEquinox))
      {
        return cachedEquinox;
      }

      DateTime equinox = CalculateEquinoxInternal(gregorianYear);
      _equinoxCache[gregorianYear] = equinox;
      return equinox;
    }

    /// <summary>
    /// Gets the exact moment of the spring equinox in local time for a given longitude.
    /// This returns the actual time when the equinox occurs at the specified location.
    /// </summary>
    /// <param name="gregorianYear">The Gregorian year.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east.</param>
    /// <returns>The DateTime of the equinox in local time (DateTimeKind.Unspecified).
    /// The returned time represents the actual moment of the equinox as observed at that longitude.</returns>
    /// <example>
    /// For 2025, the equinox occurs at approximately 09:07 UTC.
    /// At Erbil (44°E ≈ UTC+2.93): returns ~2025-03-20 12:00
    /// At Tehran (52.5°E ≈ UTC+3.5): returns ~2025-03-20 12:37
    /// </example>
    public static DateTime GetEquinoxMomentLocal(int gregorianYear, double longitudeDegrees)
    {
      DateTime equinoxUtc = CalculateSpringEquinox(gregorianYear);

      // Convert UTC to local time for the given longitude
      // Each 15 degrees = 1 hour (360 degrees / 24 hours)
      double hoursOffset = longitudeDegrees / 15.0;
      DateTime localTime = equinoxUtc.AddHours(hoursOffset);

      // Return as Unspecified to indicate this is a local time, not UTC
      return DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
    }

    /// <summary>
    /// Gets the Gregorian calendar date on which Nowruz falls for a given location.
    /// This determines which calendar day contains the equinox at the specified longitude.
    /// The time component is always midnight UTC and should be ignored - only the date matters.
    /// </summary>
    /// <param name="gregorianYear">The Gregorian year.</param>
    /// <param name="longitudeDegrees">The longitude in degrees east.</param>
    /// <returns>A DateTime representing the Nowruz date at midnight UTC (time component should be ignored).
    /// Only the Year, Month, and Day properties are meaningful.</returns>
    /// <remarks>
    /// This method returns midnight UTC as a standard reference point, but the important information
    /// is the date itself. Use <see cref="GetEquinoxMomentLocal"/> if you need the actual time of the equinox.
    /// </remarks>
    public static DateTime GetNowruzDate(int gregorianYear, double longitudeDegrees)
    {
      DateTime localEquinox = GetEquinoxMomentLocal(gregorianYear, longitudeDegrees);

      // Extract just the date components
      int year = localEquinox.Year;
      int month = localEquinox.Month;
      int day = localEquinox.Day;

      // Return midnight UTC for the date that contains the equinox at this longitude
      // The time component (00:00:00) is arbitrary and should be ignored by callers
      return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
    }

    /// <summary>
    /// Internal calculation of the spring equinox using Jean Meeus algorithm.
    /// Uses all 24 periodic terms for maximum accuracy.
    /// Accurate to within Â±1 minute for years 1800-2200.
    /// Returns the universal moment of equinox in UTC.
    /// </summary>
    private static DateTime CalculateEquinoxInternal(int year)
    {
      // Jean Meeus algorithm for spring equinox
      // Chapter 27: Equinoxes and Solstices
      // Table 27.B - All 24 periodic terms

      // Calculate Julian Ephemeris Day for mean equinox
      double Y = (year - 2000.0) / 1000.0;

      double JDE0;

      if (year >= 1000 && year < 3000)
      {
        // Formula for years 1000-3000
        JDE0 = 2451623.80984 + 365242.37404 * Y + 0.05169 * Y * Y
               - 0.00411 * Y * Y * Y - 0.00057 * Y * Y * Y * Y;
      }
      else
      {
        // Formula for years -1000 to 1000
        JDE0 = 1721139.29189 + 365242.13740 * Y + 0.06134 * Y * Y
               + 0.00111 * Y * Y * Y - 0.00071 * Y * Y * Y * Y;
      }

      // Calculate periodic terms for more accuracy
      double T = (JDE0 - 2451545.0) / 36525.0;
      double W = 35999.373 * T - 2.47;
      double deltaLambda = 1 + 0.0334 * Cos(W) + 0.0007 * Cos(2 * W);

      // Sum of ALL 24 periodic terms from Jean Meeus Table 27.B
      // Format: amplitude * Cos(A + B * T)
      double S = 485 * Cos(324.96 + 1934.136 * T)
               + 203 * Cos(337.23 + 32964.467 * T)
               + 199 * Cos(342.08 + 20.186 * T)
               + 182 * Cos(27.85 + 445267.112 * T)
               + 156 * Cos(73.14 + 45036.886 * T)
               + 136 * Cos(171.52 + 22518.443 * T)
               + 77 * Cos(222.54 + 65928.934 * T)
               + 74 * Cos(296.72 + 3034.906 * T)
               + 70 * Cos(243.58 + 9037.513 * T)
               + 58 * Cos(119.81 + 33718.147 * T)
               + 52 * Cos(297.17 + 150.678 * T)
               + 50 * Cos(21.02 + 2281.226 * T)
               // Additional 12 terms for full accuracy (Â±1 minute)
               + 45 * Cos(247.54 + 29929.562 * T)
               + 44 * Cos(325.15 + 31555.956 * T)
               + 29 * Cos(60.93 + 4443.417 * T)
               + 18 * Cos(155.12 + 67555.328 * T)
               + 17 * Cos(288.79 + 4562.452 * T)
               + 16 * Cos(198.04 + 62894.029 * T)
               + 14 * Cos(199.76 + 31436.921 * T)
               + 12 * Cos(95.39 + 14577.848 * T)
               + 12 * Cos(287.11 + 31931.756 * T)
               + 12 * Cos(320.81 + 34777.259 * T)
               + 9 * Cos(227.73 + 1222.114 * T)
               + 8 * Cos(15.45 + 16859.074 * T);

      // Calculate final JDE (Julian Ephemeris Day)
      double JDE = JDE0 + (0.00001 * S) / deltaLambda;

      // Convert JDE to DateTime
      DateTime equinoxUtc = JulianDayToDateTime(JDE);

      return equinoxUtc;
    }

    /// <summary>
    /// Converts a Julian Day Number to DateTime.
    /// </summary>
    private static DateTime JulianDayToDateTime(double julianDay)
    {
      // Algorithm from "Astronomical Algorithms" by Jean Meeus

      double Z = Math.Floor(julianDay + 0.5);
      double F = (julianDay + 0.5) - Z;

      double A;
      if (Z < 2299161)
      {
        A = Z;
      }
      else
      {
        double alpha = Math.Floor((Z - 1867216.25) / 36524.25);
        A = Z + 1 + alpha - Math.Floor(alpha / 4);
      }

      double B = A + 1524;
      double C = Math.Floor((B - 122.1) / 365.25);
      double D = Math.Floor(365.25 * C);
      double E = Math.Floor((B - D) / 30.6001);

      double day = B - D - Math.Floor(30.6001 * E) + F;
      int month = (int)(E < 14 ? E - 1 : E - 13);
      int year = (int)(month > 2 ? C - 4716 : C - 4715);

      int dayInt = (int)Math.Floor(day);
      double timeFraction = day - dayInt;

      int hours = (int)Math.Floor(timeFraction * 24);
      double minutesDecimal = (timeFraction * 24 - hours) * 60;
      int minutes = (int)Math.Floor(minutesDecimal);
      double secondsDecimal = (minutesDecimal - minutes) * 60;
      int seconds = (int)Math.Floor(secondsDecimal);
      int milliseconds = (int)Math.Floor((secondsDecimal - seconds) * 1000);

      return new DateTime(year, month, dayInt, hours, minutes, seconds, milliseconds, DateTimeKind.Utc);
    }

    /// <summary>
    /// Cosine function accepting degrees.
    /// </summary>
    private static double Cos(double degrees)
    {
      return Math.Cos(degrees * Math.PI / 180.0);
    }
  }
}