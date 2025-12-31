---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Kurdish Calendar Examples
---

# Kurdish Calendar Examples

This document provides practical, working examples for common use cases.

## Table of Contents

1. [Basic Date Operations](#basic-date-operations)
2. [Date Arithmetic](#date-arithmetic)
3. [Formatting Examples](#formatting-examples)
4. [Parsing Examples](#parsing-examples)
5. [Astronomical Dates](#astronomical-dates)
6. [Gregorian Formatting](#gregorian-formatting)
7. [Practical Applications](#practical-applications)

## Basic Date Operations

### Creating Dates

```csharp
using System;
using KurdishCalendar.Core;

// Create from year, month, day
KurdishDate date1 = new KurdishDate(2725, 1, 1);  // Newroz 2725

// From Gregorian DateTime
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishDate date2 = KurdishDate.FromDateTime(gregorian);

// Today's date
KurdishDate today = KurdishDate.Today;

// Current date and time (date only)
KurdishDate now = KurdishDate.Now;
```

### Converting Dates

```csharp
KurdishDate kurdish = new KurdishDate(2725, 6, 15);

// To Gregorian
DateTime gregorian = kurdish.ToDateTime();
Console.WriteLine(gregorian.ToString("yyyy-MM-dd"));
// Output: "2025-09-06"

// From Gregorian back to Kurdish
KurdishDate backToKurdish = KurdishDate.FromDateTime(gregorian);

// Explicit conversion
DateTime explicitGregorian = (DateTime)kurdish;

// Implicit conversion
KurdishDate implicitKurdish = gregorian;  // Error: must be explicit
```

### Accessing Properties

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

Console.WriteLine($"Year: {date.Year}");               // 2725
Console.WriteLine($"Month: {date.Month}");             // 6
Console.WriteLine($"Day: {date.Day}");                 // 15
Console.WriteLine($"Day of Week: {date.DayOfWeek}");   // e.g., Friday
Console.WriteLine($"Day of Year: {date.DayOfYear}");   // 166
Console.WriteLine($"Is Leap Year: {date.IsLeapYear}"); // false
```

## Date Arithmetic

### Adding Time Periods

```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);

// Add days
KurdishDate tomorrow = date.AddDays(1);
KurdishDate nextWeek = date.AddDays(7);
KurdishDate yesterday = date.AddDays(-1);

Console.WriteLine($"Tomorrow: {tomorrow.ToString("d", KurdishDialect.SoraniLatin)}");
// Output: "16/01/2725"

// Add months
KurdishDate nextMonth = date.AddMonths(1);
KurdishDate lastMonth = date.AddMonths(-1);
KurdishDate sixMonthsLater = date.AddMonths(6);

// Add years
KurdishDate nextYear = date.AddYears(1);
KurdishDate tenYearsAgo = date.AddYears(-10);
```

### Calculating Differences

```csharp
KurdishDate start = new KurdishDate(2725, 1, 1);
KurdishDate end = new KurdishDate(2725, 12, 29);

// Days between dates
int daysDiff = end.DaysDifference(start);
Console.WriteLine($"Days between: {daysDiff}");
// Output: "Days between: 364"

// Using DateTime conversion
TimeSpan span = end.ToDateTime() - start.ToDateTime();
Console.WriteLine($"Total days: {span.TotalDays}");

// Calculate age in years
KurdishDate birthdate = new KurdishDate(2700, 5, 10);
KurdishDate today = KurdishDate.Today;
int age = today.Year - birthdate.Year;
if (today.Month < birthdate.Month || 
    (today.Month == birthdate.Month && today.Day < birthdate.Day))
{
  age--;
}
Console.WriteLine($"Age: {age} years");
```

### Month and Year Boundaries

```csharp
KurdishDate date = new KurdishDate(2725, 1, 31);

// Adding a month from end of 31-day month
KurdishDate nextMonth = date.AddMonths(1);
Console.WriteLine(nextMonth.ToString("d", KurdishDialect.SoraniLatin));
// Output: "31/02/2725" (Gulan also has 31 days)

// Adding to 30-day month
KurdishDate date2 = new KurdishDate(2725, 7, 30);  // Rezber
KurdishDate nextMonth2 = date2.AddMonths(1);
Console.WriteLine(nextMonth2.ToString("d", KurdishDialect.SoraniLatin));
// Output: "30/08/2725" (Gelarêzan also has 30 days)

// Year transitions
KurdishDate endOfYear = new KurdishDate(2724, 12, 29);
KurdishDate nextDay = endOfYear.AddDays(1);
Console.WriteLine(nextDay.ToString("d", KurdishDialect.SoraniLatin));
// Output: "01/01/2725" (New year - Newroz)
```

## Formatting Examples

### Standard Formats

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Short format (dd/MM/yyyy)
string shortDate = date.ToString("d", KurdishDialect.SoraniLatin);
Console.WriteLine(shortDate);
// Output: "15/06/2725"

// Long format (day monthName year)
string longDate = date.ToString("D", KurdishDialect.SoraniLatin);
Console.WriteLine(longDate);
// Output: "15 Xermanan 2725"

// Month and day
string monthDay = date.ToString("M", KurdishDialect.SoraniLatin);
Console.WriteLine(monthDay);
// Output: "15 Xermanan"

// Year and month
string yearMonth = date.ToString("Y", KurdishDialect.SoraniLatin);
Console.WriteLine(yearMonth);
// Output: "Xermanan 2725"
```

### Custom Format Strings

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Format: dd MMMM yyyy
string format1 = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin);
Console.WriteLine(format1);
// Output: "15 Xermanan 2725"

// Format: dddd, d MMMM yyyy
string format2 = date.ToString("dddd, d MMMM yyyy", KurdishDialect.SoraniLatin);
Console.WriteLine(format2);
// Output: "Hênî, 15 Xermanan 2725"

// Format: MMM d, yy
string format3 = date.ToString("MMM d, yy", KurdishDialect.SoraniLatin);
Console.WriteLine(format3);
// Output: "Xer 15, 25"

// Format: d/M/yyyy
string format4 = date.ToString("d/M/yyyy", KurdishDialect.SoraniLatin);
Console.WriteLine(format4);
// Output: "15/6/2725"
```

### Multiple Dialects

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Sorani Latin
string soraniLatin = date.ToString("D", KurdishDialect.SoraniLatin);
Console.WriteLine($"Sorani Latin: {soraniLatin}");
// Output: "1 Xakelêwe 2725"

// Sorani Arabic
string soraniArabic = date.ToString("D", KurdishDialect.SoraniArabic);
Console.WriteLine($"Sorani Arabic: {soraniArabic}");
// Output: "١ خاکەلێوە ٢٧٢٥"

// Kurmanji Latin
string kurmanjiLatin = date.ToString("D", KurdishDialect.KurmanjiLatin);
Console.WriteLine($"Kurmanji Latin: {kurmanjiLatin}");
// Output: "1 Xakelêwe 2725"

// Kurmanji Arabic
string kurmanjiArabic = date.ToString("D", KurdishDialect.KurmanjiArabic);
Console.WriteLine($"Kurmanji Arabic: {kurmanjiArabic}");
// Output: "١ خاکەلێوە ٢٧٢٥"

// Hawrami Latin
string hawramiLatin = date.ToString("D", KurdishDialect.HawramiLatin);
Console.WriteLine($"Hawrami Latin: {hawramiLatin}");
// Output: "1 Newroz 2725"

// Hawrami Arabic
string hawramiArabic = date.ToString("D", KurdishDialect.HawramiArabic);
Console.WriteLine($"Hawrami Arabic: {hawramiArabic}");
// Output: "١ نەوروز ٢٧٢٥"
```

### Right-to-Left Formatting

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Arabic script defaults to RTL in long format
string arabicLong = date.ToString("D", KurdishDialect.SoraniArabic);
Console.WriteLine(arabicLong);
// Output: "٢٧٢٥ خەرمانان ١٥" (year month day)

// Numeric format is always LTR
string arabicShort = date.ToString("d", KurdishDialect.SoraniArabic);
Console.WriteLine(arabicShort);
// Output: "١٥/٠٦/٢٧٢٥" (day/month/year)

// Custom format with explicit direction
string custom = date.ToString(
  "dd MMMM yyyy", 
  KurdishDialect.SoraniArabic,
  KurdishTextDirection.RightToLeft
);
Console.WriteLine(custom);
// Output: "٢٧٢٥ خەرمانان ١٥"
```

## Parsing Examples

### Basic Parsing

```csharp
// Parse long format
KurdishDate date1 = KurdishDate.Parse(
  "15 Xakelêwe 2725", 
  KurdishDialect.SoraniLatin
);

// Parse short format
KurdishDate date2 = KurdishDate.Parse(
  "15/01/2725", 
  KurdishDialect.SoraniLatin
);

// Parse with different separators
KurdishDate date3 = KurdishDate.Parse(
  "15-01-2725", 
  KurdishDialect.SoraniLatin
);

KurdishDate date4 = KurdishDate.Parse(
  "15 01 2725", 
  KurdishDialect.SoraniLatin
);
```

### Safe Parsing

```csharp
string userInput = "15 Xakelêwe 2725";

if (KurdishDate.TryParse(userInput, KurdishDialect.SoraniLatin, out KurdishDate date))
{
  Console.WriteLine($"Successfully parsed: {date}");
}
else
{
  Console.WriteLine("Invalid date format");
}

// Try multiple dialects
bool ParseAnyDialect(string input, out KurdishDate result)
{
  KurdishDialect[] dialects = new[]
  {
    KurdishDialect.SoraniLatin,
    KurdishDialect.SoraniArabic,
    KurdishDialect.KurmanjiLatin,
    KurdishDialect.KurmanjiArabic,
    KurdishDialect.HawramiLatin,
    KurdishDialect.HawramiArabic
  };

  foreach (var dialect in dialects)
  {
    if (KurdishDate.TryParse(input, dialect, out result))
    {
      return true;
    }
  }

  result = default;
  return false;
}
```

### Parsing Arabic Script

```csharp
// Arabic numerals and month names
KurdishDate date1 = KurdishDate.Parse(
  "١٥ خاکەلێوە ٢٧٢٥",
  KurdishDialect.SoraniArabic
);

// Numeric format (RTL: year/month/day)
KurdishDate date2 = KurdishDate.Parse(
  "٢٧٢٥/٠١/١٥",
  KurdishDialect.SoraniArabic
);

// LTR numeric format also works
KurdishDate date3 = KurdishDate.Parse(
  "١٥/٠١/٢٧٢٥",
  KurdishDialect.SoraniArabic
);
```

### Round-Trip Parsing

```csharp
KurdishDate original = new KurdishDate(2725, 6, 15);

// Format to string
string formatted = original.ToString("D", KurdishDialect.SoraniLatin);
Console.WriteLine($"Formatted: {formatted}");
// Output: "15 Xermanan 2725"

// Parse back
KurdishDate parsed = KurdishDate.Parse(formatted, KurdishDialect.SoraniLatin);

// Verify round-trip
bool isEqual = original == parsed;
Console.WriteLine($"Round-trip successful: {isEqual}");
// Output: "Round-trip successful: True"
```

## Astronomical Dates

### Creating Astronomical Dates

```csharp
// Default uses Erbil (44.0°E)
KurdishAstronomicalDate astro1 = new KurdishAstronomicalDate(2725, 1, 1);

// Named locations
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var sulaymaniyah = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
var utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);

// Custom longitude
var baghdad = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);
var london = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, -0.1276);
```

### Finding Exact Nowruz Moment

```csharp
// Get exact equinox moment for Newroz 2726
KurdishAstronomicalDate newroz = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
DateTime equinoxMoment = newroz.ToDateTime();

Console.WriteLine($"Newroz 2726 begins at: {equinoxMoment:yyyy-MM-dd HH:mm:ss} UTC");
// Output: "Newroz 2726 begins at: 2026-03-20 12:46:00 UTC" (example)
```

### Comparing Locations

```csharp
// Newroz may fall on different Gregorian dates in different locations
var nowrozErbil = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
var nowrozTehran = KurdishAstronomicalDate.FromTehran(2726, 1, 1);
var nowrozNewYork = KurdishAstronomicalDate.FromLongitude(2726, 1, 1, -74.0);

Console.WriteLine($"Erbil: {nowrozErbil.ToDateTime():yyyy-MM-dd}");
Console.WriteLine($"Tehran: {nowrozTehran.ToDateTime():yyyy-MM-dd}");
Console.WriteLine($"New York: {nowrozNewYork.ToDateTime():yyyy-MM-dd}");

// They might differ by a day depending on exact equinox timing
```

### Converting Between Standard and Astronomical

```csharp
KurdishDate simplified = new KurdishDate(2725, 1, 1);

// Lossless conversion (keeps same Y/M/D values)
KurdishAstronomicalDate astro1 = simplified.ToAstronomical();
Console.WriteLine($"Lossless: {astro1.Year}/{astro1.Month}/{astro1.Day}");
// Output: "Lossless: 2725/1/1"

// Informational conversion (recalculates via Gregorian)
KurdishAstronomicalDate astro2 = simplified.ToAstronomicalRecalculated();
Console.WriteLine($"Recalculated: {astro2.Year}/{astro2.Month}/{astro2.Day}");
// Output: May differ by 1-2 days if equinox dates differ

// Reverse conversions
KurdishDate back1 = astro1.ToStandardDate(); // Lossless
KurdishDate back2 = astro1.ToStandardDateRecalculated(); // Informational
```

## Gregorian Formatting

### Basic Gregorian Formatting

```csharp
DateTime gregorian = new DateTime(2025, 12, 2);

// Kurmanji
string kurmanji = gregorian.ToKurmanjiGregorian();
Console.WriteLine(kurmanji);
// Output: "2 Kanûna Êkê 2025"

// Sorani
string sorani = gregorian.ToSoraniGregorian();
Console.WriteLine(sorani);
// Output: "2 Kanûnî Yekem 2025"

// Arabic script
string arabicKurmanji = gregorian.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
Console.WriteLine(arabicKurmanji);
// Output: "2 کانوونا ێکێ 2025"

string arabicSorani = gregorian.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Arabic
);
Console.WriteLine(arabicSorani);
// Output: "٢ کانونی یەکەم ٢٠٢٥"
```

### Custom Gregorian Formats

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Custom format string
string formatted = date.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Latin,
  "dd MMMM yyyy"
);
Console.WriteLine(formatted);
// Output: "15 Gulan 2025"

// Short format
string shortFormat = date.ToKurmanjiGregorianShort();
Console.WriteLine(shortFormat);
// Output: "15 Gul 2025"

// Long format
string longFormat = date.ToKurmanjiGregorianLong();
Console.WriteLine(longFormat);
// Output: "15 Gulan 2025"
```

### Getting Month Names Only

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Full month name
string monthName = date.GetKurmanjiMonthName();
Console.WriteLine(monthName);
// Output: "Gulan"

// Abbreviated
string monthAbbr = date.GetKurmanjiMonthName(abbreviated: true);
Console.WriteLine(monthAbbr);
// Output: "Gul"

// Arabic script
string monthArabic = date.GetKurmanjiMonthName(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
Console.WriteLine(monthArabic);
// Output: "گولان"
```

## Practical Applications

### Birthday Reminder System

```csharp
public class BirthdayReminder
{
  public class Person
  {
    public string Name { get; set; }
    public KurdishDate Birthday { get; set; }
  }

  public void CheckBirthdays(List<Person> people)
  {
    KurdishDate today = KurdishDate.Today;
    
    foreach (var person in people)
    {
      if (person.Birthday.Month == today.Month && 
          person.Birthday.Day == today.Day)
      {
        Console.WriteLine($"Today is {person.Name}'s birthday!");
        
        int age = today.Year - person.Birthday.Year;
        Console.WriteLine($"They are turning {age} years old.");
      }
    }
  }
}

// Usage
var people = new List<BirthdayReminder.Person>
{
  new BirthdayReminder.Person 
  { 
    Name = "Ahmed", 
    Birthday = new KurdishDate(2700, 6, 15) 
  },
  new BirthdayReminder.Person 
  { 
    Name = "Leyla", 
    Birthday = new KurdishDate(2705, 1, 1) 
  }
};

var reminder = new BirthdayReminder();
reminder.CheckBirthdays(people);
```

### Cultural Event Calendar

```csharp
public class CulturalEvent
{
  public string Name { get; set; }
  public KurdishDate Date { get; set; }
  public string Description { get; set; }
}

public class EventCalendar
{
  private List<CulturalEvent> events = new List<CulturalEvent>
  {
    new CulturalEvent 
    { 
      Name = "Newroz", 
      Date = new KurdishDate(2725, 1, 1),
      Description = "Kurdish New Year" 
    },
    new CulturalEvent 
    { 
      Name = "Sere Sal", 
      Date = new KurdishDate(2724, 12, 29),
      Description = "New Year's Eve" 
    }
  };

  public List<CulturalEvent> GetUpcomingEvents(int days)
  {
    KurdishDate today = KurdishDate.Today;
    KurdishDate futureDate = today.AddDays(days);

    return events
      .Where(e => e.Date >= today && e.Date <= futureDate)
      .OrderBy(e => e.Date)
      .ToList();
  }

  public void DisplayEvent(CulturalEvent evt, KurdishDialect dialect)
  {
    Console.WriteLine($"{evt.Name}");
    Console.WriteLine($"Date: {evt.Date.ToString("D", dialect)}");
    Console.WriteLine($"Description: {evt.Description}");
    
    KurdishDate today = KurdishDate.Today;
    int daysUntil = evt.Date.DaysDifference(today);
    Console.WriteLine($"Days until event: {daysUntil}");
  }
}
```

### Historical Timeline

```csharp
public class HistoricalEvent
{
  public KurdishDate Date { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

  public int YearsAgo()
  {
    KurdishDate today = KurdishDate.Today;
    return today.Year - Date.Year;
  }

  public string FormatForDisplay(KurdishDialect dialect)
  {
    return $"{Date.ToString("D", dialect)} — {Title} ({YearsAgo()} years ago)";
  }
}

// Usage
var events = new List<HistoricalEvent>
{
  new HistoricalEvent
  {
    Date = new KurdishDate(2691, 3, 16),
    Title = "Halabja Tragedy",
    Description = "Chemical attack on Halabja"
  },
  new HistoricalEvent
  {
    Date = new KurdishDate(2612, 9, 11),
    Title = "Republic of Mahabad",
    Description = "Establishment of Kurdish Republic"
  }
};

foreach (var evt in events.OrderBy(e => e.Date))
{
  Console.WriteLine(evt.FormatForDisplay(KurdishDialect.SoraniLatin));
}
```

### Agricultural Planner

```csharp
public class AgriculturePlanner
{
  public string GetCurrentSeason()
  {
    KurdishDate today = KurdishDate.Today;
    
    return today.Month switch
    {
      >= 1 and <= 3 => "Spring (Behar)",
      >= 4 and <= 6 => "Summer (Havîn)",
      >= 7 and <= 9 => "Autumn (Payîz)",
      >= 10 and <= 12 => "Winter (Zivistan)",
      _ => "Unknown"
    };
  }

  public List<string> GetPlantingRecommendations()
  {
    KurdishDate today = KurdishDate.Today;
    
    return today.Month switch
    {
      1 => new List<string> { "Wheat", "Barley", "Chickpeas" },
      2 => new List<string> { "Lentils", "Vegetables" },
      3 => new List<string> { "Melons", "Cucumbers" },
      4 => new List<string> { "Tomatoes", "Peppers" },
      5 => new List<string> { "Summer vegetables" },
      6 => new List<string> { "Late summer crops" },
      7 => new List<string> { "Autumn wheat" },
      8 => new List<string> { "Winter vegetables" },
      9 => new List<string> { "Garlic", "Onions" },
      10 => new List<string> { "Winter wheat" },
      11 => new List<string> { "Cover crops" },
      12 => new List<string> { "Planning for spring" },
      _ => new List<string>()
    };
  }

  public void DisplayMonthlyGuide(KurdishDialect dialect)
  {
    for (int month = 1; month <= 12; month++)
    {
      KurdishDate sampleDate = new KurdishDate(2725, month, 1);
      string monthName = KurdishCultureInfo.GetMonthName(month, dialect);
      
      Console.WriteLine($"\n{monthName}:");
      // Display recommendations for this month
    }
  }
}
```

### Date Range Queries

```csharp
public class DateRangeExample
{
  public List<KurdishDate> GetDatesInRange(KurdishDate start, KurdishDate end)
  {
    var dates = new List<KurdishDate>();
    KurdishDate current = start;

    while (current <= end)
    {
      dates.Add(current);
      current = current.AddDays(1);
    }

    return dates;
  }

  public List<KurdishDate> GetNewrozDates(int startYear, int endYear)
  {
    var dates = new List<KurdishDate>();

    for (int year = startYear; year <= endYear; year++)
    {
      dates.Add(new KurdishDate(year, 1, 1));
    }

    return dates;
  }

  public int CountWorkDays(KurdishDate start, KurdishDate end)
  {
    int workDays = 0;
    KurdishDate current = start;

    while (current <= end)
    {
      DayOfWeek dow = current.DayOfWeek;
      if (dow != DayOfWeek.Friday && dow != DayOfWeek.Saturday)
      {
        workDays++;
      }
      current = current.AddDays(1);
    }

    return workDays;
  }
}
```

## See Also

- [API Reference](api-reference.md) — Complete API documentation
- [Formatting and Parsing](formatting-and-parsing.md) — Detailed formatting guide
- [Astronomical Calculations](astronomical-calculations.md) — Precision features
- [Getting Started](getting-started.md) — Installation and basics

---

**بەختێکی باش! (Good luck!)**