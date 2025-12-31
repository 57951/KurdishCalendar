---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Astronomical Calculations
---

# Astronomical Calculations

Guide to astronomical precision in the Kurdish Calendar library.

## Overview

The Kurdish Calendar library provides two calendar types:

1. **KurdishDate** — Simplified calculation (fixed 21 March for Nowruz)
2. **KurdishAstronomicalDate** — Astronomical precision (actual spring equinox calculation)

## When to Use Astronomical Precision

### Use Astronomical Dates For:

✅ **Cultural Ceremonies** — Nowruz celebrations where exact timing matters
✅ **Religious Observations** — When astronomical accuracy is required
✅ **Official Documents** — Legal or governmental documents requiring precision
✅ **Academic Research** — Historical or astronomical studies
✅ **Iranian Calendar Synchronisation** — Matching official Iranian Solar Hijri calendar
✅ **Multi-Location Coordination** — When Nowruz dates vary by longitude

### Use Simplified Dates For:

✅ **General Applications** — Most everyday use cases (99.9% of applications)
✅ **Performance-Critical Code** — When speed is essential
✅ **Historical Records** — When exact equinox timing is unknown
✅ **User Interfaces** — Simple date pickers and displays
✅ **Data Storage** — Predictable date representations

## Astronomical Precision Details

### Accuracy

- **Algorithm**: Jean Meeus "Astronomical Algorithms" (1998), Chapter 27
- **Precision**: ±1 minute for years 1800-2200
- **Validation**: Tested against Fred Espenak's ephemeris data (2000-2030)

### How It Works

1. Calculates precise UTC moment of spring equinox using all 24 periodic terms
2. Adjusts for geographic longitude (each 15° = 1 hour)
3. Determines local date when equinox occurs
4. Uses this date as Nowruz (1st of Xakelêwe)

**Example:**
```
2025 Spring Equinox: 2025-03-20 09:01:00 UTC
At Erbil (44.0°E):   2025-03-20 12:01:00 local → Nowruz falls on 20 March
At Tehran (52.5°E):  2025-03-20 12:31:00 local → Nowruz falls on 20 March
At London (0.0°E):   2025-03-20 09:01:00 local → Nowruz falls on 20 March
```

## Creating Astronomical Dates

### Default Constructor (Erbil)

```csharp
// Uses Erbil longitude (44.0°E) by default
KurdishAstronomicalDate date = new KurdishAstronomicalDate(2725, 1, 1);

// From Gregorian DateTime
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishAstronomicalDate dateFromGregorian = new KurdishAstronomicalDate(gregorian);
```

### Named Location Factories

```csharp
// Erbil, capital of Kurdistan Region (44.0°E)
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

// Sulaymaniyah (45.0°E)
var sulaymaniyah = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);

// Tehran meridian - matches Iranian official calendar (52.5°E)
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);

// UTC (0.0°E)
var utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);
```

### Custom Longitude

```csharp
// Baghdad (44.3661°E)
var baghdad = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);

// Kirkuk (44.3922°E)
var kirkuk = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3922);

// Duhok (42.9875°E)
var duhok = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 42.9875);

// London (-0.1276°E) - for diaspora
var london = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, -0.1276);
```

### From Gregorian with Longitude

```csharp
DateTime gregorian = new DateTime(2025, 3, 20);

// Default Erbil
var date1 = KurdishAstronomicalDate.FromDateTime(gregorian);

// Custom longitude
var date2 = KurdishAstronomicalDate.FromDateTime(gregorian, 52.5); // Tehran
```

## Longitude Impact on Dates

### Why Longitude Matters

The spring equinox occurs at a specific UTC moment, but the calendar date depends on local time zone:

```csharp
// Same Kurdish date, different longitudes
var newroz2726Erbil = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
var newroz2726Tehran = KurdishAstronomicalDate.FromTehran(2726, 1, 1);

Console.WriteLine($"Erbil:  {newroz2726Erbil.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");
Console.WriteLine($"Tehran: {newroz2726Tehran.ToDateTime():yyyy-MM-dd HH:mm:ss} UTC");

// Might output:
// Erbil:  2026-03-20 12:46:00 UTC
// Tehran: 2026-03-20 12:46:00 UTC (same equinox moment)

// But the Gregorian date at each location might differ
var gregorianErbil = newroz2726Erbil.ToDateTime().Date;
var gregorianTehran = newroz2726Tehran.ToDateTime().Date;
```

### Longitude Values for Major Cities

| Location | Longitude | Constant |
|----------|-----------|----------|
| Erbil | 44.0°E | `DefaultLongitude` |
| Sulaymaniyah | 45.0°E | `SulaymaniyahLongitude` |
| Tehran | 52.5°E | `TehranLongitude` |
| Baghdad | 44.3661°E | (custom) |
| Kirkuk | 44.3922°E | (custom) |
| Duhok | 42.9875°E | (custom) |

## Leap Year Determination

### Simplified Method (KurdishDate)

Uses a 33-year cycle with leap years at specific positions:
- Years 1, 5, 9, 13, 17, 22, 26, 30 in each 33-year cycle

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);
bool isLeap = date.IsLeapYear;  // Uses 33-year cycle
```

### Astronomical Method (KurdishAstronomicalDate)

Calculates actual days between consecutive Nowruz dates:
- If 366 days between Nowruz dates → leap year
- If 365 days between Nowruz dates → common year

```csharp
KurdishAstronomicalDate date = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
bool isLeap = date.IsLeapYear;  // Astronomical calculation

// How it works internally:
// 1. Calculate Nowruz for year 2725
// 2. Calculate Nowruz for year 2726
// 3. Count days between them
// 4. If 366 days → leap year, if 365 days → common year
```

### Comparison

```csharp
int year = 2725;

// Simplified
KurdishDate simplified = new KurdishDate(year, 1, 1);
bool simplifiedLeap = simplified.IsLeapYear;

// Astronomical (Erbil)
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(year, 1, 1);
bool astronomicalLeap = astro.IsLeapYear;

Console.WriteLine($"Simplified:    {simplifiedLeap}");
Console.WriteLine($"Astronomical:  {astronomicalLeap}");

// Usually match, but can differ in edge cases
```

## Finding Exact Equinox Moments

### Get Equinox DateTime

```csharp
// The astronomical date represents Nowruz (1st Xakelêwe)
KurdishAstronomicalDate newroz2726 = KurdishAstronomicalDate.FromErbil(2726, 1, 1);

// Get the exact UTC moment of the spring equinox
DateTime equinoxMoment = newroz2726.ToDateTime();

Console.WriteLine($"Newroz 2726 (spring equinox) occurs at:");
Console.WriteLine($"UTC:   {equinoxMoment:yyyy-MM-dd HH:mm:ss}");
Console.WriteLine($"Local: {equinoxMoment.AddHours(44.0/15.0):yyyy-MM-dd HH:mm:ss}");

// Example output:
// UTC:   2026-03-20 12:46:23
// Local: 2026-03-20 15:42:23 (at Erbil longitude)
```

### Equinox for Multiple Years

```csharp
for (int year = 2720; year <= 2730; year++)
{
  KurdishAstronomicalDate newroz = KurdishAstronomicalDate.FromErbil(year, 1, 1);
  DateTime equinox = newroz.ToDateTime();
  
  Console.WriteLine($"Year {year}: {equinox:yyyy-MM-dd HH:mm:ss} UTC");
}
```

## Converting Between Types

### Simplified to Astronomical

```csharp
KurdishDate simplified = new KurdishDate(2725, 1, 1);

// Lossless conversion (keeps same Y/M/D values)
KurdishAstronomicalDate astro1 = simplified.ToAstronomical();
Console.WriteLine($"Lossless: {astro1.Year}/{astro1.Month}/{astro1.Day}");
// Output: "Lossless: 2725/1/1"

// Recalculated conversion (via Gregorian, may differ by 1-2 days)
KurdishAstronomicalDate astro2 = simplified.ToAstronomicalRecalculated();
Console.WriteLine($"Recalculated: {astro2.Year}/{astro2.Month}/{astro2.Day}");
// Output: May differ if equinox dates differ

// With custom longitude
KurdishAstronomicalDate astro3 = simplified.ToAstronomical(52.5); // Tehran
```

### Astronomical to Simplified

```csharp
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

// Lossless conversion (keeps same Y/M/D values)
KurdishDate simplified1 = astro.ToStandardDate();
Console.WriteLine($"Lossless: {simplified1.Year}/{simplified1.Month}/{simplified1.Day}");
// Output: "Lossless: 2725/1/1"

// Recalculated conversion (via Gregorian)
KurdishDate simplified2 = astro.ToStandardDateRecalculated();
Console.WriteLine($"Recalculated: {simplified2.Year}/{simplified2.Month}/{simplified2.Day}");
```

### Why Recalculated Conversions May Differ

```csharp
// Simplified uses fixed 21 March
KurdishDate simplified = new KurdishDate(2725, 1, 1);
DateTime gregorianSimplified = simplified.ToDateTime();
Console.WriteLine($"Simplified Nowruz 2725: {gregorianSimplified:yyyy-MM-dd}");
// Output: "Simplified Nowruz 2725: 2025-03-21"

// Astronomical calculates actual equinox (might be 20 or 22 March)
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
DateTime gregorianAstro = astro.ToDateTime();
Console.WriteLine($"Astronomical Nowruz 2725: {gregorianAstro:yyyy-MM-dd}");
// Output: "Astronomical Nowruz 2725: 2025-03-20" (actual equinox)

// Difference: 1 day
```

## Performance Considerations

### Caching

Astronomical calculations are cached automatically:

```csharp
// First call calculates and caches
var date1 = KurdishAstronomicalDate.FromErbil(2725, 1, 1);  // ~50μs

// Subsequent calls use cache
var date2 = KurdishAstronomicalDate.FromErbil(2725, 1, 1);  // ~0.1μs
var date3 = KurdishAstronomicalDate.FromErbil(2725, 6, 15); // ~0.1μs (same year)
```

### Clearing Cache

For testing or long-running applications:

```csharp
// Clear entire cache
KurdishAstronomicalDate.ClearEquinoxCache();

// Clear specific year
KurdishAstronomicalDate.ClearEquinoxCache(2725);
```

### Performance Comparison

| Operation | Simplified | Astronomical (uncached) | Astronomical (cached) |
|-----------|-----------|------------------------|----------------------|
| Date creation | ~5μs | ~50μs | ~0.1μs |
| Conversion to Gregorian | ~0.1μs | ~0.1μs | ~0.1μs |
| Leap year check | ~0.1μs | ~50μs (first time) | ~0.1μs |

## Validation and Accuracy

### Validation Method

Astronomical calculations validated against Fred Espenak's ephemeris data:

**Source:** www.Astropixels.com/ephemeris/soleq2001.html

**Validation Range:** Years 2000-2030

**Accuracy:** ±1 minute for years 1800-2200

### Example Validation

```csharp
// Known equinox from Fred Espenak's tables
// 2025 March Equinox: 2025-03-20 09:01:00 UTC

KurdishAstronomicalDate newroz2725 = KurdishAstronomicalDate.FromUtc(2725, 1, 1);
DateTime calculated = newroz2725.ToDateTime();

Console.WriteLine($"Calculated: {calculated:yyyy-MM-dd HH:mm:ss}");
// Output: "Calculated: 2025-03-20 09:01:00" (within ±1 minute)
```

### Limitations

1. **Historical dates before 1800 CE**: Calculated but not validated
2. **Future dates after 2200 CE**: Accuracy degrades slightly
3. **Longitude precision**: Use at least 4 decimal places for city-level accuracy
4. **Time zones**: Library returns UTC; convert to local time as needed

## Practical Examples

### Cultural Event Planner

```csharp
public class NewrozPlanner
{
  public void PlanNewrozCelebration(int year, double longitude)
  {
    var newroz = KurdishAstronomicalDate.FromLongitude(year, 1, 1, longitude);
    DateTime equinoxUtc = newroz.ToDateTime();
    
    // Convert to local time
    double hoursOffset = longitude / 15.0;
    DateTime localEquinox = equinoxUtc.AddHours(hoursOffset);
    
    Console.WriteLine($"Newroz {year} Astronomical Details:");
    Console.WriteLine($"Spring Equinox (UTC): {equinoxUtc:yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine($"Local Time: {localEquinox:yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine($"Gregorian Date: {localEquinox:dddd, dd MMMM yyyy}");
    Console.WriteLine($"Kurdish Date: {newroz.ToString("D", KurdishDialect.SoraniLatin)}");
  }
}

// Usage
var planner = new NewrozPlanner();
planner.PlanNewrozCelebration(2726, 44.0); // Erbil
```

### Multi-Location Coordination

```csharp
public void CompareNewrozDates(int year)
{
  var locations = new Dictionary<string, double>
  {
    { "Erbil", 44.0 },
    { "Sulaymaniyah", 45.0 },
    { "Tehran", 52.5 },
    { "London", -0.1276 },
    { "New York", -74.0 }
  };

  Console.WriteLine($"Newroz {year} across locations:");
  
  foreach (var location in locations)
  {
    var newroz = KurdishAstronomicalDate.FromLongitude(year, 1, 1, location.Value);
    DateTime gregorian = newroz.ToDateTime();
    
    Console.WriteLine($"{location.Key,-15}: {gregorian:yyyy-MM-dd HH:mm:ss} UTC");
  }
}
```

### Leap Year Analysis

```csharp
public void AnalyseLeapYears(int startYear, int endYear)
{
  Console.WriteLine("Year | Simplified | Astronomical | Match?");
  Console.WriteLine("-----|------------|--------------|-------");
  
  for (int year = startYear; year <= endYear; year++)
  {
    var simplified = new KurdishDate(year, 1, 1);
    var astronomical = KurdishAstronomicalDate.FromErbil(year, 1, 1);
    
    bool simpLeap = simplified.IsLeapYear;
    bool astroLeap = astronomical.IsLeapYear;
    bool match = simpLeap == astroLeap;
    
    Console.WriteLine($"{year} | {simpLeap,-10} | {astroLeap,-12} | {(match ? "✓" : "✗")}");
  }
}

// Usage
AnalyseLeapYears(2720, 2730);
```

## Best Practices

### 1. Use Astronomical Only When Needed

```csharp
// Good - simplified for general use
KurdishDate birthdayReminder = new KurdishDate(2700, 5, 15);

// Good - astronomical for ceremony
KurdishAstronomicalDate newrozCeremony = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
```

### 2. Specify Longitude Explicitly

```csharp
// Good - explicit longitude
var date = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);

// Acceptable - named location
var date2 = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

// Avoid - relies on default
var date3 = new KurdishAstronomicalDate(2725, 1, 1);
```

### 3. Document Which Type You're Using

```csharp
/// <summary>
/// Stores user's birthdate using simplified calculation.
/// Astronomical precision not required for birthday tracking.
/// </summary>
public KurdishDate BirthDate { get; set; }

/// <summary>
/// Stores official Nowruz date using astronomical calculation.
/// Required for ceremony scheduling and official documents.
/// </summary>
public KurdishAstronomicalDate OfficialNewroz { get; set; }
```

## See Also

- [API Reference](api-reference.md) — Complete API documentation
- [Examples](examples.md) — Practical code examples
- [Getting Started](getting-started.md) — Installation and basics
- [Testing](testing.md) — Validation and accuracy details

---

**بەختێکی باش! (Good luck!)**