using System;
using Xunit;
using KurdishCalendar.Core.Tests.Fixtures;

namespace KurdishCalendar.Core.Tests.Astronomical
{
  /// <summary>
  /// Tests for KurdishAstronomicalDate class.
  /// Validates astronomical Kurdish date calculations and conversions.
  /// </summary>
  public class KurdishAstronomicalDateTests
  {
    /// <summary>
    /// Test that Nowruz (1/1) falls on the correct Gregorian date
    /// based on astronomical equinox calculations.
    /// </summary>
    [Theory]
    [InlineData(2724, 2024, 3, 20)] // Nowruz 2724 = 20 March 2024
    [InlineData(2725, 2025, 3, 20)] // Nowruz 2725 = 20 March 2025
    [InlineData(2723, 2023, 3, 21)] // Nowruz 2723 = 21 March 2023
    [InlineData(2720, 2020, 3, 20)] // Nowruz 2720 = 20 March 2020
    public void Constructor_NowruzDate_ShouldConvertToCorrectGregorianDate(
      int kurdishYear, int expectedGregorianYear, int expectedMonth, int expectedDay)
    {
      // Arrange
      KurdishAstronomicalDate nowruz = new KurdishAstronomicalDate(kurdishYear, 1, 1);
      DateTime expected = new DateTime(expectedGregorianYear, expectedMonth, expectedDay);

      // Act
      DateTime actual = nowruz.ToDateTime();

      // Assert
      Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test conversion from Gregorian to Kurdish astronomical dates
    /// for known Nowruz dates.
    /// </summary>
    [Theory]
    [InlineData(2024, 3, 20, 2724, 1, 1)] // Nowruz 2724
    [InlineData(2025, 3, 20, 2725, 1, 1)] // Nowruz 2725
    [InlineData(2023, 3, 21, 2723, 1, 1)] // Nowruz 2723 is 21 March 2023
    [InlineData(2024, 3, 19, 2723, 12, 29)] // 2723 only has 29 days in month 12
    public void FromDateTime_NowruzDates_ShouldProduceCorrectKurdishDate(
      int gregorianYear, int gregorianMonth, int gregorianDay,
      int expectedKurdishYear, int expectedKurdishMonth, int expectedKurdishDay)
    {
      // Arrange
      DateTime gregorian = new DateTime(gregorianYear, gregorianMonth, gregorianDay);

      // Act
      KurdishAstronomicalDate kurdish = KurdishAstronomicalDate.FromDateTime(gregorian);

      // Assert
      Assert.Equal(expectedKurdishYear, kurdish.Year);
      Assert.Equal(expectedKurdishMonth, kurdish.Month);
      Assert.Equal(expectedKurdishDay, kurdish.Day);
    }

    /// <summary>
    /// Test that astronomical leap year detection matches known leap years.
    /// A year is leap if there are 366 days between consecutive Nowruz dates.
    /// </summary>
    [Theory]
    [InlineData(2714, true)]   // ✅ LEAP (366 days)
    [InlineData(2715, false)]  // ✅ common (365 days)
    [InlineData(2718, true)]   // ✅ LEAP (366 days)
    [InlineData(2719, false)]  // ✅ common (365 days)
    [InlineData(2722, true)]   // ✅ LEAP (366 days)
    [InlineData(2723, false)]  // ✅ common (365 days)
    [InlineData(2724, false)]  // ✅ common (365 days)
    public void IsLeapYear_ShouldMatchAstronomicalCalculations(int kurdishYear, bool expectedIsLeap)
    {
      // Arrange
      KurdishAstronomicalDate date = new KurdishAstronomicalDate(kurdishYear, 1, 1);

      // Act
      bool actual = date.IsLeapYear;

      // Assert
      Assert.Equal(expectedIsLeap, actual);
    }

    /// <summary>
    /// Test that the last month has correct number of days based on leap year.
    /// </summary>
    [Theory]
    [InlineData(2714, 12, 30, true)]   // ✅ 2714 is leap
    [InlineData(2715, 12, 29, false)]  // ✅ 2715 is common
    [InlineData(2718, 12, 30, true)]   // ✅ 2718 is leap
    [InlineData(2719, 12, 29, false)]  // ✅ 2719 is common
    [InlineData(2722, 12, 30, true)]   // ✅ 2722 is leap
    [InlineData(2723, 12, 29, false)]  // ✅ 2723 is common
    public void Constructor_LastDayOfYear_ShouldRespectLeapYear(
      int year, int month, int day, bool isLeapYear)
    {
      // Act
      KurdishAstronomicalDate date = new KurdishAstronomicalDate(year, month, day);

      // Assert
      Assert.Equal(year, date.Year);
      Assert.Equal(month, date.Month);
      Assert.Equal(day, date.Day);
      Assert.Equal(isLeapYear, date.IsLeapYear);
    }

    /// <summary>
    /// Test that invalid dates throw appropriate exceptions.
    /// </summary>
    [Theory]
    [InlineData(2724, 12, 30)] // Common year: 12th month has only 29 days
    [InlineData(2724, 13, 1)]  // Invalid month: 13
    [InlineData(2724, 0, 1)]   // Invalid month: 0
    [InlineData(2724, 1, 32)]  // Invalid day: 32 (first month has 31 days)
    [InlineData(2724, 7, 31)]  // Invalid day: 31 (seventh month has 30 days)
    public void Constructor_InvalidDate_ShouldThrowException(int year, int month, int day)
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishAstronomicalDate(year, month, day));
    }

    /// <summary>
    /// Test round-trip conversion: Kurdish -> Gregorian -> Kurdish.
    /// </summary>
    [Theory]
    [InlineData(2724, 1, 1)]
    [InlineData(2724, 6, 15)]
    [InlineData(2724, 12, 29)]
    [InlineData(2722, 12, 30)] // Leap year
    public void RoundTrip_KurdishToGregorianToKurdish_ShouldPreserveDate(
      int year, int month, int day)
    {
      // Arrange
      KurdishAstronomicalDate original = new KurdishAstronomicalDate(year, month, day);

      // Act
      DateTime gregorian = original.ToDateTime();
      KurdishAstronomicalDate roundTrip = KurdishAstronomicalDate.FromDateTime(gregorian);

      // Assert
      Assert.Equal(original.Year, roundTrip.Year);
      Assert.Equal(original.Month, roundTrip.Month);
      Assert.Equal(original.Day, roundTrip.Day);
    }

    /// <summary>
    /// Test AddDays operation.
    /// </summary>
    [Theory]
    [InlineData(2724, 1, 1, 10, 2724, 1, 11)]    // Add 10 days within month
    [InlineData(2724, 1, 25, 10, 2724, 2, 4)]    // Add 10 days across month boundary
    [InlineData(2724, 1, 15, -10, 2724, 1, 5)]   // Year 2723 has 29 days in month 12
    [InlineData(2724, 12, 25, 10, 2725, 1, 6)]   // Year 2724 has 29 days in month 12
    public void AddDays_ShouldCalculateCorrectDate(
      int startYear, int startMonth, int startDay, int daysToAdd,
      int expectedYear, int expectedMonth, int expectedDay)
    {
      // Arrange
      KurdishAstronomicalDate start = new KurdishAstronomicalDate(startYear, startMonth, startDay);

      // Act
      KurdishAstronomicalDate result = start.AddDays(daysToAdd);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test AddMonths operation.
    /// </summary>
    [Theory]
    [InlineData(2724, 1, 15, 1, 2724, 2, 15)]   // Add 1 month
    [InlineData(2724, 1, 15, 6, 2724, 7, 15)]   // Add 6 months
    [InlineData(2724, 1, 15, 12, 2725, 1, 15)]  // Add 12 months (1 year)
    [InlineData(2724, 11, 15, 2, 2725, 1, 15)]  // Add across year boundary
    [InlineData(2724, 3, 15, -2, 2724, 1, 15)]  // Subtract months
    public void AddMonths_ShouldCalculateCorrectDate(
      int startYear, int startMonth, int startDay, int monthsToAdd,
      int expectedYear, int expectedMonth, int expectedDay)
    {
      // Arrange
      KurdishAstronomicalDate start = new KurdishAstronomicalDate(startYear, startMonth, startDay);

      // Act
      KurdishAstronomicalDate result = start.AddMonths(monthsToAdd);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test AddYears operation.
    /// </summary>
    [Theory]
    [InlineData(2724, 1, 15, 1, 2725, 1, 15)]   // Add 1 year
    [InlineData(2724, 6, 15, 5, 2729, 6, 15)]   // Add 5 years
    [InlineData(2724, 1, 15, -1, 2723, 1, 15)]  // Subtract 1 year
    public void AddYears_ShouldCalculateCorrectDate(
      int startYear, int startMonth, int startDay, int yearsToAdd,
      int expectedYear, int expectedMonth, int expectedDay)
    {
      // Arrange
      KurdishAstronomicalDate start = new KurdishAstronomicalDate(startYear, startMonth, startDay);

      // Act
      KurdishAstronomicalDate result = start.AddYears(yearsToAdd);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test DayOfYear calculation.
    /// </summary>
    [Theory]
    [InlineData(2724, 1, 1, 1)]      // First day of year
    [InlineData(2724, 1, 31, 31)]    // Last day of first month
    [InlineData(2724, 2, 1, 32)]     // First day of second month
    [InlineData(2724, 7, 1, 187)]    // First day of seventh month (31*6 + 1)
    [InlineData(2724, 12, 29, 365)]  // Last day of common year
    [InlineData(2722, 12, 30, 366)]  // Last day of leap year
    public void DayOfYear_ShouldCalculateCorrectly(
      int year, int month, int day, int expectedDayOfYear)
    {
      // Arrange
      KurdishAstronomicalDate date = new KurdishAstronomicalDate(year, month, day);

      // Act
      int actualDayOfYear = date.DayOfYear;

      // Assert
      Assert.Equal(expectedDayOfYear, actualDayOfYear);
    }

    /// <summary>
    /// Test comparison operators.
    /// </summary>
    [Fact]
    public void ComparisonOperators_ShouldWorkCorrectly()
    {
      // Arrange
      KurdishAstronomicalDate earlier = new KurdishAstronomicalDate(2724, 1, 1);
      KurdishAstronomicalDate later = new KurdishAstronomicalDate(2724, 1, 2);
      KurdishAstronomicalDate same = new KurdishAstronomicalDate(2724, 1, 1);

      // Assert
      Assert.True(earlier < later);
      Assert.True(later > earlier);
      Assert.True(earlier <= same);
      Assert.True(earlier >= same);
      Assert.True(earlier == same);
      Assert.True(earlier != later);
    }

    /// <summary>
    /// Test that different longitudes can produce different dates
    /// when equinox falls near midnight.
    /// </summary>
    [Fact]
    public void FromLongitude_DifferentLongitudes_CanProduceDifferentNowruzDates()
    {
      // Arrange
      int year = 2703; // (Gregorian 2003) Equinox at 01:00 UTC
      int month = 1;
      int day = 1;

      // Act
      // UTC: Equinox at 01:00 means Nowruz is March 21
      KurdishAstronomicalDate utcDate = KurdishAstronomicalDate.FromUtc(year, month, day);
      DateTime utcGregorian = utcDate.ToDateTime();

      // Tehran (52.5°E): Equinox at 01:00 + 3.5 hours = 04:30, still March 21
      KurdishAstronomicalDate tehranDate = KurdishAstronomicalDate.FromTehran(year, month, day);
      DateTime tehranGregorian = tehranDate.ToDateTime();

      // Assert
      // Both should map to March 21, 2003
      Assert.Equal(new DateTime(2003, 3, 21), utcGregorian);
      Assert.Equal(new DateTime(2003, 3, 21), tehranGregorian);
    }

    /// <summary>
    /// Test static factory methods for different reference locations.
    /// </summary>
    [Fact]
    public void StaticFactoryMethods_ShouldCreateDatesWithCorrectLongitude()
    {
      // Act
      KurdishAstronomicalDate erbil = KurdishAstronomicalDate.FromErbil(2724, 1, 1);
      KurdishAstronomicalDate sulaymaniyah = KurdishAstronomicalDate.FromSulaymaniyah(2724, 1, 1);
      KurdishAstronomicalDate tehran = KurdishAstronomicalDate.FromTehran(2724, 1, 1);
      KurdishAstronomicalDate utc = KurdishAstronomicalDate.FromUtc(2724, 1, 1);

      // Assert
      Assert.Equal(44.0, erbil.Longitude);
      Assert.Equal(45.0, sulaymaniyah.Longitude);
      Assert.Equal(52.5, tehran.Longitude);
      Assert.Equal(0.0, utc.Longitude);
    }
  }
}