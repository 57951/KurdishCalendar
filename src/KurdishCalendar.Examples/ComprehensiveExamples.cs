using System;
using KurdishCalendar.Core;

namespace KurdishCalendar.Examples
{
  /// <summary>
  /// Demonstrates the complete Kurdish calendar system including both traditional Kurdish calendar
  /// and Gregorian calendar with Kurdish month names, in multiple dialects.
  /// </summary>
  public static class ComprehensiveKurdishCalendarExamples
  {
    public static void RunAllExamples()
    {
      Console.WriteLine("=== KURDISH CALENDAR COMPREHENSIVE EXAMPLES ===\n");

      TraditionalKurdishCalendarExamples();
      Console.WriteLine();

      GregorianWithKurdishMonthNamesExamples();
      Console.WriteLine();

      CalendarComparisonExamples();
      Console.WriteLine();

      PracticalUseCasesExamples();
    }

    /// <summary>
    /// Examples using the traditional Kurdish (Solar Hijri) calendar.
    /// </summary>
    private static void TraditionalKurdishCalendarExamples()
    {
      Console.WriteLine("--- Traditional Kurdish Calendar ---");

      // Create a Kurdish date (Nowroz 2725)
      KurdishDate newroz = new KurdishDate(2725, 1, 1);

      // Format in different dialects
      Console.WriteLine("Nowroz 2725 in different dialects:");
      Console.WriteLine($"  Sorani Latin:   {newroz.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Sorani Arabic:  {newroz.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine($"  Kurmanji Latin: {newroz.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  Kurmanji Arabic: {newroz.ToString("D", KurdishDialect.KurmanjiArabic)}");
      Console.WriteLine($"  Hawrami Latin:  {newroz.ToString("D", KurdishDialect.HawramiLatin)}");
      Console.WriteLine($"  Hawrami Arabic: {newroz.ToString("D", KurdishDialect.HawramiArabic)}");

      Console.WriteLine("\nAll 12 Kurdish calendar months (Sorani Latin):");
      for (int month = 1; month <= 12; month++)
      {
        KurdishDate date = new KurdishDate(2725, month, 1);
        string monthName = KurdishCultureInfo.GetMonthName(month, KurdishDialect.SoraniLatin);
        Console.WriteLine($"  {month,2}. {monthName,-12} - {date.ToString("d", KurdishDialect.SoraniLatin)}");
      }
    }

    /// <summary>
    /// Examples using Gregorian calendar with Kurdish month names.
    /// </summary>
    private static void GregorianWithKurdishMonthNamesExamples()
    {
      Console.WriteLine("--- Gregorian Calendar with Kurdish Month Names ---");

      DateTime today = new DateTime(2025, 12, 28);

      Console.WriteLine("December 28, 2025 in Kurdish Gregorian formats:");
      Console.WriteLine("\nSorani Gregorian:");
      Console.WriteLine($"  Latin:  {today.ToSoraniGregorian()}");
      Console.WriteLine($"  Arabic: {today.ToSoraniGregorian(GregorianSoraniFormatter.ScriptType.Arabic)}");
      Console.WriteLine($"  Short:  {today.ToSoraniGregorianShort()}");

      Console.WriteLine("\nKurmanji Gregorian:");
      Console.WriteLine($"  Latin:  {today.ToKurmanjiGregorian()}");
      Console.WriteLine($"  Arabic: {today.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
      Console.WriteLine($"  Short:  {today.ToKurmanjiGregorianShort()}");

      Console.WriteLine("\nAll 12 Gregorian months with Kurmanji names:");
      for (int month = 1; month <= 12; month++)
      {
        DateTime date = new DateTime(2025, month, 15);
        string formatted = date.ToKurmanjiGregorianLong();
        Console.WriteLine($"  {month,2}. {formatted}");
      }

      Console.WriteLine("\nAll 12 Gregorian months with Sorani names:");
      for (int month = 1; month <= 12; month++)
      {
        DateTime date = new DateTime(2025, month, 15);
        string formatted = date.ToSoraniGregorianLong();
        Console.WriteLine($"  {month,2}. {formatted}");
      }
    }

    /// <summary>
    /// Side-by-side comparison of traditional Kurdish and Gregorian calendars.
    /// </summary>
    private static void CalendarComparisonExamples()
    {
      Console.WriteLine("--- Calendar System Comparison ---");

      // Same moment in time, different calendar systems
      DateTime gregorianDate = new DateTime(2025, 3, 21); // Nowroz in Gregorian
      KurdishDate kurdishDate = new KurdishDate(gregorianDate);

      Console.WriteLine("Nowroz 2025 in different representations:");
      Console.WriteLine($"  Gregorian (English):          {gregorianDate:d MMMM yyyy}");
      Console.WriteLine($"  Gregorian (Kurmanji Kurdish): {gregorianDate.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"  Gregorian (Sorani Kurdish):   {gregorianDate.ToSoraniGregorianLong()}");
      Console.WriteLine($"  Kurdish Calendar (Sorani):    {kurdishDate.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Kurdish Calendar (Kurmanji):  {kurdishDate.ToString("D", KurdishDialect.KurmanjiLatin)}");

      Console.WriteLine("\nImportant: Notice the difference:");
      Console.WriteLine("  - Gregorian with Kurdish names: Still uses January-December");
      Console.WriteLine("  - Traditional Kurdish calendar: Uses Xakelêwe-Reşeme");
    }

    /// <summary>
    /// Practical real-world usage scenarios.
    /// </summary>
    private static void PracticalUseCasesExamples()
    {
      Console.WriteLine("--- Practical Use Cases ---");

      Console.WriteLine("\n1. Government Document (KRI - uses Gregorian with Kurmanji names):");
      DateTime documentDate = DateTime.Now;
      Console.WriteLine($"   Date: {documentDate.ToKurmanjiGregorianLong()}");

      Console.WriteLine("\n2. Cultural Event Announcement (uses traditional Kurdish calendar):");
      KurdishDate newrozCelebration = new KurdishDate(2725, 1, 1);
      Console.WriteLine($"   Sorani:   {newrozCelebration.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"   Kurmanji: {newrozCelebration.ToString("D", KurdishDialect.KurmanjiLatin)}");

      Console.WriteLine("\n3. Bilingual Date Display:");
      DateTime today = DateTime.Now;
      KurdishDate kurdishToday = new KurdishDate(today);
      Console.WriteLine($"   English:   {today:dddd, d MMMM yyyy}");
      Console.WriteLine($"   Kurmanji:  {today.ToKurmanjiGregorianLong()} (Gregorian)");
      Console.WriteLine($"   Kurdish:   {kurdishToday.ToString("D", KurdishDialect.KurmanjiLatin)} (Traditional)");

      Console.WriteLine("\n4. Short Format for Data Entry:");
      Console.WriteLine($"   Gregorian (numeric): {today:dd/MM/yyyy}");
      Console.WriteLine($"   Kurmanji Gregorian:  {today.ToKurmanjiGregorianShort()}");
      Console.WriteLine($"   Kurdish Traditional: {kurdishToday.ToString("d", KurdishDialect.SoraniLatin)}");

      Console.WriteLine("\n5. RTL/LTR Text Direction:");
      Console.WriteLine("   Latin (LTR default):");
      Console.WriteLine($"     {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine("   Latin (forced RTL):");
      Console.WriteLine($"     {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Latin, KurdishTextDirection.RightToLeft)}");
      Console.WriteLine("   Arabic (RTL default):");
      Console.WriteLine($"     {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
      Console.WriteLine("   Arabic (forced LTR):");
      Console.WriteLine($"     {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic, KurdishTextDirection.LeftToRight)}");

      Console.WriteLine("\n6. Date Arithmetic:");
      KurdishDate startDate = new KurdishDate(2725, 1, 1);
      KurdishDate futureDate = startDate.AddMonths(6);
      int daysDiff = futureDate.DaysDifference(startDate);
      Console.WriteLine($"   Start: {startDate.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"   +6 months: {futureDate.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"   Days between: {daysDiff}");
    }

    /// <summary>
    /// Demonstrates all supported format strings.
    /// </summary>
    public static void FormatStringExamples()
    {
      Console.WriteLine("\n--- Format String Examples ---");

      DateTime gregorian = new DateTime(2025, 12, 25);
      KurdishDate kurdish = new KurdishDate(2725, 10, 5);

      Console.WriteLine("Gregorian formats (Kurmanji):");
      Console.WriteLine($"  dd/MM/yyyy:    {gregorian.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Latin, "dd/MM/yyyy")}");
      Console.WriteLine($"  dd MMM yy:     {gregorian.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Latin, "dd MMM yy")}");
      Console.WriteLine($"  dd MMMM yyyy:  {gregorian.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"  Short format:  {gregorian.ToKurmanjiGregorianShort()}");

      Console.WriteLine("\nKurdish calendar formats (Sorani):");
      Console.WriteLine($"  d:             {kurdish.ToString("d", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  D:             {kurdish.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  dd MMM yyyy:   {kurdish.ToString("dd MMM yyyy", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  dd MMMM yyyy:  {kurdish.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin)}");
    }

    /// <summary>
    /// Shows the difference between dialects for the same calendar system.
    /// </summary>
    public static void DialectComparisonExamples()
    {
      Console.WriteLine("\n--- Dialect Comparison (Same Date, Different Dialects) ---");

      KurdishDate date = new KurdishDate(2725, 1, 15);

      Console.WriteLine("Traditional Kurdish Calendar:");
      Console.WriteLine($"  Sorani:   {date.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Kurmanji: {date.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  Hawrami:  {date.ToString("D", KurdishDialect.HawramiLatin)}");

      Console.WriteLine("\nNote: All use the same month names (Xakelêwe, Gulan, etc.)");
      Console.WriteLine("      Differences are in day names and number formatting");
    }
  }
}