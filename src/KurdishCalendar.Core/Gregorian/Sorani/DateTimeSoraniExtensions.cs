using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Extension methods for DateTime to support Sorani Gregorian date formatting.
  /// </summary>
  public static class DateTimeSoraniExtensions
  {
    /// <summary>
    /// Converts a Gregorian DateTime to a string with Sorani Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="format">Optional custom format string.</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Formatted date string.</returns>
    public static string ToSoraniGregorian(
      this DateTime date,
      GregorianSoraniFormatter.ScriptType script = GregorianSoraniFormatter.ScriptType.Latin,
      string? format = null,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianSoraniFormatter.Format(date, script, format, textDirection);
    }

    /// <summary>
    /// Converts a Gregorian DateTime to a short format string with Sorani Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Short format date string (e.g., "28 Kan1 2025").</returns>
    public static string ToSoraniGregorianShort(
      this DateTime date,
      GregorianSoraniFormatter.ScriptType script = GregorianSoraniFormatter.ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianSoraniFormatter.FormatShort(date, script, textDirection);
    }

    /// <summary>
    /// Converts a Gregorian DateTime to a long format string with Sorani Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Long format date string (e.g., "28 Kanûnî Yekem 2025").</returns>
    public static string ToSoraniGregorianLong(
      this DateTime date,
      GregorianSoraniFormatter.ScriptType script = GregorianSoraniFormatter.ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianSoraniFormatter.FormatLong(date, script, textDirection);
    }

    /// <summary>
    /// Gets the Sorani Kurdish month name for this DateTime.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="abbreviated">Whether to return abbreviated month name.</param>
    /// <returns>The Sorani month name.</returns>
    public static string GetSoraniMonthName(
      this DateTime date,
      GregorianSoraniFormatter.ScriptType script = GregorianSoraniFormatter.ScriptType.Latin,
      bool abbreviated = false)
    {
      return GregorianSoraniFormatter.GetMonthName(date.Month, script, abbreviated);
    }
  }
}