using System;
using Xunit;
using Xunit.Abstractions;

namespace KurdishCalendar.Core.Tests.Diagnostics
{
  /// <summary>
  /// Diagnostic tests to verify Arabic numeral handling and date arithmetic.
  /// </summary>
  public class DiagnosticTests
  {
    private readonly ITestOutputHelper _output;

    public DiagnosticTests(ITestOutputHelper output)
    {
      _output = output;
    }

    [Fact]
    public void Diagnostic_ArabicNumerals_ShowUnicodeValues()
    {
      string testInput = "١٥/٠١/٢٧٢٤";
      
      _output.WriteLine($"Test input: {testInput}");
      _output.WriteLine($"Length: {testInput.Length}");
      
      for (int i = 0; i < testInput.Length; i++)
      {
        char c = testInput[i];
        _output.WriteLine($"  [{i}] '{c}' = U+{((int)c):X4} (dec {(int)c})");
      }

      // Test if parser can handle it
      bool result = KurdishDateParser.TryParse(testInput, KurdishDialect.SoraniArabic, out KurdishDate date);
      
      _output.WriteLine($"\nParsing result: {result}");
      if (result)
      {
        _output.WriteLine($"Parsed date: {date.Year}/{date.Month}/{date.Day}");
      }
      else
      {
        _output.WriteLine("Parsing FAILED");
        
        // Try to manually convert and see what happens
        string converted = testInput
          .Replace('٠', '0')
          .Replace('١', '1')
          .Replace('٢', '2')
          .Replace('٣', '3')
          .Replace('٤', '4')
          .Replace('٥', '5')
          .Replace('٦', '6')
          .Replace('٧', '7')
          .Replace('٨', '8')
          .Replace('٩', '9');
        
        _output.WriteLine($"After conversion: '{converted}'");
      }
    }

    [Fact]
    public void Diagnostic_Year2723_LeapYearStatus()
    {
      // Check if year 2723 is a leap year
      var date2723 = new KurdishAstronomicalDate(2723, 1, 1);
      var date2724 = new KurdishAstronomicalDate(2724, 1, 1);

      _output.WriteLine($"Year 2723 is leap: {date2723.IsLeapYear}");
      _output.WriteLine($"Year 2724 is leap: {date2724.IsLeapYear}");

      // Check Nowruz dates
      DateTime nowruz2723 = date2723.ToDateTime();
      DateTime nowruz2724 = date2724.ToDateTime();

      _output.WriteLine($"\nNowruz 2723: {nowruz2723:yyyy-MM-dd}");
      _output.WriteLine($"Nowruz 2724: {nowruz2724:yyyy-MM-dd}");

      int daysIn2723 = (nowruz2724 - nowruz2723).Days;
      _output.WriteLine($"Days in year 2723: {daysIn2723}");
      
      // Check month 12 day count
      try
      {
        var lastDay29 = new KurdishAstronomicalDate(2723, 12, 29);
        _output.WriteLine($"\n2723/12/29 is valid: Yes → {lastDay29.ToDateTime():yyyy-MM-dd}");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"\n2723/12/29 is valid: No - {ex.Message}");
      }

      try
      {
        var lastDay30 = new KurdishAstronomicalDate(2723, 12, 30);
        _output.WriteLine($"2723/12/30 is valid: Yes → {lastDay30.ToDateTime():yyyy-MM-dd}");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"2723/12/30 is valid: No - {ex.Message}");
      }
    }

    [Fact]
    public void Diagnostic_AddDays_ShowCalculation()
    {
      // Test case 1: Subtract 10 days from 2724/1/15
      var start1 = new KurdishAstronomicalDate(2724, 1, 15);
      var result1 = start1.AddDays(-10);

      _output.WriteLine("Test 1: 2724/1/15 - 10 days");
      _output.WriteLine($"  Result: {result1.Year}/{result1.Month}/{result1.Day}");
      _output.WriteLine($"  Expected: 2723/12/24");
      _output.WriteLine($"  Match: {result1.Year == 2723 && result1.Month == 12 && result1.Day == 24}");

      // Check if 2723 has 30 days in month 12
      var date2723 = new KurdishAstronomicalDate(2723, 1, 1);
      _output.WriteLine($"  Year 2723 is leap: {date2723.IsLeapYear}");
      
      // Test case 2: Add 10 days to 2724/12/25
      var start2 = new KurdishAstronomicalDate(2724, 12, 25);
      var result2 = start2.AddDays(10);

      _output.WriteLine("\nTest 2: 2724/12/25 + 10 days");
      _output.WriteLine($"  Result: {result2.Year}/{result2.Month}/{result2.Day}");
      _output.WriteLine($"  Expected: 2725/1/5");
      _output.WriteLine($"  Match: {result2.Year == 2725 && result2.Month == 1 && result2.Day == 5}");
      
      // Check if 2724 has 29 or 30 days in month 12
      var date2724 = new KurdishAstronomicalDate(2724, 1, 1);
      _output.WriteLine($"  Year 2724 is leap: {date2724.IsLeapYear}");
    }
  }
}