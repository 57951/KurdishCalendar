using System;
using Xunit;
using KurdishCalendar.Core;

namespace KurdishCalendar.Tests
{
  public class GregorianKurmanjiFormatterTests
  {
    [Fact]
    public void GetMonthName_ValidMonth_ReturnsLatinName()
    {
      // Act & Assert
      Assert.Equal("Kanûna Duyê", GregorianKurmanjiFormatter.GetMonthName(1, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Şubat", GregorianKurmanjiFormatter.GetMonthName(2, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Adar", GregorianKurmanjiFormatter.GetMonthName(3, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Nîsan", GregorianKurmanjiFormatter.GetMonthName(4, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Gulan", GregorianKurmanjiFormatter.GetMonthName(5, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Hezîran", GregorianKurmanjiFormatter.GetMonthName(6, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Tîrmeh", GregorianKurmanjiFormatter.GetMonthName(7, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Tebax", GregorianKurmanjiFormatter.GetMonthName(8, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Eylûl", GregorianKurmanjiFormatter.GetMonthName(9, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Çiriya Êkê", GregorianKurmanjiFormatter.GetMonthName(10, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Çiriya Duyê", GregorianKurmanjiFormatter.GetMonthName(11, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Equal("Kanûna Êkê", GregorianKurmanjiFormatter.GetMonthName(12, GregorianKurmanjiFormatter.ScriptType.Latin));
    }

    [Fact]
    public void GetMonthName_ValidMonth_ReturnsArabicName()
    {
      // Act & Assert
      Assert.Equal("کانوونا دویێ", GregorianKurmanjiFormatter.GetMonthName(1, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("شوبات", GregorianKurmanjiFormatter.GetMonthName(2, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("ئادار", GregorianKurmanjiFormatter.GetMonthName(3, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("نیسان", GregorianKurmanjiFormatter.GetMonthName(4, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("گولان", GregorianKurmanjiFormatter.GetMonthName(5, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("حەزیران", GregorianKurmanjiFormatter.GetMonthName(6, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("تیرمەه", GregorianKurmanjiFormatter.GetMonthName(7, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("تەباخ", GregorianKurmanjiFormatter.GetMonthName(8, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("ئەیلوول", GregorianKurmanjiFormatter.GetMonthName(9, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("چریا ێکێ", GregorianKurmanjiFormatter.GetMonthName(10, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("چریا دویێ", GregorianKurmanjiFormatter.GetMonthName(11, GregorianKurmanjiFormatter.ScriptType.Arabic));
      Assert.Equal("کانوونا ێکێ", GregorianKurmanjiFormatter.GetMonthName(12, GregorianKurmanjiFormatter.ScriptType.Arabic));
    }

    [Fact]
    public void GetMonthName_Abbreviated_ReturnsShortLatinName()
    {
      // Act & Assert
      Assert.Equal("Kan Duy", GregorianKurmanjiFormatter.GetMonthName(1, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Şub", GregorianKurmanjiFormatter.GetMonthName(2, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Ada", GregorianKurmanjiFormatter.GetMonthName(3, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Nîs", GregorianKurmanjiFormatter.GetMonthName(4, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Gul", GregorianKurmanjiFormatter.GetMonthName(5, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Hez", GregorianKurmanjiFormatter.GetMonthName(6, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Tîr", GregorianKurmanjiFormatter.GetMonthName(7, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Teb", GregorianKurmanjiFormatter.GetMonthName(8, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Eyl", GregorianKurmanjiFormatter.GetMonthName(9, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Çir Êk", GregorianKurmanjiFormatter.GetMonthName(10, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Çir Duy", GregorianKurmanjiFormatter.GetMonthName(11, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
      Assert.Equal("Kan Êk", GregorianKurmanjiFormatter.GetMonthName(12, GregorianKurmanjiFormatter.ScriptType.Latin, abbreviated: true));
    }

    [Fact]
    public void GetMonthName_Abbreviated_ReturnsShortArabicName()
    {
      // Act & Assert
      Assert.Equal("کان دوی", GregorianKurmanjiFormatter.GetMonthName(1, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("شوب", GregorianKurmanjiFormatter.GetMonthName(2, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("ئادا", GregorianKurmanjiFormatter.GetMonthName(3, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("نیس", GregorianKurmanjiFormatter.GetMonthName(4, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("گول", GregorianKurmanjiFormatter.GetMonthName(5, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("حەز", GregorianKurmanjiFormatter.GetMonthName(6, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("تیر", GregorianKurmanjiFormatter.GetMonthName(7, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("تەب", GregorianKurmanjiFormatter.GetMonthName(8, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("ئەیل", GregorianKurmanjiFormatter.GetMonthName(9, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("چر ێک", GregorianKurmanjiFormatter.GetMonthName(10, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("چر دوی", GregorianKurmanjiFormatter.GetMonthName(11, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
      Assert.Equal("کان ێک", GregorianKurmanjiFormatter.GetMonthName(12, GregorianKurmanjiFormatter.ScriptType.Arabic, abbreviated: true));
    }

    [Fact]
    public void GetMonthName_InvalidMonth_ThrowsException()
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() => 
        GregorianKurmanjiFormatter.GetMonthName(0, GregorianKurmanjiFormatter.ScriptType.Latin));
      Assert.Throws<ArgumentOutOfRangeException>(() => 
        GregorianKurmanjiFormatter.GetMonthName(13, GregorianKurmanjiFormatter.ScriptType.Latin));
    }

    [Fact]
    public void Format_LatinScript_DefaultFormat_ReturnsFormattedDate()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = GregorianKurmanjiFormatter.Format(date, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("2 Kanûna Êkê 2025", result);
    }

    [Fact]
    public void Format_ArabicScript_ReturnsArabicMonthName()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.Format(date, GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL (year month day)
      Assert.Equal("٢٠٢٥ گولان ١٥", result);
    }

    [Fact]
    public void Format_ArabicScript_WithLTR_ReturnsLTRFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Arabic,
        textDirection: KurdishTextDirection.LeftToRight);

      // Assert - Explicit LTR (day month year)
      Assert.Equal("١٥ گولان ٢٠٢٥", result);
    }

    [Fact]
    public void Format_LatinScript_WithRTL_ReturnsRTLFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Latin,
        textDirection: KurdishTextDirection.RightToLeft);

      // Assert - Explicit RTL (year month day)
      Assert.Equal("2025 Gulan 15", result);
    }

    [Fact]
    public void Format_CustomFormat_MMMM_ReturnsFullMonthName()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 25);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Latin, 
        format: "d MMMM yyyy");

      // Assert
      Assert.Equal("25 Kanûna Êkê 2025", result);
    }

    [Fact]
    public void Format_CustomFormat_MMM_ReturnsAbbreviatedMonthName()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 25);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Latin, 
        format: "dd MMM yy");

      // Assert
      Assert.Equal("25 Kan Êk 25", result);
    }

    [Fact]
    public void Format_CustomFormat_ddMMyyyy_ReturnsNumericDate()
    {
      // Arrange
      DateTime date = new DateTime(2025, 12, 2);

      // Act
      string result = GregorianKurmanjiFormatter.Format(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Latin, 
        format: "dd/MM/yyyy");

      // Assert
      Assert.Equal("02/12/2025", result);
    }

    [Fact]
    public void FormatShort_LatinScript_ReturnsShortFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatShort(date, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Gul 2025", result);
    }

    [Fact]
    public void FormatShort_ArabicScript_ReturnsArabicShortFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatShort(date, GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL
      Assert.Equal("٢٠٢٥ گول ١٥", result);
    }

    [Fact]
    public void FormatShort_ArabicScript_WithLTR_ReturnsLTRShortFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatShort(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Arabic,
        KurdishTextDirection.LeftToRight);

      // Assert - Explicit LTR
      Assert.Equal("١٥ گول ٢٠٢٥", result);
    }

    [Fact]
    public void FormatLong_LatinScript_ReturnsLongFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatLong(date, GregorianKurmanjiFormatter.ScriptType.Latin);

      // Assert
      Assert.Equal("15 Gulan 2025", result);
    }

    [Fact]
    public void FormatLong_ArabicScript_ReturnsArabicLongFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatLong(date, GregorianKurmanjiFormatter.ScriptType.Arabic);

      // Assert - Arabic script defaults to RTL
      Assert.Equal("٢٠٢٥ گولان ١٥", result);
    }

    [Fact]
    public void FormatLong_ArabicScript_WithLTR_ReturnsLTRLongFormat()
    {
      // Arrange
      DateTime date = new DateTime(2025, 5, 15);

      // Act
      string result = GregorianKurmanjiFormatter.FormatLong(
        date, 
        GregorianKurmanjiFormatter.ScriptType.Arabic,
        KurdishTextDirection.LeftToRight);

      // Assert - Explicit LTR
      Assert.Equal("١٥ گولان ٢٠٢٥", result);
    }
  }
}