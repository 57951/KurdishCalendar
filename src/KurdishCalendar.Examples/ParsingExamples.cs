using System;
using KurdishCalendar.Core;

namespace KurdishCalendar.Examples
{
  /// <summary>
  /// Examples demonstrating Kurdish date parsing functionality.
  /// </summary>
  public static class ParsingExamples
  {
    public static void RunAllExamples()
    {
      Console.WriteLine("═══════════════════════════════════════════════════════════════");
      Console.WriteLine("           KURDISH DATE PARSING EXAMPLES");
      Console.WriteLine("═══════════════════════════════════════════════════════════════");
      Console.WriteLine();

      Example14_BasicParsing();
      Example15_ParseDifferentFormats();
      Example16_ParseArabicScript();
      Example17_ParseAstronomicalDates();
      Example18_TryParsePattern();
      Example19_RoundTripFormatting();
      Example20_ErrorHandling();
    }

    internal static void Example14_BasicParsing()
    {
      PrintSection("Example 14: Basic Parsing");

      // Parse long format (day month year)
      KurdishDate date1 = KurdishDate.Parse("15 Xakelêwe 2725", KurdishDialect.SoraniLatin);
      Console.WriteLine($"Parsed: '15 Xakelêwe 2725' → {date1.Year}/{date1.Month}/{date1.Day}");

      // Parse short format (day/month/year)
      KurdishDate date2 = KurdishDate.Parse("15/01/2725", KurdishDialect.SoraniLatin);
      Console.WriteLine($"Parsed: '15/01/2725' → {date2.Year}/{date2.Month}/{date2.Day}");

      // Parse with different dialects
      KurdishDate date3 = KurdishDate.Parse("6 Befranbar 2725", KurdishDialect.KurmanjiLatin);
      Console.WriteLine($"Parsed: '6 Befranbar 2725' (Kurmanji) → {date3.Year}/{date3.Month}/{date3.Day}");

      KurdishDate date4 = KurdishDate.Parse("20 Gelawêj 2725", KurdishDialect.HawramiLatin);
      Console.WriteLine($"Parsed: '20 Gelawêj 2725' (Hawrami) → {date4.Year}/{date4.Month}/{date4.Day}");

      Console.WriteLine();
    }

    internal static void Example15_ParseDifferentFormats()
    {
      PrintSection("Example 15: Parsing Different Formats");

      string[] inputs = new[]
      {
        "15/01/2725",     // Slash separator
        "15-01-2725",     // Dash separator
        "15 01 2725",     // Space separator
        "2725 Xakelêwe 15", // RTL order: year month day
        "15 Xakelêwe 2725"  // LTR order: day month year
      };

      foreach (string input in inputs)
      {
        KurdishDate date = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);
        Console.WriteLine($"'{input}' → {date.ToString("D", KurdishDialect.SoraniLatin)}");
      }

      Console.WriteLine();
    }

    internal static void Example16_ParseArabicScript()
    {
      PrintSection("Example 16: Parsing Arabic Script Dates");

      // Parse long format in Arabic script
      KurdishDate date1 = KurdishDate.Parse("١٥ خاکەلێوە ٢٧٢٥", KurdishDialect.SoraniArabic);
      Console.WriteLine($"Parsed Arabic: '١٥ خاکەلێوە ٢٧٢٥'");
      Console.WriteLine($"  → {date1.Year}/{date1.Month}/{date1.Day}");
      Console.WriteLine($"  → {date1.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Parse short format in Arabic script (RTL: year/month/day)
      KurdishDate date2 = KurdishDate.Parse("٢٧٢٥/٠١/١٥", KurdishDialect.SoraniArabic);
      Console.WriteLine($"Parsed Arabic RTL: '٢٧٢٥/٠١/١٥'");
      Console.WriteLine($"  → {date2.Year}/{date2.Month}/{date2.Day}");
      Console.WriteLine($"  → {date2.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();

      // Parse Kurmanji Arabic
      KurdishDate date3 = KurdishDate.Parse("٦ بەفرانبار ٢٧٢٥", KurdishDialect.KurmanjiArabic);
      Console.WriteLine($"Parsed Kurmanji Arabic: '٦ بەفرانبار ٢٧٢٥'");
      Console.WriteLine($"  → {date3.Year}/{date3.Month}/{date3.Day}");
      Console.WriteLine($"  → {date3.ToString("D", KurdishDialect.KurmanjiLatin)}");
      Console.WriteLine();
    }

    internal static void Example17_ParseAstronomicalDates()
    {
      PrintSection("Example 17: Parsing Astronomical Dates");

      // Parse with default Erbil longitude
      KurdishAstronomicalDate date1 = KurdishAstronomicalDate.Parse(
        "1 Xakelêwe 2725",
        KurdishDialect.SoraniLatin
      );
      Console.WriteLine($"Parsed astronomical (Erbil): '1 Xakelêwe 2725'");
      Console.WriteLine($"  → Gregorian: {date1.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Parse with custom longitude (Sulaymaniyah)
      KurdishAstronomicalDate date2 = KurdishAstronomicalDate.Parse(
        "1 Xakelêwe 2725",
        KurdishDialect.SoraniLatin,
        45.4375 // Sulaymaniyah longitude
      );
      Console.WriteLine($"Parsed astronomical (Sulaymaniyah): '1 Xakelêwe 2725'");
      Console.WriteLine($"  → Gregorian: {date2.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Parse from different formats
      KurdishAstronomicalDate date3 = KurdishAstronomicalDate.Parse(
        "15/06/2725",
        KurdishDialect.SoraniLatin,
        44.0
      );
      Console.WriteLine($"Parsed astronomical short format: '15/06/2725'");
      Console.WriteLine($"  → {date3.ToString("D", KurdishDialect.SoraniLatin)}");
      Console.WriteLine();
    }

    internal static void Example18_TryParsePattern()
    {
      PrintSection("Example 18: Safe Parsing with TryParse");

      string[] inputs = new[]
      {
        "15 Xakelêwe 2725",    // Valid
        "invalid date",         // Invalid
        "99/99/9999",          // Invalid numbers
        "15 InvalidMonth 2725", // Invalid month
        "",                    // Empty
        "6 Befranbar 2725"     // Valid
      };

      foreach (string input in inputs)
      {
        if (KurdishDate.TryParse(input, KurdishDialect.SoraniLatin, out KurdishDate result))
        {
          Console.WriteLine($"✓ '{input}' → {result.ToString("D", KurdishDialect.SoraniLatin)}");
        }
        else
        {
          Console.WriteLine($"✗ '{input}' → Failed to parse");
        }
      }

      Console.WriteLine();
    }

    internal static void Example19_RoundTripFormatting()
    {
      PrintSection("Example 19: Round-Trip Formatting (Format → Parse → Format)");

      KurdishDate original = new KurdishDate(2725, 6, 15);

      Console.WriteLine($"Original date: {original.Year}/{original.Month}/{original.Day}");
      Console.WriteLine();

      // Round-trip in different formats and dialects
      TestRoundTrip(original, "d", KurdishDialect.SoraniLatin, "Short Sorani Latin");
      TestRoundTrip(original, "D", KurdishDialect.SoraniLatin, "Long Sorani Latin");
      TestRoundTrip(original, "D", KurdishDialect.SoraniArabic, "Long Sorani Arabic");
      TestRoundTrip(original, "D", KurdishDialect.KurmanjiLatin, "Long Kurmanji Latin");
      TestRoundTrip(original, "D", KurdishDialect.KurmanjiArabic, "Long Kurmanji Arabic");
      TestRoundTrip(original, "D", KurdishDialect.HawramiLatin, "Long Hawrami Latin");

      Console.WriteLine();
    }

    internal static void Example20_ErrorHandling()
    {
      PrintSection("Example 20: Error Handling");

      Console.WriteLine("Demonstrating exception handling with Parse():");
      Console.WriteLine();

      string[] invalidInputs = new[]
      {
        "not a date",
        "99/99/9999",
        "15 InvalidMonth 2725",
        ""
      };

      foreach (string input in invalidInputs)
      {
        try
        {
          KurdishDate date = KurdishDate.Parse(input, KurdishDialect.SoraniLatin);
          Console.WriteLine($"✓ Parsed: '{input}'");
        }
        catch (FormatException ex)
        {
          Console.WriteLine($"✗ Exception for '{input}': {ex.Message}");
        }
      }

      Console.WriteLine();
      Console.WriteLine("Tip: Use TryParse() to avoid exceptions!");
      Console.WriteLine();
    }

    // Helper methods

    internal static void TestRoundTrip(KurdishDate original, string format, KurdishDialect dialect, string description)
    {
      // Format
      string formatted = original.ToString(format, dialect);

      // Parse back
      KurdishDate parsed = KurdishDate.Parse(formatted, dialect);

      // Check equality
      bool isEqual = original.Equals(parsed);

      Console.WriteLine($"{description}:");
      Console.WriteLine($"  Format: '{formatted}'");
      Console.WriteLine($"  Parse:  {parsed.Year}/{parsed.Month}/{parsed.Day}");
      Console.WriteLine($"  Match:  {(isEqual ? "✓" : "✗")}");
      Console.WriteLine();
    }

    private static void PrintSection(string title)
    {
      Console.WriteLine($"--- {title} ---");
      Console.WriteLine();
    }
  }
}