using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests
{
  public class DateTimeKurmanjiExtensionsTests
  {
    [Fact]
    public void ToKurmanjiGregorian_LatinScript_ReturnsLatinFormatted()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = date.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("2 Kanûna Êkê 2025", result);
    }

    [Fact]
    public void ToKurmanjiGregorian_ArabicScript_ReturnsArabicFormatted()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = date.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL (year month day)
      Assert.Equal("٢٠٢٥ کانوونا ێکێ ٢", result);
    }

    [Fact]
    public void ToKurmanjiGregorian_ArabicScript_WithLTR_ReturnsLTRFormatted()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = date.ToKurmanjiGregorian(
        script: GregorianKurmanjiFormatter.ScriptType.Arabic,
        format: null,
        textDirection: KurdishTextDirection.LeftToRight);

      // Assert - Explicit LTR (day month year)
      Assert.Equal("٢ کانوونا ێکێ ٢٠٢٥", result);
    }

    [Fact]
    public void ToKurmanjiGregorian_LatinScript_WithRTL_ReturnsRTLFormatted()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = date.ToKurmanjiGregorian(
        script: GregorianKurmanjiFormatter.ScriptType.Latin,
        format: null,
        textDirection: KurdishTextDirection.RightToLeft);

      // Assert - Explicit RTL (year month day)
      Assert.Equal("2025 Kanûna Êkê 2", result);
    }

    [Fact]
    public void ToKurmanjiGregorianShort_LatinScript_ReturnsShortFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = date.ToKurmanjiGregorianShort(GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Gul 2025", result);
    }

    [Fact]
    public void ToKurmanjiGregorianShort_ArabicScript_ReturnsArabicShortFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = date.ToKurmanjiGregorianShort(GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL
      Assert.Equal("٢٠٢٥ گول ١٥", result);
    }

    [Fact]
    public void ToKurmanjiGregorianLong_LatinScript_ReturnsLongFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = date.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Gulan 2025", result);
    }

    [Fact]
    public void ToKurmanjiGregorianLong_ArabicScript_ReturnsArabicLongFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = date.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL
      Assert.Equal("٢٠٢٥ گولان ١٥", result);
    }

    [Fact]
    public void GetKurmanjiMonthName_ReturnsCorrectMonthName()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 1);

      // Act
      string result = date.GetKurmanjiMonthName(GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("Kanûna Êkê", result);
    }

    [Fact]
    public void GetKurmanjiMonthName_Abbreviated_ReturnsShortName()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 1);

      // Act
      string result = date.GetKurmanjiMonthName(GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true);

      // Assert
      Assert.Equal("Kan Êk", result);
    }

    [Fact]
    public void ToKurmanjiGregorian_WithCustomFormat_ReturnsCustomFormatted()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 25);

      // Act
      string result = date.ToKurmanjiGregorian(
        GregorianKurmanjiFormatter.ScriptType.Latin,
        format: "d MMMM yyyy");

      // Assert
      Assert.Equal("25 Kanûna Êkê 2025", result);
    }

    [Fact]
    public void ToKurmanjiGregorian_AllMonths_ReturnsCorrectNames()
    {
      // Arrange & Act & Assert - Using default Latin script which is LTR by default
      Assert.Equal("1 Kanûna Duyê 2025", new DateTime(2025, 1, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Şubat 2025", new DateTime(2025, 2, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Adar 2025", new DateTime(2025, 3, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Nîsan 2025", new DateTime(2025, 4, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Gulan 2025", new DateTime(2025, 5, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Hezîran 2025", new DateTime(2025, 6, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Tîrmeh 2025", new DateTime(2025, 7, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Tebax 2025", new DateTime(2025, 8, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Eylûl 2025", new DateTime(2025, 9, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Çiriya Êkê 2025", new DateTime(2025, 10, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Çiriya Duyê 2025", new DateTime(2025, 11, 1).ToKurmanjiGregorian());
      Assert.Equal("1 Kanûna Êkê 2025", new DateTime(2025, 12, 1).ToKurmanjiGregorian());
    }
  }
}