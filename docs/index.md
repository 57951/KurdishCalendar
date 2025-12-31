---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - .NET 10 Library
---

# Kurdish Calendar

This .NET 10 library provides comprehensive support for the Kurdish solar calendar system (simplified and astronomical) with multi-dialect and multi-script support, as well as providing the Gregorian calendar in Kurdish Sorani and Kurdish Kurmanji.

## Quick Navigation

### Getting Started
- [Getting Started Guide](getting-started.md) — Installation and first steps
- [Examples](examples.md) — Practical code examples

### API Documentation
- [API Reference](api-reference.md) — Complete API documentation
- [Formatting and Parsing](formatting-and-parsing.md) — Date formatting and parsing
- [Dialects and Scripts](dialects-and-scripts.md) — Language support details

### Advanced Topics
- [Astronomical Calculations](astronomical-calculations.md) — Precision features
- [Gregorian Formatting](gregorian-formatting.md) — Gregorian dates with Kurdish names

### Development
- [Testing](testing.md) — Test coverage and validation
- [Contributing](contributing.md) — Contribution guidelines
- [FAQ](faq.md) — Frequently asked questions

## What is this Library?

The Kurdish Calendar library provides:

1. **Kurdish Calendar Support** — Full implementation of the Kurdish solar calendar system
2. **Multi-Dialect** — Sorani, Kurmanji, and Hawrami dialects
3. **Multi-Script** — Both Latin and Arabic scripts
4. **Dual Precision** — Standard (fast) and astronomical (precise) date calculations
5. **Gregorian Integration** — Format Gregorian dates with Kurdish month names

## Calendar System Overview

The Kurdish calendar is a solar calendar based on the Solar Hijri system:

- **Epoch**: Year 1 ≈ 700 BCE (Median Empire founding)
- **New Year**: Nowruz (spring equinox, fixed at 21 March in simplified calculation)
- **12 Months**: First 6 have 31 days, months 7-11 have 30 days, month 12 has 29/30 days
- **Leap Years**: 33-year cycle (simplified) or astronomical calculation

## Key Features

### Two Calendar Types

**KurdishDate** — Simplified calculation
- Uses fixed 21 March for Nowruz
- 33-year leap year cycle
- Fast and predictable
- Suitable for 99.9% of applications

**KurdishAstronomicalDate** — Astronomical precision
- Calculates actual spring equinox using Jean Meeus algorithms
- Accuracy: ±1 minute for years 1800-2200
- Accounts for longitude differences
- Required for cultural ceremonies, official documents, academic research

### Ten Dialect/Script Combinations

| Calendar | Dialect | Latin Script | Arabic Script |
|----------|---------|--------------|---------------|
| **Kurdish Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |
| | Hawrami | ✓ | ✓ |
| **Gregorian Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |

### Comprehensive Functionality

- ✅ Date creation and conversion
- ✅ Date arithmetic (add/subtract days, months, years)
- ✅ Date comparison and sorting
- ✅ Flexible formatting with custom format strings
- ✅ Parsing from multiple formats
- ✅ Thread-safe immutable types
- ✅ Zero external dependencies

## Installation

```bash
dotnet add package KurdishCalendar.Core
```

## Quick Example

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
```

## Documentation Structure

### For New Users
1. Start with [Getting Started](getting-started.md)
2. Review [Examples](examples.md)
3. Check [FAQ](faq.md) for common questions

### For API Users
1. Read [API Reference](api-reference.md)
2. Understand [Formatting and Parsing](formatting-and-parsing.md)
3. Learn about [Dialects and Scripts](dialects-and-scripts.md)

### For Advanced Use Cases
1. Learn about [Astronomical Calculations](astronomical-calculations.md)
2. Explore [Gregorian Formatting](gregorian-formatting.md)
3. Review [Testing](testing.md) for accuracy guarantees

## Accuracy and Validation

### Standard Dates
- Uses fixed 21 March for Nowruz
- 33-year leap year cycle
- Validated for internal consistency
- Fast and predictable

### Astronomical Dates
- Jean Meeus algorithms from "Astronomical Algorithms" (1998)
- Validated against Fred Espenak's ephemeris data (years 2000-2030)
- Accuracy: ±1 minute for years 1800-2200
- Accounts for longitude differences

### Test Coverage
- 150+ unit tests
- All dialect/script combinations tested
- Edge cases and boundaries covered
- Round-trip conversion verification

## Reference Sources

### Astronomical
- Jean Meeus — "Astronomical Algorithms" (1991/1998)
- Fred Espenak — Solstice & Equinox Tables (www.Astropixels.com)

### Linguistic Sources
- Kurdistan Regional Government (gov.krd) publications
- D.N. MacKenzie — "The Dialect of Awroman" (1966)
- Zaniary.com — Hawrami month names

## Known Limitations

1. Standard dates use fixed 21 March (actual equinox varies 19-22 March)
2. Historical dates before 1800 CE calculated but not validated
3. All month/day names sourced from documented references (regional variations may exist)
4. Library handles calendar dates; astronomical calculator provides equinox timing for date determination

## Support

- **Documentation**: This docs directory
- **Examples**: [examples.md](examples.md)
- **Use Cases**: [use-cases](use-cases.md)
- **FAQ**: [faq.md](faq.md)
- **GitHub Issues**: Report problems
- **Contributing**: See [contributing.md](contributing.md)

## Licence

MIT Licence — See [license.md](license.md) for details

---

**بەختێکی باش! (Good luck!)**