using System;
using KurdishCalendar.Core;

namespace KurdishCalendar.Examples
{
  internal class AstronomicalExamples
  {
    public static void RunAllExamples()
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
      Console.WriteLine("║   Kurdish Calendar - Astronomical Features Demonstration        ║");
      Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
      Console.WriteLine();

      Example1_BasicUsage();
      Example2_LocationComparison();
      Example3_ConversionMethods();
      Example4_NowrozTiming();
      Example5_HistoricalDates();
      Example6_LongTermProjection();

      Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════╗");
      Console.WriteLine("║   All examples completed successfully!                          ║");
      Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
    }

    private static void Example1_BasicUsage()
    {
      Console.WriteLine("═══ Example 1: Basic Usage ═══\n");

      // Create astronomical dates with different reference locations
      KurdishAstronomicalDate erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
      KurdishAstronomicalDate slemani = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
      KurdishAstronomicalDate tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
      KurdishAstronomicalDate utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);

      Console.WriteLine($"Nowroz 2725 in Erbil:       {erbil.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine($"Nowroz 2725 in Sulaymaniyah: {slemani.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine($"Nowroz 2725 in Tehran:       {tehran.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine($"Nowroz 2725 at UTC:          {utc.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");
      Console.WriteLine();
    }

    private static void Example2_LocationComparison()
    {
      Console.WriteLine("═══ Example 2: Location-Based Date Differences ═══\n");

      // Check if Nowroz falls on the same day in different locations
      for (int year = 2725; year <= 2730; year++)
      {
        var erbilDate = KurdishAstronomicalDate.FromErbil(year, 1, 1);
        var tehranDate = KurdishAstronomicalDate.FromTehran(year, 1, 1);

        DateTime erbilGreg = erbilDate.ToDateTime();
        DateTime tehranGreg = tehranDate.ToDateTime();

        string match = erbilGreg.Date == tehranGreg.Date ? "✓ Same" : "✗ Different";

        Console.WriteLine($"Year {year}: Erbil={erbilGreg:MMM dd}, Tehran={tehranGreg:MMM dd} [{match}]");
      }
      Console.WriteLine();
    }

    private static void Example3_ConversionMethods()
    {
      Console.WriteLine("═══ Example 3: Lossless vs Informational Conversion ═══\n");

      // Create a standard date
      KurdishDate standard = new KurdishDate(2725, 1, 1);
      Console.WriteLine($"Standard date: {standard.Year}/{standard.Month}/{standard.Day}");
      Console.WriteLine($"  → Gregorian: {standard.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Lossless conversion (keeps same Y/M/D)
      KurdishAstronomicalDate lossless = standard.ToAstronomical();
      Console.WriteLine($"Lossless conversion: {lossless.Year}/{lossless.Month}/{lossless.Day}");
      Console.WriteLine($"  → Gregorian: {lossless.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Informational conversion (recalculates via Gregorian)
      KurdishAstronomicalDate informational = standard.ToAstronomicalRecalculated();
      Console.WriteLine($"Informational conversion: {informational.Year}/{informational.Month}/{informational.Day}");
      Console.WriteLine($"  → Gregorian: {informational.ToDateTime():yyyy-MM-dd}");
      Console.WriteLine();

      // Explain the difference
      if (lossless.Day != informational.Day || lossless.Month != informational.Month)
      {
        Console.WriteLine("Note: The dates differ because:");
        Console.WriteLine("  - Standard uses fixed 21 March for Nowroz");
        Console.WriteLine("  - Astronomical calculates the actual equinox date");
        Console.WriteLine("  - The actual equinox might be March 19 or 21 in some years");
      }
      else
      {
        Console.WriteLine("Note: In this year, both methods produce the same result");
        Console.WriteLine("  because the actual equinox falls on the assumed date.");
      }
      Console.WriteLine();
    }

    private static void Example4_NowrozTiming()
    {
      Console.WriteLine("═══ Example 4: Precise Nowroz Timing ═══\n");

      int currentYear = DateTime.UtcNow.Year;
      int kurdishYear = currentYear + 700;

      Console.WriteLine($"Finding the exact moment of Nowroz {kurdishYear} (Gregorian {currentYear}):\n");

      // Calculate for different locations
      var locations = new[]
      {
        ("Erbil", 44.0),
        ("Sulaymaniyah", 45.0),
        ("Duhok", 43.0),
        ("Baghdad", 44.4),
        ("Tehran", 52.5)
      };

      foreach ((string name, double longitude) in locations)
      {
        var nowroz = KurdishAstronomicalDate.FromLongitude(kurdishYear, 1, 1, longitude);
        DateTime moment = nowroz.ToDateTime();

        // Calculate local time
        double hoursOffset = longitude / 15.0;
        DateTime localTime = moment.AddHours(hoursOffset);

        Console.WriteLine($"{name,-15} (Long: {longitude,5:F1}°E):");
        Console.WriteLine($"  UTC:   {moment:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  Local: {localTime:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine();
      }
    }

    private static void Example5_HistoricalDates()
    {
      Console.WriteLine("═══ Example 5: Converting Historical Dates ═══\n");

      var historicalEvents = new[]
      {
        ("End of World War I", new DateTime(1918, 11, 11)),
        ("End of World War II", new DateTime(1945, 5, 8)),
        ("Moon Landing", new DateTime(1969, 7, 20)),
        ("Fall of Berlin Wall", new DateTime(1989, 11, 9)),
        ("September 11 Attacks", new DateTime(2001, 9, 11))
      };

      foreach ((string eventName, DateTime gregorianDate) in historicalEvents)
      {
        var astroDate = KurdishAstronomicalDate.FromDateTime(gregorianDate);
        var standardDate = KurdishDate.FromDateTime(gregorianDate);

        Console.WriteLine($"{eventName}:");
        Console.WriteLine($"  Gregorian:   {gregorianDate:yyyy-MM-dd}");
        Console.WriteLine($"  Astronomical: {astroDate.Year}/{astroDate.Month:D2}/{astroDate.Day:D2}");
        Console.WriteLine($"  Standard:     {standardDate.Year}/{standardDate.Month:D2}/{standardDate.Day:D2}");

        if (astroDate.Day != standardDate.Day || astroDate.Month != standardDate.Month || astroDate.Year != standardDate.Year)
        {
          Console.WriteLine($"  (Dates differ due to equinox calculation method)");
        }
        Console.WriteLine();
      }
    }

    private static void Example6_LongTermProjection()
    {
      Console.WriteLine("═══ Example 6: Long-Term Nowroz Date Projection ═══\n");
      Console.WriteLine("Shows how the equinox date varies over decades:\n");
      Console.WriteLine("Year   | Standard | Astronomical | Difference");
      Console.WriteLine("-------|----------|--------------|------------");

      for (int kurdishYear = 2700; kurdishYear <= 2800; kurdishYear += 10)
      {
        var standardNewroz = new KurdishDate(kurdishYear, 1, 1);
        var astroNewroz = KurdishAstronomicalDate.FromErbil(kurdishYear, 1, 1);

        DateTime standardGreg = standardNewroz.ToDateTime();
        DateTime astroGreg = astroNewroz.ToDateTime();

        int daysDiff = (astroGreg.Date - standardGreg.Date).Days;
        string diffStr = daysDiff == 0 ? "Same" : $"{daysDiff:+0;-0} day";

        Console.WriteLine($"{kurdishYear} | {standardGreg:MMM dd}   | {astroGreg:MMM dd}       | {diffStr}");
      }

      Console.WriteLine("\nNote: Over long periods, the astronomical calculation reveals");
      Console.WriteLine("      the variation in equinox dates that the standard method ignores.");
      Console.WriteLine();
    }
  }
}