using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides parsing services for Kurdish date strings.
  /// Supports parsing dates in various Kurdish dialects and scripts.
  /// Now includes support for Gregorian month names (including multi-word) and Arabic numerals.
  /// </summary>
  public static class KurdishDateParser
  {
    /// <summary>
    /// Attempts to parse a Kurdish date string.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="result">When successful, contains the parsed KurdishDate.</param>
    /// <returns>True if parsing succeeded; otherwise, false.</returns>
    public static bool TryParse(string input, KurdishDialect dialect, out KurdishDate result)
    {
      result = default;

      if (string.IsNullOrWhiteSpace(input))
      {
        return false;
      }

      try
      {
        result = Parse(input, dialect);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Parses a Kurdish date string.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <returns>A KurdishDate representing the parsed date.</returns>
    /// <exception cref="FormatException">Thrown when the input string is not in a valid format.</exception>
    public static KurdishDate Parse(string input, KurdishDialect dialect)
    {
      if (string.IsNullOrWhiteSpace(input))
      {
        throw new FormatException("Input string cannot be null or empty.");
      }

      input = input.Trim();

      // For Gregorian dialects, parse as Gregorian date then convert to Kurdish
      if (IsGregorianDialect(dialect))
      {
        return ParseGregorianDate(input, dialect);
      }

      // Try numeric short format first (dd/MM/yyyy or yyyy/MM/dd)
      if (TryParseNumericFormat(input, dialect, out int day, out int month, out int year))
      {
        return new KurdishDate(year, month, day);
      }

      // Try long format with month names (e.g., "15 XakelÃªwe 2725" or "2725 XakelÃªwe 15")
      if (TryParseLongFormat(input, dialect, out day, out month, out year))
      {
        return new KurdishDate(year, month, day);
      }

      throw new FormatException($"Unable to parse '{input}' as a Kurdish date in {dialect} dialect.");
    }

    /// <summary>
    /// Attempts to parse a Kurdish astronomical date string.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="longitude">The longitude for astronomical calculations (default: Erbil).</param>
    /// <param name="result">When successful, contains the parsed KurdishAstronomicalDate.</param>
    /// <returns>True if parsing succeeded; otherwise, false.</returns>
    public static bool TryParseAstronomical(string input, KurdishDialect dialect, double longitude, out KurdishAstronomicalDate result)
    {
      result = default;

      if (TryParse(input, dialect, out KurdishDate standardDate))
      {
        result = KurdishAstronomicalDate.FromLongitude(standardDate.Year, standardDate.Month, standardDate.Day, longitude);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Parses a Kurdish astronomical date string.
    /// </summary>
    /// <param name="input">The string to parse.</param>
    /// <param name="dialect">The Kurdish dialect used in the input string.</param>
    /// <param name="longitude">The longitude for astronomical calculations (default: Erbil).</param>
    /// <returns>A KurdishAstronomicalDate representing the parsed date.</returns>
    /// <exception cref="FormatException">Thrown when the input string is not in a valid format.</exception>
    public static KurdishAstronomicalDate ParseAstronomical(string input, KurdishDialect dialect, double longitude = 44.0)
    {
      KurdishDate standardDate = Parse(input, dialect);
      return KurdishAstronomicalDate.FromLongitude(standardDate.Year, standardDate.Month, standardDate.Day, longitude);
    }

    /// <summary>
    /// Determines if a dialect uses Gregorian month names.
    /// </summary>
    private static bool IsGregorianDialect(KurdishDialect dialect)
    {
      return dialect == KurdishDialect.SoraniGregorianLatin ||
             dialect == KurdishDialect.SoraniGregorianArabic ||
             dialect == KurdishDialect.KurmanjiGregorianLatin ||
             dialect == KurdishDialect.KurmanjiGregorianArabic;
    }

    /// <summary>
    /// Parses a Gregorian date with Kurdish month names and converts to Kurdish calendar.
    /// Example: "15 KanÃ»nÃ® Duhem 2024" (Gregorian January 15, 2024)
    /// Handles multi-word month names like "KanÃ»nÃ® Duhem", "TiÅŸrÃ®nÃ® Yekem", etc.
    /// </summary>
    private static KurdishDate ParseGregorianDate(string input, KurdishDialect dialect)
    {
      // Convert Arabic numerals if needed
      bool isArabicScript = KurdishCultureInfo.IsArabicScript(dialect);

      // Try to match month names (including multi-word ones) in the string
      int gregorianMonth = 0;
      string? matchedMonthName = null;
      int monthStartIndex = -1;
      int monthEndIndex = -1;

      // Try each Gregorian month (1-12)
      for (int i = 1; i <= 12; i++)
      {
        string fullMonthName = KurdishCultureInfo.GetMonthName(i, dialect);
        string abbreviatedMonthName = KurdishCultureInfo.GetMonthName(i, dialect, abbreviated: true);

        // Check if the full month name appears in the input
        int index = input.IndexOf(fullMonthName, StringComparison.OrdinalIgnoreCase);
        if (index >= 0)
        {
          gregorianMonth = i;
          matchedMonthName = fullMonthName;
          monthStartIndex = index;
          monthEndIndex = index + fullMonthName.Length;
          break;
        }

        // Check abbreviated name
        index = input.IndexOf(abbreviatedMonthName, StringComparison.OrdinalIgnoreCase);
        if (index >= 0)
        {
          gregorianMonth = i;
          matchedMonthName = abbreviatedMonthName;
          monthStartIndex = index;
          monthEndIndex = index + abbreviatedMonthName.Length;
          break;
        }
      }

      if (gregorianMonth == 0)
      {
        throw new FormatException($"Could not find Gregorian month name in: '{input}'");
      }

      // Extract the parts before and after the month name
      string beforeMonth = input.Substring(0, monthStartIndex).Trim();
      string afterMonth = input.Substring(monthEndIndex).Trim();

      // One should be the day, the other the year
      if (string.IsNullOrWhiteSpace(beforeMonth) || string.IsNullOrWhiteSpace(afterMonth))
      {
        throw new FormatException($"Missing day or year in: '{input}'");
      }

      // Convert Arabic numerals if needed
      string num1Str = isArabicScript ? ConvertArabicNumerals(beforeMonth) : beforeMonth;
      string num2Str = isArabicScript ? ConvertArabicNumerals(afterMonth) : afterMonth;

      if (!int.TryParse(num1Str, out int num1) || !int.TryParse(num2Str, out int num2))
      {
        throw new FormatException($"Could not parse day/year numbers in: '{input}'");
      }

      // Determine which is day and which is year
      int gregorianDay, gregorianYear;

      if (num1 > 31)
      {
        // First number is year
        gregorianYear = num1;
        gregorianDay = num2;
      }
      else if (num2 > 31)
      {
        // Second number is year
        gregorianDay = num1;
        gregorianYear = num2;
      }
      else
      {
        // Ambiguous - use text direction
        KurdishTextDirection defaultDirection = isArabicScript
          ? KurdishTextDirection.RightToLeft
          : KurdishTextDirection.LeftToRight;

        if (defaultDirection == KurdishTextDirection.RightToLeft)
        {
          // RTL: year comes first
          gregorianYear = num1;
          gregorianDay = num2;
        }
        else
        {
          // LTR: day comes first
          gregorianDay = num1;
          gregorianYear = num2;
        }
      }

      // Create Gregorian DateTime
      DateTime gregorianDate;
      try
      {
        gregorianDate = new DateTime(gregorianYear, gregorianMonth, gregorianDay);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw new FormatException($"Invalid Gregorian date: {gregorianYear}-{gregorianMonth:D2}-{gregorianDay:D2}", ex);
      }

      // Convert to Kurdish calendar
      return KurdishDate.FromDateTime(gregorianDate);
    }

    private static bool TryParseNumericFormat(string input, KurdishDialect dialect, out int day, out int month, out int year)
    {
      day = 0;
      month = 0;
      year = 0;

      // Normalize separators (handle /, -, and spaces)
      string normalized = input.Replace('-', '/').Replace(' ', '/');

      string[] parts = normalized.Split('/');

      if (parts.Length != 3)
      {
        return false;
      }

      // Convert Kurdish Arabic numerals to Western if needed
      bool isArabicScript = KurdishCultureInfo.IsArabicScript(dialect);

      string part0 = isArabicScript ? ConvertArabicNumerals(parts[0]) : parts[0];
      string part1 = isArabicScript ? ConvertArabicNumerals(parts[1]) : parts[1];
      string part2 = isArabicScript ? ConvertArabicNumerals(parts[2]) : parts[2];

      if (!int.TryParse(part0, out int num0) ||
          !int.TryParse(part1, out int num1) ||
          !int.TryParse(part2, out int num2))
      {
        return false;
      }

      // Intelligently determine the order based on number values
      // Year is typically 4 digits (2700+), month is 1-12, day is 1-31

      // Check if first number is clearly a year (> 31)
      if (num0 > 31)
      {
        // Format: YYYY/MM/DD or YYYY/DD/MM
        year = num0;

        // Determine which is month and which is day
        if (num1 >= 1 && num1 <= 12 && num2 >= 1 && num2 <= 31)
        {
          // Could be YYYY/MM/DD
          month = num1;
          day = num2;
        }
        else if (num2 >= 1 && num2 <= 12 && num1 >= 1 && num1 <= 31)
        {
          // Could be YYYY/DD/MM
          day = num1;
          month = num2;
        }
        else
        {
          return false;
        }
      }
      // Check if last number is clearly a year (> 31)
      else if (num2 > 31)
      {
        // Format: DD/MM/YYYY or MM/DD/YYYY
        year = num2;

        // Determine which is month and which is day
        if (num0 >= 1 && num0 <= 31 && num1 >= 1 && num1 <= 12)
        {
          // Could be DD/MM/YYYY
          day = num0;
          month = num1;
        }
        else if (num1 >= 1 && num1 <= 31 && num0 >= 1 && num0 <= 12)
        {
          // Could be MM/DD/YYYY
          month = num0;
          day = num1;
        }
        else
        {
          return false;
        }
      }
      // All numbers <= 31, ambiguous - use cultural convention
      else
      {
        // For Arabic script (RTL), default to YYYY/MM/DD
        // For Latin script (LTR), default to DD/MM/YYYY
        KurdishTextDirection defaultDirection = isArabicScript
          ? KurdishTextDirection.RightToLeft
          : KurdishTextDirection.LeftToRight;

        if (defaultDirection == KurdishTextDirection.RightToLeft)
        {
          // RTL: year/month/day
          year = num0;
          month = num1;
          day = num2;
        }
        else
        {
          // LTR: day/month/year
          day = num0;
          month = num1;
          year = num2;
        }
      }

      // Validate ranges
      return year >= 1 && year <= 9999 &&
             month >= 1 && month <= 12 &&
             day >= 1 && day <= 31;
    }

    private static bool TryParseLongFormat(string input, KurdishDialect dialect, out int day, out int month, out int year)
    {
      day = 0;
      month = 0;
      year = 0;

      // Split by whitespace
      string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      if (parts.Length != 3)
      {
        return false;
      }

      // Convert Kurdish Arabic numerals if needed
      bool isArabicScript = KurdishCultureInfo.IsArabicScript(dialect);

      // Try to find which part is the month name
      int monthIndex = -1;
      for (int i = 0; i < parts.Length; i++)
      {
        if (TryParseMonthName(parts[i], dialect, out int parsedMonth))
        {
          month = parsedMonth;
          monthIndex = i;
          break;
        }
      }

      if (monthIndex == -1)
      {
        return false;
      }

      // Parse the remaining two parts as day and year
      string[] numbers = new string[2];
      int numberIndex = 0;

      for (int i = 0; i < parts.Length; i++)
      {
        if (i != monthIndex)
        {
          numbers[numberIndex++] = isArabicScript ? ConvertArabicNumerals(parts[i]) : parts[i];
        }
      }

      if (!int.TryParse(numbers[0], out int num0) || !int.TryParse(numbers[1], out int num1))
      {
        return false;
      }

      // Determine which number is day and which is year
      // Year is typically 4 digits (2700+), day is 1-2 digits (1-31)
      if (num0 > 31)
      {
        year = num0;
        day = num1;
      }
      else if (num1 > 31)
      {
        day = num0;
        year = num1;
      }
      else
      {
        // Ambiguous - use text direction to determine order
        KurdishTextDirection defaultDirection = KurdishCultureInfo.IsArabicScript(dialect)
          ? KurdishTextDirection.RightToLeft
          : KurdishTextDirection.LeftToRight;

        if (defaultDirection == KurdishTextDirection.RightToLeft)
        {
          // RTL: first number is year
          year = num0;
          day = num1;
        }
        else
        {
          // LTR: first number is day
          day = num0;
          year = num1;
        }
      }

      // Validate ranges
      return year >= 1 && year <= 9999 &&
             month >= 1 && month <= 12 &&
             day >= 1 && day <= 31;
    }

    private static bool TryParseMonthName(string monthName, KurdishDialect dialect, out int month)
    {
      month = 0;

      if (string.IsNullOrWhiteSpace(monthName))
      {
        return false;
      }

      // Try each month
      for (int i = 1; i <= 12; i++)
      {
        string expectedName = KurdishCultureInfo.GetMonthName(i, dialect);

        // Case-insensitive comparison
        if (string.Equals(monthName, expectedName, StringComparison.OrdinalIgnoreCase))
        {
          month = i;
          return true;
        }
      }

      return false;
    }

    private static string ConvertArabicNumerals(string arabicNumber)
    {
      if (string.IsNullOrWhiteSpace(arabicNumber))
      {
        return arabicNumber;
      }

      // Eastern Arabic numerals: ٠١٢٣٤٥٦٧٨٩ (U+0660 to U+0669)
      string result = arabicNumber;

      result = result.Replace('٠', '0');
      result = result.Replace('١', '1');
      result = result.Replace('٢', '2');
      result = result.Replace('٣', '3');
      result = result.Replace('٤', '4');
      result = result.Replace('٥', '5');
      result = result.Replace('٦', '6');
      result = result.Replace('٧', '7');
      result = result.Replace('٨', '8');
      result = result.Replace('٩', '9');

      return result;
    }
  }
}