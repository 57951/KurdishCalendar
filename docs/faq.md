---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Frequently Asked Questions
---

# Frequently Asked Questions

Common questions about the Kurdish Calendar library.

## General Questions

### What is the Kurdish Calendar?

The Kurdish calendar is a solar calendar based on the Solar Hijri system, with Nowruz (spring equinox) marking the new year. It consists of 12 months with the first 6 months having 31 days, months 7-11 having 30 days, and month 12 having 29 or 30 days in leap years.

### What year is it in the Kurdish calendar?

The Kurdish calendar epoch is approximately 700 BCE (founding of the Median Empire). To convert:
- Gregorian 2025 = Kurdish 2725 (2025 + 700)

### When is Nowruz?

Nowruz occurs at the spring equinox:
- **Simplified calculation**: Fixed at 21 March
- **Astronomical calculation**: Actual equinox moment (typically 19-22 March)

The exact date can vary by location due to time zones and longitude.

## Library Usage

### Should I use KurdishDate or KurdishAstronomicalDate?

**Use `KurdishDate` (simplified) for:**
- General applications (99.9% of use cases)
- Birthday tracking
- Historical records
- User interfaces
- Performance-critical code

**Use `KurdishAstronomicalDate` for:**
- Cultural ceremonies requiring exact timing
- Official documents
- Academic research
- Iranian calendar synchronisation
- Multi-location coordination

### How accurate is the astronomical calculation?

The astronomical calculation is accurate to **±1 minute** for years 1800-2200, validated against published ephemeris data from Fred Espenak (www.Astropixels.com).

### Which dialect should I use?

| Region | Recommended Dialect |
|--------|---------------------|
| Iraqi Kurdistan | Sorani or Kurmanji |
| Iran | Sorani |
| Turkey | Kurmanji |
| Syrian Kurdistan | Kurmanji |
| Hawraman region | Hawrami |

Choose based on your target audience's preferences.

### Which script should I use?

| Context | Recommended Script |
|---------|-------------------|
| Iraq (official) | Arabic or Latin (both common) |
| Iraq (modern apps) | Latin (increasingly common) |
| Iran | Arabic (primarily) |
| Turkey | Latin (primarily) |
| Syria | Arabic or Latin |
| Diaspora | Latin (primarily) |

Allow users to choose their preferred script when possible.

## Date Conversion

### Why does my date differ by one day?

This can happen when:

1. **Simplified vs Astronomical**: Simplified uses fixed 21 March; astronomical uses actual equinox (can be 19-22 March)

```csharp
// Simplified: Always 21 March
KurdishDate simplified = new KurdishDate(2725, 1, 1);
Console.WriteLine(simplified.ToDateTime()); // 2025-03-21

// Astronomical: Actual equinox (might be 20 March)
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
Console.WriteLine(astro.ToDateTime()); // 2025-03-20
```

2. **Time zones**: The equinox occurs at a specific UTC moment, but the calendar date depends on local time zone

### How do I convert between Kurdish and Gregorian?

```csharp
// Kurdish → Gregorian
KurdishDate kurdish = new KurdishDate(2725, 6, 15);
DateTime gregorian = kurdish.ToDateTime();

// Gregorian → Kurdish
DateTime gregorian = new DateTime(2025, 9, 6);
KurdishDate kurdish = KurdishDate.FromDateTime(gregorian);
```

### Can I convert to/from other calendar systems?

The library converts to/from Gregorian. For other calendar systems (Islamic, Hebrew, etc.), convert via Gregorian as an intermediate step:

```csharp
// Kurdish → Gregorian → Other calendar
KurdishDate kurdish = new KurdishDate(2725, 1, 1);
DateTime gregorian = kurdish.ToDateTime();
// Use .NET or third-party library for other calendars
```

## Formatting and Parsing

### How do I format dates in different languages?

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Sorani Latin
date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "1 Xakelêwe 2725"

// Sorani Arabic
date.ToString("D", KurdishDialect.SoraniArabic);
// Output: "١ خاکەلێوە ٢٧٢٥"

// Kurmanji Latin
date.ToString("D", KurdishDialect.KurmanjiLatin);
// Output: "1 Xakelêwe 2725"

// Hawrami Latin
date.ToString("D", KurdishDialect.HawramiLatin);
// Output: "1 Newroz 2725"
```

### What format strings are supported?

Standard formats:
- `d` — Short date (15/06/2725)
- `D` — Long date (15 Xermanan 2725)
- `M` — Month and day (15 Xermanan)
- `Y` — Year and month (Xermanan 2725)

Custom formats using tokens:
- `dd` — Day with leading zero
- `MMMM` — Full month name
- `MMM` — Abbreviated month name
- `yyyy` — Four-digit year

See [Formatting Guide](formatting-and-parsing.md) for complete details.

### How do I parse user input?

Use `TryParse` for safe parsing:

```csharp
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

### Why doesn't my date parse?

Common issues:

1. **Wrong dialect**: Ensure dialect matches input
2. **Invalid separator**: Use `/`, `-`, or space (not `.` or `,`)
3. **Wrong format**: Supported formats are `dd/MM/yyyy` or `dd MonthName yyyy`
4. **Case sensitivity**: Month names are case-insensitive, but must match dialect

## Leap Years

### How are leap years determined?

**Simplified (33-year cycle):**
Leap years occur at positions 1, 5, 9, 13, 17, 22, 26, 30 in each 33-year cycle.

**Astronomical:**
Year is leap if there are 366 days between consecutive Nowruz dates.

### Do simplified and astronomical leap years match?

Usually yes, but occasionally they differ:

```csharp
int year = 2725;

KurdishDate simplified = new KurdishDate(year, 1, 1);
Console.WriteLine($"Simplified leap: {simplified.IsLeapYear}");

KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(year, 1, 1);
Console.WriteLine($"Astronomical leap: {astro.IsLeapYear}");

// Usually match, but can differ in edge cases
```

## Performance

### Which is faster: KurdishDate or KurdishAstronomicalDate?

**KurdishDate (Simplified):**
- Date creation: ~5μs
- Very fast and predictable

**KurdishAstronomicalDate:**
- Date creation (uncached): ~50μs
- Date creation (cached): ~0.1μs
- First equinox calculation is slower, subsequent calls are cached

For most applications, the difference is negligible.

### Should I cache dates?

Date types are lightweight value types (structs). Create them as needed rather than caching:

```csharp
// Good - Create when needed
KurdishDate GetBirthday() 
{
  return new KurdishDate(2700, 5, 15);
}

// Unnecessary - No need to cache
private static readonly KurdishDate _birthday = new KurdishDate(2700, 5, 15);
```

Astronomical dates have internal equinox caching, so you don't need to cache the dates themselves.

## Gregorian Dates with Kurdish Names

### What's the difference between Kurdish calendar and Gregorian with Kurdish names?

**Kurdish Calendar:**
- Year 2725 = Gregorian 2025
- Nowruz (1 Xakelêwe) = 21 March
- Different month structure (6×31 + 5×30 + 1×29/30)
- Uses distinct month names in each Kurdish dialect

**Gregorian with Kurdish Names:**
- Same year as Gregorian (2025)
- Same month structure as Gregorian
- Uses Kurdish Sorani or Kurdish Kurmanji names for Gregorian months

```csharp
DateTime gregorian = new DateTime(2025, 1, 15);

// Gregorian with Kurdish names
string formatted = gregorian.ToSoraniGregorian();
// Output: "15 Kanûnî Duhem 2025" (January 15, 2025)

// Kurdish calendar
KurdishDate kurdish = KurdishDate.FromDateTime(gregorian);
Console.WriteLine(kurdish.ToString("D", KurdishDialect.SoraniLatin));
// Output: "26 Befranbar 2724" (Different date entirely)
```

### When should I use Gregorian formatting?

Use Gregorian with Kurdish names for:
- Official KRG documents
- Business communication
- International coordination
- Mixed calendar contexts

Use Kurdish calendar for:
- Cultural events
- Traditional contexts
- Historical records

## Month and Day Names

### Are the month and day names correct for my region?

The library uses documented sources:
- **Sorani/Kurmanji Gregorian**: Kurdistan Regional Government (gov.krd)
- **Hawrami days**: D.N. MacKenzie (1966)
- **Hawrami months**: Zaniary.com

Regional variations in dialect may exist due, the library represents the most standardised forms.

### Why are some names different from what I know?

Kurdish has regional variations. The library aims to use widely accepted forms based on authoritative sources. If a dialect is missing, please contribute with authoritive sources and references.

### Can I add my local dialect variation?

Yes! See the [Contributing Guide](contributing.md) for how to propose new dialects. We need:
- Complete month and day names
- Authoritative sources
- Native speaker validation

We are particularly keen to add:
- Pehlewani/Kirmashani
- Zaza

## Technical Questions

### Is the library thread-safe?

Yes. Date types are immutable value types (structs), making them inherently thread-safe. The internal equinox cache uses thread-safe operations.

### Can I use this in ASP.NET Core / Blazor / MAUI?

Yes. The library is a .NET Standard/Core library with no dependencies, so it works in any .NET application:
- ASP.NET Core web applications
- Blazor (Server and WebAssembly)
- .NET MAUI mobile apps
- Console applications
- Desktop applications (WPF, WinForms, Avalonia UI)

### Does it work on mobile devices?

Yes. Tested on:
- Android (via .NET MAUI)
- iOS (via .NET MAUI)
- All platforms that support .NET 10.0+

### What are the dependencies?

**Zero external dependencies.** The library only uses:
- System.Runtime
- System.Collections (for internal dictionaries)

This makes it lightweight and easy to integrate.

### Can I use this with Entity Framework?

Yes, but store as Gregorian DateTime in the database:

```csharp
public class Event
{
  public int Id { get; set; }
  
  // Store as DateTime in database
  public DateTime EventDateGregorian { get; set; }
  
  // Not mapped - computed from EventDateGregorian
  [NotMapped]
  public KurdishDate EventDateKurdish 
  { 
    get => KurdishDate.FromDateTime(EventDateGregorian);
    set => EventDateGregorian = value.ToDateTime();
  }
}
```

## Troubleshooting

### "ArgumentOutOfRangeException: Month must be between 1 and 12"

You're trying to create an invalid date:

```csharp
// Wrong - Month 13 doesn't exist
var date = new KurdishDate(2725, 13, 1);

// Correct
var date = new KurdishDate(2725, 12, 1);
```

### "FormatException: Unable to parse"

Parsing failed. Check:
1. Input format matches supported patterns
2. Dialect matches input language
3. Month names are spelled correctly

```csharp
// Use TryParse to handle errors gracefully
if (!KurdishDate.TryParse(input, dialect, out KurdishDate date))
{
  Console.WriteLine("Invalid format. Expected: dd/MM/yyyy or dd MonthName yyyy");
}
```

### Date is one day off

Check if you're mixing simplified and astronomical dates:

```csharp
// These might differ by a day
KurdishDate simplified = new KurdishDate(2725, 1, 1);
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

Console.WriteLine(simplified.ToDateTime());  // 2025-03-21
Console.WriteLine(astro.ToDateTime());       // 2025-03-20 (actual equinox)
```

### Arabic script or numerals not displaying correctly

Ensure you're using:
1. UTF-8 encoding in your files
2. UTF-8 output (some terminals do not support RTL)
2. Font that supports Eastern Arabic-Indic numerals
3. Correct dialect (e.g., `SoraniArabic` not `SoraniLatin`)

## Getting Help

### Where can I find more examples?

- [Examples](examples.md) — Comprehensive code examples
- [Getting Started](getting-started.md) — Tutorial
- [API Reference](api-reference.md) — Complete API documentation

### How do I report a bug?

See [Contributing Guide](contributing.md) for bug reporting guidelines.

### How do I request a feature?

Open a GitHub Issue with:
- Use case description
- Proposed API
- Example usage

### Still have questions?

- **Documentation**: Check all docs in `/docs` directory
- **GitHub Issues**: Search existing issues
- **Source Code**: Browse code for implementation details

---

**بەختێکی باش! (Good luck!)**