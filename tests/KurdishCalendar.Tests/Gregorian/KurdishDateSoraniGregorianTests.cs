using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests
{
  public class KurdishDateSoraniGregorianTests
  {
    [Fact]
    public void ToString_SoraniGregorianLatin_ReturnsFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15); // 15th Xakelêwe 2725

      // Act
      string result = date.ToString("D", KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.Contains("15", result);
      Assert.Contains("2725", result);
    }

    [Fact]
    public void ToString_SoraniGregorianArabic_ReturnsArabicFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 15);

      // Act
      string result = date.ToString("D", KurdishDialect.SoraniGregorianArabic);

      // Assert - Should use Arabic-Indic numerals
      Assert.Contains("١٥", result); // 15 in Arabic-Indic
      Assert.Contains("٢٧٢٥", result); // 2725 in Arabic-Indic
    }

    [Fact]
    public void ToString_ShortFormat_SoraniGregorianLatin_ReturnsNumericDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 12, 25);

      // Act
      string result = date.ToString("d", KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.Matches(@"\d{2}/\d{2}/\d{4}", result);
    }

    [Fact]
    public void ToString_CustomFormat_SoraniGregorianLatin_ReturnsFormattedDate()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 6, 15);

      // Act
      string result = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.Contains("15", result);
      Assert.Contains("2725", result);
      Assert.Contains("Hûzeyran", result); // Month 6 = June = Hûzeyran in Sorani Gregorian
    }

    [Fact]
    public void IsArabicScript_SoraniGregorianArabic_ReturnsTrue()
    {
      // Act
      bool isArabic = KurdishCultureInfo.IsArabicScript(KurdishDialect.SoraniGregorianArabic);

      // Assert
      Assert.True(isArabic);
    }

    [Fact]
    public void IsArabicScript_SoraniGregorianLatin_ReturnsFalse()
    {
      // Act
      bool isArabic = KurdishCultureInfo.IsArabicScript(KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.False(isArabic);
    }

    [Fact]
    public void GetMonthName_SoraniGregorianLatin_ReturnsGregorianMonthName()
    {
      // Act
      string monthName = KurdishCultureInfo.GetMonthName(1, KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.Equal("Kanûnî Duhem", monthName); // January in Sorani Gregorian
    }

    [Fact]
    public void AllMonths_SoraniGregorianLatin_HaveNames()
    {
      // Arrange & Act & Assert
      for (int month = 1; month <= 12; month++)
      {
        string fullName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.SoraniGregorianLatin);
        string abbrevName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.SoraniGregorianLatin, abbreviated: true);

        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
        Assert.NotNull(abbrevName);
        Assert.NotEmpty(abbrevName);
      }
    }

    [Fact]
    public void CompareDialects_SoraniVsKurmanji_SameCalendarDifferentDialect()
    {
      // Arrange
      KurdishDate date = new KurdishDate(2725, 1, 1);

      // Act
      string sorani = date.ToString("D", KurdishDialect.SoraniGregorianLatin);
      string kurmanji = date.ToString("D", KurdishDialect.KurmanjiGregorianLatin);

      // Assert
      // Both should format the same date (traditional Kurdish calendar)
      // Difference is in dialect-specific formatting nuances
      Assert.Contains("2725", sorani);
      Assert.Contains("2725", kurmanji);
    }
  }
}