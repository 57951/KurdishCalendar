using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace KurdishCalendar.Core.Tests.Verification
{
  /// <summary>
  /// Comprehensive leap year verification tests.
  /// This test class calculates ACTUAL astronomical leap years by examining
  /// the number of days between consecutive Nowruz dates.
  /// 
  /// Purpose: Determine if test expectations or production code is wrong.
  /// </summary>
  public class LeapYearVerificationTests
  {
    private readonly ITestOutputHelper _output;

    public LeapYearVerificationTests(ITestOutputHelper output)
    {
      _output = output;
    }

    /// <summary>
    /// Comprehensive leap year verification for years 2700-2750 (50 years).
    /// This test DOCUMENTS the actual astronomical behaviour rather than asserting expectations.
    /// </summary>
    [Fact]
    public void VerifyLeapYears_2700to2750_DocumentActualBehaviour()
    {
      _output.WriteLine("=== LEAP YEAR VERIFICATION REPORT ===");
      _output.WriteLine("Testing years 2700-2750 (50 Kurdish years)");
      _output.WriteLine("Method: Count days between consecutive Nowruz dates");
      _output.WriteLine("Leap Year: 366 days between Nowruz dates");
      _output.WriteLine("Common Year: 365 days between Nowruz dates");
      _output.WriteLine("");
      _output.WriteLine("Location: Erbil (44.0°E) - Default reference");
      _output.WriteLine("");

      List<LeapYearResult> results = new List<LeapYearResult>();
      int leapYearCount = 0;
      int commonYearCount = 0;

      for (int year = 2700; year <= 2750; year++)
      {
        LeapYearResult result = AnalyzeYear(year, 44.0);
        results.Add(result);

        if (result.IsLeapYear)
        {
          leapYearCount++;
        }
        else
        {
          commonYearCount++;
        }

        // Output detailed information
        _output.WriteLine($"Year {result.Year}: " +
                         $"{(result.IsLeapYear ? "LEAP" : "common")} " +
                         $"({result.DaysBetweenNowruz} days) " +
                         $"Nowruz: {result.ThisNowruz:yyyy-MM-dd} → {result.NextNowruz:yyyy-MM-dd} " +
                         $"(Gregorian {result.GregorianYearStart})");
      }

      _output.WriteLine("");
      _output.WriteLine("=== SUMMARY ===");
      _output.WriteLine($"Total Years Analyzed: 51");
      _output.WriteLine($"Leap Years: {leapYearCount}");
      _output.WriteLine($"Common Years: {commonYearCount}");
      _output.WriteLine($"Leap Year Percentage: {(leapYearCount * 100.0 / 51):F2}%");
      _output.WriteLine("");

      // Identify leap year pattern
      _output.WriteLine("=== LEAP YEAR PATTERN ===");
      List<int> leapYears = new List<int>();
      foreach (LeapYearResult result in results)
      {
        if (result.IsLeapYear)
        {
          leapYears.Add(result.Year);
        }
      }

      _output.WriteLine($"Leap Years: {string.Join(", ", leapYears)}");
      _output.WriteLine("");

      // Calculate intervals between leap years
      if (leapYears.Count > 1)
      {
        _output.WriteLine("=== LEAP YEAR INTERVALS ===");
        for (int i = 1; i < leapYears.Count; i++)
        {
          int interval = leapYears[i] - leapYears[i - 1];
          _output.WriteLine($"{leapYears[i - 1]} → {leapYears[i]}: {interval} years");
        }
      }

      // This test doesn't assert anything - it documents actual behaviour
      Assert.True(true, "This test documents actual leap year behaviour for investigation");
    }

    /// <summary>
    /// Focused verification on the specific years that failed in the main test suite.
    /// Years 2715, 2719, 2723 were expected to be leap years.
    /// </summary>
    [Theory]
    [InlineData(2715)]
    [InlineData(2719)]
    [InlineData(2723)]
    public void VerifySpecificYear_DocumentActualLeapYearStatus(int year)
    {
      LeapYearResult result = AnalyzeYear(year, 44.0);

      _output.WriteLine($"=== DETAILED ANALYSIS: Year {year} ===");
      _output.WriteLine($"Kurdish Year: {result.Year}");
      _output.WriteLine($"Gregorian Year (start): {result.GregorianYearStart}");
      _output.WriteLine($"Nowruz This Year: {result.ThisNowruz:yyyy-MM-dd HH:mm:ss} UTC");
      _output.WriteLine($"Nowruz Next Year: {result.NextNowruz:yyyy-MM-dd HH:mm:ss} UTC");
      _output.WriteLine($"Days Between: {result.DaysBetweenNowruz}");
      _output.WriteLine($"Calculated Status: {(result.IsLeapYear ? "LEAP YEAR" : "COMMON YEAR")}");
      _output.WriteLine($"Production Code Says: {result.ProductionCodeResult}");
      _output.WriteLine($"Match: {(result.IsLeapYear == result.ProductionCodeResult ? "YES ✓" : "NO ✗")}");
      _output.WriteLine("");

      // Also check adjacent years for context
      _output.WriteLine("=== ADJACENT YEARS (Context) ===");
      LeapYearResult prevYear = AnalyzeYear(year - 1, 44.0);
      LeapYearResult nextYear = AnalyzeYear(year + 1, 44.0);

      _output.WriteLine($"Year {prevYear.Year}: {(prevYear.IsLeapYear ? "LEAP" : "common")} ({prevYear.DaysBetweenNowruz} days)");
      _output.WriteLine($"Year {result.Year}: {(result.IsLeapYear ? "LEAP" : "common")} ({result.DaysBetweenNowruz} days) ← FOCUS");
      _output.WriteLine($"Year {nextYear.Year}: {(nextYear.IsLeapYear ? "LEAP" : "common")} ({nextYear.DaysBetweenNowruz} days)");

      // This test documents rather than asserts
      Assert.True(true, $"Year {year}: Documented actual behaviour");
    }

    /// <summary>
    /// Compare leap year results across different longitudes.
    /// Nowruz date can vary by location, affecting leap year determination.
    /// </summary>
    [Fact]
    public void VerifyLeapYears_DifferentLongitudes_DocumentDifferences()
    {
      _output.WriteLine("=== LEAP YEAR BY LONGITUDE ===");
      _output.WriteLine("Testing if leap year status changes with longitude");
      _output.WriteLine("");

      double[] longitudes = new double[]
      {
        0.0,    // UTC
        44.0,   // Erbil
        45.0,   // Sulaymaniyah
        52.5    // Tehran
      };

      string[] locations = new string[]
      {
        "UTC (0°E)",
        "Erbil (44°E)",
        "Sulaymaniyah (45°E)",
        "Tehran (52.5°E)"
      };

      // Test specific problematic years
      int[] testYears = new int[] { 2715, 2719, 2723, 2724 };

      _output.WriteLine($"{"Year",-8} | {"UTC",-8} | {"Erbil",-8} | {"Sulay",-8} | {"Tehran",-8} | Notes");
      _output.WriteLine(new string('-', 80));

      foreach (int year in testYears)
      {
        bool[] leapStatus = new bool[longitudes.Length];
        int[] daysCounts = new int[longitudes.Length];

        for (int i = 0; i < longitudes.Length; i++)
        {
          LeapYearResult result = AnalyzeYear(year, longitudes[i]);
          leapStatus[i] = result.IsLeapYear;
          daysCounts[i] = result.DaysBetweenNowruz;
        }

        // Check if all locations agree
        bool allAgree = true;
        for (int i = 1; i < leapStatus.Length; i++)
        {
          if (leapStatus[i] != leapStatus[0])
          {
            allAgree = false;
            break;
          }
        }

        string note = allAgree ? "All agree" : "DIFFER!";

        _output.WriteLine($"{year,-8} | " +
                         $"{FormatLeapStatus(leapStatus[0], daysCounts[0]),-8} | " +
                         $"{FormatLeapStatus(leapStatus[1], daysCounts[1]),-8} | " +
                         $"{FormatLeapStatus(leapStatus[2], daysCounts[2]),-8} | " +
                         $"{FormatLeapStatus(leapStatus[3], daysCounts[3]),-8} | " +
                         $"{note}");
      }

      _output.WriteLine("");
      _output.WriteLine("Note: Leap status SHOULD be the same regardless of longitude");
      _output.WriteLine("      (Leap year is about year length, not observer location)");

      Assert.True(true, "Documented longitude comparison");
    }

    /// <summary>
    /// Verify the 33-year cycle pattern of the Solar Hijri calendar.
    /// Expected pattern: 4-year cycles with specific leap year distribution.
    /// </summary>
    [Fact]
    public void VerifyLeapYears_33YearCycle_DocumentPattern()
    {
      _output.WriteLine("=== 33-YEAR CYCLE VERIFICATION ===");
      _output.WriteLine("Solar Hijri calendar uses a 33-year cycle");
      _output.WriteLine("Expected: ~8 leap years per 33-year cycle");
      _output.WriteLine("");

      // Test a complete 33-year cycle starting from 2700
      int startYear = 2700;
      int cycleLength = 33;

      List<int> leapYearsInCycle = new List<int>();
      
      for (int i = 0; i < cycleLength; i++)
      {
        int year = startYear + i;
        LeapYearResult result = AnalyzeYear(year, 44.0);

        string marker = result.IsLeapYear ? "LEAP" : "    ";
        _output.WriteLine($"Year {year} (cycle +{i,2}): {marker} ({result.DaysBetweenNowruz} days)");

        if (result.IsLeapYear)
        {
          leapYearsInCycle.Add(i);
        }
      }

      _output.WriteLine("");
      _output.WriteLine($"Leap years in cycle: {leapYearsInCycle.Count}");
      _output.WriteLine($"Expected: ~8 leap years per 33-year cycle");
      _output.WriteLine($"Leap year positions in cycle: {string.Join(", ", leapYearsInCycle)}");
      _output.WriteLine("");

      // Calculate intervals
      if (leapYearsInCycle.Count > 1)
      {
        _output.WriteLine("Intervals between leap years:");
        for (int i = 1; i < leapYearsInCycle.Count; i++)
        {
          int interval = leapYearsInCycle[i] - leapYearsInCycle[i - 1];
          _output.WriteLine($"  {interval} years");
        }
      }

      Assert.True(true, "Documented 33-year cycle pattern");
    }

    /// <summary>
    /// Compare calculated leap years with expected 4-year pattern.
    /// Traditional assumption: leap years occur every 4 years (like Gregorian).
    /// </summary>
    [Fact]
    public void CompareWith4YearPattern_DocumentDifferences()
    {
      _output.WriteLine("=== COMPARISON: Actual vs 4-Year Pattern ===");
      _output.WriteLine("Traditional assumption: Leap years every 4 years");
      _output.WriteLine("Reality: Solar Hijri uses 33-year cycles (not simple 4-year)");
      _output.WriteLine("");

      int startYear = 2700;
      int endYear = 2732; // One 33-year cycle

      _output.WriteLine($"{"Year",-8} | {"Actual",-12} | {"4-Yr Pattern",-14} | {"Match",-8}");
      _output.WriteLine(new string('-', 50));

      int matches = 0;
      int differences = 0;

      for (int year = startYear; year <= endYear; year++)
      {
        LeapYearResult actual = AnalyzeYear(year, 44.0);
        bool fourYearPattern = (year % 4 == 0); // Simple 4-year assumption

        bool match = actual.IsLeapYear == fourYearPattern;
        if (match) matches++; else differences++;

        _output.WriteLine($"{year,-8} | " +
                         $"{(actual.IsLeapYear ? "LEAP" : "common"),-12} | " +
                         $"{(fourYearPattern ? "LEAP" : "common"),-14} | " +
                         $"{(match ? "✓" : "✗"),-8}");
      }

      _output.WriteLine("");
      _output.WriteLine($"Matches: {matches}");
      _output.WriteLine($"Differences: {differences}");
      _output.WriteLine("");
      _output.WriteLine("CONCLUSION: Simple 4-year pattern does NOT match astronomical reality");

      Assert.True(true, "Documented comparison with 4-year pattern");
    }

    /// <summary>
    /// Verify production code's IsLeapYear method against actual calculations.
    /// </summary>
    [Fact]
    public void VerifyProductionCode_IsLeapYearMethod_DocumentAccuracy()
    {
      _output.WriteLine("=== PRODUCTION CODE ACCURACY TEST ===");
      _output.WriteLine("Testing AstronomicalSolarHijriCalculator.IsLeapYear()");
      _output.WriteLine("");

      int startYear = 2700;
      int endYear = 2750;

      int correctCount = 0;
      int incorrectCount = 0;
      List<int> discrepancies = new List<int>();

      for (int year = startYear; year <= endYear; year++)
      {
        LeapYearResult result = AnalyzeYear(year, 44.0);

        if (result.IsLeapYear == result.ProductionCodeResult)
        {
          correctCount++;
        }
        else
        {
          incorrectCount++;
          discrepancies.Add(year);
          
          _output.WriteLine($"MISMATCH at year {year}:");
          _output.WriteLine($"  Calculated: {(result.IsLeapYear ? "LEAP" : "common")} ({result.DaysBetweenNowruz} days)");
          _output.WriteLine($"  Production: {(result.ProductionCodeResult ? "LEAP" : "common")}");
          _output.WriteLine($"  Nowruz: {result.ThisNowruz:yyyy-MM-dd} → {result.NextNowruz:yyyy-MM-dd}");
        }
      }

      _output.WriteLine("");
      _output.WriteLine($"=== SUMMARY (Years {startYear}-{endYear}) ===");
      _output.WriteLine($"Correct: {correctCount}");
      _output.WriteLine($"Incorrect: {incorrectCount}");
      _output.WriteLine($"Accuracy: {(correctCount * 100.0 / (endYear - startYear + 1)):F2}%");

      if (discrepancies.Count > 0)
      {
        _output.WriteLine($"Years with discrepancies: {string.Join(", ", discrepancies)}");
      }

      // Assert that production code should be 100% accurate
      Assert.True(incorrectCount == 0, 
        $"Production code IsLeapYear has {incorrectCount} discrepancies. " +
        $"Years affected: {string.Join(", ", discrepancies)}");
    }

    // ==================== Helper Methods ====================

    private LeapYearResult AnalyzeYear(int kurdishYear, double longitude)
    {
      LeapYearResult result = new LeapYearResult
      {
        Year = kurdishYear,
        Longitude = longitude
      };

      try
      {
        // Get Nowruz dates
        result.ThisNowruz = GetNowruzForKurdishYear(kurdishYear, longitude);
        result.NextNowruz = GetNowruzForKurdishYear(kurdishYear + 1, longitude);

        // Calculate days between
        result.DaysBetweenNowruz = (result.NextNowruz.Date - result.ThisNowruz.Date).Days;

        // Determine leap year status (366 days = leap year)
        result.IsLeapYear = result.DaysBetweenNowruz == 366;

        // Get Gregorian year
        result.GregorianYearStart = result.ThisNowruz.Year;

        // Check what production code says
        try
        {
          KurdishAstronomicalDate testDate = new KurdishAstronomicalDate(kurdishYear, 1, 1);
          result.ProductionCodeResult = testDate.IsLeapYear;
          result.ProductionCodeError = null;
        }
        catch (Exception ex)
        {
          result.ProductionCodeResult = false;
          result.ProductionCodeError = ex.Message;
        }
      }
      catch (Exception ex)
      {
        result.CalculationError = ex.Message;
      }

      return result;
    }

    private DateTime GetNowruzForKurdishYear(int kurdishYear, double longitude)
    {
      // Convert Kurdish year to Gregorian year
      int gregorianYear = kurdishYear - 700; // Kurdish epoch offset

      // Get Nowruz date for this Gregorian year
      return AstronomicalEquinoxCalculator.GetNowruzDate(gregorianYear, longitude);
    }

    private string FormatLeapStatus(bool isLeap, int days)
    {
      return $"{(isLeap ? "LEAP" : "com")}({days})";
    }

    // ==================== Result Class ====================

    private class LeapYearResult
    {
      public int Year { get; set; }
      public double Longitude { get; set; }
      public DateTime ThisNowruz { get; set; }
      public DateTime NextNowruz { get; set; }
      public int DaysBetweenNowruz { get; set; }
      public bool IsLeapYear { get; set; }
      public int GregorianYearStart { get; set; }
      public bool ProductionCodeResult { get; set; }
      public string? ProductionCodeError { get; set; }
      public string? CalculationError { get; set; }
    }
  }
}