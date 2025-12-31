using System;
using Xunit;

namespace KurdishCalendar.Core.Tests.Parsing
{
  /// <summary>
  /// Tests for KurdishDateParser class.
  /// Validates parsing of Kurdish date strings in various formats and dialects.
  /// </summary>
  public class KurdishDateParserTests
  {
    /// <summary>
    /// Test parsing numeric short format dates (dd/MM/yyyy).
    /// Latin scripts use left-to-right order.
    /// </summary>
    [Theory]
    [InlineData("15/01/2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("01/12/2724", KurdishDialect.KurmanjiLatin, 2724, 12, 1)]
    [InlineData("31/06/2724", KurdishDialect.HawramiLatin, 2724, 6, 31)]
    public void Parse_NumericFormat_LatinScript_ShouldParseCorrectly(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test parsing numeric format dates in Arabic script (yyyy/MM/dd).
    /// Arabic scripts use right-to-left order.
    /// </summary>
    [Theory]
    [InlineData("١٥/٠١/٢٧٢٤", KurdishDialect.SoraniArabic, 2724, 1, 15)]        // LTR: DD/MM/YYYY
    [InlineData("٢٧٢٤ خاکەلێوە ١٥", KurdishDialect.SoraniArabic, 2724, 1, 15)]  // RTL: YYYY MonthName DD
    public void Parse_NumericFormat_ArabicScript_ShouldParseCorrectly(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test parsing long format with month names (Sorani Latin).
    /// </summary>
    [Theory]
    [InlineData("15 Xakelêwe 2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("20 Gulan 2724", KurdishDialect.SoraniLatin, 2724, 2, 20)]
    [InlineData("1 Reşeme 2724", KurdishDialect.SoraniLatin, 2724, 12, 1)]
    [InlineData("2724 Xakelêwe 15", KurdishDialect.SoraniLatin, 2724, 1, 15)] // Reverse order
    public void Parse_LongFormat_SoraniLatin_ShouldParseCorrectly(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test parsing with Hawrami month names.
    /// </summary>
    [Theory]
    [InlineData("1 Newroz 2724", KurdishDialect.HawramiLatin, 2724, 1, 1)]
    [InlineData("15 Pajerej 2724", KurdishDialect.HawramiLatin, 2724, 2, 15)]
    [InlineData("29 Siyawkam 2724", KurdishDialect.HawramiLatin, 2724, 12, 29)]
    public void Parse_LongFormat_HawramiLatin_ShouldParseCorrectly(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test TryParse method with valid inputs.
    /// </summary>
    [Theory]
    [InlineData("15/01/2724", KurdishDialect.SoraniLatin)]
    [InlineData("15 Xakelêwe 2724", KurdishDialect.SoraniLatin)]
    [InlineData("١٥/٠١/٢٧٢٤", KurdishDialect.SoraniArabic)]
    public void TryParse_ValidInput_ShouldReturnTrue(string input, KurdishDialect dialect)
    {
      // Act
      bool result = KurdishDateParser.TryParse(input, dialect, out KurdishDate date);

      // Assert
      Assert.True(result);
      Assert.NotEqual(default(KurdishDate), date);
    }

    /// <summary>
    /// Test TryParse method with invalid inputs.
    /// </summary>
    [Theory]
    [InlineData("", KurdishDialect.SoraniLatin)]
    [InlineData("   ", KurdishDialect.SoraniLatin)]
    [InlineData("invalid", KurdishDialect.SoraniLatin)]
    [InlineData("32/01/2724", KurdishDialect.SoraniLatin)] // Invalid day
    [InlineData("15/13/2724", KurdishDialect.SoraniLatin)] // Invalid month
    [InlineData("abc def ghi", KurdishDialect.SoraniLatin)]
    public void TryParse_InvalidInput_ShouldReturnFalse(string input, KurdishDialect dialect)
    {
      // Act
      bool result = KurdishDateParser.TryParse(input, dialect, out KurdishDate date);

      // Assert
      Assert.False(result);
      Assert.Equal(default(KurdishDate), date);
    }

    /// <summary>
    /// Test Parse method with invalid inputs (should throw).
    /// </summary>
    [Theory]
    [InlineData("", KurdishDialect.SoraniLatin)]
    [InlineData("   ", KurdishDialect.SoraniLatin)]
    [InlineData("invalid", KurdishDialect.SoraniLatin)]
    public void Parse_InvalidInput_ShouldThrowException(string input, KurdishDialect dialect)
    {
      // Act & Assert
      Assert.Throws<FormatException>(() => KurdishDateParser.Parse(input, dialect));
    }

    /// <summary>
    /// Test parsing with different separator characters.
    /// </summary>
    [Theory]
    [InlineData("15/01/2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("15-01-2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("15 01 2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    public void Parse_DifferentSeparators_ShouldParseCorrectly(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test case-insensitive month name matching.
    /// </summary>
    [Theory]
    [InlineData("15 xakelêwe 2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("15 XAKELÊWE 2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    [InlineData("15 XaKeLêWe 2724", KurdishDialect.SoraniLatin, 2724, 1, 15)]
    public void Parse_MonthNames_ShouldBeCaseInsensitive(
      string input, KurdishDialect dialect, int expectedYear, int expectedMonth, int expectedDay)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.Equal(expectedYear, result.Year);
      Assert.Equal(expectedMonth, result.Month);
      Assert.Equal(expectedDay, result.Day);
    }

    /// <summary>
    /// Test parsing astronomical dates.
    /// </summary>
    [Theory]
    [InlineData("15 Xakelêwe 2724", KurdishDialect.SoraniLatin, 44.0)]
    [InlineData("1 Newroz 2724", KurdishDialect.HawramiLatin, 44.0)]
    public void ParseAstronomical_ValidInput_ShouldParseCorrectly(
      string input, KurdishDialect dialect, double longitude)
    {
      // Act
      KurdishAstronomicalDate result = KurdishDateParser.ParseAstronomical(input, dialect, longitude);

      // Assert
      Assert.NotEqual(default(KurdishAstronomicalDate), result);
      Assert.Equal(longitude, result.Longitude);
    }

    /// <summary>
    /// Test TryParseAstronomical method.
    /// </summary>
    [Theory]
    [InlineData("15 Xakelêwe 2724", KurdishDialect.SoraniLatin, 44.0, true)]
    [InlineData("invalid", KurdishDialect.SoraniLatin, 44.0, false)]
    public void TryParseAstronomical_ShouldReturnExpectedResult(
      string input, KurdishDialect dialect, double longitude, bool expectedSuccess)
    {
      // Act
      bool result = KurdishDateParser.TryParseAstronomical(
        input, dialect, longitude, out KurdishAstronomicalDate date);

      // Assert
      Assert.Equal(expectedSuccess, result);

      if (expectedSuccess)
      {
        Assert.NotEqual(default(KurdishAstronomicalDate), date);
      }
      else
      {
        Assert.Equal(default(KurdishAstronomicalDate), date);
      }
    }

    /// <summary>
    /// Test parsing dates with Gregorian month names (Sorani).
    /// </summary>
    [Theory]
    [InlineData("15 Kanûnî Duhem 2024", KurdishDialect.SoraniGregorianLatin)]
    [InlineData("20 Nîsan 2024", KurdishDialect.SoraniGregorianLatin)]
    public void Parse_GregorianMonthNames_SoraniLatin_ShouldParseCorrectly(
      string input, KurdishDialect dialect)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.NotEqual(default(KurdishDate), result);
    }

    /// <summary>
    /// Test parsing dates with Gregorian month names (Kurmanji).
    /// </summary>
    [Theory]
    [InlineData("15 Kanûna Duyê 2024", KurdishDialect.KurmanjiGregorianLatin)]
    [InlineData("20 Nîsan 2024", KurdishDialect.KurmanjiGregorianLatin)]
    public void Parse_GregorianMonthNames_KurmanjiLatin_ShouldParseCorrectly(
      string input, KurdishDialect dialect)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.NotEqual(default(KurdishDate), result);
    }

    /// <summary>
    /// Test that whitespace is handled correctly.
    /// </summary>
    [Theory]
    [InlineData("  15 Xakelêwe 2724  ", KurdishDialect.SoraniLatin)]
    [InlineData("15  Xakelêwe  2724", KurdishDialect.SoraniLatin)]
    [InlineData("  15/01/2724  ", KurdishDialect.SoraniLatin)]
    public void Parse_ExtraWhitespace_ShouldBeTrimmedAndParse(
      string input, KurdishDialect dialect)
    {
      // Act
      KurdishDate result = KurdishDateParser.Parse(input, dialect);

      // Assert
      Assert.NotEqual(default(KurdishDate), result);
    }
  }
}