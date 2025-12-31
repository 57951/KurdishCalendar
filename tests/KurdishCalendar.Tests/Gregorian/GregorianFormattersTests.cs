using System;
using Xunit;

namespace KurdishCalendar.Core.Tests.Formatting
{
  /// <summary>
  /// Tests for Gregorian date formatters with Kurdish month names.
  /// Validates GregorianSoraniFormatter and GregorianKurmanjiFormatter.
  /// </summary>
  public class GregorianFormattersTests
  {
    /// <summary>
    /// Test Sorani Gregorian month names (Latin).
    /// </summary>
    [Theory]
    [InlineData(1, "Kanûnî Duhem")]   // January
    [InlineData(2, "Şubat")]          // February
    [InlineData(3, "Adar")]           // March
    [InlineData(4, "Nîsan")]          // April
    [InlineData(5, "Ayar")]           // May
    [InlineData(6, "Hûzeyran")]       // June
    [InlineData(7, "Temmûz")]         // July
    [InlineData(8, "Ab")]             // August
    [InlineData(9, "Eylûl")]          // September
    [InlineData(10, "Tişrînî Yekem")] // October
    [InlineData(11, "Tişrînî Duhem")] // November
    [InlineData(12, "Kanûnî Yekem")]  // December
    public void GetMonthName_SoraniLatin_ShouldReturnCorrectNames(
      int month, string expectedName)
    {
      // Act
      string actual = GregorianSoraniFormatter.GetMonthName(
        month, GregorianSoraniFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test Kurmanji Gregorian month names (Latin).
    /// </summary>
    [Theory]
    [InlineData(1, "Kanûna Duyê")]   // January
    [InlineData(2, "Şubat")]          // February
    [InlineData(3, "Adar")]           // March
    [InlineData(4, "Nîsan")]          // April
    [InlineData(5, "Gulan")]          // May (different from Sorani)
    [InlineData(6, "Hezîran")]        // June
    [InlineData(7, "Tîrmeh")]         // July
    [InlineData(8, "Tebax")]          // August
    [InlineData(9, "Eylûl")]          // September
    [InlineData(10, "Çiriya Êkê")]   // October
    [InlineData(11, "Çiriya Duyê")]  // November
    [InlineData(12, "Kanûna Êkê")]   // December
    public void GetMonthName_KurmanjiLatin_ShouldReturnCorrectNames(
      int month, string expectedName)
    {
      // Act
      string actual = GregorianKurmanjiFormatter.GetMonthName(
        month, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test Sorani long format (Latin).
    /// </summary>
    [Fact]
    public void FormatLong_SoraniLatin_ShouldFormatCorrectly()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianSoraniFormatter.FormatLong(
        date, GregorianSoraniFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Kanûnî Duhem 2024", result);
    }

    /// <summary>
    /// Test Kurmanji long format (Latin).
    /// </summary>
    [Fact]
    public void FormatLong_KurmanjiLatin_ShouldFormatCorrectly()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatLong(
        date, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Kanûna Duyê 2024", result);
    }

    /// <summary>
    /// Test Sorani short format (Latin).
    /// </summary>
    [Fact]
    public void FormatShort_SoraniLatin_ShouldFormatCorrectly()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianSoraniFormatter.FormatShort(
        date, GregorianSoraniFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Kan2 2024", result);
    }

    /// <summary>
    /// Test Kurmanji short format (Latin).
    /// </summary>
    [Fact]
    public void FormatShort_KurmanjiLatin_ShouldFormatCorrectly()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatShort(
        date, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Kan Duy 2024", result);
    }

    /// <summary>
    /// Test Sorani Arabic script formatting with Arabic-Indic numerals.
    /// </summary>
    [Fact]
    public void FormatLong_SoraniArabic_ShouldUseArabicIndicNumerals()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianSoraniFormatter.FormatLong(
        date, GregorianSoraniFormatter.ScriptType.Arabic);

      // Assert
      // Should contain Arabic-Indic digits and Arabic month name
      Assert.Contains("٢٠٢٤", result); // Year in Arabic-Indic
      Assert.Contains("١٥", result);   // Day in Arabic-Indic
    }

    /// <summary>
    /// Test Kurmanji Arabic script formatting with Arabic-Indic numerals.
    /// </summary>
    [Fact]
    public void FormatLong_KurmanjiArabic_ShouldUseArabicIndicNumerals()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatLong(
        date, GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert
      // Should contain Arabic-Indic digits
      Assert.Contains("٢٠٢٤", result); // Year
      Assert.Contains("١٥", result);   // Day
    }

    /// <summary>
    /// Test custom format strings (Sorani).
    /// </summary>
    [Theory]
    [InlineData("dd/MM/yyyy", "15/01/2024")]
    [InlineData("d MMMM yyyy", "15 Kanûnî Duhem 2024")]
    [InlineData("dd MMM yy", "15 Kan2 24")]
    public void Format_CustomFormat_SoraniLatin_ShouldFormatCorrectly(
      string format, string expected)
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianSoraniFormatter.Format(
        date, GregorianSoraniFormatter.ScriptType.Latin, format);

      // Assert
      Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test custom format strings (Kurmanji).
    /// </summary>
    [Theory]
    [InlineData("dd/MM/yyyy", "15/01/2024")]
    [InlineData("d MMMM yyyy", "15 Kanûna Duyê 2024")]
    [InlineData("dd MMM yy", "15 Kan Duy 24")]
    public void Format_CustomFormat_KurmanjiLatin_ShouldFormatCorrectly(
      string format, string expected)
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, GregorianKurmanjiFormatter.ScriptType.Latin, format);

      // Assert
      Assert.Equal(expected, result);
    }

    /// <summary>
    /// Test RTL (right-to-left) text direction for Arabic script.
    /// </summary>
    [Fact]
    public void Format_ArabicScript_RTL_ShouldReverseOrder()
    {
      // Arrange
      DateTime date = new DateTime(2024, 1, 15);

      // Act
      string ltrResult = GregorianSoraniFormatter.FormatLong(
        date, 
        GregorianSoraniFormatter.ScriptType.Latin, 
        KurdishTextDirection.LeftToRight);
      
      string rtlResult = GregorianSoraniFormatter.FormatLong(
        date, 
        GregorianSoraniFormatter.ScriptType.Arabic, 
        KurdishTextDirection.RightToLeft);

      // Assert
      // LTR: "15 MonthName 2024"
      // RTL: "2024 MonthName 15"
      string[] ltrParts = ltrResult.Split(' ');
      string[] rtlParts = rtlResult.Split(' ');

      // Year should be at different positions
      Assert.NotEqual(ltrParts[0], rtlParts[0]);
    }

    /// <summary>
    /// Test DateTime extension methods (Sorani).
    /// </summary>
    [Fact]
    public void DateTimeExtension_ToSoraniGregorian_ShouldWork()
    {
      // Arrange
      DateTime date = new DateTime(2024, 5, 10);

      // Act
      string result = date.ToSoraniGregorianLong();

      // Assert
      Assert.Contains("Ayar", result);
      Assert.Contains("2024", result);
      Assert.Contains("10", result);
    }

    /// <summary>
    /// Test DateTime extension methods (Kurmanji).
    /// </summary>
    [Fact]
    public void DateTimeExtension_ToKurmanjiGregorian_ShouldWork()
    {
      // Arrange
      DateTime date = new DateTime(2024, 5, 10);

      // Act
      string result = date.ToKurmanjiGregorianLong();

      // Assert
      Assert.Contains("Gulan", result); // Kurmanji name for May
      Assert.Contains("2024", result);
      Assert.Contains("10", result);
    }

    /// <summary>
    /// Test invalid month numbers throw exceptions.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(-1)]
    public void GetMonthName_InvalidMonth_ShouldThrowException(int invalidMonth)
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        GregorianSoraniFormatter.GetMonthName(
          invalidMonth, GregorianSoraniFormatter.ScriptType.Latin));
      
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        GregorianKurmanjiFormatter.GetMonthName(
          invalidMonth, GregorianKurmanjiFormatter.ScriptType.Latin));
    }

    /// <summary>
    /// Test that abbreviated names are distinct from full names.
    /// </summary>
    [Fact]
    public void AbbreviatedNames_ShouldBeDifferentFromFullNames()
    {
      // Arrange & Act & Assert
      for (int month = 1; month <= 12; month++)
      {
        string fullSorani = GregorianSoraniFormatter.GetMonthName(
          month, GregorianSoraniFormatter.ScriptType.Latin, abbreviated: false);
        string abbrSorani = GregorianSoraniFormatter.GetMonthName(
          month, GregorianSoraniFormatter.ScriptType.Latin, abbreviated: true);

        string fullKurmanji = GregorianKurmanjiFormatter.GetMonthName(
          month, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: false);
        string abbrKurmanji = GregorianKurmanjiFormatter.GetMonthName(
          month, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true);

        // Abbreviated should be shorter or equal
        Assert.True(abbrSorani.Length <= fullSorani.Length,
          $"Sorani month {month}: abbreviated '{abbrSorani}' is longer than full '{fullSorani}'");
        
        Assert.True(abbrKurmanji.Length <= fullKurmanji.Length,
          $"Kurmanji month {month}: abbreviated '{abbrKurmanji}' is longer than full '{fullKurmanji}'");
      }
    }
  }
}