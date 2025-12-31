using System;
using Xunit;

namespace KurdishCalendar.Core.Tests.Culture
{
  /// <summary>
  /// Tests for KurdishCultureInfo class.
  /// Validates month names, day names, and number formatting across dialects and scripts.
  /// </summary>
  public class KurdishCultureInfoTests
  {
    /// <summary>
    /// Test that all 12 months have names in all dialects.
    /// </summary>
    [Theory]
    [InlineData(KurdishDialect.SoraniLatin)]
    [InlineData(KurdishDialect.SoraniArabic)]
    [InlineData(KurdishDialect.KurmanjiLatin)]
    [InlineData(KurdishDialect.KurmanjiArabic)]
    [InlineData(KurdishDialect.HawramiLatin)]
    [InlineData(KurdishDialect.HawramiArabic)]
    [InlineData(KurdishDialect.SoraniGregorianLatin)]
    [InlineData(KurdishDialect.SoraniGregorianArabic)]
    [InlineData(KurdishDialect.KurmanjiGregorianLatin)]
    [InlineData(KurdishDialect.KurmanjiGregorianArabic)]
    public void GetMonthName_AllMonthsAllDialects_ShouldReturnNames(KurdishDialect dialect)
    {
      // Act & Assert
      for (int month = 1; month <= 12; month++)
      {
        string fullName = KurdishCultureInfo.GetMonthName(month, dialect, abbreviated: false);
        string abbreviatedName = KurdishCultureInfo.GetMonthName(month, dialect, abbreviated: true);

        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
        Assert.NotNull(abbreviatedName);
        Assert.NotEmpty(abbreviatedName);

        // Abbreviated should be shorter than or equal to full name
        Assert.True(abbreviatedName.Length <= fullName.Length,
          $"Abbreviated name '{abbreviatedName}' is longer than full name '{fullName}' " +
          $"for month {month} in {dialect}");
      }
    }

    /// <summary>
    /// Test specific Sorani Latin month names (Kurdish calendar).
    /// </summary>
    [Theory]
    [InlineData(1, "Xakelêwe")]
    [InlineData(2, "Gulan")]
    [InlineData(3, "Cozerdan")]
    [InlineData(6, "Xermanan")]
    [InlineData(12, "Reşeme")]
    public void GetMonthName_SoraniLatin_ShouldReturnCorrectNames(int month, string expectedName)
    {
      // Act
      string actual = KurdishCultureInfo.GetMonthName(month, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test specific Sorani Gregorian month names.
    /// </summary>
    [Theory]
    [InlineData(1, "Kanûnî Duhem")]  // January - Second Kanun
    [InlineData(4, "Nîsan")]          // April
    [InlineData(10, "Tişrînî Yekem")] // October - First Tişrîn
    [InlineData(12, "Kanûnî Yekem")]  // December - First Kanun
    public void GetMonthName_SoraniGregorianLatin_ShouldReturnCorrectNames(
      int month, string expectedName)
    {
      // Act
      string actual = KurdishCultureInfo.GetMonthName(month, KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test that all 7 days of the week have names in all dialects.
    /// </summary>
    [Theory]
    [InlineData(KurdishDialect.SoraniLatin)]
    [InlineData(KurdishDialect.SoraniArabic)]
    [InlineData(KurdishDialect.KurmanjiLatin)]
    [InlineData(KurdishDialect.KurmanjiArabic)]
    [InlineData(KurdishDialect.HawramiLatin)]
    [InlineData(KurdishDialect.HawramiArabic)]
    public void GetDayName_AllDaysAllDialects_ShouldReturnNames(KurdishDialect dialect)
    {
      // Act & Assert
      for (int day = 0; day < 7; day++)
      {
        DayOfWeek dayOfWeek = (DayOfWeek)day;
        string fullName = KurdishCultureInfo.GetDayName(dayOfWeek, dialect, abbreviated: false);
        string abbreviatedName = KurdishCultureInfo.GetDayName(dayOfWeek, dialect, abbreviated: true);

        Assert.NotNull(fullName);
        Assert.NotEmpty(fullName);
        Assert.NotNull(abbreviatedName);
        Assert.NotEmpty(abbreviatedName);

        // Abbreviated should be shorter than or equal to full name
        Assert.True(abbreviatedName.Length <= fullName.Length,
          $"Abbreviated day name '{abbreviatedName}' is longer than full name '{fullName}' " +
          $"for {dayOfWeek} in {dialect}");
      }
    }

    /// <summary>
    /// Test specific Sorani Latin day names.
    /// </summary>
    [Theory]
    [InlineData(DayOfWeek.Sunday, "Yekşemme")]
    [InlineData(DayOfWeek.Monday, "Duşemme")]
    [InlineData(DayOfWeek.Friday, "Hênî")]
    [InlineData(DayOfWeek.Saturday, "Şemme")]
    public void GetDayName_SoraniLatin_ShouldReturnCorrectNames(
      DayOfWeek dayOfWeek, string expectedName)
    {
      // Act
      string actual = KurdishCultureInfo.GetDayName(dayOfWeek, KurdishDialect.SoraniLatin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test Arabic script detection.
    /// </summary>
    [Theory]
    [InlineData(KurdishDialect.SoraniArabic, true)]
    [InlineData(KurdishDialect.SoraniLatin, false)]
    [InlineData(KurdishDialect.KurmanjiArabic, true)]
    [InlineData(KurdishDialect.KurmanjiLatin, false)]
    [InlineData(KurdishDialect.HawramiArabic, true)]
    [InlineData(KurdishDialect.HawramiLatin, false)]
    [InlineData(KurdishDialect.SoraniGregorianArabic, true)]
    [InlineData(KurdishDialect.SoraniGregorianLatin, false)]
    [InlineData(KurdishDialect.KurmanjiGregorianArabic, true)]
    [InlineData(KurdishDialect.KurmanjiGregorianLatin, false)]
    public void IsArabicScript_ShouldCorrectlyIdentifyScript(
      KurdishDialect dialect, bool expectedIsArabic)
    {
      // Act
      bool actual = KurdishCultureInfo.IsArabicScript(dialect);

      // Assert
      Assert.Equal(expectedIsArabic, actual);
    }

    /// <summary>
    /// Test number formatting in Latin script (should use Western digits).
    /// </summary>
    [Theory]
    [InlineData(KurdishDialect.SoraniLatin, 2024, "2024")]
    [InlineData(KurdishDialect.KurmanjiLatin, 15, "15")]
    [InlineData(KurdishDialect.HawramiLatin, 1, "1")]
    public void FormatNumber_LatinScript_ShouldUseWesternDigits(
      KurdishDialect dialect, int number, string expected)
    {
      // Act
      string actual = KurdishCultureInfo.FormatNumber(number, dialect);

      // Assert
      Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test number formatting in Arabic script (should use Arabic-Indic numerals).
    /// Arabic-Indic digits: ٠١٢٣٤٥٦٧٨٩ (U+0660 to U+0669)
    /// </summary>
    [Theory]
    [InlineData(KurdishDialect.SoraniArabic, 0, "٠")]
    [InlineData(KurdishDialect.SoraniArabic, 1, "١")]
    [InlineData(KurdishDialect.SoraniArabic, 2, "٢")]
    [InlineData(KurdishDialect.SoraniArabic, 9, "٩")]
    [InlineData(KurdishDialect.SoraniArabic, 2024, "٢٠٢٤")]
    [InlineData(KurdishDialect.KurmanjiArabic, 15, "١٥")]
    public void FormatNumber_ArabicScript_ShouldUseArabicIndicDigits(
      KurdishDialect dialect, int number, string expected)
    {
      // Act
      string actual = KurdishCultureInfo.FormatNumber(number, dialect);

      // Assert
      Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test that invalid month numbers throw exceptions.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(-1)]
    [InlineData(100)]
    public void GetMonthName_InvalidMonth_ShouldThrowException(int invalidMonth)
    {
      // Act & Assert
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        KurdishCultureInfo.GetMonthName(invalidMonth, KurdishDialect.SoraniLatin));
    }

    /// <summary>
    /// Test Hawrami month names (distinct from Sorani/Kurmanji).
    /// Source: https://zaniary.com/blog/61ec1e25bfc4e
    /// </summary>
    [Theory]
    [InlineData(1, "Newroz")]      // Hawrami name for first month
    [InlineData(2, "Pajerej")]     // Hawrami name for second month
    [InlineData(12, "Siyawkam")]   // Hawrami name for twelfth month
    public void GetMonthName_HawramiLatin_ShouldReturnCorrectNames(int month, string expectedName)
    {
      // Act
      string actual = KurdishCultureInfo.GetMonthName(month, KurdishDialect.HawramiLatin);

      // Assert
      Assert.Equal(expectedName, actual);
    }

    /// <summary>
    /// Test Hawrami day name for Friday (hellîne) which is distinct from Kurmanji.
    /// Source: D.N. MacKenzie's "The Dialect of Awroman"
    /// </summary>
    [Fact]
    public void GetDayName_HawramiFriday_ShouldReturnHellîne()
    {
      // Act
      string actual = KurdishCultureInfo.GetDayName(
        DayOfWeek.Friday, 
        KurdishDialect.HawramiLatin,
        abbreviated: false);

      // Assert
      Assert.Equal("Hellîne", actual);
    }

    /// <summary>
    /// Test that month names are distinct across Kurdish calendar vs Gregorian calendar.
    /// </summary>
    [Fact]
    public void MonthNames_KurdishVsGregorian_ShouldBeDifferent()
    {
      // Arrange
      int month = 1; // January vs Xakelêwe

      // Act
      string kurdishCalendar = KurdishCultureInfo.GetMonthName(
        month, KurdishDialect.SoraniLatin);
      string gregorianCalendar = KurdishCultureInfo.GetMonthName(
        month, KurdishDialect.SoraniGregorianLatin);

      // Assert
      Assert.NotEqual(kurdishCalendar, gregorianCalendar);
      Assert.Equal("Xakelêwe", kurdishCalendar);
      Assert.Equal("Kanûnî Duhem", gregorianCalendar);
    }

    /// <summary>
    /// Test that abbreviated names are actually shorter than full names
    /// for at least some cases.
    /// </summary>
    [Fact]
    public void AbbreviatedNames_ShouldBeShorterThanFullNames_InSomeCases()
    {
      // Arrange
      int shorterCount = 0;

      // Act
      for (int month = 1; month <= 12; month++)
      {
        string full = KurdishCultureInfo.GetMonthName(
          month, KurdishDialect.SoraniLatin, abbreviated: false);
        string abbr = KurdishCultureInfo.GetMonthName(
          month, KurdishDialect.SoraniLatin, abbreviated: true);

        if (abbr.Length < full.Length)
        {
          shorterCount++;
        }
      }

      // Assert - At least half of the abbreviations should be shorter
      Assert.True(shorterCount >= 6,
        $"Expected at least 6 abbreviations to be shorter, but only {shorterCount} were");
    }
  }
}