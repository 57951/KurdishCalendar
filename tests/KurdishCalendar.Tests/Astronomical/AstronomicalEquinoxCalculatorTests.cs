using System;
using Xunit;
using KurdishCalendar.Core.Tests.Fixtures;

namespace KurdishCalendar.Core.Tests.Astronomical
{
  /// <summary>
  /// Tests for the AstronomicalEquinoxCalculator class.
  /// Validates against authoritative astronomical data from Fred Espenak (Jean Meeus algorithms).
  /// </summary>
  public class AstronomicalEquinoxCalculatorTests
  {
    /// <summary>
    /// Test that calculated spring equinoxes match Fred Espenak's published data
    /// for years 2000-2030 within acceptable tolerance (±2 minutes).
    /// 
    /// Source: Fred Espenak, www.Astropixels.com
    /// Based on: "Astronomical Algorithms" by Jean Meeus
    /// </summary>
    [Theory]
    [InlineData(2000, 2000, 3, 20, 7, 35)] // Mar 20 07:35 UTC
    [InlineData(2001, 2001, 3, 20, 13, 31)] // Mar 20 13:31 UTC
    [InlineData(2002, 2002, 3, 20, 19, 16)] // Mar 20 19:16 UTC
    [InlineData(2003, 2003, 3, 21, 1, 0)]   // Mar 21 01:00 UTC
    [InlineData(2004, 2004, 3, 20, 6, 49)]  // Mar 20 06:49 UTC
    [InlineData(2005, 2005, 3, 20, 12, 34)] // Mar 20 12:34 UTC
    [InlineData(2010, 2010, 3, 20, 17, 32)] // Mar 20 17:32 UTC
    [InlineData(2015, 2015, 3, 20, 22, 45)] // Mar 20 22:45 UTC
    [InlineData(2020, 2020, 3, 20, 3, 50)]  // Mar 20 03:50 UTC
    [InlineData(2024, 2024, 3, 20, 3, 7)]   // Mar 20 03:07 UTC
    [InlineData(2025, 2025, 3, 20, 9, 2)]   // Mar 20 09:02 UTC
    [InlineData(2030, 2030, 3, 20, 13, 52)] // Mar 20 13:52 UTC
    public void CalculateSpringEquinox_ShouldMatchEspenakData_WithinTolerance(
      int year, int expectedYear, int expectedMonth, int expectedDay,
      int expectedHour, int expectedMinute)
    {
      // Arrange
      DateTime expected = new DateTime(
        expectedYear, expectedMonth, expectedDay,
        expectedHour, expectedMinute, 0, DateTimeKind.Utc);

      // Act
      DateTime actual = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert
      TimeSpan difference = actual - expected;
      double minutesDifference = Math.Abs(difference.TotalMinutes);

      Assert.True(
        minutesDifference <= AstronomicalReferenceData.EquinoxToleranceMinutes,
        $"Equinox for {year} differs by {minutesDifference:F2} minutes. " +
        $"Expected: {expected:yyyy-MM-dd HH:mm} UTC, " +
        $"Actual: {actual:yyyy-MM-dd HH:mm} UTC");
    }

    /// <summary>
    /// Test equinox calculation for all years 2000-2030 systematically.
    /// </summary>
    [Fact]
    public void CalculateSpringEquinox_AllYears2000To2030_ShouldMatchReferenceData()
    {
      // Act & Assert
      foreach (int year in AstronomicalReferenceData.SpringEquinoxDatesUtc.Keys)
      {
        DateTime expected = AstronomicalReferenceData.SpringEquinoxDatesUtc[year];
        DateTime actual = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

        TimeSpan difference = actual - expected;
        double minutesDifference = Math.Abs(difference.TotalMinutes);

        Assert.True(
          minutesDifference <= AstronomicalReferenceData.EquinoxToleranceMinutes,
          $"Year {year}: Expected {expected:yyyy-MM-dd HH:mm}, " +
          $"Actual {actual:yyyy-MM-dd HH:mm}, " +
          $"Difference: {minutesDifference:F2} minutes");
      }
    }

    /// <summary>
    /// Test that Nowruz date calculation accounts for longitude correctly.
    /// Erbil (44°E) should be ~2.93 hours ahead of UTC.
    /// </summary>
    [Theory]
    [InlineData(2024, 44.0, 2024, 3, 20)]     // Erbil: equinox at 03:07 UTC + 2:56 = 06:03 local, still 20 March
    [InlineData(2024, 52.5, 2024, 3, 20)]     // Tehran: equinox at 03:07 UTC + 3:30 = 06:37 local, still 20 March
    [InlineData(2003, 44.0, 2003, 3, 21)]     // Erbil: equinox at 01:00 UTC + 2:56 = 03:56 local, still 21 March
    [InlineData(2007, 44.0, 2007, 3, 21)]     // Erbil: equinox at 00:07 UTC + 2:56 = 03:03 local, still 21 March
    public void GetNowruzDate_ShouldAccountForLongitude(
      int year, double longitude, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Arrange
      DateTime expected = new DateTime(expectedYear, expectedMonth, expectedDay);

      // Act
      DateTime actual = AstronomicalEquinoxCalculator.GetNowruzDate(year, longitude);

      // Assert
      Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test that equinox times are consistent across multiple calls (caching works correctly).
    /// </summary>
    [Fact]
    public void CalculateSpringEquinox_MultipleCalls_ShouldReturnIdenticalResults()
    {
      // Arrange
      int year = 2024;

      // Act
      DateTime first = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);
      DateTime second = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);
      DateTime third = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert
      Assert.Equal(first, second);
      Assert.Equal(second, third);
      Assert.Equal(first, third);
    }

    /// <summary>
    /// Test cache clearing functionality.
    /// </summary>
    [Fact]
    public void ClearCache_ShouldAllowRecalculation()
    {
      // Arrange
      int year = 2024;
      DateTime firstCalculation = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Act
      AstronomicalEquinoxCalculator.ClearCache();
      DateTime secondCalculation = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert - Results should still be identical (deterministic calculation)
      Assert.Equal(firstCalculation, secondCalculation);
    }

    /// <summary>
    /// Test that equinox dates fall within expected range (March 19-21).
    /// This is a sanity check for the astronomical algorithm.
    /// </summary>
    [Theory]
    [InlineData(2000)]
    [InlineData(2010)]
    [InlineData(2020)]
    [InlineData(2024)]
    [InlineData(2025)]
    [InlineData(2030)]
    public void CalculateSpringEquinox_ShouldFallInMarch19To21Range(int year)
    {
      // Act
      DateTime equinox = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert
      Assert.Equal(3, equinox.Month); // March
      Assert.InRange(equinox.Day, 19, 21); // Days 19-21
    }

    /// <summary>
    /// Test edge case: year 1900 (historical).
    /// </summary>
    [Fact]
    public void CalculateSpringEquinox_Year1900_ShouldCalculateWithoutError()
    {
      // Arrange
      int year = 1900;

      // Act
      DateTime equinox = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert
      Assert.Equal(3, equinox.Month);
      Assert.InRange(equinox.Day, 19, 21);
      Assert.Equal(DateTimeKind.Utc, equinox.Kind);
    }

    /// <summary>
    /// Test edge case: year 2100 (future).
    /// </summary>
    [Fact]
    public void CalculateSpringEquinox_Year2100_ShouldCalculateWithoutError()
    {
      // Arrange
      int year = 2100;

      // Act
      DateTime equinox = AstronomicalEquinoxCalculator.CalculateSpringEquinox(year);

      // Assert
      Assert.Equal(3, equinox.Month);
      Assert.InRange(equinox.Day, 19, 21);
      Assert.Equal(DateTimeKind.Utc, equinox.Kind);
    }
  }
}