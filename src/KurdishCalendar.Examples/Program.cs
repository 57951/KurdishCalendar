using System;
using KurdishCalendar.Core;

namespace KurdishCalendar.Examples
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;

      while (true)
      {
        ShowMenu();
        string choice = Console.ReadLine()?.Trim() ?? "";

        if (choice == "0" || choice.ToLower() == "q")
        {
          Console.WriteLine("\nGoodbye!");
          break;
        }

        Console.Clear();
        RunExample(choice);

        Console.WriteLine("\n\nPress any key to return to menu...");
        Console.ReadKey();
        Console.Clear();
      }
    }

    private static void ShowMenu()
    {
      PrintHeader("Kurdish Calendar Library - Interactive Examples");

      Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
      Console.WriteLine("║                    STANDARD CALENDAR EXAMPLES                      ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║  1. Basic Simplified Dates        - Creating and displaying dates  ║");
      Console.WriteLine("║  2. Date Arithmetic             - Adding days, months, years       ║");
      Console.WriteLine("║  3. Multi-Dialect Formatting    - Sorani, Kurmanji, Hawrami        ║");
      Console.WriteLine("║  4. Text Direction              - RTL and LTR formatting           ║");
      Console.WriteLine("║  5. Date Comparisons            - Comparing and sorting dates      ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║                  ASTRONOMICAL CALENDAR EXAMPLES                    ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║  6. Astronomical Basics         - Precise equinox calculations     ║");
      Console.WriteLine("║  7. Location Comparison         - Erbil vs Tehran dates            ║");
      Console.WriteLine("║  8. Conversion Methods          - Lossless vs Informational        ║");
      Console.WriteLine("║  9. Precise Nowroz Timing       - Exact moment calculations        ║");
      Console.WriteLine("║ 10. Historical Dates            - Converting historical events     ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║                     INTEGRATION EXAMPLES                           ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║ 11. Interface Usage             - Polymorphic IKurdishDate         ║");
      Console.WriteLine("║ 12. Real-World Scenarios        - Practical applications           ║");
      Console.WriteLine("║ 13. 🌟 Simplified vs Astronomical  - SEE THE REAL DIFFERENCES!     ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║                      PARSING EXAMPLES                              ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║ 14. Basic Parsing               - Parse dates from strings         ║");
      Console.WriteLine("║ 15. Different Formats           - Numeric, long, RTL/LTR           ║");
      Console.WriteLine("║ 16. Arabic Script Parsing       - Eastern Arabic numerals          ║");
      Console.WriteLine("║ 17. Astronomical Parsing        - Parse with longitude             ║");
      Console.WriteLine("║ 18. Safe Parsing (TryParse)     - Error-free parsing               ║");
      Console.WriteLine("║ 19. Round-Trip Examples         - Format → Parse → Format          ║");
      Console.WriteLine("║ 20. Error Handling              - Parse() vs TryParse()            ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║              GREGORIAN WITH KURDISH MONTH NAMES                    ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║ 21. Sorani Gregorian Basics     - Gregorian with Sorani names      ║");
      Console.WriteLine("║ 22. Kurmanji Gregorian Basics   - Gregorian with Kurmanji names    ║");
      Console.WriteLine("║ 23. Calendar System Comparison  - Traditional vs Gregorian         ║");
      Console.WriteLine("║ 24. All Dialects Demo           - See all month names              ║");
      Console.WriteLine("╠════════════════════════════════════════════════════════════════════╣");
      Console.WriteLine("║ 98. Run All Parsing Examples    - All parsing demos                ║");
      Console.WriteLine("║ 99. Run All Examples            - Execute all examples in order    ║");
      Console.WriteLine("║  0. Exit (or Q)                                                    ║");
      Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
      Console.Write("\nEnter your choice: ");
    }

    private static void RunExample(string choice)
    {
      switch (choice)
      {
        case "1":
          Example1_BasicSimplifiedDates();
          break;
        case "2":
          Example2_DateArithmetic();
          break;
        case "3":
          Example3_MultiDialectFormatting();
          break;
        case "4":
          Example4_TextDirection();
          break;
        case "5":
          Example5_DateComparisons();
          break;
        case "6":
          Example6_AstronomicalBasics();
          break;
        case "7":
          Example7_LocationComparison();
          break;
        case "8":
          Example8_ConversionMethods();
          break;
        case "9":
          Example9_PreciseNowrozTiming();
          break;
        case "10":
          Example10_HistoricalDates();
          break;
        case "11":
          Example11_InterfaceUsage();
          break;
        case "12":
          Example12_RealWorldScenarios();
          break;
        case "13":
          Example13_SimplifiedVsAstronomicalDifferences();
          break;
        case "14":
          ParsingExamples.Example14_BasicParsing();
          break;
        case "15":
          ParsingExamples.Example15_ParseDifferentFormats();
          break;
        case "16":
          ParsingExamples.Example16_ParseArabicScript();
          break;
        case "17":
          ParsingExamples.Example17_ParseAstronomicalDates();
          break;
        case "18":
          ParsingExamples.Example18_TryParsePattern();
          break;
        case "19":
          ParsingExamples.Example19_RoundTripFormatting();
          break;
        case "20":
          ParsingExamples.Example20_ErrorHandling();
          break;
        case "21":
          Example21_SoraniGregorianBasics();
          break;
        case "22":
          Example22_KurmanjiGregorianBasics();
          break;
        case "23":
          Example23_CalendarSystemComparison();
          break;
        case "24":
          Example24_AllDialectsDemo();
          break;
        case "98":
          RunAllParsingExamples();
          break;
        case "99":
          RunAllExamples();
          break;
        default:
          Console.WriteLine("Invalid choice. Please try again.");
          break;
      }
    }

    private static void RunAllExamples()
    {
      PrintHeader("Running All Examples");

      Console.WriteLine("═══ STANDARD CALENDAR EXAMPLES ═══\n");
      Example1_BasicSimplifiedDates();
      Example2_DateArithmetic();
      Example3_MultiDialectFormatting();
      Example4_TextDirection();
      Example5_DateComparisons();

      Console.WriteLine("\n═══ ASTRONOMICAL CALENDAR EXAMPLES ═══\n");
      Example6_AstronomicalBasics();
      Example7_LocationComparison();
      Example8_ConversionMethods();
      Example9_PreciseNowrozTiming();
      Example10_HistoricalDates();

      Console.WriteLine("\n═══ INTEGRATION EXAMPLES ═══\n");
      Example11_InterfaceUsage();
      Example12_RealWorldScenarios();
      Example13_SimplifiedVsAstronomicalDifferences();

      Console.WriteLine("\n═══ PARSING EXAMPLES ═══\n");
      RunAllParsingExamples();

      PrintHeader("All Examples Completed Successfully!");
    }

    private static void RunAllParsingExamples()
    {
      PrintHeader("Running All Parsing Examples");
      ParsingExamples.RunAllExamples();
    }

    private static void PrintHeader(string title)
    {
      Console.WriteLine();
      Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
      Console.WriteLine($"║ {title,-64} ║");
      Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
      Console.WriteLine();
    }

    private static void PrintSection(string title)
    {
      Console.WriteLine($"\n═══ {title} ═══\n");
    }

    // ═══════════════════════════════════════════════════════════════
    // STANDARD CALENDAR EXAMPLES
    // ═══════════════════════════════════════════════════════════════

    private static void Example1_BasicSimplifiedDates()
    {
      PrintSection("Example 1: Basic Simplified Dates");

      // Get today's date
      KurdishDate today = KurdishDate.Today;
      Console.WriteLine($"Today (Kurdish):    {today.Year}/{today.Month:D2}/{today.Day:D2}");
      Console.WriteLine($"Today (Gregorian):  {today.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine($"Day of week:        {today.DayOfWeek}");
      Console.WriteLine($"Day of year:        {today.DayOfYear}");
      Console.WriteLine($"Is leap year:       {today.IsLeapYear}");
      Console.WriteLine();

      // Create specific dates
      KurdishDate newroz2725 = new KurdishDate(2725, 1, 1);
      Console.WriteLine($"Newroz 2725:        {newroz2725.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  → Gregorian:      {newroz2725.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Create from Gregorian
      DateTime christmas = new DateTime(2025, 12, 25);
      KurdishDate kurdishChristmas = KurdishDate.FromDateTime(christmas);
      Console.WriteLine($"Christmas 2025:     {christmas:yyyy-MM-dd}");
      Console.WriteLine($"  → Kurdish:        {kurdishChristmas.Year}/{kurdishChristmas.Month:D2}/{kurdishChristmas.Day:D2}");
      Console.WriteLine();
    }

    private static void Example2_DateArithmetic()
    {
      PrintSection("Example 2: Date Arithmetic");

      KurdishDate date = new KurdishDate(2725, 6, 15);
      Console.WriteLine($"Starting date:      {date.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Add days
      KurdishDate plus30 = date.AddDays(30);
      Console.WriteLine($"Add 30 days:        {plus30.ToString("D", KurdishDialect.SoraniLatin)}");

      KurdishDate minus10 = date.AddDays(-10);
      Console.WriteLine($"Subtract 10 days:   {minus10.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Add months
      KurdishDate plus3months = date.AddMonths(3);
      Console.WriteLine($"Add 3 months:       {plus3months.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Add years
      KurdishDate plus5years = date.AddYears(5);
      Console.WriteLine($"Add 5 years:        {plus5years.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Calculate difference
      KurdishDate futureDate = new KurdishDate(2726, 1, 1);
      int daysBetween = futureDate.DaysDifference(date);
      Console.WriteLine($"Days from {date.ToString("D", KurdishDialect.SoraniLatin)} until Newroz 2726: {daysBetween} days");
      Console.WriteLine();
    }

    private static void Example3_MultiDialectFormatting()
    {
      PrintSection("Example 3: Multi-Dialect Formatting");

      KurdishDate date = new KurdishDate(2725, 3, 15);

      Console.WriteLine("Same date in different dialects:\n");

      // Sorani Latin
      Console.WriteLine("Sorani Latin:");
      Console.WriteLine($"  Short:  {date.ToString("d", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Long:   {date.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Full:   {date.ToString("f", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Sorani Arabic
      Console.WriteLine("Sorani Arabic:");
      Console.WriteLine($"  Short:  {date.ToString("d", KurdishDialect.SoraniArabic)}");
      Console.WriteLine($"  Long:   {date.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine($"  Full:   {date.ToString("f", KurdishDialect.SoraniArabic)}");
      Console.WriteLine();

      // Kurmanji Latin
      Console.WriteLine("Kurmanji Latin:");
      Console.WriteLine($"  Short:  {date.ToString("d", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  Long:   {date.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine();

      // Hawrami Arabic
      Console.WriteLine("Hawrami Arabic:");
      Console.WriteLine($"  Short:  {date.ToString("d", KurdishDialect.HawramiArabic)}");
      Console.WriteLine($"  Long:   {date.ToString("D", KurdishDialect.HawramiArabic)}");
      Console.WriteLine();
    }

    private static void Example4_TextDirection()
    {
      PrintSection("Example 4: Text Direction (RTL/LTR)");

      KurdishDate date = new KurdishDate(2725, 6, 15);

      Console.WriteLine("Sorani Arabic - Default RTL:");
      Console.WriteLine($"  {date.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine();

      Console.WriteLine("Sorani Arabic - Forced LTR:");
      Console.WriteLine($"  {date.ToString("D", KurdishDialect.SoraniArabic, KurdishTextDirection.LeftToRight)}");
      Console.WriteLine();

      Console.WriteLine("Sorani Latin - Default LTR:");
      Console.WriteLine($"  {date.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      Console.WriteLine("Sorani Latin - Forced RTL:");
      Console.WriteLine($"  {date.ToString("D", KurdishDialect.SoraniLatin, KurdishTextDirection.RightToLeft)}");
      Console.WriteLine();
    }

    private static void Example5_DateComparisons()
    {
      PrintSection("Example 5: Date Comparisons");

      KurdishDate date1 = new KurdishDate(2725, 1, 1);
      KurdishDate date2 = new KurdishDate(2725, 6, 15);
      KurdishDate date3 = new KurdishDate(2725, 1, 1);

      Console.WriteLine($"Date 1: {date1.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"Date 2: {date2.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"Date 3: {date3.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      Console.WriteLine($"date1 == date2: {date1 == date2}");
      Console.WriteLine($"date1 == date3: {date1 == date3}");
      Console.WriteLine($"date1 < date2:  {date1 < date2}");
      Console.WriteLine($"date2 > date1:  {date2 > date1}");
      Console.WriteLine();

      // Sorting example
      KurdishDate[] dates = new KurdishDate[] { date2, date1, new KurdishDate(2726, 1, 1) };
      Array.Sort(dates);

      Console.WriteLine("Sorted dates:");
      foreach (var d in dates)
      {
        Console.WriteLine($"  {d.ToString("D", KurdishDialect.SoraniLatin)}");
      }
      Console.WriteLine();
    }

    // ═══════════════════════════════════════════════════════════════
    // ASTRONOMICAL CALENDAR EXAMPLES
    // ═══════════════════════════════════════════════════════════════

    private static void Example6_AstronomicalBasics()
    {
      PrintHeader("Example 6: Astronomical Calendar Basics");

      Console.WriteLine("🌟 ASTRONOMICAL PRECISION - Uses actual spring equinox!");
      Console.WriteLine("   (Simplified calendar assumes fixed 21 March every year)\n");

      PrintSection("Creating Astronomical Dates");

      // Create astronomical dates with different reference locations
      KurdishAstronomicalDate erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
      KurdishAstronomicalDate slemani = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
      KurdishAstronomicalDate tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
      KurdishAstronomicalDate utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);

      Console.WriteLine("Nowruz 2725 (1st day of Kurdish year) at different locations:\n");
      Console.WriteLine($"Erbil (44°E):        {erbil.ToDateTime():yyyy-MM-dd} (Gregorian date)");
      Console.WriteLine($"Sulaymaniyah (45°E): {slemani.ToDateTime():yyyy-MM-dd} (Gregorian date)");
      Console.WriteLine($"Tehran (52.5°E):     {tehran.ToDateTime():yyyy-MM-dd} (Gregorian date)");
      Console.WriteLine($"UTC (0°):            {utc.ToDateTime():yyyy-MM-dd} (Gregorian date)");
      Console.WriteLine();

      Console.WriteLine("📍 All locations have Nowruz on the same Gregorian date this year.");
      Console.WriteLine("   The calculation accounts for the precise equinox moment at each longitude.");
      Console.WriteLine();

      PrintSection("Actual Equinox Time");
      Console.WriteLine("For Kurdish year 2725 (Gregorian 2025):\n");

      DateTime equinoxUtc = KurdishAstronomicalDate.GetEquinoxMomentUtc(2725);
      Console.WriteLine($"Universal moment: {equinoxUtc:yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine();

      Console.WriteLine("Local time at different locations:");
      DateTime erbilEquinox = KurdishAstronomicalDate.GetEquinoxMoment(2725, 44.0);
      DateTime tehranEquinox = KurdishAstronomicalDate.GetEquinoxMoment(2725, 52.5);

      Console.WriteLine($"  Erbil:  {erbilEquinox:yyyy-MM-dd HH:mm:ss}");
      Console.WriteLine($"  Tehran: {tehranEquinox:yyyy-MM-dd HH:mm:ss}");
      Console.WriteLine();

      Console.WriteLine("ℹ️  Tip: Use Example 9 to see detailed equinox timing for multiple locations.");
    }

    private static void Example7_LocationComparison()
    {
      PrintSection("Example 7: Location-Based Date Differences");

      Console.WriteLine("🌍 Checking if Nowroz falls on the same Gregorian day:\n");
      Console.WriteLine("Year | Erbil      | Tehran     | Match?");
      Console.WriteLine("-----|------------|------------|--------");

      for (int year = 2725; year <= 2730; year++)
      {
        var erbilDate = KurdishAstronomicalDate.FromErbil(year, 1, 1);
        var tehranDate = KurdishAstronomicalDate.FromTehran(year, 1, 1);

        DateTime erbilGreg = erbilDate.ToDateTime();
        DateTime tehranGreg = tehranDate.ToDateTime();

        string match = erbilGreg.Date == tehranGreg.Date ? "✓ Yes" : "✗ No";

        Console.WriteLine($"{year} | {erbilGreg:yyyy-MM-dd} | {tehranGreg:yyyy-MM-dd} | {match}");
      }
      Console.WriteLine();
    }

    private static void Example8_ConversionMethods()
    {
      PrintSection("Example 8: Simplified ↔ Astronomical Conversion");

      Console.WriteLine("🔄 Two conversion methods:\n");

      KurdishDate simplified = new KurdishDate(2725, 1, 1);
      Console.WriteLine($"Simplified date:       {simplified.Year}/{simplified.Month}/{simplified.Day}");
      Console.WriteLine($"  → Gregorian:       {simplified.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Astronomical conversion
      KurdishAstronomicalDate astronomical = simplified.ToAstronomical();
      Console.WriteLine($"Astronomical conversion: {astronomical.Year}/{astronomical.Month}/{astronomical.Day}");
      Console.WriteLine($"  → Gregorian:       {astronomical.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine("  ℹ️  Keeps same Y/M/D, uses astronomical equinox");
      Console.WriteLine();

      // Informational conversion
      KurdishAstronomicalDate informational = simplified.ToAstronomicalRecalculated();
      Console.WriteLine($"Informational:       {informational.Year}/{informational.Month}/{informational.Day}");
      Console.WriteLine($"  → Gregorian:       {informational.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine("  ℹ️  Recalculates via Gregorian for accuracy");
      Console.WriteLine();

      // Reverse conversion
      KurdishDate backToSimplified = astronomical.ToSimplifiedDate();
      Console.WriteLine($"Back to simplified:    {backToSimplified.Year}/{backToSimplified.Month}/{backToSimplified.Day}");
      Console.WriteLine();
    }

    private static void Example9_PreciseNowrozTiming()
    {
      PrintHeader("Example 9: Precise Nowroz Timing");

      int currentYear = DateTime.UtcNow.Year;
      int kurdishYear = currentYear + 700;

      Console.WriteLine($"Spring Equinox for Kurdish Year {kurdishYear} (Gregorian {currentYear})");
      Console.WriteLine("═══════════════════════════════════════════════════════════════════════\n");

      // Get the UTC moment of the equinox
      DateTime equinoxUtc = KurdishAstronomicalDate.GetEquinoxMomentUtc(kurdishYear);
      Console.WriteLine($"⏰ Equinox occurs at: {equinoxUtc:yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine("   (This is the same instant globally)\n");

      PrintSection("Equinox Moment in Local Time");
      Console.WriteLine("Different locations experience the equinox at different local times:");
      Console.WriteLine("(approximately 15° longitude = 1 hour difference)\n");

      var locations = new[]
      {
        ("Erbil", 44.0),
        ("Sulaymaniyah", 45.4375),
        ("Duhok", 43.0),
        ("Baghdad", 44.4),
        ("Tehran", 52.5),
        ("UTC (Greenwich)", 0.0)
      };

      foreach ((string name, double longitude) in locations)
      {
        DateTime equinoxLocal = KurdishAstronomicalDate.GetEquinoxMoment(kurdishYear, longitude);
        double hoursOffset = longitude / 15.0;
        string utcOffset = $"UTC{(hoursOffset >= 0 ? "+" : "")}{hoursOffset:F2}";

        Console.WriteLine($"  {name,-18} (Longitude {longitude,7:F2}° ≈ {utcOffset}):");
        Console.WriteLine($"    Local time: {equinoxLocal:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine();
      }

      PrintSection("Nowruz Calendar Date by Location");
      Console.WriteLine("All locations shown above have Nowruz on the same Gregorian date this year.");
      Console.WriteLine("However, if the equinox occurred very close to midnight, some locations");
      Console.WriteLine("might observe it on different calendar dates.\n");

      Console.WriteLine("Nowruz dates for this year:");
      foreach ((string name, double longitude) in locations)
      {
        DateTime equinoxLocal = KurdishAstronomicalDate.GetEquinoxMoment(kurdishYear, longitude);
        Console.WriteLine($"  {name,-18}: {equinoxLocal:yyyy-MM-dd} (local date)");
      }
      Console.WriteLine();

      PrintSection("Edge Case Example");
      Console.WriteLine("If the equinox occurred at 22:00 UTC (hypothetical):");
      Console.WriteLine("  - New York (UTC-4.93):  March 20, 17:07 → Nowruz is March 20");
      Console.WriteLine("  - Erbil (UTC+2.93):     March 21, 00:53 → Nowruz is March 21  ⚠️ Different date!");
      Console.WriteLine("  - Tokyo (UTC+9.32):     March 21, 07:18 → Nowruz is March 21");
      Console.WriteLine();
      Console.WriteLine("The library correctly handles these edge cases!");
    }

    private static void Example10_HistoricalDates()
    {
      PrintSection("Example 10: Historical Date Conversion");

      Console.WriteLine("📜 Historical events in Kurdish calendar:\n");

      var events = new[]
      {
        ("Moon Landing", new DateTime(1969, 7, 20)),
        ("Fall of Berlin Wall", new DateTime(1989, 11, 9)),
        ("September 11", new DateTime(2001, 9, 11)),
        ("Arab Spring Begins", new DateTime(2010, 12, 17))
      };

      foreach ((string eventName, DateTime gregorianDate) in events)
      {
        var astroDate = KurdishAstronomicalDate.FromDateTime(gregorianDate);
        var simplifiedDate = KurdishDate.FromDateTime(gregorianDate);

        Console.WriteLine($"{eventName}:");
        Console.WriteLine($"  Gregorian:       {gregorianDate:yyyy-MM-dd}");
        Console.WriteLine($"  Kurdish (Astro): {astroDate.Year}/{astroDate.Month:D2}/{astroDate.Day:D2}");
        Console.WriteLine($"  Kurdish (Smp):   {simplifiedDate.Year}/{simplifiedDate.Month:D2}/{simplifiedDate.Day:D2}");

        if (astroDate.Year != simplifiedDate.Year || astroDate.Month != simplifiedDate.Month || astroDate.Day != simplifiedDate.Day)
        {
          Console.WriteLine($"  ⚠️  Dates differ due to calculation method");
        }
        Console.WriteLine();
      }
    }

    // ═══════════════════════════════════════════════════════════════
    // INTEGRATION EXAMPLES
    // ═══════════════════════════════════════════════════════════════

    private static void Example11_InterfaceUsage()
    {
      PrintSection("Example 11: Using IKurdishDate Interface");

      Console.WriteLine("🔧 Both date types can be used polymorphically:\n");

      IKurdishDate[] dates = new IKurdishDate[]
      {
        new KurdishDate(2725, 6, 15),
        KurdishAstronomicalDate.FromErbil(2725, 6, 15),
        new KurdishDate(2726, 1, 1),
        KurdishAstronomicalDate.FromTehran(2726, 1, 1)
      };

      foreach (var date in dates)
      {
        string type = date is KurdishDate ? "Simplified    " : "Astronomical";
        Console.WriteLine($"{type}: {date.Year}/{date.Month:D2}/{date.Day:D2} → {date.ToDateTime():yyyy-MM-dd}");
      }
      Console.WriteLine();
    }

    private static void Example12_RealWorldScenarios()
    {
      PrintSection("Example 12: Real-World Scenarios");

      // Scenario 1: Birthday reminder
      Console.WriteLine("📅 Scenario 1: Birthday Reminder System");
      KurdishDate birthdayKurdish = new KurdishDate(2700, 3, 15);
      DateTime birthdayGregorian = birthdayKurdish.ToDateTime();
      Console.WriteLine($"  Kurdish birthday:  {birthdayKurdish.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  First celebration: {birthdayGregorian:yyyy-MM-dd}");

      KurdishDate nextBirthday = new KurdishDate(2725, 3, 15);
      Console.WriteLine($"  2725 celebration:  {nextBirthday.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Scenario 2: Event countdown
      Console.WriteLine("⏳ Scenario 2: Days Until Newroz 2726");
      KurdishDate today = KurdishDate.Today;
      KurdishDate newroz2726 = new KurdishDate(2726, 1, 1);
      int daysUntil = newroz2726.DaysDifference(today);
      Console.WriteLine($"  Today:         {today.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Newroz 2726:   {newroz2726.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Days until:    {daysUntil} days");
      Console.WriteLine();

      // Scenario 3: Cultural event planning
      Console.WriteLine("🎉 Scenario 3: Planning Nowroz Ceremony");
      var nowrozAstro = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
      DateTime exactMoment = nowrozAstro.ToDateTime();

      double erbilOffset = 44.0 / 15.0;
      DateTime localTime = exactMoment.AddHours(erbilOffset);

      Console.WriteLine($"  Nowroz 2726 begins:");
      Console.WriteLine($"    UTC:   {exactMoment:yyyy-MM-dd HH:mm:ss}");
      Console.WriteLine($"    Erbil: {localTime:yyyy-MM-dd HH:mm:ss} (approx)");
      Console.WriteLine($"  ✨ Perfect timing for family gathering!");
      Console.WriteLine();

      // Scenario 4: Document dating
      Console.WriteLine("📄 Scenario 4: Official Document");
      KurdishDate docDate = KurdishDate.Today;
      Console.WriteLine($"  Sorani Arabic:  {docDate.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine($"  Kurmanji Latin: {docDate.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  Gregorian:      {docDate.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();
    }

    // ═══════════════════════════════════════════════════════════════
    // Example 13: Simplified vs Astronomical - THE REAL DIFFERENCES
    // ═══════════════════════════════════════════════════════════════
    private static void Example13_SimplifiedVsAstronomicalDifferences()
    {
      PrintHeader("🌟 STANDARD vs ASTRONOMICAL: See the REAL Differences!");

      Console.WriteLine("The spring equinox doesn't always fall on 21 March!");
      Console.WriteLine("It can occur on 20, 21, or 22 March depending on the year.");
      Console.WriteLine("This creates REAL differences between simplified and astronomical dates.\n");

      // Years where the equinox falls on different dates
      int[] interestingYears = new int[] {
        2024,  // 20 March (early!)
        2025,  // 21 March (normal)
        2026,  // 21 March (normal)
        2027,  // 21 March (normal)
        2028,  // 20 March (leap year - early!)
        2044,  // 20 March (will be earliest since 1796!)
        2096,  // 20 March (earliest in 21st century)
        2102   // 21 March (latest in 21st century)
      };

      Console.WriteLine("═══════════════════════════════════════════════════════════════");
      Console.WriteLine("YEAR-BY-YEAR COMPARISON: Simplified vs Astronomical");
      Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

      foreach (int gregorianYear in interestingYears)
      {
        // Create a date in late March of that year
        DateTime marchDate = new DateTime(gregorianYear, 3, 25);

        // Simplified calendar always uses 21 March
        KurdishDate simplified = new KurdishDate(marchDate);

        // Astronomical calendar uses actual equinox
        KurdishAstronomicalDate astronomical = KurdishAstronomicalDate.FromDateTime(marchDate);

        Console.WriteLine($"📅 Gregorian Year {gregorianYear}:");
        Console.WriteLine($"   Simplified:     {simplified.Year}/{simplified.Month:D2}/{simplified.Day:D2}");
        Console.WriteLine($"   Astronomical: {astronomical.Year}/{astronomical.Month:D2}/{astronomical.Day:D2}");

        if (simplified.Year != astronomical.Year)
        {
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine($"   ⚠️  YEAR DIFFERS! Difference: {Math.Abs(simplified.Year - astronomical.Year)} year(s)");
          Console.ResetColor();
        }
        else if (simplified.Month != astronomical.Month || simplified.Day != astronomical.Day)
        {
          Console.ForegroundColor = ConsoleColor.Cyan;
          Console.WriteLine($"   ℹ️  Dates differ but same year");
          Console.ResetColor();
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine($"   ✓ Same date");
          Console.ResetColor();
        }
        Console.WriteLine();
      }

      Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
      Console.WriteLine("DETAILED EXAMPLE: March 2024 (Early Equinox Year)");
      Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

      // 20 March 2024 - equinox falls on 20 March!
      DateTime march20 = new DateTime(2024, 3, 20);
      DateTime march21 = new DateTime(2024, 3, 21);
      DateTime march22 = new DateTime(2024, 3, 22);

      Console.WriteLine("🌍 Equinox occurred on 20 March 2024 at 03:06 UTC");
      Console.WriteLine("   (Earliest spring equinox in 128 years!)\n");

      void CompareDates(DateTime gregDate, string label)
      {
        KurdishDate Smp = new KurdishDate(gregDate);
        KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromDateTime(gregDate);

        Console.WriteLine($"{label}: {gregDate:yyyy-MM-dd}");
        Console.WriteLine($"  Simplified:     {Smp.ToString("D", KurdishDialect.SoraniLatin)}");
        Console.WriteLine($"  Astronomical: {astro.ToString("D", KurdishDialect.SoraniLatin)}");

        if (Smp.Year != astro.Year || Smp.Month != astro.Month || Smp.Day != astro.Day)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"  >>> DATES DIFFER! <<<");
          Console.ResetColor();
        }
        Console.WriteLine();
      }

      CompareDates(march20, "20 March");
      CompareDates(march21, "21 March");
      CompareDates(march22, "22 March");

      Console.WriteLine("═══════════════════════════════════════════════════════════════");
      Console.WriteLine("LOCATION MATTERS: Same Gregorian Date, Different Kurdish Dates");
      Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

      DateTime teSmpate = new DateTime(2024, 3, 21, 12, 0, 0); // Noon on 21 March

      KurdishDate simplifiedDate = new KurdishDate(teSmpate);
      KurdishAstronomicalDate erbilDate = KurdishAstronomicalDate.FromDateTime(teSmpate, 44.0); // Erbil longitude
      KurdishAstronomicalDate tehranDate = KurdishAstronomicalDate.FromDateTime(teSmpate, 51.4); // Tehran longitude

      Console.WriteLine($"Gregorian: {teSmpate:yyyy-MM-dd HH:mm} UTC");
      Console.WriteLine($"\nSimplified (fixed 21 March):     {simplifiedDate.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"Astronomical (Erbil, 44°E):    {erbilDate.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"Astronomical (Tehran, 51.4°E): {tehranDate.ToString("D", KurdishDialect.SoraniLatin)}");

      Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
      Console.WriteLine("KEY TAKEAWAYS");
      Console.WriteLine("═══════════════════════════════════════════════════════════════");
      Console.WriteLine("✓ Simplified calendar: Simple, predictable, 99.9% use cases");
      Console.WriteLine("✓ Astronomical calendar: Precise, accounts for actual equinox");
      Console.WriteLine("✓ Differences occur in:");
      Console.WriteLine("  - Leap years (equinox shifts ~18 hours earlier)");
      Console.WriteLine("  - Years with early/late equinoxes (March 19 or 21)");
      Console.WriteLine("  - Different geographic longitudes");
      Console.WriteLine("✓ For cultural celebrations and official dates: use Simplified");
      Console.WriteLine("✓ For astronomical accuracy and research: use Astronomical");
    }

    // ═══════════════════════════════════════════════════════════════
    // Example 21: Sorani Gregorian Basics
    // ═══════════════════════════════════════════════════════════════
    private static void Example21_SoraniGregorianBasics()
    {
      PrintHeader("Example 21: Gregorian Calendar with Sorani Month Names");

      Console.WriteLine("The Gregorian calendar with Sorani Kurdish month names is used");
      Console.WriteLine("for official purposes in Kurdistan Region.\n");

      DateTime today = DateTime.Now;

      PrintSection("Basic Formatting");
      Console.WriteLine("Today's date in different formats:");
      Console.WriteLine($"  English:       {today:dddd, d MMMM yyyy}");
      Console.WriteLine($"  Sorani Latin:  {today.ToSoraniGregorianLong()}");
      Console.WriteLine($"  Sorani Arabic: {today.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Arabic)}");
      Console.WriteLine();

      PrintSection("Short Format");
      Console.WriteLine($"  Latin:  {today.ToSoraniGregorianShort()}");
      Console.WriteLine($"  Arabic: {today.ToSoraniGregorianShort(GregorianSoraniFormatter.ScriptType.Arabic)}");
      Console.WriteLine();

      PrintSection("All 12 Months in Sorani");
      Console.WriteLine("Gregorian months with Sorani Kurdish names:\n");
      for (int month = 1; month <= 12; month++)
      {
        DateTime date = new DateTime(2025, month, 15);
        string latin = date.ToSoraniGregorianLong();
        string arabic = date.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Arabic);
        Console.WriteLine($"  {month,2}. {latin,-30} | {arabic}");
      }
      Console.WriteLine();

      PrintSection("Text Direction Control");
      Console.WriteLine("Latin script (LTR default):");
      Console.WriteLine($"  {today.ToSoraniGregorianLong()}");
      Console.WriteLine("\nLatin script (forced RTL):");
      Console.WriteLine($"  {today.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Latin, KurdishTextDirection.RightToLeft)}");
      Console.WriteLine("\nArabic script (RTL default):");
      Console.WriteLine($"  {today.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Arabic)}");
      Console.WriteLine("\nArabic script (forced LTR):");
      Console.WriteLine($"  {today.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Arabic, KurdishTextDirection.LeftToRight)}");
    }

    // ═══════════════════════════════════════════════════════════════
    // Example 22: Kurmanji Gregorian Basics
    // ═══════════════════════════════════════════════════════════════
    private static void Example22_KurmanjiGregorianBasics()
    {
      PrintHeader("Example 22: Gregorian Calendar with Kurmanji Month Names");

      Console.WriteLine("Kurmanji speakers also use the Gregorian calendar with Kurdish month names.\n");

      DateTime today = DateTime.Now;

      PrintSection("Basic Formatting");
      Console.WriteLine("Today's date in different formats:");
      Console.WriteLine($"  English:         {today:dddd, d MMMM yyyy}");
      Console.WriteLine($"  Kurmanji Latin:  {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"  Kurmanji Arabic: {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
      Console.WriteLine();

      PrintSection("Custom Format Strings");
      Console.WriteLine("Using custom format tokens:");
      Console.WriteLine($"  dd/MM/yyyy:   {today.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Latin, "dd/MM/yyyy")}");
      Console.WriteLine($"  dd MMM yy:    {today.ToKurmanjiGregorian(GregorianKurmanjiFormatter.ScriptType.Latin, "dd MMM yy")}");
      Console.WriteLine($"  Short format: {today.ToKurmanjiGregorianShort()}");
      Console.WriteLine($"  Long format:  {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine();

      PrintSection("All 12 Months in Kurmanji");
      Console.WriteLine("Gregorian months with Kurmanji Kurdish names:\n");
      for (int month = 1; month <= 12; month++)
      {
        DateTime date = new DateTime(2025, month, 1);
        string latin = date.ToKurmanjiGregorianLong();
        string arabic = date.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic);
        Console.WriteLine($"  {month,2}. {latin,-30} | {arabic}");
      }
      Console.WriteLine();

      PrintSection("Practical Usage");
      Console.WriteLine("📄 Government document header:");
      Console.WriteLine($"   Date: {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"   ڕێکەوت: {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
    }

    // ═══════════════════════════════════════════════════════════════
    // Example 23: Calendar System Comparison
    // ═══════════════════════════════════════════════════════════════
    private static void Example23_CalendarSystemComparison()
    {
      PrintHeader("Example 23: Traditional Kurdish vs Gregorian Calendars");

      Console.WriteLine("Understanding the difference between calendar systems:\n");

      DateTime nowrozGregorian = new DateTime(2025, 3, 21);
      KurdishDate nowrozKurdish = new KurdishDate(nowrozGregorian);

      PrintSection("Same Moment, Different Calendars");
      Console.WriteLine("Nowroz 2025 in different representations:\n");

      Console.WriteLine("GREGORIAN CALENDAR:");
      Console.WriteLine($"  English simplified:   {nowrozGregorian:d MMMM yyyy}");
      Console.WriteLine($"  Kurmanji Gregorian:   {nowrozGregorian.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"  Sorani Gregorian:     {nowrozGregorian.ToSoraniGregorianLong()}");
      Console.WriteLine($"  → Uses months: January, February, March... (Kanûna Duyê, Şubat, Adar...)");
      Console.WriteLine();

      Console.WriteLine("TRADITIONAL KURDISH CALENDAR:");
      Console.WriteLine($"  Sorani:   {nowrozKurdish.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Kurmanji: {nowrozKurdish.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  → Uses months: Xakelêwe, Gulan, Cozerdan...");
      Console.WriteLine();

      PrintSection("Month Names Side-by-Side");
      Console.WriteLine("║ # ║ Gregorian (Sorani)    ║ Gregorian (Kurmanji)  ║ Kurdish (Traditional) ║");
      Console.WriteLine("╠═══╬═══════════════════════╬═══════════════════════╬═══════════════════════╣");

      string[] kurdishMonths = { "Xakelêwe", "Gulan", "Cozerdan", "Pûşper", "Gelawêj", "Xermanan",
                                  "Rezber", "Gelarêzan", "Sermawez", "Befranbar", "Rêbendan", "Reşeme" };

      for (int i = 1; i <= 12; i++)
      {
        DateTime date = new DateTime(2025, i, 1);
        string soraniName = GregorianSoraniFormatter.GetMonthName(i, GregorianSoraniFormatter.ScriptType.Latin);
        string kurmanjiName = GregorianKurmanjiFormatter.GetMonthName(i, GregorianKurmanjiFormatter.ScriptType.Latin);
        string kurdishName = kurdishMonths[i - 1];
        Console.WriteLine($"║{i,2} ║ {soraniName,-21} ║ {kurmanjiName,-21} ║ {kurdishName,-21} ║");
      }
      Console.WriteLine();

      PrintSection("When to Use Each System");
      Console.WriteLine("✓ GREGORIAN with Kurdish month names:");
      Console.WriteLine("  - Official government documents (KRI)");
      Console.WriteLine("  - Business correspondence");
      Console.WriteLine("  - International coordination");
      Console.WriteLine("  - Modern administrative purposes");
      Console.WriteLine();
      Console.WriteLine("✓ TRADITIONAL Kurdish calendar:");
      Console.WriteLine("  - Cultural events (Nowroz, traditional celebrations)");
      Console.WriteLine("  - Literary and artistic works");
      Console.WriteLine("  - Historical references");
      Console.WriteLine("  - Preserving Kurdish heritage");
    }

    // ═══════════════════════════════════════════════════════════════
    // Example 24: All Dialects Demo
    // ═══════════════════════════════════════════════════════════════
    private static void Example24_AllDialectsDemo()
    {
      PrintHeader("Example 24: Complete Dialect Demonstration");

      DateTime today = DateTime.Now;
      KurdishDate kurdishToday = new KurdishDate(today);

      PrintSection("Today's Date in ALL Available Formats");
      Console.WriteLine();

      Console.WriteLine("GREGORIAN CALENDAR WITH KURDISH MONTH NAMES:");
      Console.WriteLine("──────────────────────────────────────────────");
      Console.WriteLine($"  Sorani Latin:      {today.ToSoraniGregorianLong()}");
      Console.WriteLine($"  Sorani Arabic:     {today.ToSoraniGregorianLong(GregorianSoraniFormatter.ScriptType.Arabic)}");
      Console.WriteLine($"  Kurmanji Latin:    {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine($"  Kurmanji Arabic:   {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
      Console.WriteLine();

      Console.WriteLine("TRADITIONAL KURDISH CALENDAR:");
      Console.WriteLine("──────────────────────────────────────────────");
      Console.WriteLine($"  Sorani Latin:      {kurdishToday.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"  Sorani Arabic:     {kurdishToday.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine($"  Kurmanji Latin:    {kurdishToday.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine($"  Kurmanji Arabic:   {kurdishToday.ToString("D", KurdishDialect.KurmanjiArabic)}");
      Console.WriteLine($"  Hawrami Latin:     {kurdishToday.ToString("D", KurdishDialect.HawramiLatin)}");
      Console.WriteLine($"  Hawrami Arabic:    {kurdishToday.ToString("D", KurdishDialect.HawramiArabic)}");
      Console.WriteLine();

      Console.WriteLine("STANDARD GREGORIAN (for reference):");
      Console.WriteLine("──────────────────────────────────────────────");
      Console.WriteLine($"  English:           {today:dddd, d MMMM yyyy}");
      Console.WriteLine($"  ISO 8601:          {today:yyyy-MM-dd}");
      Console.WriteLine();

      PrintSection("Bilingual Document Example");
      Console.WriteLine("═══════════════════════════════════════════════════════════");
      Console.WriteLine("                  OFFICIAL DOCUMENT HEADER");
      Console.WriteLine("═══════════════════════════════════════════════════════════");
      Console.WriteLine();
      Console.WriteLine($"Date (English):  {today:dddd, d MMMM yyyy}");
      Console.WriteLine($"ڕێکەوت (کوردی):  {today.ToKurmanjiGregorianLong(GregorianKurmanjiFormatter.ScriptType.Arabic)}");
      Console.WriteLine($"Date (Kurdî):    {today.ToKurmanjiGregorianLong()}");
      Console.WriteLine();
      Console.WriteLine($"Kurdish Calendar: {kurdishToday.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine($"ساڵی کوردی:       {kurdishToday.ToString("D", KurdishDialect.SoraniArabic)}");
      Console.WriteLine();
      Console.WriteLine("═══════════════════════════════════════════════════════════");
    }
  }
}