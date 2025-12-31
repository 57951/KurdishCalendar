# Kurdish Calendar

A .NET 10 library for Kurdish calendar date handling, conversion, and formatting with support for Sorani, Kurmanji, and Hawrami dialects in both Latin and Arabic scripts.

## Overview

The Kurdish Calendar library provides comprehensive support for the Kurdish solar calendar system, based on the Solar Hijri calendar with Nowruz (spring equinox) as the start of the year. It handles conversions between Gregorian and Kurdish calendars, date arithmetic, formatting, and parsing across multiple Kurdish dialects and scripts.

This library is designed for applications requiring authentic Kurdish calendar support, including cultural heritage archives, genealogy systems, academic research, agricultural planning, and applications serving Kurdish-speaking communities.

## What Problem Does This Solve?

1. **Authentic Kurdish Calendar Support** — Provides a proper Kurdish calendar implementation with culturally accurate month and day names
2. **Multi-Dialect Support** — Handles Sorani, Kurmanji, and Hawrami dialects in both Latin and Arabic scripts
3. **Astronomical Precision** — Offers both simplified (fast) and astronomically precise (accurate) date calculations
4. **Gregorian Integration** — Formats Gregorian dates with Kurdish Sorani and Kurdish Kurmanji month names as used in Kurdistan Region
5. **Bidirectional Conversion** — Seamlessly converts between Gregorian and Kurdish calendars

## Features

- ✅ **Dual Calendar Types**
  - `KurdishDate` — Simplified calculation (suitable for 99.9% of use cases)
  - `KurdishAstronomicalDate` — Astronomical precision using Jean Meeus algorithms
- ✅ **Multiple Dialects** — Sorani, Kurmanji, and Hawrami
- ✅ **Dual Script Support** — Latin and Arabic scripts
- ✅ **Gregorian Integration** — Format Gregorian dates with Kurdish Sorani and Kurdish Kurmanji month names
- ✅ **Date Arithmetic** — Add/subtract days, months, years
- ✅ **Date Parsing** — Parse date strings in multiple formats
- ✅ **Formatting Options** — Customisable date formatting
- ✅ **Strongly Typed** — Full type safety, no `var` usage
- ✅ **Zero Dependencies** — Self-contained library
- ✅ **Thread Safe** — Immutable date types
- ✅ **Validated** — Tested against authoritative astronomical data

## Requirements

- .NET 10.0 or later
- No external dependencies

## Installation

### NuGet Package Manager

```bash
dotnet add package KurdishCalendar.Core
```

### Package Manager Console

```powershell
Install-Package KurdishCalendar.Core
```

### .csproj File

```xml
<PackageReference Include="KurdishCalendar.Core" Version="1.0.0" />
```

## Quick Start

### Basic Usage

```csharp
using KurdishCalendar.Core;

// Create a Kurdish date
KurdishDate date = new KurdishDate(2725, 1, 1); // Newroz 2725

// Convert from Gregorian
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishDate kurdish = KurdishDate.FromDateTime(gregorian);

// Get today's date
KurdishDate today = KurdishDate.Today;

// Convert to Gregorian
DateTime gregorianDate = date.ToDateTime();

// Format the date
string formatted = date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "1 Xakelêwe 2725"
```

### Date Arithmetic

```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);

// Add days, months, years
KurdishDate tomorrow = date.AddDays(1);
KurdishDate nextMonth = date.AddMonths(1);
KurdishDate nextYear = date.AddYears(1);

// Calculate differences
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 12, 29);
int daysDifference = date2.DaysDifference(date1);
```

### Formatting Dates

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Short format
string shortFormat = date.ToString("d", KurdishDialect.SoraniLatin);
// Output: "15/06/2725"

// Long format
string longFormat = date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

// Arabic script
string arabicFormat = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniArabic);
// Output: "١٥ خەرمانان ٢٧٢٥"
```

### Parsing Dates

```csharp
// Parse from string
KurdishDate parsed = KurdishDate.Parse("15 Xakelêwe 2725", KurdishDialect.SoraniLatin);

// Safe parsing
if (KurdishDate.TryParse("15/01/2725", KurdishDialect.SoraniLatin, out KurdishDate result))
{
  Console.WriteLine($"Parsed successfully: {result}");
}
```

## Supported Dialects and Scripts

The library supports ten dialect/script combinations:

| Calendar | Dialect | Latin Script | Arabic Script |
|----------|---------|--------------|---------------|
| **Kurdish Calendar** | Sorani | `KurdishDialect.SoraniLatin` | `KurdishDialect.SoraniArabic` |
| | Kurmanji | `KurdishDialect.KurmanjiLatin` | `KurdishDialect.KurmanjiArabic` |
| | Hawrami | `KurdishDialect.HawramiLatin` | `KurdishDialect.HawramiArabic` |
| **Gregorian Calendar** | Sorani | `KurdishDialect.SoraniGregorianLatin` | `KurdishDialect.SoraniGregorianArabic` |
| | Kurmanji | `KurdishDialect.KurmanjiGregorianLatin` | `KurdishDialect.KurmanjiGregorianArabic` |

## Calendar System

### Kurdish Calendar Basics

- **Epoch**: Year 1 corresponds to approximately 700 BCE (founding of Median Empire)
- **12 months**: First 6 months have 31 days, months 7-11 have 30 days, month 12 has 29 or 30 days
- **Leap years**: Determined by 33-year cycle (simplified) or astronomical calculation
- **New Year (Nowruz)**: Spring equinox (around 20-21 March in Gregorian calendar)

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

## Validation and Accuracy

### Standard Dates (`KurdishDate`)
- Uses fixed 21 March for Nowruz calculation
- 33-year leap year cycle pattern
- Suitable for 99.9% of applications
- Fast and predictable
- Validated for consistency

### Astronomical Dates (`KurdishAstronomicalDate`)
- Calculates precise spring equinox using Jean Meeus algorithms from "Astronomical Algorithms" (1998)
- Accuracy: ±1 minute for years 1800-2200
- Validated against Fred Espenak's ephemeris data for years 2000-2030
- Accounts for longitude differences (different locations may observe Nowruz on different Gregorian dates)
- Automatically caches equinox calculations for performance

**When to use astronomical dates:**
- Cultural/religious ceremony timing requiring exact equinox moment
- Official government or legal documents
- Academic/historical research
- Synchronising with Iranian Solar Hijri calendar
- Long-term projections (50+ years)

**When to use simplified dates:**
- General applications (calendars, schedulers, reminders)
- Performance-critical code
- When fixed 21 March assumption is acceptable

### Test Coverage
- 150+ unit tests
- Validated against authoritative astronomical data (Fred Espenak's tables)
- All dialect/script combinations tested
- Edge cases and boundaries covered
- Round-trip conversion verification

## Limitations and Caveats

### Known Limitations

1. **Standard dates assume fixed Nowruz** — `KurdishDate` uses 21 March for Nowruz. Actual astronomical equinox varies between 20-22 March.

2. **Historical accuracy** — Dates before 1800 CE are calculated but not validated against historical records. Astronomical algorithms are accurate for 1800-2200 CE.

3. **Month and day names** — Gregorian month names (Sorani and Kurmanji) verified against Kurdistan Regional Government (gov.krd) publications. Kurdish calendar month names use established transliterations. Hawrami day names sourced from D.N. MacKenzie's "The Dialect of Awroman" (1966). Hawrami month names from Zaniary.com documentation. Local dialect variations may exist.

4. **Dates only, no time of day** — The library handles calendar dates, not specific times of day. The astronomical equinox calculator provides the UTC moment of the equinox, which is then used to determine the calendar date. For time-of-day requirements beyond determining calendar dates, use .NET `DateTime` or `DateTimeOffset`.

5. **Gregorian calendar dependency** — All conversions rely on .NET's `System.DateTime` Gregorian calendar implementation.

6. **Leap year differences** — Standard dates use a 33-year cycle. Astronomical dates calculate actual 366-day years, which may differ.

7. **Longitude matters for astronomical dates** — Different longitudes may result in Nowruz falling on different Gregorian dates (e.g., Erbil vs Tehran vs UTC).

## Astronomical Precision

For applications requiring exact spring equinox timing, use `KurdishAstronomicalDate`:

```csharp
// Default uses Erbil (44.0°E)
KurdishAstronomicalDate astroDate = new KurdishAstronomicalDate(2725, 1, 1);

// Named locations
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var sulaymaniyah = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);

// Custom longitude
var baghdad = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);

// Get exact equinox moment
DateTime exactNowruz = astroDate.ToDateTime();
```

**Note**: The Jean Meeus algorithms are mathematically sound and widely used, but astronomical calculations are approximations. For years 1800-2200, accuracy is ±1 minute compared to modern ephemeris data.

## Gregorian Dates with Kurdish Month Names

Format Gregorian calendar dates with Kurdish month names as commonly used in Kurdistan Region:

```csharp
DateTime date = new DateTime(2025, 12, 2);

// Kurmanji
string kurmanji = date.ToKurmanjiGregorian();
// Output: "2 Kanûna Êkê 2025"

// Sorani
string sorani = date.ToSoraniGregorian();
// Output: "2 Kanûnî Yekem 2025"

// Arabic script
string arabic = date.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
// Output: "2 کانوونا ێکێ 2025"
```

## Documentation

- [Getting Started](docs/getting-started.md) — Comprehensive tutorial
- [API Reference](docs/api-reference.md) — Complete API documentation
- [Astronomical Calculations](docs/astronomical-calculations.md) — Precision features
- [Formatting and Parsing](docs/formatting-and-parsing.md) — Format and parse dates
- [Dialects and Scripts](docs/dialects-and-scripts.md) — Language support
- [Gregorian Formatting](docs/gregorian-formatting.md) — Gregorian dates with Kurdish names
- [Testing](docs/testing.md) — Validation and test coverage
- [Examples](docs/examples.md) — Practical code examples
- [FAQ](docs/faq.md) — Frequently asked questions

## Reference Sources

### Astronomical Calculations
- **Jean Meeus** — "Astronomical Algorithms" (1991/1998), Chapter 27: Equinoxes and Solstices
- **Fred Espenak** — Solstice & Equinox Tables, www.Astropixels.com (validation data for years 2000-2030)
- **NASA JPL Horizons** — Modern ephemeris reference

### Linguistic Sources
- **Kurdistan Regional Government** (gov.krd) — Official Sorani and Kurmanji terminology
- **D.N. MacKenzie** — "The Dialect of Awroman (Hawrämän-i Luhön)" (1966), pages 21-22 for Hawrami day names
- **Zaniary.com** — Hawrami month names (https://zaniary.com/blog/61ec1e25bfc4e)
- **UNESCO** — Nowruz cultural documentation

### Calendar System
- Iranian Solar Hijri calendar as reference implementation
- Kurdish cultural practices and traditional knowledge

## Contributing

Contributions are welcome. Please see [CONTRIBUTING.md](docs/contributing.md) for guidelines.

Areas where contributions are particularly valuable:
- Additional dialect support
- Test coverage expansion
- Documentation translations

## Licence

This library is licensed under the MIT Licence. See [LICENSE](LICENSE.md) for full text.

## Acknowledgements

- Astronomical algorithms courtesy of Jean Meeus
- Equinox validation data from Fred Espenak's published tables
- Linguistic research by D.N. MacKenzie and others
- Kurdistan Regional Government for standardised terminology
- UNESCO for Nowruz cultural documentation
- Open-source community for feedback and contributions

## Support

For issues, questions, or contributions:
- **GitHub Issues**: [Report an issue](https://github.com/57951/KurdishCalendar/issues)
- **Documentation**: See `docs/` directory
- **Examples**: See [docs/examples.md](docs/examples.md)

---

**بەختێکی باش! (Good luck!)**