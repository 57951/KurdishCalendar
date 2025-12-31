# KurdishCalendar.Core

Core library for Kurdish calendar date handling, conversion, and formatting.

## Overview

KurdishCalendar.Core is a reusable .NET 10 library providing comprehensive support for the Kurdish solar calendar system with multi-dialect and multi-script capabilities.

## Features

- **Two calendar types**: Simplified and astronomical calculations
- **Three dialects**: Sorani, Kurmanji, and Hawrami
- **Two scripts**: Latin and Arabic
- **Gregorian integration**: Format Gregorian dates with Kurdish Sorani or Kurdish Kurmanji month names
- **Thread-safe**: Immutable readonly struct types
- **Zero dependencies**: Lightweight and easy to integrate

## Installation

```bash
dotnet add package KurdishCalendar.Core
```

Or via Package Manager:

```powershell
Install-Package KurdishCalendar.Core
```

Or add to `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="KurdishCalendar.Core" Version="1.0.0" />
</ItemGroup>
```

## Quick Start

```csharp
using KurdishCalendar.Core;

// Create a Kurdish date
KurdishDate newroz = new KurdishDate(2725, 1, 1);

// Format in different dialects
Console.WriteLine(newroz.ToString("D", KurdishDialect.SoraniLatin));
// Output: "1 Xakelêwe 2725"

Console.WriteLine(newroz.ToString("D", KurdishDialect.SoraniArabic));
// Output: "١ خاکەلێوە ٢٧٢٥"

// Convert to Gregorian
DateTime gregorian = newroz.ToDateTime();
Console.WriteLine(gregorian.ToString("yyyy-MM-dd"));
// Output: "2025-03-21"

// Parse Kurdish dates
KurdishDate parsed = KurdishDate.Parse("15 Xermanan 2725", KurdishDialect.SoraniLatin);
```

## Core Types

### KurdishDate

Simplified calendar calculations using fixed 21 March for Nowruz.

```csharp
// Create dates
KurdishDate date = new KurdishDate(2725, 6, 15);
KurdishDate today = KurdishDate.Today;

// Date arithmetic
KurdishDate tomorrow = date.AddDays(1);
KurdishDate nextMonth = date.AddMonths(1);
KurdishDate nextYear = date.AddYears(1);

// Calculate differences
int days = date2.DaysDifference(date1);

// Properties
int year = date.Year;
int month = date.Month;
int day = date.Day;
DayOfWeek dayOfWeek = date.DayOfWeek;
int dayOfYear = date.DayOfYear;
bool isLeapYear = date.IsLeapYear;
```

### KurdishAstronomicalDate

Astronomical precision using actual spring equinox calculation (±1 minute accuracy for years 1800-2200).

```csharp
// Create astronomical dates
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
var custom = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);

// Get exact equinox moment
DateTime equinox = erbil.ToDateTime();
Console.WriteLine($"Newroz 2725: {equinox:yyyy-MM-dd HH:mm:ss} UTC");
```

## Supported Dialects

| Calendar | Dialect | Latin Script | Arabic Script |
|----------|---------|--------------|---------------|
| **Kurdish Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |
| | Hawrami | ✓ | ✓ |
| **Gregorian Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |

## Formatting

### Standard Formats

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Short date
date.ToString("d", KurdishDialect.SoraniLatin);
// Output: "15/06/2725"

// Long date
date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

// Custom format
date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

date.ToString("dddd, d MMMM yyyy", KurdishDialect.SoraniLatin);
// Output: "Hênî, 15 Xermanan 2725"
```

### Gregorian Formatting

```csharp
using KurdishCalendar.Core;

DateTime gregorian = new DateTime(2025, 5, 15);

// Sorani Gregorian
string sorani = gregorian.ToSoraniGregorian();
// Output: "15 Ayar 2025"

// Kurmanji Gregorian
string kurmanji = gregorian.ToKurmanjiGregorian();
// Output: "15 Gulan 2025"

// Arabic script
string arabicSorani = gregorian.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Arabic
);
// Output: "١٥ ئایار ٢٠٢٥"
```

## Parsing

```csharp
// Parse with dialect
KurdishDate date1 = KurdishDate.Parse("15 Xermanan 2725", KurdishDialect.SoraniLatin);

// Safe parsing
if (KurdishDate.TryParse("15/06/2725", KurdishDialect.SoraniLatin, out KurdishDate date2))
{
  Console.WriteLine($"Parsed: {date2}");
}

// Parse Arabic script
KurdishDate date3 = KurdishDate.Parse("١٥ خەرمانان ٢٧٢٥", KurdishDialect.SoraniArabic);

// Parse astronomical dates
KurdishAstronomicalDate astro = KurdishAstronomicalDate.Parse(
  "1 Xakelêwe 2725",
  KurdishDialect.SoraniLatin,
  44.0 // Erbil longitude
);
```

## Date Comparison

```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);

// Comparison operators
bool equal = date1 == date2;        // false
bool notEqual = date1 != date2;     // true
bool lessThan = date1 < date2;      // true
bool greaterThan = date1 > date2;   // false

// CompareTo
int comparison = date1.CompareTo(date2); // -1

// Sorting
List<KurdishDate> dates = new List<KurdishDate> { date2, date1 };
dates.Sort();
// Result: { date1, date2 }
```

## Calendar System

The Kurdish calendar is a solar calendar based on the Solar Hijri system:

- **Epoch**: Year 1 ≈ 700 BCE (Median Empire founding)
- **New Year**: Nowruz (spring equinox, simplified uses fixed 21 March)
- **12 Months**: 
  - Months 1-6: 31 days each
  - Months 7-11: 30 days each
  - Month 12: 29 days (30 in leap years)
- **Leap Years**: 33-year cycle (simplified) or astronomical calculation

### Month Names (Sorani Latin)

1. Xakelêwe (31 days)
2. Gulan (31 days)
3. Cozerdan (31 days)
4. Pûşper (31 days)
5. Gelawêj (31 days)
6. Xermanan (31 days)
7. Rezber (30 days)
8. Gelarêzan (30 days)
9. Sermawez (30 days)
10. Befranbar (30 days)
11. Rêbendan (30 days)
12. Reşeme (29/30 days)

## Accuracy and Validation

### Simplified Dates (KurdishDate)

- Uses fixed 21 March for Nowruz
- 33-year leap year cycle
- Fast and predictable
- Suitable for 99.9% of applications

### Astronomical Dates (KurdishAstronomicalDate)

- Jean Meeus algorithms from "Astronomical Algorithms" (1998)
- Accuracy: ±1 minute for years 1800-2200
- Validated against Fred Espenak's ephemeris data (2000-2030)
- Accounts for longitude differences

### Test Coverage

- 150+ unit tests
- >95% code coverage
- All dialect/script combinations tested
- Edge cases and boundary conditions covered

## Performance

| Operation | Simplified | Astronomical (uncached) | Astronomical (cached) |
|-----------|-----------|------------------------|----------------------|
| Date creation | ~5μs | ~50μs | ~0.1μs |
| Conversion to Gregorian | ~0.1μs | ~0.1μs | ~0.1μs |

## Requirements

- **.NET 10.0** or higher
- **Zero dependencies**

## Compatibility

- .NET 10.0+
- .NET Core 3.1+
- .NET Standard 2.0+
- All platforms: Windows, Linux, macOS
- Mobile: Android, iOS (via .NET MAUI)

## Documentation

Complete documentation available in the `/docs` directory:

- [Getting Started](../../docs/getting-started.md)
- [Examples](../../docs/examples.md)
- [Use Cases](../../docs/use-cases.md)
- [API Reference](../../docs/api-reference.md)
- [Formatting and Parsing](../../docs/formatting-and-parsing.md)
- [Dialects and Scripts](../../docs/dialects-and-scripts.md)
- [Astronomical Calculations](../../docs/astronomical-calculations.md)
- [Gregorian Formatting](../../docs/gregorian-formatting.md)
- [Testing](../../docs/testing.md)
- [FAQ](docs/faq.md)

## Examples Project

See the `KurdishCalendar.Examples` project for comprehensive working examples.

## Tests

Run tests from the `KurdishCalendar.Tests` project:

```bash
cd KurdishCalendar.Tests
dotnet test
```

## Licence

MIT Licence — See [LICENSE.md](LICENSE.md) for details.

## Acknowledgements

### Astronomical Algorithms
- Jean Meeus — "Astronomical Algorithms" (1991/1998)
- Fred Espenak — Solstice & Equinox Tables

### Linguistic Sources
- Kurdistan Regional Government (gov.krd)
- D.N. MacKenzie — "The Dialect of Awroman" (1966)
- Zaniary.com

## Support

- **Documentation**: See `/docs` directory
- **Issues**: GitHub Issues
- **Discussions**: GitHub Discussions

---

**بەختێکی باش! (Good luck!)**