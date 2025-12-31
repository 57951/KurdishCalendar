using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Extension methods for DateTime to support Kurmanji Gregorian date formatting.
  /// </summary>
  public static class DateTimeKurmanjiExtensions
  {
    /// <summary>
    /// Converts a Gregorian DateTime to a string with Kurmanji Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="format">Optional custom format string.</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Formatted date string.</returns>
    public static string ToKurmanjiGregorian(
      this DateTime date,
      GregorianKurmanjiFormatter.ScriptType script = GregorianKurmanjiFormatter.ScriptType.Latin,
      string? format = null,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianKurmanjiFormatter.Format(date, script, format, textDirection);
    }

    /// <summary>
    /// Converts a Gregorian DateTime to a short format string with Kurmanji Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Short format date string (e.g., "28 Kan Êk 2025").</returns>
    public static string ToKurmanjiGregorianShort(
      this DateTime date,
      GregorianKurmanjiFormatter.ScriptType script = GregorianKurmanjiFormatter.ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianKurmanjiFormatter.FormatShort(date, script, textDirection);
    }

    /// <summary>
    /// Converts a Gregorian DateTime to a long format string with Kurmanji Kurdish month names.
    /// </summary>
    /// <param name="date">The date to format.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="textDirection">Optional text direction override.</param>
    /// <returns>Long format date string (e.g., "28 Kanûna Êkê 2025").</returns>
    public static string ToKurmanjiGregorianLong(
      this DateTime date,
      GregorianKurmanjiFormatter.ScriptType script = GregorianKurmanjiFormatter.ScriptType.Latin,
      KurdishTextDirection? textDirection = null)
    {
      return GregorianKurmanjiFormatter.FormatLong(date, script, textDirection);
    }

    /// <summary>
    /// Gets the Kurmanji Kurdish month name for this DateTime.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="script">The script type (Latin or Arabic).</param>
    /// <param name="abbreviated">Whether to return abbreviated month name.</param>
    /// <returns>The Kurmanji month name.</returns>
    public static string GetKurmanjiMonthName(
      this DateTime date,
      GregorianKurmanjiFormatter.ScriptType script = GregorianKurmanjiFormatter.ScriptType.Latin,
      bool abbreviated = false)
    {
      return GregorianKurmanjiFormatter.GetMonthName(date.Month, script, abbreviated);
    }
  }
}