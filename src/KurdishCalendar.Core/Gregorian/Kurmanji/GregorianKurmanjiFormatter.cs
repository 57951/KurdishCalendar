using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides formatting for Gregorian dates using Kurmanji Kurdish month names.
  /// Used in Kurdistan Region of Iraq where the Gregorian calendar is used with Kurdish month names.
  /// </summary>
  public static class GregorianKurmanjiFormatter
  {
    /// <summary>
    /// Script type for displaying Kurmanji Gregorian month names.
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
      { 1, "Kanûna Duyê" },
      { 2, "Şubat" },
      { 3, "Adar" },
      { 4, "Nîsan" },
      { 5, "Gulan" },
      { 6, "Hezîran" },
      { 7, "Tîrmeh" },
      { 8, "Tebax" },
      { 9, "Eylûl" },
      { 10, "Çiriya Êkê" },
      { 11, "Çiriya Duyê" },
      { 12, "Kanûna Êkê" }
    };

    private static readonly Dictionary<int, string> MonthNamesArabic = new Dictionary<int, string>
    {
      { 1, "کانوونا دویێ" },
      { 2, "شوبات" },
      { 3, "ئادار" },
      { 4, "نیسان" },
      { 5, "گولان" },
      { 6, "حەزیران" },
      { 7, "تیرمەه" },
      { 8, "تەباخ" },
      { 9, "ئەیلوول" },
      { 10, "چریا ێکێ" },
      { 11, "چریا دویێ" },
      { 12, "کانوونا ێکێ" }
    };

    private static readonly Dictionary<int, string> MonthNamesShortLatin = new Dictionary<int, string>
    {
      { 1, "Kan Duy" },
      { 2, "Şub" },
      { 3, "Ada" },
      { 4, "Nîs" },
      { 5, "Gul" },
      { 6, "Hez" },
      { 7, "Tîr" },
      { 8, "Teb" },
      { 9, "Eyl" },
      { 10, "Çir Êk" },
      { 11, "Çir Duy" },
      { 12, "Kan Êk" }
    };

    private static readonly Dictionary<int, string> MonthNamesShortArabic = new Dictionary<int, string>
    {
      { 1, "کان دوی" },
      { 2, "شوب" },
      { 3, "ئادا" },
      { 4, "نیس" },
      { 5, "گول" },
      { 6, "حەز" },
      { 7, "تیر" },
      { 8, "تەب" },
      { 9, "ئەیل" },
      { 10, "چر ێک" },
      { 11, "چر دوی" },
      { 12, "کان ێک" }
    };

    /// <summary>
    /// Gets the Kurmanji Kurdish name for a Gregorian month.
    /// </summary>
    /// <param name="month">The month number (1-12).</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="abbreviated">Whether to return the abbreviated month name. Defaults to false.</param>
    /// <returns>The Kurmanji Kurdish month name for the specified Gregorian month.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when month is not between 1 and 12.</exception>
    /// <example>
    /// <code>
    /// // Get full Latin name for January
    /// string month = GregorianKurmanjiFormatter.GetMonthName(1); // Returns "Kanûna Duyê"
    /// 
    /// // Get abbreviated Arabic name for December
    /// string monthAbbr = GregorianKurmanjiFormatter.GetMonthName(12, ScriptType.Arabic, true); // Returns "کان ێک"
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

    /// <summary>
    /// Converts Western numerals to Arabic-Indic numerals for Arabic script.
    /// </summary>
    private static string FormatNumber(int number, ScriptType script)
    {
      if (script != ScriptType.Arabic)
      {
        return number.ToString();
      }

      // Convert to Arabic-Indic numerals (٠-٩, U+0660 to U+0669)
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
    /// Formats a Gregorian DateTime using Kurmanji Kurdish month names with a custom format string.
    /// </summary>
    /// <param name="date">The Gregorian date to format.</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="format">Optional custom format string. Supports: dd, d, MM, MMM, MMMM, yy, yyyy. If null or empty, defaults to long format.</param>
    /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
    /// <returns>Formatted date string with Kurmanji Kurdish month names.</returns>
    /// <example>
    /// <code>
    /// DateTime date = new DateTime(2025, 1, 28);
    /// 
    /// // Custom format
    /// string formatted = GregorianKurmanjiFormatter.Format(date, ScriptType.Latin, "dd MMM yyyy");
    /// // Returns: "28 Kan Duy 2025"
    /// 
    /// // Default long format
    /// string longFormat = GregorianKurmanjiFormatter.Format(date);
    /// // Returns: "28 Kanûna Duyê 2025"
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

      // Parse and replace format tokens
      string result = format;
      
      // Replace year tokens
      if (result.Contains("yyyy"))
      {
        result = result.Replace("yyyy", FormatNumber(date.Year, script));
      }
      else if (result.Contains("yy"))
      {
        int yearLastTwo = date.Year % 100;
        result = result.Replace("yy", FormatNumber(yearLastTwo, script));
      }

      // Replace month tokens (do MMMM before MMM to avoid partial replacement)
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

      // Replace day tokens
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
    /// Formats a Gregorian DateTime using Kurmanji Kurdish month names in a short date format.
    /// </summary>
    /// <param name="date">The Gregorian date to format.</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
    /// <returns>Short format date string with abbreviated Kurmanji Kurdish month name.</returns>
    /// <example>
    /// <code>
    /// DateTime date = new DateTime(2025, 1, 28);
    /// 
    /// // Latin script (LTR)
    /// string shortLatin = GregorianKurmanjiFormatter.FormatShort(date);
    /// // Returns: "28 Kan Duy 2025"
    /// 
    /// // Arabic script (RTL)
    /// string shortArabic = GregorianKurmanjiFormatter.FormatShort(date, ScriptType.Arabic);
    /// // Returns: "٢٠٢٥ کان دویێ ٢٨"
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
    /// Formats a Gregorian DateTime using Kurmanji Kurdish month names in a long date format.
    /// </summary>
    /// <param name="date">The Gregorian date to format.</param>
    /// <param name="script">The script type (Latin or Arabic). Defaults to Latin.</param>
    /// <param name="textDirection">Optional text direction override. If null, defaults based on script (RTL for Arabic, LTR for Latin).</param>
    /// <returns>Long format date string with full Kurmanji Kurdish month name.</returns>
    /// <example>
    /// <code>
    /// DateTime date = new DateTime(2025, 1, 28);
    /// 
    /// // Latin script (LTR)
    /// string longLatin = GregorianKurmanjiFormatter.FormatLong(date);
    /// // Returns: "28 Kanûna Duyê 2025"
    /// 
    /// // Arabic script (RTL)
    /// string longArabic = GregorianKurmanjiFormatter.FormatLong(date, ScriptType.Arabic);
    /// // Returns: "٢٠٢٥ کانوونا دویێ ٢٨"
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