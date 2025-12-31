using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests
{
  public class KurdishDateKurmanjiGregorianTests
  {
    [Fact]
    public void ToString_KurmanjiGregorianLatin_ReturnsFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15); // 15th Xakelêwe 2725

      // Act
      string result = date.ToString("D", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      // Note: KurdishDate uses traditional Kurdish calendar months (Xakelêwe, Gulan, etc.)
      // KurmanjiGregorianLatin is a dialect option, but the month names come from the calendar system
      // This test verifies the dialect formatting works
      Assert.Contains("15", result);
      Assert.Contains("2725", result);
    }

    [Fact]
    public void ToString_KurmanjiGregorianArabic_ReturnsArabicFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      string result = date.ToString("D", KurdishDialect.KurmanjiGregorianArabic);

      // Assert - Should use Arabic-Indic numerals
      Assert.Contains("١٥", result); // 15 in Arabic-Indic
      Assert.Contains("٢٧٢٥", result); // 2725 in Arabic-Indic
    }

    [Fact]
    public void ToString_ShortFormat_KurmanjiGregorianLatin_ReturnsNumericDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 12, 25);

      // Act
      string result = date.ToString("d", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      Assert.Matches(@"\d{2}/\d{2}/\d{4}", result);
    }

    [Fact]
    public void ToString_ShortFormat_KurmanjiGregorianArabic_ReturnsArabicNumericDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 12, 25);

      // Act
      string result = date.ToString("d", KurdishDialect.KurmanjiGregorianArabic);

      // Assert
      Assert.Contains("/", result);
      Assert.Contains("٢٥", result); // Day or month
      Assert.Contains("٢٧٢٥", result); // Year
    }

    [Fact]
    public void ToString_CustomFormat_KurmanjiGregorianLatin_ReturnsFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 6, 15);

      // Act
      string result = date.ToString("dd MMMM yyyy", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      Assert.Contains("15", result);
      Assert.Contains("2725", result);
      // KurmanjiGregorianLatin uses Gregorian month names, so month 6 = June = Hezîran
      Assert.Contains("Hezîran", result);
    }

    [Fact]
    public void ToString_AbbreviatedMonth_KurmanjiGregorianLatin_ReturnsShortMonthName()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 1);

      // Act
      string result = date.ToString("dd MMM yyyy", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      Assert.Contains("01", result);
      Assert.Contains("Kan Duy", result); // Abbreviated January in Kurmanji Gregorian
      Assert.Contains("2725", result);
    }

    [Fact]
    public void ConvertFromGregorian_FormatAsKurmanjiGregorian_WorksCorrectly()
    {
      // Arrange
      DateTime gregorian = new DateTime(2025, 3, 21); // Nowroz (approximately)

      // Act
      KurdishDate kurdish = new KurdishDate(gregorian);
      string formatted = kurdish.ToString("D", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      Assert.Equal(2725, kurdish.Year);
      // Note: Exact month/day depends on astronomical calculation
      // March 21 in Gregorian typically falls around start of Kurdish year
      Assert.True(kurdish.Month >= 1 && kurdish.Month <= 2);
      Assert.Contains("2725", formatted);
    }

    [Fact]
    public void IsArabicScript_KurmanjiGregorianArabic_ReturnsTrue()
    {
      // This tests that the IsArabicScript method recognizes the new dialect
      // Act
      bool isArabic = KurdishCultureInfo.IsArabicScript(KurdishDialect.KurmanjiGregorianArabic);

      // Assert
      Assert.True(isArabic);
    }

    [Fact]
    public void IsArabicScript_KurmanjiGregorianLatin_ReturnsFalse()
    {
      // Act
      bool isArabic = KurdishCultureInfo.IsArabicScript(KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      Assert.False(isArabic);
    }

    [Fact]
    public void GetMonthName_KurmanjiGregorianLatin_ReturnsGregorianMonthName()
    {
      // Act
      string monthName = KurdishCultureInfo.GetMonthName(1, KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      // KurmanjiGregorianLatin uses Gregorian month names (January = Kanûna Duyê)
      Assert.Equal("Kanûna Duyê", monthName);
    }

    [Fact]
    public void GetMonthName_Abbreviated_KurmanjiGregorianLatin_ReturnsShortName()
    {
      // Act
      string monthName = KurdishCultureInfo.GetMonthName(1, KurdishDialect.KurmanjiGregorianLatin, abbreviated: true);

      // Assert
      // Abbreviated January in Kurmanji Gregorian
      Assert.Equal("Kan Duy", monthName);
    }

    [Fact]
    public void AllMonths_KurmanjiGregorianLatin_HaveNames()
    {
      // Arrange & Act & Assert
      for (int month = 1; month <= 12; month++)
      {
        string fullName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.KurmanjiGregorianLatin);
        string abbrevName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.KurmanjiGregorianLatin, abbreviated: true);

        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
        Assert.NotNull(abbrevName);
        Assert.NotEmpty(abbrevName);
      }
    }

    [Fact]
    public void AllMonths_KurmanjiGregorianArabic_HaveNames()
    {
      // Arrange & Act & Assert
      for (int month = 1; month <= 12; month++)
      {
        string fullName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.KurmanjiGregorianArabic);
        string abbrevName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.KurmanjiGregorianArabic, abbreviated: true);

        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
        Assert.NotNull(abbrevName);
        Assert.NotEmpty(abbrevName);
      }
    }
  }
}