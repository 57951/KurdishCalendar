using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides formatting for Gregorian dates using Sorani Kurdish month names.
  /// </summary>
  public static class GregorianSoraniFormatter
  {
    /// <summary>
    /// Script type for displaying Sorani Gregorian month names.
    /// </summary>
    public enum ScriptType
    {
      /// <summary>
      /// Latin script (default).
      /// </summary>
      Latin,

      /// <summary>
      /// Arabic script with Arabic-Indic numerals.
      /// </summary>
      Arabic
    }

    private static readonly Dictionary<int, string> MonthNamesLatin = new Dictionary<int, string>
    {
      { 1, "Kanûnî Duhem" },
      { 2, "Şubat" },
      { 3, "Adar" },
      { 4, "Nîsan" },
      { 5, "Ayar" },
      { 6, "Hûzeyran" },
      { 7, "Temmûz" },
      { 8, "Ab" },
      { 9, "Eylûl" },
      { 10, "Tişrînî Yekem" },
      { 11, "Tişrînî Duhem" },
      { 12, "Kanûnî Yekem" }
    };

    private static readonly Dictionary<int, string> MonthNamesArabic = new Dictionary<int, string>
    {
      { 1, "كانونى دووەم" },
      { 2, "شوبات" },
      { 3, "ئادار" },
      { 4, "نيسان" },
      { 5, "ئايار" },
      { 6, "حوزەيران" },
      { 7, "تەمووز" },
      { 8, "ئاب" },
      { 9, "ئەيلول" },
      { 10, "تشرينى يەكەم" },
      { 11, "تشرينى دووەم" },
      { 12, "كانونى يەكەم" }
    };

    private static readonly Dictionary<int, string> MonthNamesShortLatin = new Dictionary<int, string>
    {
      { 1, "Kan2" },
      { 2, "Şub" },
      { 3, "Ada" },
      { 4, "Nîs" },
      { 5, "Aya" },
      { 6, "Hûz" },
      { 7, "Tem" },
      { 8, "Ab" },
      { 9, "Eyl" },
      { 10, "Tiş1" },
      { 11, "Tiş2" },
      { 12, "Kan1" }
    };

    private static readonly Dictionary<int, string> MonthNamesShortArabic = new Dictionary<int, string>
    {
      { 1, "كان" },
      { 2, "شو" },
      { 3, "ئاد" },
      { 4, "نيس" },
      { 5, "ئاي" },
      { 6, "حوز" },
      { 7, "تەم" },
      { 8, "ئاب" },
      { 9, "ئەي" },
      { 10, "تشر١" },
      { 11, "تشر٢" },
      { 12, "كان١" }
    };

    /// <summary>
    /// Gets the Sorani Kurdish name for a Gregorian month.
    /// </summary>
    /// <param name="month">The month number (1-12).</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="abbreviated">Whether to return the abbreviated month name. Defaults to false.</param>
    /// <returns>The Sorani Kurdish month name for the specified Gregorian month.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when month is not between 1 and 12.</exception>
    /// <example>
    /// <code>
    /// // Get full Latin name for January
    /// string month = GregorianSoraniFormatter.GetMonthName(1); // Returns "Kanûnî Duhem"
    /// 
    /// // Get abbreviated Arabic name for December
    /// string monthAbbr = GregorianSoraniFormatter.GetMonthName(12, ScriptType.Arabic, true); // Returns "كان١"
    /// </code>
    /// </example>
    public static string GetMonthName(int month, ScriptType script = ScriptType.Latin, bool abbreviated = false)
    {
      if (month < 1 || month > 12)
      {
        throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
      }

      Dictionary<int, string> names = (script, abbreviated) switch
      {
        (ScriptType.Arabic, true) => MonthNamesShortArabic,
        (ScriptType.Arabic, false) => MonthNamesArabic,
        (ScriptType.Latin, true) => MonthNamesShortLatin,
        (ScriptType.Latin, false) => MonthNamesLatin,
        _ => throw new ArgumentException($"Unsupported script type: {script}")
      };

      return names[month];
    }

    private static string FormatNumber(int number, ScriptType script)
    {
      if (script != ScriptType.Arabic)
      {
        return number.ToString();
      }

      string westernNumber = number.ToString();
      string arabicNumber = string.Empty;

      foreach (char c in westernNumber)
      {
        if (char.IsDigit(c))
        {
          int digit = c - '0';
          arabicNumber += (char)(0x0660 + digit);
        }
        else
        {
          arabicNumber += c;
        }
      }

      return arabicNumber;
    }


  /// <summary>
  /// Formats a Gregorian DateTime using Sorani Kurdish month names with a custom format string.
  /// </summary>
  /// <param name="date">The Gregorian date to format.</param>
  /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
  /// <param name="format">Optional custom format string. Supports: dd, d, MM, MMM, MMMM, yy, yyyy. If null or empty, defaults to long format.</param>
  /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
  /// <returns>Formatted date string with Sorani Kurdish month names.</returns>
  /// <example>
  /// <code>
  /// DateTime date = new DateTime(2025, 1, 28);
  /// 
  /// // Custom format
  /// string formatted = GregorianSoraniFormatter.Format(date, ScriptType.Latin, "dd MMM yyyy");
  /// // Returns: "28 Kan2 2025"
  /// 
  /// // Default long format
  /// string longFormat = GregorianSoraniFormatter.Format(date);
  /// // Returns: "28 Kanûnî Duhem 2025"
  /// </code>
  /// </example>
    public static string Format(
      DateTime date,
      ScriptType script = ScriptType.Latin,
      string? format = null,
      KurdishTextDirection? textDirection = null)
    {
      if (string.IsNullOrWhiteSpace(format))
      {
        return FormatLong(date, script, textDirection);
      }

      KurdishTextDirection direction = textDirection ??
        (script == ScriptType.Arabic ? KurdishTextDirection.RightToLeft : KurdishTextDirection.LeftToRight);

      string result = format;
      
      if (result.Contains("yyyy"))
      {
        result = result.Replace("yyyy", FormatNumber(date.Year, script));
      }
      else if (result.Contains("yy"))
      {
        int yearLastTwo = date.Year % 100;
        result = result.Replace("yy", FormatNumber(yearLastTwo, script));
      }

      if (result.Contains("MMMM"))
      {
        result = result.Replace("MMMM", GetMonthName(date.Month, script, abbreviated: false));
      }
      else if (result.Contains("MMM"))
      {
        result = result.Replace("MMM", GetMonthName(date.Month, script, abbreviated: true));
      }
      else if (result.Contains("MM"))
      {
        result = result.Replace("MM", FormatNumber(date.Month, script).PadLeft(2, script == ScriptType.Arabic ? '٠' : '0'));
      }

      if (result.Contains("dd"))
      {
        result = result.Replace("dd", FormatNumber(date.Day, script).PadLeft(2, script == ScriptType.Arabic ? '٠' : '0'));
      }
      else if (result.Contains("d"))
      {
        result = result.Replace("d", FormatNumber(date.Day, script));
      }

      return result;
    }


    /// <summary>
    /// Formats a Gregorian DateTime using Sorani Kurdish month names in a short date format.
    /// </summary>
    /// <param name="date">The Gregorian date to format.</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
    /// <returns>Short format date string with abbreviated Sorani Kurdish month name.</returns>
    /// <example>
    /// <code>
    /// DateTime date = new DateTime(2025, 1, 28);
    /// 
    /// // Latin script (LTR)
    /// string shortLatin = GregorianSoraniFormatter.FormatShort(date);
    /// // Returns: "28 Kan2 2025"
    /// 
    /// // Arabic script (RTL)
    /// string shortArabic = GregorianSoraniFormatter.FormatShort(date, ScriptType.Arabic);
    /// // Returns: "٢٠٢٥ كان٢ ٢٨"
    /// </code>
    /// </example>
    public static string FormatShort(
      DateTime date,
      ScriptType script = ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      KurdishTextDirection direction = textDirection ??
        (script == ScriptType.Arabic ? KurdishTextDirection.RightToLeft : KurdishTextDirection.LeftToRight);

      string day = FormatNumber(date.Day, script);
      string monthName = GetMonthName(date.Month, script, abbreviated: true);
      string year = FormatNumber(date.Year, script);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        return $"{year} {monthName} {day}";
      }

      return $"{day} {monthName} {year}";
    }


    /// <summary>
    /// Formats a Gregorian DateTime using Sorani Kurdish month names in a long date format.
    /// </summary>
    /// <param name="date">The Gregorian date to format.</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
    /// <returns>Long format date string with full Sorani Kurdish month name.</returns>
    /// <example>
    /// <code>
    /// DateTime date = new DateTime(2025, 1, 28);
    /// 
    /// // Latin script (LTR)
    /// string longLatin = GregorianSoraniFormatter.FormatLong(date);
    /// // Returns: "28 Kanûnî Duhem 2025"
    /// 
    /// // Arabic script (RTL)
    /// string longArabic = GregorianSoraniFormatter.FormatLong(date, ScriptType.Arabic);
    /// // Returns: "٢٠٢٥ كانونى دووەم ٢٨"
    /// </code>
    /// </example>
    public static string FormatLong(
      DateTime date,
      ScriptType script = ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      KurdishTextDirection direction = textDirection ??
        (script == ScriptType.Arabic ? KurdishTextDirection.RightToLeft : KurdishTextDirection.LeftToRight);

      string day = FormatNumber(date.Day, script);
      string monthName = GetMonthName(date.Month, script, abbreviated: false);
      string year = FormatNumber(date.Year, script);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        return $"{year} {monthName} {day}";
      }

      return $"{day} {monthName} {year}";
    }
  }
}