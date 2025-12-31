---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Formatting and Parsing
---

# Formatting and Parsing

Complete guide to formatting and parsing Kurdish dates.

## Table of Contents

1. [Date Formatting](#date-formatting)
2. [Format Patterns](#format-patterns)
3. [Text Direction](#text-direction)
4. [Date Parsing](#date-parsing)
5. [Round-Trip Conversion](#round-trip-conversion)

## Date Formatting

### Standard Format Specifiers

The library supports standard .NET-style format specifiers:

| Specifier | Description | Example (Sorani Latin) |
|-----------|-------------|------------------------|
| `d` | Short date | "15/06/2725" |
| `D` | Long date | "15 Xermanan 2725" |
| `M`, `m` | Month and day | "15 Xermanan" |
| `Y`, `y` | Year and month | "Xermanan 2725" |

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

string shortDate = date.ToString("d", KurdishDialect.SoraniLatin);
// Output: "15/06/2725"

string longDate = date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

string monthDay = date.ToString("M", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan"

string yearMonth = date.ToString("Y", KurdishDialect.SoraniLatin);
// Output: "Xermanan 2725"
```

### Custom Format Patterns

Build custom format strings using format tokens:

| Token | Description | Example |
|-------|-------------|---------|
| `d` | Day (1-31) | "5", "15" |
| `dd` | Day (01-31) | "05", "15" |
| `ddd` | Abbreviated day name | "Hên" |
| `dddd` | Full day name | "Hênî" |
| `M` | Month (1-12) | "1", "12" |
| `MM` | Month (01-12) | "01", "12" |
| `MMM` | Abbreviated month name | "Xak", "Xer" |
| `MMMM` | Full month name | "Xakelêwe", "Xermanan" |
| `y`, `yy` | Two-digit year | "25" |
| `yyyy` | Four-digit year | "2725" |

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Day month year
string format1 = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

// Day of week, day month year
string format2 = date.ToString("dddd, d MMMM yyyy", KurdishDialect.SoraniLatin);
// Output: "Hênî, 15 Xermanan 2725"

// Abbreviated formats
string format3 = date.ToString("ddd, d MMM yy", KurdishDialect.SoraniLatin);
// Output: "Hên, 15 Xer 25"

// Numeric with slashes
string format4 = date.ToString("dd/MM/yyyy", KurdishDialect.SoraniLatin);
// Output: "15/06/2725"
```

### Dialect-Specific Formatting

Each dialect produces different month and day names:

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Sorani Latin
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "1 Xakelêwe 2725"

// Sorani Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniArabic));
// Output: "١ خاکەلێوە ٢٧٢٥"

// Kurmanji Latin
Console.WriteLine(date.ToString("D", KurdishDialect.KurmanjiLatin));
// Output: "1 Xakelêwe 2725"

// Kurmanji Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.KurmanjiArabic));
// Output: "١ خاکەلێوە ٢٧٢٥"

// Hawrami Latin
Console.WriteLine(date.ToString("D", KurdishDialect.HawramiLatin));
// Output: "1 Newroz 2725"

// Hawrami Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.HawramiArabic));
// Output: "١ نەوروز ٢٧٢٥"
```

### Arabic Script Formatting

Arabic script uses Eastern Arabic-Indic numerals (٠-٩):

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Automatic numeral conversion
string formatted = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniArabic);
// Output: "١٥ خەرمانان ٢٧٢٥"

// Short format
string shortFormat = date.ToString("d", KurdishDialect.SoraniArabic);
// Output: "١٥/٠٦/٢٧٢٥"
```

**Eastern Arabic-Indic Numerals:**
- ٠ (0), ١ (1), ٢ (2), ٣ (3), ٤ (4)
- ٥ (5), ٦ (6), ٧ (7), ٨ (8), ٩ (9)

## Format Patterns

### Common Patterns

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
KurdishDialect dialect = KurdishDialect.SoraniLatin;

// ISO 8601-style
date.ToString("yyyy-MM-dd", dialect);
// Output: "2725-06-15"

// Long with day of week
date.ToString("dddd, dd MMMM yyyy", dialect);
// Output: "Hênî, 15 Xermanan 2725"

// Month and year only
date.ToString("MMMM yyyy", dialect);
// Output: "Xermanan 2725"

// Abbreviated
date.ToString("ddd, MMM d, yy", dialect);
// Output: "Hên, Xer 15, 25"
```

### Escape Sequences

Use quotes to include literal text:

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Single quotes
string format1 = date.ToString("'Newroz' yyyy", KurdishDialect.SoraniLatin);
// Output: "Newroz 2725"

// Double quotes
string format2 = date.ToString("\"Year\" yyyy", KurdishDialect.SoraniLatin);
// Output: "Year 2725"

// Backslash escape
string format3 = date.ToString(@"yyyy\-MM\-dd", KurdishDialect.SoraniLatin);
// Output: "2725-06-15"
```

## Text Direction

### Default Text Direction

Text direction is determined by script:

- **Latin script**: Left-to-Right (LTR)
- **Arabic script**: Right-to-Left (RTL) for long format, LTR for numeric format

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Latin script (LTR): day month year
string latin = date.ToString("D", KurdishDialect.SoraniLatin);
// Output: "15 Xermanan 2725"

// Arabic script (RTL): year month day
string arabic = date.ToString("D", KurdishDialect.SoraniArabic);
// Output: "٢٧٢٥ خەرمانان ١٥"

// Numeric format is always LTR: day/month/year
string numericArabic = date.ToString("d", KurdishDialect.SoraniArabic);
// Output: "١٥/٠٦/٢٧٢٥"
```

### Overriding Text Direction

Explicitly specify text direction when needed:

```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Force RTL for Latin script
string rtlLatin = date.ToString(
  "D", 
  KurdishDialect.SoraniLatin, 
  KurdishTextDirection.RightToLeft
);
// Output: "2725 Xermanan 15"

// Force LTR for Arabic script
string ltrArabic = date.ToString(
  "D", 
  KurdishDialect.SoraniArabic, 
  KurdishTextDirection.LeftToRight
);
// Output: "١٥ خەرمانان ٢٧٢٥"
```

## Date Parsing

### Supported Input Formats

The parser handles multiple formats:

1. **Numeric formats**: `dd/MM/yyyy`, `dd-MM-yyyy`, `dd MM yyyy`
2. **Long formats**: `dd MonthName yyyy` or `yyyy MonthName dd`
3. **Mixed order**: Automatic detection based on number magnitude

```csharp
// Numeric format (day/month/year for Latin)
KurdishDate date1 = KurdishDate.Parse("15/06/2725", KurdishDialect.SoraniLatin);

// Numeric with different separators
KurdishDate date2 = KurdishDate.Parse("15-06-2725", KurdishDialect.SoraniLatin);
KurdishDate date3 = KurdishDate.Parse("15 06 2725", KurdishDialect.SoraniLatin);

// Long format (LTR)
KurdishDate date4 = KurdishDate.Parse("15 Xermanan 2725", KurdishDialect.SoraniLatin);

// Long format (RTL - auto-detected)
KurdishDate date5 = KurdishDate.Parse("2725 Xermanan 15", KurdishDialect.SoraniLatin);
```

### Arabic Script Parsing

Arabic script uses Eastern Arabic-Indic numerals:

```csharp
// Numeric format (RTL: year/month/day for Arabic)
KurdishDate date1 = KurdishDate.Parse("٢٧٢٥/٠٦/١٥", KurdishDialect.SoraniArabic);

// Long format with Arabic month names
KurdishDate date2 = KurdishDate.Parse("١٥ خەرمانان ٢٧٢٥", KurdishDialect.SoraniArabic);

// RTL order
KurdishDate date3 = KurdishDate.Parse("٢٧٢٥ خەرمانان ١٥", KurdishDialect.SoraniArabic);
```

**Automatic Numeral Conversion:**
The parser automatically converts Eastern Arabic-Indic numerals to Western numerals:
- ٠→0, ١→1, ٢→2, ٣→3, ٤→4
- ٥→5, ٦→6, ٧→7, ٨→8, ٩→9

### Case-Insensitive Parsing

Month names are matched case-insensitively:

```csharp
// All of these work
KurdishDate.Parse("15 XERMANAN 2725", KurdishDialect.SoraniLatin);
KurdishDate.Parse("15 xermanan 2725", KurdishDialect.SoraniLatin);
KurdishDate.Parse("15 Xermanan 2725", KurdishDialect.SoraniLatin);
```

### Safe Parsing with TryParse

Use `TryParse` for user input to avoid exceptions:

```csharp
string userInput = GetUserInput();

if (KurdishDate.TryParse(userInput, KurdishDialect.SoraniLatin, out KurdishDate result))
{
  Console.WriteLine($"Valid date: {result}");
}
else
{
  Console.WriteLine("Invalid date format. Please use: dd/MM/yyyy or dd MonthName yyyy");
}
```

### Parsing Gregorian Dates with Kurdish Month Names

Parse Gregorian dates formatted with Kurdish month names:

```csharp
// Sorani Gregorian month names
KurdishDate date1 = KurdishDate.Parse(
  "15 Kanûnî Duhem 2025", 
  KurdishDialect.SoraniGregorianLatin
);

// Kurmanji Gregorian month names
KurdishDate date2 = KurdishDate.Parse(
  "15 Kanûna Duyê 2025", 
  KurdishDialect.KurmanjiGregorianLatin
);

// Arabic script
KurdishDate date3 = KurdishDate.Parse(
  "١٥ کانونی دووەم ٢٠٢٥",
  KurdishDialect.SoraniGregorianArabic
);
```

### Parsing Astronomical Dates

Parse dates with astronomical precision:

```csharp
// With default Erbil longitude
KurdishAstronomicalDate astro1 = KurdishAstronomicalDate.Parse(
  "1 Xakelêwe 2725",
  KurdishDialect.SoraniLatin
);

// With custom longitude
KurdishAstronomicalDate astro2 = KurdishAstronomicalDate.Parse(
  "1 Xakelêwe 2725",
  KurdishDialect.SoraniLatin,
  45.4375  // Sulaymaniyah
);

// Safe parsing
if (KurdishAstronomicalDate.TryParse(
  "1 Xakelêwe 2725",
  KurdishDialect.SoraniLatin,
  44.0,
  out KurdishAstronomicalDate result))
{
  Console.WriteLine($"Astronomical date: {result.ToDateTime():yyyy-MM-dd}");
}
```

## Round-Trip Conversion

### Format and Parse Back

Verify that formatting and parsing are reversible:

```csharp
KurdishDate original = new KurdishDate(2725, 6, 15);

// Format to string
string formatted = original.ToString("D", KurdishDialect.SoraniLatin);
Console.WriteLine($"Formatted: {formatted}");
// Output: "15 Xermanan 2725"

// Parse back
KurdishDate parsed = KurdishDate.Parse(formatted, KurdishDialect.SoraniLatin);

// Verify equality
bool isEqual = original == parsed;
Console.WriteLine($"Round-trip successful: {isEqual}");
// Output: "Round-trip successful: True"
```

### All Dialects Round-Trip

```csharp
KurdishDate original = new KurdishDate(2725, 1, 1);

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
  string formatted = original.ToString("D", dialect);
  KurdishDate parsed = KurdishDate.Parse(formatted, dialect);
  
  bool success = original == parsed;
  Console.WriteLine($"{dialect}: {success}");
}
// All output: True
```

## Best Practices

### 1. Always Specify Dialect

```csharp
// Good
string formatted = date.ToString("D", KurdishDialect.SoraniLatin);

// Avoid (uses default)
string formatted = date.ToString();
```

### 2. Use TryParse for User Input

```csharp
// Good - handles invalid input gracefully
if (KurdishDate.TryParse(input, dialect, out KurdishDate date))
{
  // Process date
}

// Avoid - may throw exception
KurdishDate date = KurdishDate.Parse(input, dialect);
```

### 3. Match Format to Usage

```csharp
// For display to users
string display = date.ToString("D", dialect);  // Long format

// For storage/transmission
string storage = date.ToString("yyyy-MM-dd", dialect);  // ISO format

// For logs
string log = date.ToString("yyyy-MM-dd HH:mm:ss", dialect);  // If you need timestamp
```

### 4. Be Consistent with Text Direction

```csharp
// Let the library handle it based on script
string formatted = date.ToString("D", KurdishDialect.SoraniArabic);

// Only override when you have a specific requirement
string customDirection = date.ToString(
  "D", 
  KurdishDialect.SoraniArabic,
  KurdishTextDirection.LeftToRight
);
```

### 5. Validate Parsed Dates

```csharp
if (KurdishDate.TryParse(input, dialect, out KurdishDate date))
{
  // Additional validation
  if (date.Year < 2700 || date.Year > 2800)
  {
    Console.WriteLine("Year out of expected range");
    return;
  }
  
  // Process valid date
  ProcessDate(date);
}
```

## Error Handling

### Parse Exceptions

```csharp
try
{
  KurdishDate date = KurdishDate.Parse("invalid", KurdishDialect.SoraniLatin);
}
catch (FormatException ex)
{
  Console.WriteLine($"Parsing failed: {ex.Message}");
}
```

### Common Parsing Errors

1. **Invalid month name**: "15 InvalidMonth 2725"
2. **Invalid date values**: "32/13/2725"
3. **Wrong separator**: "15.06.2725" (use `/`, `-`, or space)
4. **Missing components**: "15 2725" (missing month)
5. **Wrong dialect**: Parsing Arabic text with Latin dialect

## See Also

- [API Reference](api-reference.md) — Complete API documentation
- [Examples](examples.md) — Practical code examples
- [Dialects and Scripts](dialects-and-scripts.md) — Language support details

---

**بەختێکی باش! (Good luck!)**