namespace KurdishCalendar.Core
{
  /// <summary>
  /// Represents the Kurdish dialect and script combination for date localisation.
  /// Includes both traditional Kurdish calendar dialects and Gregorian calendar with Kurdish month names.
  /// </summary>
  public enum KurdishDialect
  {
    /// <summary>
    /// Sorani dialect in Latin script (Kurdish calendar).
    /// </summary>
    SoraniLatin,

    /// <summary>
    /// Sorani dialect in Arabic script (Kurdish calendar).
    /// </summary>
    SoraniArabic,

    /// <summary>
    /// Sorani dialect in Latin script with Gregorian month names.
    /// Used for Gregorian calendar dates with Sorani Kurdish month names.
    /// </summary>
    SoraniGregorianLatin,

    /// <summary>
    /// Sorani dialect in Arabic script with Gregorian month names.
    /// Used for Gregorian calendar dates with Sorani Kurdish month names.
    /// </summary>
    SoraniGregorianArabic,

    /// <summary>
    /// Kurmanji dialect in Latin script (Kurdish calendar).
    /// </summary>
    KurmanjiLatin,

    /// <summary>
    /// Kurmanji dialect in Arabic script (Kurdish calendar).
    /// </summary>
    KurmanjiArabic,

    /// <summary>
    /// Kurmanji dialect in Latin script with Gregorian month names.
    /// Used for Gregorian calendar dates with Kurmanji Kurdish month names.
    /// </summary>
    KurmanjiGregorianLatin,

    /// <summary>
    /// Kurmanji dialect in Arabic script with Gregorian month names.
    /// Used for Gregorian calendar dates with Kurmanji Kurdish month names.
    /// </summary>
    KurmanjiGregorianArabic,

    /// <summary>
    /// Hawrami dialect in Latin script (Kurdish calendar).
    /// </summary>
    HawramiLatin,

    /// <summary>
    /// Hawrami dialect in Arabic script (Kurdish calendar).
    /// </summary>
    HawramiArabic
  }
}