---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Gregorian Formatting
---

# Gregorian Formatting

Guide to formatting Gregorian dates with Kurdish month names.

## Overview

The Kurdish Calendar library supports formatting Gregorian calendar dates with Kurdish month names in both Sorani and Kurmanji dialects. This is commonly used in Kurdistan Region of Iraq and other Kurdish-speaking areas that use the Gregorian calendar with localised month names.

## Use Cases

### When to Use Gregorian Formatting

✅ **Official Documents** — Government forms and certificates in KRG
✅ **Business Communication** — Invoices, contracts, letters
✅ **International Coordination** — When working with Gregorian-using systems
✅ **Modern Applications** — Apps that need both Kurdish names and Gregorian dates
✅ **Mixed Calendars** — Documents showing both calendar systems

### When to Use Kurdish Calendar

✅ **Cultural Events** — Nowruz and traditional celebrations
✅ **Historical Records** — Genealogy and historical documentation
✅ **Traditional Context** — When Kurdish calendar is the primary system

## Sorani Gregorian Formatting

### Month Names (Sorani)

**Source:** Kurdistan Regional Government (gov.krd)

| Month | Latin | Arabic |
|-------|-------|--------|
| January | Kanûnî Duhem | کانونی دووەم |
| February | Şubat | شوبات |
| March | Adar | ئادار |
| April | Nîsan | نیسان |
| May | Ayar | ئایار |
| June | Hûzeyran | حوزەیران |
| July | Temmûz | تەمووز |
| August | Ab | ئاب |
| September | Eylûl | ئەیلول |
| October | Tişrînî Yekem | تشرینی یەکەم |
| November | Tişrînî Duhem | تشرینی دووەم |
| December | Kanûnî Yekem | کانونی یەکەم |

### Basic Usage

```csharp
using KurdishCalendar.Core;

DateTime gregorian = new DateTime(2025, 1, 28);

// Default (Latin script, long format)
string formatted = gregorian.ToSoraniGregorian();
Console.WriteLine(formatted);
// Output: "28 Kanûnî Duhem 2025"

// Arabic script
string arabicFormatted = gregorian.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Arabic
);
Console.WriteLine(arabicFormatted);
// Output: "٢٨ کانونی دووەم ٢٠٢٥"
```

### Format Methods

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Long format (default)
string longFormat = date.ToSoraniGregorianLong();
// Output: "15 Ayar 2025"

// Short format (abbreviated month)
string shortFormat = date.ToSoraniGregorianShort();
// Output: "15 Aya 2025"

// Custom format
string customFormat = date.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Latin,
  "dd MMMM yyyy"
);
// Output: "15 Ayar 2025"
```

### Getting Month Names

```csharp
DateTime date = new DateTime(2025, 12, 2);

// Full month name
string monthName = date.GetSoraniMonthName();
// Output: "Kanûnî Yekem"

// Abbreviated month name
string monthAbbr = date.GetSoraniMonthName(abbreviated: true);
// Output: "Kan1"

// Arabic script
string monthArabic = date.GetSoraniMonthName(
  GregorianSoraniFormatter.ScriptType.Arabic
);
// Output: "کانونی یەکەم"
```

## Kurmanji Gregorian Formatting

### Month Names (Kurmanji)

**Source:** Kurdistan Regional Government (gov.krd)

| Month | Latin | Arabic |
|-------|-------|--------|
| January | Kanûna Duyê | کانوونا دویێ |
| February | Şubat | شوبات |
| March | Adar | ئادار |
| April | Nîsan | نیسان |
| May | Gulan | گولان |
| June | Hezîran | حەزیران |
| July | Tîrmeh | تیرمەه |
| August | Tebax | تەباخ |
| September | Eylûl | ئەیلووڵ |
| October | Çiriya Êkê | چریا ێکێ |
| November | Çiriya Duyê | چریا دویێ |
| December | Kanûna Êkê | کانوونا ێکێ |

### Basic Usage

```csharp
DateTime gregorian = new DateTime(2025, 1, 28);

// Default (Latin script, long format)
string formatted = gregorian.ToKurmanjiGregorian();
Console.WriteLine(formatted);
// Output: "28 Kanûna Duyê 2025"

// Arabic script
string arabicFormatted = gregorian.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
Console.WriteLine(arabicFormatted);
// Output: "٢٨ کانوونا دویێ ٢٠٢٥"
```

### Format Methods

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Long format (default)
string longFormat = date.ToKurmanjiGregorianLong();
// Output: "15 Gulan 2025"

// Short format (abbreviated month)
string shortFormat = date.ToKurmanjiGregorianShort();
// Output: "15 Gul 2025"

// Custom format
string customFormat = date.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Latin,
  "dd MMMM yyyy"
);
// Output: "15 Gulan 2025"
```

### Getting Month Names

```csharp
DateTime date = new DateTime(2025, 12, 2);

// Full month name
string monthName = date.GetKurmanjiMonthName();
// Output: "Kanûna Êkê"

// Abbreviated month name
string monthAbbr = date.GetKurmanjiMonthName(abbreviated: true);
// Output: "Kan Êk"

// Arabic script
string monthArabic = date.GetKurmanjiMonthName(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
// Output: "کانوونا ێکێ"
```

## Custom Format Strings

### Supported Format Tokens

| Token | Description | Example |
|-------|-------------|---------|
| `d` | Day (1-31) | "5", "15" |
| `dd` | Day (01-31) | "05", "15" |
| `M` | Month (1-12) | "1", "12" |
| `MM` | Month (01-12) | "01", "12" |
| `MMM` | Abbreviated month name | "Kan2", "Gul" |
| `MMMM` | Full month name | "Kanûnî Duhem", "Gulan" |
| `yy` | Two-digit year | "25" |
| `yyyy` | Four-digit year | "2025" |

### Format Examples

```csharp
DateTime date = new DateTime(2025, 5, 15);

// ISO 8601-style
string iso = GregorianSoraniFormatter.Format(
  date, 
  GregorianSoraniFormatter.ScriptType.Latin,
  "yyyy-MM-dd"
);
// Output: "2025-05-15"

// Full month and year
string monthYear = GregorianKurmanjiFormatter.Format(
  date,
  GregorianKurmanjiFormatter.ScriptType.Latin,
  "MMMM yyyy"
);
// Output: "Gulan 2025"

// Day and abbreviated month
string dayMonth = GregorianSoraniFormatter.Format(
  date,
  GregorianSoraniFormatter.ScriptType.Latin,
  "d MMM"
);
// Output: "15 Aya"

// Short year format
string shortYear = GregorianKurmanjiFormatter.Format(
  date,
  GregorianKurmanjiFormatter.ScriptType.Latin,
  "dd/MM/yy"
);
// Output: "15/05/25"
```

## Text Direction

### Default Text Direction

Text direction is determined by script:

- **Latin script**: Left-to-Right (LTR)
- **Arabic script**: Right-to-Left (RTL)

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Latin (LTR): day month year
string latin = date.ToSoraniGregorianLong();
// Output: "15 Ayar 2025"

// Arabic (RTL): year month day
string arabic = date.ToSoraniGregorianLong(
  GregorianSoraniFormatter.ScriptType.Arabic
);
// Output: "٢٠٢٥ ئایار ١٥"
```

### Overriding Text Direction

```csharp
DateTime date = new DateTime(2025, 5, 15);

// Force LTR for Arabic script
string ltrArabic = GregorianSoraniFormatter.FormatLong(
  date,
  GregorianSoraniFormatter.ScriptType.Arabic,
  KurdishTextDirection.LeftToRight
);
// Output: "١٥ ئایار ٢٠٢٥"

// Force RTL for Latin script
string rtlLatin = GregorianSoraniFormatter.FormatLong(
  date,
  GregorianSoraniFormatter.ScriptType.Latin,
  KurdishTextDirection.RightToLeft
);
// Output: "2025 Ayar 15"
```

## Arabic Script Numerals

Arabic script formatting uses Eastern Arabic-Indic numerals (٠-٩):

```csharp
DateTime date = new DateTime(2025, 12, 28);

string formatted = date.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
// Output: "٢٨ کانوونا ێکێ ٢٠٢٥"
```

**Numeral Mapping:**
- 0→٠, 1→١, 2→٢, 3→٣, 4→٤
- 5→٥, 6→٦, 7→٧, 8→٨, 9→٩

## Practical Examples

### Document Header

```csharp
public string GenerateDocumentHeader(DateTime issueDate, bool useSorani)
{
  string dateFormatted;
  
  if (useSorani)
  {
    dateFormatted = issueDate.ToSoraniGregorianLong();
  }
  else
  {
    dateFormatted = issueDate.ToKurmanjiGregorianLong();
  }
  
  return $"Document issued on: {dateFormatted}";
}

// Usage
DateTime today = DateTime.Today;
string header = GenerateDocumentHeader(today, useSorani: true);
// Output: "Document issued on: 30 Kanûnî Yekem 2024"
```

### Invoice Formatting

```csharp
public class Invoice
{
  public DateTime InvoiceDate { get; set; }
  public DateTime DueDate { get; set; }
  
  public string FormatInvoiceDates(bool useArabicScript)
  {
    var script = useArabicScript 
      ? GregorianKurmanjiFormatter.ScriptType.Arabic 
      : GregorianKurmanjiFormatter.ScriptType.Latin;
    
    string invoiceDateStr = InvoiceDate.ToKurmanjiGregorian(script, "dd MMMM yyyy");
    string dueDateStr = DueDate.ToKurmanjiGregorian(script, "dd MMMM yyyy");
    
    return $"Invoice Date: {invoiceDateStr}\nDue Date: {dueDateStr}";
  }
}

// Usage
var invoice = new Invoice
{
  InvoiceDate = new DateTime(2025, 1, 15),
  DueDate = new DateTime(2025, 2, 15)
};

Console.WriteLine(invoice.FormatInvoiceDates(useArabicScript: false));
// Output:
// Invoice Date: 15 Kanûna Duyê 2025
// Due Date: 15 Şubat 2025
```

### Calendar Display

```csharp
public void DisplayMonthCalendar(int year, int month, bool useSorani)
{
  DateTime firstDay = new DateTime(year, month, 1);
  
  string monthName = useSorani
    ? firstDay.GetSoraniMonthName()
    : firstDay.GetKurmanjiMonthName();
  
  Console.WriteLine($"{monthName} {year}");
  Console.WriteLine("Su Mo Tu We Th Fr Sa");
  
  // Display calendar grid...
}

// Usage
DisplayMonthCalendar(2025, 5, useSorani: true);
// Output:
// Ayar 2025
// Su Mo Tu We Th Fr Sa
// ...
```

### Bilingual Date Display

```csharp
public string FormatBilingualDate(DateTime date)
{
  string sorani = date.ToSoraniGregorianLong();
  string kurmanji = date.ToKurmanjiGregorianLong();
  string gregorian = date.ToString("dd MMMM yyyy"); // English
  
  return $"{sorani} / {kurmanji}\n{gregorian}";
}

// Usage
DateTime date = new DateTime(2025, 5, 15);
Console.WriteLine(FormatBilingualDate(date));
// Output:
// 15 Ayar 2025 / 15 Gulan 2025
// 15 May 2025
```

## Comparison with Kurdish Calendar

### Same Date, Different Calendars

```csharp
DateTime gregorian = new DateTime(2025, 3, 21);

// Gregorian with Kurdish month names
string gregorianSorani = gregorian.ToSoraniGregorian();
Console.WriteLine($"Gregorian: {gregorianSorani}");
// Output: "Gregorian: 21 Adar 2025"

// Kurdish calendar
KurdishDate kurdish = KurdishDate.FromDateTime(gregorian);
string kurdishFormatted = kurdish.ToString("D", KurdishDialect.SoraniLatin);
Console.WriteLine($"Kurdish:   {kurdishFormatted}");
// Output: "Kurdish:   1 Xakelêwe 2725"
```

### When to Use Which

| Context | Use |
|---------|-----|
| KRG official documents | Gregorian with Kurdish names |
| International business | Gregorian with Kurdish names |
| Cultural events | Kurdish calendar |
| Historical records | Kurdish calendar |
| Modern applications | Both (bilingual) |

## Parsing Gregorian Dates with Kurdish Names

```csharp
// Parse Gregorian date with Sorani month names
KurdishDate date1 = KurdishDate.Parse(
  "15 Kanûnî Duhem 2025",
  KurdishDialect.SoraniGregorianLatin
);

// This creates a KurdishDate from the Gregorian input
DateTime gregorian1 = date1.ToDateTime();
Console.WriteLine(gregorian1.ToString("yyyy-MM-dd"));
// Output: "2025-01-15"

// Parse with Kurmanji month names
KurdishDate date2 = KurdishDate.Parse(
  "15 Kanûna Duyê 2025",
  KurdishDialect.KurmanjiGregorianLatin
);

DateTime gregorian2 = date2.ToDateTime();
Console.WriteLine(gregorian2.ToString("yyyy-MM-dd"));
// Output: "2025-01-15"
```

## Best Practices

### 1. Be Consistent with Dialect

```csharp
// Good - consistent dialect throughout application
public class ApplicationSettings
{
  public bool UseSoraniDialect { get; set; } = true;
  
  public string FormatGregorianDate(DateTime date)
  {
    return UseSoraniDialect 
      ? date.ToSoraniGregorian()
      : date.ToKurmanjiGregorian();
  }
}
```

### 2. Provide Script Selection

```csharp
// Good - let users choose script preference
public enum ScriptPreference
{
  Latin,
  Arabic
}

public string FormatDate(DateTime date, ScriptPreference script)
{
  var scriptType = script == ScriptPreference.Arabic
    ? GregorianSoraniFormatter.ScriptType.Arabic
    : GregorianSoraniFormatter.ScriptType.Latin;
  
  return date.ToSoraniGregorian(scriptType);
}
```

### 3. Match Format to Purpose

```csharp
// Short format for space-constrained UIs
string uiDate = date.ToSoraniGregorianShort();

// Long format for documents and formal communication
string documentDate = date.ToSoraniGregorianLong();

// ISO format for data storage and APIs
string storageDate = GregorianSoraniFormatter.Format(
  date,
  GregorianSoraniFormatter.ScriptType.Latin,
  "yyyy-MM-dd"
);
```

### 4. Document Which Calendar You're Using

```csharp
/// <summary>
/// Stores the contract signing date using Gregorian calendar
/// with Sorani Kurdish month names for official KRG documents.
/// </summary>
public DateTime ContractDate { get; set; }

/// <summary>
/// Stores cultural event date using Kurdish calendar
/// for traditional celebrations like Nowruz.
/// </summary>
public KurdishDate CulturalEventDate { get; set; }
```

## Month Name Etymology

### Syriac/Aramaic Origin

Both Sorani and Kurmanji Gregorian month names derive from Syriac/Aramaic:

- **Kanûn** (ܟܢܘܢ) — "hearth" or "brazier"
- **Şubat** (ܫܒܛ) — from Hebrew Shevat
- **Adar** (ܐܕܪ) — from Hebrew Adar
- **Nîsan** (ܢܝܣܢ) — from Akkadian Nisannu
- **Tişrîn** (ܬܫܪܝܢ) — "beginning"

These names are shared across the Levant and Mesopotamia in various forms.

## See Also

- [Dialects and Scripts](dialects-and-scripts.md) — Language support details
- [Formatting and Parsing](formatting-and-parsing.md) — Format string details
- [API Reference](api-reference.md) — Complete API documentation
- [Examples](examples.md) — Practical code examples

---

**بەختێکی باش! (Good luck!)**