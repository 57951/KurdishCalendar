---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Getting Started
---

# Getting Started with Kurdish Calendar

This guide will help you start using the Kurdish Calendar library in your .NET applications.

## Prerequisites

- .NET 10.0 SDK or later
- A code editor (Visual Studio, VS Code, Rider, etc.)
- Basic knowledge of C# and .NET

## Installation

You can add the library package to new projects via [NuGet](https://www.nuget.org/packages/KurdishCalendar.Core).

### Create a New Project

```bash
# Create a console application
dotnet new console -n MyKurdishCalendarApp
cd MyKurdishCalendarApp

# Add the package
dotnet add package KurdishCalendar.Core
```

### Add to Existing Project

```bash
dotnet add package KurdishCalendar.Core
```

## Your First Kurdish Date

Create a new file `Program.cs`:

```csharp
using System;
using KurdishCalendar.Core;

// Get today's Kurdish date
KurdishDate today = KurdishDate.Today;
Console.WriteLine($"Today in Kurdish calendar: {today.Year}/{today.Month}/{today.Day}");

// Create a specific date (Newroz 2725)
KurdishDate newroz = new KurdishDate(2725, 1, 1);
Console.WriteLine($"Newroz 2725: {newroz.ToString("D", KurdishDialect.SoraniLatin)}");

// Convert to Gregorian
DateTime gregorian = newroz.ToDateTime();
Console.WriteLine($"Newroz 2725 in Gregorian: {gregorian:yyyy-MM-dd}");
```

Run it:

```bash
dotnet run
```

Expected output:
```
Today in Kurdish calendar: 2725/10/10
Newroz 2725: 1 Xakelêwe 2725
Newroz 2725 in Gregorian: 2025-03-21
```

## Working with Dates

### Creating Dates

```csharp
// From year/month/day
KurdishDate date1 = new KurdishDate(2725, 6, 15);

// From Gregorian DateTime
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishDate date2 = KurdishDate.FromDateTime(gregorian);

// Today's date
KurdishDate today = KurdishDate.Today;

// Current moment
KurdishDate now = KurdishDate.Now;
```

### Converting Dates

```csharp
KurdishDate kurdish = new KurdishDate(2725, 1, 1);

// To Gregorian
DateTime gregorian = kurdish.ToDateTime();

// From Gregorian
KurdishDate backToKurdish = KurdishDate.FromDateTime(gregorian);

// Explicit conversion
DateTime explicitGregorian = (DateTime)kurdish;

// Round-trip verification
System.Diagnostics.Debug.Assert(kurdish == backToKurdish);
```

### Date Properties

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

int year = date.Year;           // 2725
int month = date.Month;         // 6
int day = date.Day;             // 15
DayOfWeek dayOfWeek = date.DayOfWeek;  // e.g., Friday
int dayOfYear = date.DayOfYear; // 166
bool isLeapYear = date.IsLeapYear;  // false
```

## Date Arithmetic

### Adding and Subtracting

```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);

// Add days
KurdishDate tomorrow = date.AddDays(1);
KurdishDate nextWeek = date.AddDays(7);
KurdishDate yesterday = date.AddDays(-1);

// Add months
KurdishDate nextMonth = date.AddMonths(1);
KurdishDate lastMonth = date.AddMonths(-1);

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

// Or use DateTime conversion
TimeSpan span = end.ToDateTime() - start.ToDateTime();
int totalDays = (int)span.TotalDays;
```

## Formatting Dates

### Standard Formats

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Short format (dd/MM/yyyy)
string shortDate = date.ToString("d", KurdishDialect.SoraniLatin);
// "15/06/2725"

// Long format (day monthName year)
string longDate = date.ToString("D", KurdishDialect.SoraniLatin);
// "15 Xermanan 2725"

// Month and day
string monthDay = date.ToString("M", KurdishDialect.SoraniLatin);
// "15 Xermanan"

// Year and month
string yearMonth = date.ToString("Y", KurdishDialect.SoraniLatin);
// "Xermanan 2725"
```

### Custom Formats

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Custom format tokens:
// d/dd = day, M/MM = month number, MMM = month abbr, MMMM = month name
// y/yy/yyyy = year, ddd = day name abbr, dddd = day name

string custom1 = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin);
// "15 Xermanan 2725"

string custom2 = date.ToString("dddd, d MMMM yyyy", KurdishDialect.SoraniLatin);
// "Hênî, 15 Xermanan 2725"

string custom3 = date.ToString("MMM d, yy", KurdishDialect.SoraniLatin);
// "Xer 15, 25"
```

### Different Dialects

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Sorani (Latin)
string soraniLatin = date.ToString("D", KurdishDialect.SoraniLatin);
// "1 Xakelêwe 2725"

// Sorani (Arabic)
string soraniArabic = date.ToString("D", KurdishDialect.SoraniArabic);
// "١ خاکەلێوە ٢٧٢٥"

// Kurmanji (Latin)
string kurmanjiLatin = date.ToString("D", KurdishDialect.KurmanjiLatin);
// "1 Xakelêwe 2725"

// Hawrami (Latin)
string hawramiLatin = date.ToString("D", KurdishDialect.HawramiLatin);
// "1 Newroz 2725"
```

## Parsing Dates

### Basic Parsing

```csharp
// Parse from long format
KurdishDate date1 = KurdishDate.Parse(
  "15 Xakelêwe 2725", 
  KurdishDialect.SoraniLatin
);

// Parse from short format
KurdishDate date2 = KurdishDate.Parse(
  "15/01/2725", 
  KurdishDialect.SoraniLatin
);

// Parse Arabic script
KurdishDate date3 = KurdishDate.Parse(
  "١٥ خاکەلێوە ٢٧٢٥", 
  KurdishDialect.SoraniArabic
);
```

### Safe Parsing

```csharp
// Use TryParse for user input
string userInput = GetUserInput();

if (KurdishDate.TryParse(userInput, KurdishDialect.SoraniLatin, out KurdishDate date))
{
  Console.WriteLine($"Valid date: {date}");
}
else
{
  Console.WriteLine("Invalid date format");
}
```

### Supported Parse Formats

```csharp
// Numeric formats (day/month/year for Latin, year/month/day for Arabic)
KurdishDate.Parse("15/01/2725", KurdishDialect.SoraniLatin);
KurdishDate.Parse("15-01-2725", KurdishDialect.SoraniLatin);
KurdishDate.Parse("15 01 2725", KurdishDialect.SoraniLatin);

// Long formats (case-insensitive month names)
KurdishDate.Parse("15 Xakelêwe 2725", KurdishDialect.SoraniLatin);
KurdishDate.Parse("2725 Xakelêwe 15", KurdishDialect.SoraniLatin); // Also works

// Arabic script (year/month/day order)
KurdishDate.Parse("٢٧٢٥/٠١/١٥", KurdishDialect.SoraniArabic);
KurdishDate.Parse("٢٧٢٥ خاکەلێوە ١٥", KurdishDialect.SoraniArabic);
```

## Comparing Dates

### Comparison Operators

```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);

bool isEqual = date1 == date2;       // false
bool isNotEqual = date1 != date2;    // true
bool isLess = date1 < date2;         // true
bool isGreater = date1 > date2;      // false
bool isLessOrEqual = date1 <= date2; // true
bool isGreaterOrEqual = date1 >= date2; // false
```

### Using CompareTo

```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);

int comparison = date1.CompareTo(date2);
// -1 if date1 < date2
//  0 if date1 == date2
//  1 if date1 > date2

// Sorting
List<KurdishDate> dates = new List<KurdishDate> { /* ... */ };
dates.Sort(); // Sorts chronologically
```

## Astronomical Precision

For applications requiring exact spring equinox timing:

### Basic Astronomical Dates

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
```

### When to Use Astronomical Dates

Use astronomical dates when:
- Planning cultural or religious ceremonies
- Creating official or legal documents
- Conducting academic or historical research
- Synchronising with Iranian Solar Hijri calendar
- Making long-term projections (decades or centuries)

Use simplified dates for:
- General applications (calendars, schedulers, reminders)
- Performance-critical code
- When 21 March assumption is acceptable

### Converting Between Simplified and Astronomical

```csharp
KurdishDate simplified = new KurdishDate(2725, 1, 1);

// Lossless conversion (keeps same year/month/day)
KurdishAstronomicalDate astro1 = simplified.ToAstronomical();

// Informational conversion (recalculates via Gregorian)
KurdishAstronomicalDate astro2 = simplified.ToAstronomicalRecalculated();

// Reverse conversions
KurdishDate back1 = astro1.ToStandardDate(); // Lossless
KurdishDate back2 = astro1.ToStandardDateRecalculated(); // Informational
```

## Gregorian Dates with Kurdish Month Names

The library supports formatting Gregorian calendar dates with Kurdish month names:

### Basic Usage

```csharp
DateTime gregorian = new DateTime(2025, 12, 2);

// Kurmanji (default Latin script)
string kurmanji = gregorian.ToKurmanjiGregorian();
// "2 Kanûna Êkê 2025"

// Sorani (default Latin script)
string sorani = gregorian.ToSoraniGregorian();
// "2 Kanûnî Yekem 2025"

// Arabic script
string arabic = gregorian.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
// "2 کانوونا ێکێ 2025"
```

### Custom Formats

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Custom format string
string formatted = date.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Latin,
  "dd MMMM yyyy"
);
// "15 Gulan 2025"

// Short format
string shortFormat = date.ToKurmanjiGregorianShort();
// "15 Gul 2025"

// Long format
string longFormat = date.ToKurmanjiGregorianLong();
// "15 Gulan 2025"
```

## Best Practices

### 1. Choose the Right Date Type

```csharp
// For most applications
KurdishDate date = KurdishDate.Today;

// For precise timing
KurdishAstronomicalDate astroDate = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
```

### 2. Use Safe Parsing for User Input

```csharp
// Always use TryParse for untrusted input
if (KurdishDate.TryParse(userInput, dialect, out KurdishDate date))
{
  // Process valid date
}
else
{
  // Handle invalid input
}
```

### 3. Store Dates in Gregorian Format

```csharp
// Store Gregorian DateTime in database
DateTime gregorian = kurdishDate.ToDateTime();
SaveToDatabase(gregorian);

// Convert back when displaying
KurdishDate displayDate = KurdishDate.FromDateTime(gregorian);
```

### 4. Cache Astronomical Calculations

```csharp
// Astronomical dates automatically cache equinox calculations
// No manual caching needed
var date1 = KurdishAstronomicalDate.FromErbil(2725, 1, 1); // Calculates
var date2 = KurdishAstronomicalDate.FromErbil(2725, 6, 15); // Uses cache
```

### 5. Be Explicit with Dialects

```csharp
// Always specify the dialect explicitly
string formatted = date.ToString("D", KurdishDialect.SoraniLatin);

// Not recommended (uses default Sorani Latin)
string formatted = date.ToString();
```

## Common Patterns

### Pattern 1: Birthday Reminder System

```csharp
public class BirthdayReminder
{
  public void CheckBirthdays(List<Person> people)
  {
    KurdishDate today = KurdishDate.Today;
    
    foreach (var person in people)
    {
      KurdishDate birthday = person.BirthdayKurdish;
      
      if (birthday.Month == today.Month && birthday.Day == today.Day)
      {
        SendBirthdayGreeting(person);
      }
    }
  }
}
```

### Pattern 2: Historical Event Timeline

```csharp
public class HistoricalEvent
{
  public KurdishDate EventDate { get; set; }
  public string Description { get; set; }
  
  public int YearsAgo()
  {
    KurdishDate today = KurdishDate.Today;
    return today.Year - EventDate.Year;
  }
  
  public string FormatForDisplay(KurdishDialect dialect)
  {
    return $"{EventDate.ToString("D", dialect)} - {Description}";
  }
}
```

### Pattern 3: Seasonal Agricultural Planner

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
    // Return crops appropriate for current month
    // ...
  }
}
```

## Next Steps

Now that you understand the basics:

1. **Explore Examples** — See [examples.md](examples.md) for more code samples
2. **Read API Reference** — Detailed documentation in [api-reference.md](api-reference.md)
3. **Learn About Astronomy** — Understand precision features in [astronomical-calculations.md](astronomical-calculations.md)
4. **Master Formatting** — Complete formatting guide in [formatting-and-parsing.md](formatting-and-parsing.md)
5. **Understand Dialects** — Dialect details in [dialects-and-scripts.md](dialects-and-scripts.md)

## Getting Help

- **Documentation**: Browse the `docs/` directory
- **FAQ**: See [faq.md](faq.md) for common questions
- **Examples**: Review [examples.md](examples.md) for working code
- **Issues**: Report problems on GitHub
- **Contributing**: See [contributing.md](contributing.md) to contribute

---

Happy coding! **بەختێکی باش!**