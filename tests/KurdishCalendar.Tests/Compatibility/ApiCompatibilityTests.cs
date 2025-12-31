using System;
using KurdishCalendar.Core;

/// <summary>
/// Tests to verify that the longitude fix does NOT break existing functionality.
/// Users should still be able to get dates for different locations.
/// </summary>
class ApiCompatibilityTests
{
  public static void ApiCompatibilityTest()
  {
    Console.WriteLine("=== API Compatibility Test ===\n");
    Console.WriteLine("Verifying that users can still get equinox dates for different locations.\n");
    
    bool allPassed = true;
    
    allPassed &= TestPublicAPIsStillWork();
    allPassed &= TestDifferentLocationsProduceDifferentDates();
    allPassed &= TestLongitudeIsPreserved();
    allPassed &= TestConversionsBetweenKurdishAndGregorian();
    
    Console.WriteLine(allPassed 
      ? "\n✅ ALL COMPATIBILITY TESTS PASSED - API is intact!" 
      : "\n❌ SOME TESTS FAILED - API may be broken");
  }
  
  static bool TestPublicAPIsStillWork()
  {
    Console.WriteLine("Test 1: Public APIs still work as expected");
    
    try
    {
      // Test all the named location constructors
      var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
      Console.WriteLine($"  ✓ FromErbil(2725, 1, 1) = {erbil.ToDateTime():yyyy-MM-dd}");
      
      var sulay = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
      Console.WriteLine($"  ✓ FromSulaymaniyah(2725, 1, 1) = {sulay.ToDateTime():yyyy-MM-dd}");
      
      var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
      Console.WriteLine($"  ✓ FromTehran(2725, 1, 1) = {tehran.ToDateTime():yyyy-MM-dd}");
      
      var utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);
      Console.WriteLine($"  ✓ FromUtc(2725, 1, 1) = {utc.ToDateTime():yyyy-MM-dd}");
      
      // Test custom longitude
      var custom = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 77.0); // Delhi
      Console.WriteLine($"  ✓ FromLongitude(2725, 1, 1, 77.0) = {custom.ToDateTime():yyyy-MM-dd}");
      
      // Test DateTime conversion
      var fromDt = KurdishAstronomicalDate.FromDateTime(new DateTime(2025, 3, 20));
      Console.WriteLine($"  ✓ FromDateTime(2025-03-20) = {fromDt.Year}/{fromDt.Month}/{fromDt.Day}");
      
      Console.WriteLine("  ✅ PASS\n");
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"  ❌ FAIL: {ex.Message}\n");
      return false;
    }
  }
  
  static bool TestDifferentLocationsProduceDifferentDates()
  {
    Console.WriteLine("Test 2: Different longitudes can produce different Nowruz dates");
    
    // For years where equinox is near midnight UTC,
    // western longitudes should see different dates than eastern
    
    int year = 2724; // Kurdish year
    
    // Extreme west: -120° (west coast America)
    var west = KurdishAstronomicalDate.FromLongitude(year, 1, 1, -120.0);
    DateTime westDate = west.ToDateTime();
    
    // Extreme east: +120° (eastern Asia)
    var east = KurdishAstronomicalDate.FromLongitude(year, 1, 1, 120.0);
    DateTime eastDate = east.ToDateTime();
    
    Console.WriteLine($"  West (-120°): {westDate:yyyy-MM-dd} (Longitude: {west.Longitude}°)");
    Console.WriteLine($"  East (+120°): {eastDate:yyyy-MM-dd} (Longitude: {east.Longitude}°)");
    
    // They might differ by 1 day depending on equinox timing
    int daysDiff = Math.Abs((eastDate - westDate).Days);
    bool passed = daysDiff <= 1; // Should be 0 or 1
    
    Console.WriteLine($"  Days difference: {daysDiff}");
    Console.WriteLine(passed ? "  ✅ PASS\n" : "  ❌ FAIL\n");
    return passed;
  }
  
  static bool TestLongitudeIsPreserved()
  {
    Console.WriteLine("Test 3: Longitude is preserved in KurdishAstronomicalDate instances");
    
    double[] testLongitudes = { 0.0, 44.0, 52.5, -118.0, 77.0, 139.7 };
    bool allCorrect = true;
    
    foreach (double lon in testLongitudes)
    {
      var date = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, lon);
      bool matches = Math.Abs(date.Longitude - lon) < 0.001;
      
      Console.WriteLine($"  {lon,7:F1}° -> date.Longitude = {date.Longitude,7:F1}° {(matches ? "✓" : "✗")}");
      allCorrect &= matches;
    }
    
    Console.WriteLine(allCorrect ? "  ✅ PASS\n" : "  ❌ FAIL\n");
    return allCorrect;
  }
  
  static bool TestConversionsBetweenKurdishAndGregorian()
  {
    Console.WriteLine("Test 4: Round-trip conversions work correctly");
    
    bool allPassed = true;
    
    // Test 1: Kurdish -> Gregorian -> Kurdish
    var originalKurdish = KurdishAstronomicalDate.FromErbil(2725, 6, 15);
    DateTime gregorian = originalKurdish.ToDateTime();
    var backToKurdish = KurdishAstronomicalDate.FromDateTime(gregorian, 44.0);
    
    bool test1 = originalKurdish.Year == backToKurdish.Year &&
                 originalKurdish.Month == backToKurdish.Month &&
                 originalKurdish.Day == backToKurdish.Day;
    
    Console.WriteLine($"  Kurdish 2725/6/15 -> Gregorian {gregorian:yyyy-MM-dd} -> Kurdish {backToKurdish.Year}/{backToKurdish.Month}/{backToKurdish.Day}");
    Console.WriteLine($"  Round-trip Kurdish: {(test1 ? "✓" : "✗")}");
    allPassed &= test1;
    
    // Test 2: Gregorian -> Kurdish -> Gregorian
    var originalGregorian = new DateTime(2025, 8, 15);
    var kurdish = KurdishAstronomicalDate.FromDateTime(originalGregorian, 44.0);
    var backToGregorian = kurdish.ToDateTime();
    
    bool test2 = originalGregorian.Date == backToGregorian.Date;
    
    Console.WriteLine($"  Gregorian {originalGregorian:yyyy-MM-dd} -> Kurdish {kurdish.Year}/{kurdish.Month}/{kurdish.Day} -> Gregorian {backToGregorian:yyyy-MM-dd}");
    Console.WriteLine($"  Round-trip Gregorian: {(test2 ? "✓" : "✗")}");
    allPassed &= test2;
    
    Console.WriteLine(allPassed ? "  ✅ PASS\n" : "  ❌ FAIL\n");
    return allPassed;
  }
}