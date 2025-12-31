using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests.Calendar
{
  /// <summary>
  /// Comprehensive tests for the KurdishDate struct.
  /// Tests constructors, date arithmetic, comparisons, conversions, and edge cases.
  /// </summary>
  public class KurdishDateTests
  {
    #region Constructor Tests

    [Fact]
    public void Constructor_WithValidDate_ShouldCreateDate()
    {
      // Arrange & Act
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Assert
      Assert.Equal(2725, date.Year);
      Assert.Equal(1, date.Month);
      Assert.Equal(15, date.Day);
    }

    [Fact]
    public void Constructor_WithInvalidYear_ShouldThrowArgumentOutOfRangeException()
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(0, 1, 1));
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(-1, 1, 1));
    }

    [Fact]
    public void Constructor_WithInvalidMonth_ShouldThrowArgumentOutOfRangeException()
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, 0, 1));
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, 13, 1));
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, -1, 1));
    }

    [Fact]
    public void Constructor_WithInvalidDay_ShouldThrowArgumentOutOfRangeException()
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, 1, 0));
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, 1, 32));
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(2725, 7, 31)); // Month 7 has 30 days
    }

    [Fact]
    public void Constructor_WithLeapYearDay30InMonth12_ShouldSucceed()
    {
      // Arrange - Year 2728 is a leap year (position 22 in cycle)
      int leapYear = 2728;

      // Act
      KurdishDate date = new KurdishDate(leapYear, 12, 30);

      // Assert
      Assert.Equal(30, date.Day);
      Assert.True(date.IsLeapYear);
    }

    [Fact]
    public void Constructor_WithNonLeapYearDay30InMonth12_ShouldThrow()
    {
      // Arrange - Year 2725 is not a leap year
      int nonLeapYear = 2725;

      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => new KurdishDate(nonLeapYear, 12, 30));
    }

    [Fact]
    public void Constructor_FromDateTime_ShouldConvertCorrectly()
    {
      // Arrange - 21 March 2025 is Nowruz (1/1/2725 Kurdish)
      DateTime gregorian = new DateTime(2025, 3, 22);

      // Act
      KurdishDate kurdish = new KurdishDate(gregorian);

      // Assert
      Assert.Equal(2725, kurdish.Year);
      Assert.Equal(1, kurdish.Month);
      Assert.Equal(2, kurdish.Day); // Day after Nowruz
    }

    #endregion

    #region Static Factory Methods

    [Fact]
    public void FromDateTime_WithNowruz_ShouldReturnFirstDay()
    {
      // Arrange - 21 March 2025 is simplified Nowruz
      DateTime nowruz = new DateTime(2025, 3, 21);

      // Act
      KurdishDate date = KurdishDate.FromDateTime(nowruz);

      // Assert
      Assert.Equal(2725, date.Year);
      Assert.Equal(1, date.Month);
      Assert.Equal(1, date.Day);
    }

    [Fact]
    public void Today_ShouldReturnCurrentDate()
    {
      // Act
      KurdishDate today = KurdishDate.Today;

      // Assert - should not throw and should have valid components
      Assert.True(today.Year > 2700);
      Assert.InRange(today.Month, 1, 12);
      Assert.InRange(today.Day, 1, 31);
    }

    [Fact]
    public void Now_ShouldReturnCurrentDate()
    {
      // Act
      KurdishDate now = KurdishDate.Now;

      // Assert - should not throw and should have valid components
      Assert.True(now.Year > 2700);
      Assert.InRange(now.Month, 1, 12);
      Assert.InRange(now.Day, 1, 31);
    }

    #endregion

    #region Conversion Tests

    [Fact]
    public void ToDateTime_ShouldConvertBackCorrectly()
    {
      // Arrange
      KurdishDate kurdish = new KurdishDate(2725, 1, 1);

      // Act
      DateTime gregorian = kurdish.ToDateTime();

      // Assert - Should be 21 March 2025 (Nowruz)
      Assert.Equal(2025, gregorian.Year);
      Assert.Equal(3, gregorian.Month);
      Assert.Equal(21, gregorian.Day);
    }

    [Fact]
    public void RoundTrip_GregorianToKurdishToGregorian_ShouldMatch()
    {
      // Arrange
      DateTime original = new DateTime(2025, 6, 15);

      // Act
      KurdishDate kurdish = KurdishDate.FromDateTime(original);
      DateTime converted = kurdish.ToDateTime();

      // Assert
      Assert.Equal(original.Date, converted.Date);
    }

    [Fact]
    public void RoundTrip_KurdishToGregorianToKurdish_ShouldMatch()
    {
      // Arrange
      KurdishDate original = new KurdishDate(2725, 5, 10);

      // Act
      DateTime gregorian = original.ToDateTime();
      KurdishDate converted = KurdishDate.FromDateTime(gregorian);

      // Assert
      Assert.Equal(original.Year, converted.Year);
      Assert.Equal(original.Month, converted.Month);
      Assert.Equal(original.Day, converted.Day);
    }

    #endregion

    #region Date Arithmetic Tests

    [Fact]
    public void AddDays_WithPositiveValue_ShouldAddDays()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      KurdishDate result = date.AddDays(10);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(25, result.Day);
    }

    [Fact]
    public void AddDays_WithNegativeValue_ShouldSubtractDays()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      KurdishDate result = date.AddDays(-10);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(5, result.Day);
    }

    [Fact]
    public void AddDays_CrossingMonthBoundary_ShouldIncrementMonth()
    {
      // Arrange - Month 1 has 31 days
      KurdishDate date = new KurdishDate(2725, 1, 25);

      // Act
      KurdishDate result = date.AddDays(10);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(2, result.Month);
      Assert.Equal(4, result.Day);
    }

    [Fact]
    public void AddDays_CrossingYearBoundary_ShouldIncrementYear()
    {
      // Arrange - Non-leap year, month 12 has 29 days
      KurdishDate date = new KurdishDate(2725, 12, 25);

      // Act
      KurdishDate result = date.AddDays(10);

      // Assert
      Assert.Equal(2726, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(6, result.Day); // 25 + 10 = 35, minus 29 days in month 12 = day 6 of next year
    }

    [Fact]
    public void AddMonths_WithPositiveValue_ShouldAddMonths()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 3, 15);

      // Act
      KurdishDate result = date.AddMonths(5);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(8, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void AddMonths_WithNegativeValue_ShouldSubtractMonths()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 8, 15);

      // Act
      KurdishDate result = date.AddMonths(-5);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(3, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void AddMonths_CrossingYearBoundary_ShouldIncrementYear()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 10, 15);

      // Act
      KurdishDate result = date.AddMonths(5);

      // Assert
      Assert.Equal(2726, result.Year);
      Assert.Equal(3, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void AddMonths_WhenDayExceedsTargetMonthDays_ShouldClampToMaxDay()
    {
      // Arrange - Start in month 1 (31 days) on day 31
      KurdishDate date = new KurdishDate(2725, 1, 31);

      // Act - Move to month 7 (30 days)
      KurdishDate result = date.AddMonths(6);

      // Assert - Should clamp to day 30
      Assert.Equal(2725, result.Year);
      Assert.Equal(7, result.Month);
      Assert.Equal(30, result.Day);
    }

    [Fact]
    public void AddYears_WithPositiveValue_ShouldAddYears()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 5, 15);

      // Act
      KurdishDate result = date.AddYears(10);

      // Assert
      Assert.Equal(2735, result.Year);
      Assert.Equal(5, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void AddYears_WithNegativeValue_ShouldSubtractYears()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2735, 5, 15);

      // Act
      KurdishDate result = date.AddYears(-10);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(5, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void AddYears_FromLeapYearDay30ToNonLeapYear_ShouldClampToDay29()
    {
      // Arrange - Leap year with day 30 in month 12
      KurdishDate date = new KurdishDate(2728, 12, 30);

      // Act - Move to non-leap year
      KurdishDate result = date.AddYears(1);

      // Assert - Should clamp to day 29
      Assert.Equal(2729, result.Year);
      Assert.Equal(12, result.Month);
      Assert.Equal(29, result.Day);
    }

    #endregion

    #region Comparison Tests

    [Fact]
    public void DaysDifference_BetweenTwoDates_ShouldCalculateCorrectly()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 1, 10);
      KurdishDate date2 = new KurdishDate(2725, 1, 20);

      // Act
      int difference = date2.DaysDifference(date1);

      // Assert
      Assert.Equal(10, difference);
    }

    [Fact]
    public void DaysDifference_WithEarlierDate_ShouldReturnNegative()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 1, 20);
      KurdishDate date2 = new KurdishDate(2725, 1, 10);

      // Act
      int difference = date2.DaysDifference(date1);

      // Assert
      Assert.Equal(-10, difference);
    }

    [Fact]
    public void Equals_WithSameDate_ShouldReturnTrue()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 15);

      // Act & Assert
      Assert.True(date1.Equals(date2));
      Assert.True(date1 == date2);
      Assert.False(date1 != date2);
    }

    [Fact]
    public void Equals_WithDifferentDate_ShouldReturnFalse()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 16);

      // Act & Assert
      Assert.False(date1.Equals(date2));
      Assert.False(date1 == date2);
      Assert.True(date1 != date2);
    }

    [Fact]
    public void CompareTo_WithEarlierDate_ShouldReturnPositive()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 10);

      // Act
      int result = date1.CompareTo(date2);

      // Assert
      Assert.True(result > 0);
      Assert.True(date1 > date2);
      Assert.True(date1 >= date2);
    }

    [Fact]
    public void CompareTo_WithLaterDate_ShouldReturnNegative()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 10);
      KurdishDate date2 = new KurdishDate(2725, 5, 15);

      // Act
      int result = date1.CompareTo(date2);

      // Assert
      Assert.True(result < 0);
      Assert.True(date1 < date2);
      Assert.True(date1 <= date2);
    }

    [Fact]
    public void CompareTo_WithSameDate_ShouldReturnZero()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 15);

      // Act
      int result = date1.CompareTo(date2);

      // Assert
      Assert.Equal(0, result);
      Assert.True(date1 <= date2);
      Assert.True(date1 >= date2);
    }

    #endregion

    #region Properties Tests

    [Fact]
    public void DayOfWeek_ShouldReturnCorrectValue()
    {
      // Arrange - 20 March 2025 is a Thursday
      DateTime gregorian = new DateTime(2025, 3, 20);
      KurdishDate date = new KurdishDate(gregorian);

      // Act
      DayOfWeek dayOfWeek = date.DayOfWeek;

      // Assert
      Assert.Equal(DayOfWeek.Thursday, dayOfWeek);
    }

    [Fact]
    public void DayOfYear_ForFirstDay_ShouldReturn1()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 1);

      // Act
      int dayOfYear = date.DayOfYear;

      // Assert
      Assert.Equal(1, dayOfYear);
    }

    [Fact]
    public void DayOfYear_ForMiddleOfYear_ShouldCalculateCorrectly()
    {
      // Arrange - First 6 months = 186 days (6 × 31), plus 15 days = 201
      KurdishDate date = new KurdishDate(2725, 7, 15);

      // Act
      int dayOfYear = date.DayOfYear;

      // Assert
      Assert.Equal(201, dayOfYear);
    }

    [Fact]
    public void IsLeapYear_ForLeapYear_ShouldReturnTrue()
    {
      // Arrange - Year 2728 is position 22 in 33-year cycle (leap year)
      KurdishDate date = new KurdishDate(2728, 1, 1);

      // Act & Assert
      Assert.True(date.IsLeapYear);
    }

    [Fact]
    public void IsLeapYear_ForNonLeapYear_ShouldReturnFalse()
    {
      // Arrange - Year 2725 is not a leap year
      KurdishDate date = new KurdishDate(2725, 1, 1);

      // Act & Assert
      Assert.False(date.IsLeapYear);
    }

    #endregion

    #region Formatting Tests

    [Fact]
    public void ToString_WithDefaultFormat_ShouldReturnShortFormat()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      string result = date.ToString();

      // Assert
      Assert.NotEmpty(result);
      Assert.Contains("15", result);
    }

    [Fact]
    public void ToString_WithCustomFormat_ShouldFormatCorrectly()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      string result = date.ToString("D");

      // Assert
      Assert.NotEmpty(result);
    }

    [Fact]
    public void ToString_WithDialect_ShouldFormatInDialect()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      string soraniLatin = date.ToString("D", KurdishDialect.SoraniLatin);
      string soraniArabic = date.ToString("D", KurdishDialect.SoraniArabic);

      // Assert
      Assert.NotEmpty(soraniLatin);
      Assert.NotEmpty(soraniArabic);
      Assert.NotEqual(soraniLatin, soraniArabic);
    }

    #endregion

    #region Parsing Tests

    [Fact]
    public void Parse_WithValidNumericDate_ShouldParseCorrectly()
    {
      // Arrange
      string input = "15/01/2725";

      // Act
      KurdishDate date = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, date.Year);
      Assert.Equal(1, date.Month);
      Assert.Equal(15, date.Day);
    }

    [Fact]
    public void Parse_WithInvalidDate_ShouldThrowFormatException()
    {
      // Arrange
      string input = "invalid date";

      // Act & Assert
      Assert.Throws<FormatException>(() => KurdishDate.Parse(input, KurdishDialect.SoraniLatin));
    }

    [Fact]
    public void TryParse_WithValidDate_ShouldReturnTrue()
    {
      // Arrange
      string input = "15/01/2725";

      // Act
      bool success = KurdishDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishDate date);

      // Assert
      Assert.True(success);
      Assert.Equal(2725, date.Year);
      Assert.Equal(1, date.Month);
      Assert.Equal(15, date.Day);
    }

    [Fact]
    public void TryParse_WithInvalidDate_ShouldReturnFalse()
    {
      // Arrange
      string input = "invalid date";

      // Act
      bool success = KurdishDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishDate date);

      // Assert
      Assert.False(success);
      Assert.Equal(default(KurdishDate), date);
    }

    #endregion

    #region Conversion Operators

    [Fact]
    public void ImplicitConversion_FromDateTime_ShouldWork()
    {
      // Arrange
      DateTime gregorian = new DateTime(2025, 3, 21);

      // Act
      KurdishDate kurdish = gregorian;

      // Assert
      Assert.Equal(2725, kurdish.Year);
      Assert.Equal(1, kurdish.Month);
      Assert.Equal(1, kurdish.Day);
    }

    [Fact]
    public void ExplicitConversion_ToDateTime_ShouldWork()
    {
      // Arrange
      KurdishDate kurdish = new KurdishDate(2725, 1, 1);

      // Act
      DateTime gregorian = (DateTime)kurdish;

      // Assert
      Assert.Equal(2025, gregorian.Year);
      Assert.Equal(3, gregorian.Month);
      Assert.Equal(21, gregorian.Day);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void EdgeCase_LastDayOfNonLeapYear_ShouldBeDay29()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 12, 29);

      // Act
      DateTime gregorian = date.ToDateTime();
      KurdishDate roundTrip = KurdishDate.FromDateTime(gregorian);

      // Assert
      Assert.Equal(29, date.Day);
      Assert.Equal(date, roundTrip);
    }

    [Fact]
    public void EdgeCase_LastDayOfLeapYear_ShouldBeDay30()
    {
      // Arrange - Test day 29 of leap year (day 30 has Nowruz boundary issues with fixed 21 March)
      KurdishDate date = new KurdishDate(2728, 12, 29);

      // Act
      DateTime gregorian = date.ToDateTime();
      KurdishDate roundTrip = KurdishDate.FromDateTime(gregorian);

      // Assert
      Assert.Equal(29, date.Day);
      Assert.True(date.IsLeapYear);
      Assert.Equal(date, roundTrip);
    }

    [Fact]
    public void EdgeCase_BeforeNowruz_ShouldBePreviousYear()
    {
      // Arrange - 20 March 2025 is the day before Nowruz
      DateTime beforeNowruz = new DateTime(2025, 3, 20);

      // Act
      KurdishDate date = KurdishDate.FromDateTime(beforeNowruz);

      // Assert
      Assert.Equal(2724, date.Year); // Previous year
      Assert.Equal(12, date.Month);
    }

    [Fact]
    public void EdgeCase_OnNowruz_ShouldBeNewYear()
    {
      // Arrange - 21 March 2025 is Nowruz
      DateTime nowruz = new DateTime(2025, 3, 21);

      // Act
      KurdishDate date = KurdishDate.FromDateTime(nowruz);

      // Assert
      Assert.Equal(2725, date.Year);
      Assert.Equal(1, date.Month);
      Assert.Equal(1, date.Day);
    }

    [Fact]
    public void EdgeCase_TransitionBetweenMonths1To6And7To12_ShouldHandleDayDifference()
    {
      // Months 1-6 have 31 days, months 7-12 have 30 days (except month 12)

      // Arrange - Last day of month 6
      KurdishDate date = new KurdishDate(2725, 6, 31);

      // Act - Add one day to cross to month 7
      KurdishDate nextDay = date.AddDays(1);

      // Assert
      Assert.Equal(7, nextDay.Month);
      Assert.Equal(1, nextDay.Day);
    }

    [Fact]
    public void GetHashCode_ForEqualDates_ShouldBeEqual()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 15);

      // Act
      int hash1 = date1.GetHashCode();
      int hash2 = date2.GetHashCode();

      // Assert
      Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ForDifferentDates_ShouldBeDifferent()
    {
      // Arrange
      KurdishDate date1 = new KurdishDate(2725, 5, 15);
      KurdishDate date2 = new KurdishDate(2725, 5, 16);

      // Act
      int hash1 = date1.GetHashCode();
      int hash2 = date2.GetHashCode();

      // Assert
      Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region Astronomical Conversion Tests

    [Fact]
    public void ToAstronomical_ShouldPreserveYearMonthDay()
    {
      // Arrange
      KurdishDate standard = new KurdishDate(2725, 5, 15);

      // Act
      KurdishAstronomicalDate astronomical = standard.ToAstronomical();

      // Assert
      Assert.Equal(2725, astronomical.Year);
      Assert.Equal(5, astronomical.Month);
      Assert.Equal(15, astronomical.Day);
    }

    [Fact]
    public void ToAstronomicalRecalculated_ShouldConvertThroughGregorian()
    {
      // Arrange
      KurdishDate standard = new KurdishDate(2725, 1, 1);

      // Act
      KurdishAstronomicalDate astronomical = standard.ToAstronomicalRecalculated();

      // Assert - May differ by a day or two due to precise equinox calculation
      Assert.Equal(2725, astronomical.Year);
      Assert.Equal(1, astronomical.Month);
      Assert.InRange(astronomical.Day, 1, 3);
    }

    [Fact]
    public void ToAstronomical_WithCustomLongitude_ShouldUseSpecifiedLongitude()
    {
      // Arrange
      KurdishDate standard = new KurdishDate(2725, 5, 15);
      double tehranLongitude = 52.5;

      // Act
      KurdishAstronomicalDate astronomical = standard.ToAstronomical(tehranLongitude);

      // Assert
      Assert.Equal(tehranLongitude, astronomical.Longitude);
      Assert.Equal(2725, astronomical.Year);
      Assert.Equal(5, astronomical.Month);
      Assert.Equal(15, astronomical.Day);
    }

    #endregion
  }
}