using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests
{
  /// <summary>
  /// Unit tests for Kurdish date parsing functionality.
  /// </summary>
  public class KurdishDateParsingTests
  {
    // ═══════════════════════════════════════════════════════════════
    // KURDISH DATE PARSING TESTS
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public void Parse_LongFormatSoraniLatin_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_LongFormatKurmanjiLatin_ReturnsCorrectDate()
    {
      // Arrange
      string input = "6 Befranbar 2725";

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.KurmanjiLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(10, result.Month);
      Assert.Equal(6, result.Day);
    }

    [Fact]
    public void Parse_LongFormatHawramiLatin_ReturnsCorrectDate()
    {
      // Arrange
      string input = "20 Gelawêj 2725";

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.HawramiLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(5, result.Month);
      Assert.Equal(20, result.Day);
    }

    [Fact]
    public void Parse_ShortFormatLTR_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15/01/2725"; // day/month/year (LTR)

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_ShortFormatArabicISO_ReturnsCorrectDate()
    {
      // Arrange
      string input = "١٥/٠١/٢٧٢٥";
      // Expects: Day=15, Month=1, Year=2725

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniArabic);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_LongFormatArabicScript_ReturnsCorrectDate()
    {
      // Arrange
      string input = "١٥ خاکەلێوە ٢٧٢٥"; // 15 Xakelêwe 2725 in Arabic script

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniArabic);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_LongFormatRTLOrder_ReturnsCorrectDate()
    {
      // Arrange
      string input = "2725 Xakelêwe 15"; // RTL order: year month day

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_WithDashSeparator_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15-01-2725";

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_WithSpaceSeparator_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15 01 2725";

      // Act
      KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void Parse_AllMonthsSoraniLatin_ReturnsCorrectMonth()
    {
      // Test parsing each month name
      string[] monthNames = new[]
      {
        "Xakelêwe", "Gulan", "Cozerdan", "Pûşper", "Gelawêj", "Xermanan",
        "Rezber", "Gelarêzan", "Sermawez", "Befranbar", "Rêbendan", "Reşeme"
      };

      for (int i = 0; i < monthNames.Length; i++)
      {
        int expectedMonth = i + 1;
        string input = $"1 {monthNames[i]} 2725";

        KurdishDate result = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);

        Assert.Equal(expectedMonth, result.Month);
      }
    }

    [Fact]
    public void Parse_NullOrEmptyInput_ThrowsFormatException()
    {
      // Arrange & Act & Assert
      Assert.Throws<FormatException>(() => KurdishDate.Parse(null!, KurdishDialect.SoraniLatin));
      Assert.Throws<FormatException>(() => KurdishDate.Parse("", KurdishDialect.SoraniLatin));
      Assert.Throws<FormatException>(() => KurdishDate.Parse("   ", KurdishDialect.SoraniLatin));
    }

    [Fact]
    public void Parse_InvalidFormat_ThrowsFormatException()
    {
      // Arrange
      string input = "not a date";

      // Act & Assert
      Assert.Throws<FormatException>(() => KurdishDate.Parse(input, KurdishDialect.SoraniLatin));
    }

    [Fact]
    public void Parse_InvalidMonthName_ThrowsFormatException()
    {
      // Arrange
      string input = "15 InvalidMonth 2725";

      // Act & Assert
      Assert.Throws<FormatException>(() => KurdishDate.Parse(input, KurdishDialect.SoraniLatin));
    }

    [Fact]
    public void TryParse_ValidInput_ReturnsTrue()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";

      // Act
      bool success = KurdishDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishDate result);

      // Assert
      Assert.True(success);
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalse()
    {
      // Arrange
      string input = "not a date";

      // Act
      bool success = KurdishDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishDate result);

      // Assert
      Assert.False(success);
      Assert.Equal(default(KurdishDate), result);
    }

    [Fact]
    public void TryParse_NullInput_ReturnsFalse()
    {
      // Act
      bool success = KurdishDate.TryParse(null!, KurdishDialect.SoraniLatin, out KurdishDate result);

      // Assert
      Assert.False(success);
    }

    // ═══════════════════════════════════════════════════════════════
    // KURDISH ASTRONOMICAL DATE PARSING TESTS
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public void ParseAstronomical_LongFormat_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";

      // Act
      KurdishAstronomicalDate result = KurdishAstronomicalDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void ParseAstronomical_WithCustomLongitude_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";
      double longitude = 45.4375; // Sulaymaniyah

      // Act
      KurdishAstronomicalDate result = KurdishAstronomicalDate.Parse(input, KurdishDialect.SoraniLatin, longitude);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void ParseAstronomical_ShortFormat_ReturnsCorrectDate()
    {
      // Arrange
      string input = "15/01/2725";

      // Act
      KurdishAstronomicalDate result = KurdishAstronomicalDate.Parse(input, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void ParseAstronomical_ArabicScript_ReturnsCorrectDate()
    {
      // Arrange
      string input = "١٥ خاکەلێوە ٢٧٢٥";

      // Act
      KurdishAstronomicalDate result = KurdishAstronomicalDate.Parse(input, KurdishDialect.SoraniArabic);

      // Assert
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void TryParseAstronomical_ValidInput_ReturnsTrue()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";

      // Act
      bool success = KurdishAstronomicalDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishAstronomicalDate result);

      // Assert
      Assert.True(success);
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    [Fact]
    public void TryParseAstronomical_InvalidInput_ReturnsFalse()
    {
      // Arrange
      string input = "not a date";

      // Act
      bool success = KurdishAstronomicalDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishAstronomicalDate result);

      // Assert
      Assert.False(success);
    }

    [Fact]
    public void TryParseAstronomical_WithLongitude_ValidInput_ReturnsTrue()
    {
      // Arrange
      string input = "15 Xakelêwe 2725";
      double longitude = 45.4375;

      // Act
      bool success = KurdishAstronomicalDate.TryParse(input, KurdishDialect.SoraniLatin, longitude, out KurdishAstronomicalDate result);

      // Assert
      Assert.True(success);
      Assert.Equal(2725, result.Year);
      Assert.Equal(1, result.Month);
      Assert.Equal(15, result.Day);
    }

    // ═══════════════════════════════════════════════════════════════
    // ROUND-TRIP TESTS (FORMAT -> PARSE)
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public void RoundTrip_ShortFormatSoraniLatin_PreservesDate()
    {
      // Arrange
      KurdishDate original = new KurdishDate(2725, 6, 15);

      // Act
      string formatted = original.ToString("d", KurdishDialect.SoraniLatin);
      KurdishDate parsed = KurdishDate.Parse(formatted, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(original, parsed);
    }

    [Fact]
    public void RoundTrip_LongFormatSoraniLatin_PreservesDate()
    {
      // Arrange
      KurdishDate original = new KurdishDate(2725, 6, 15);

      // Act
      string formatted = original.ToString("D", KurdishDialect.SoraniLatin);
      KurdishDate parsed = KurdishDate.Parse(formatted, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(original, parsed);
    }

    [Fact]
    public void RoundTrip_LongFormatSoraniArabic_PreservesDate()
    {
      // Arrange
      KurdishDate original = new KurdishDate(2725, 6, 15);

      // Act
      string formatted = original.ToString("D", KurdishDialect.SoraniArabic);
      KurdishDate parsed = KurdishDate.Parse(formatted, KurdishDialect.SoraniArabic);

      // Assert
      Assert.Equal(original, parsed);
    }

    [Fact]
    public void RoundTrip_AstronomicalDate_PreservesDate()
    {
      // Arrange
      KurdishAstronomicalDate original = KurdishAstronomicalDate.FromErbil(2725, 6, 15);

      // Act
      string formatted = original.ToString("D", KurdishDialect.SoraniLatin);
      KurdishAstronomicalDate parsed = KurdishAstronomicalDate.Parse(formatted, KurdishDialect.SoraniLatin, 44.0);

      // Assert
      Assert.Equal(original.Year, parsed.Year);
      Assert.Equal(original.Month, parsed.Month);
      Assert.Equal(original.Day, parsed.Day);
    }
  }
}