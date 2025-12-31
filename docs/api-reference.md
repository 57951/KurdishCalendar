---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - API Reference
---

# API Reference

Complete API documentation for Kurdish Calendar library.

## Table of Contents

1. [Core Types](#core-types)
2. [KurdishDate](#kurdishdate)
3. [KurdishAstronomicalDate](#kurdishastronomicaldate)
4. [KurdishDialect](#kurdishdialect)
5. [Formatters](#formatters)
6. [Parsers](#parsers)
7. [Extension Methods](#extension-methods)
8. [Culture Information](#culture-information)

## Core Types

### IKurdishDate

Base interface for all Kurdish date types.

```csharp
public interface IKurdishDate : IComparable<IKurdishDate>, IEquatable<IKurdishDate>
{
  int Year { get; }
  int Month { get; }
  int Day { get; }
  DayOfWeek DayOfWeek { get; }
  int DayOfYear { get; }
  bool IsLeapYear { get; }
  DateTime ToDateTime();
  string ToString();
}
```

## KurdishDate

Standard Kurdish date type using simplified calculation (fixed 21 March for Nowruz).

### Constructors

```csharp
// Create from year, month, day
public KurdishDate(int year, int month, int day)

// Create from Gregorian DateTime
public KurdishDate(DateTime gregorianDate)
```

**Parameters:**
- `year` — Kurdish year (must be 1 or greater)
- `month` — Month (1-12)
- `day` — Day of month
- `gregorianDate` — Gregorian DateTime to convert

**Exceptions:**
- `ArgumentOutOfRangeException` — Invalid year, month, or day

**Example:**
```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(new DateTime(2025, 3, 21));
```

### Static Properties

```csharp
public static KurdishDate Today { get; }
public static KurdishDate Now { get; }
```

**Returns:**
- `Today` — Current date (UTC) as KurdishDate
- `Now` — Current moment (UTC) as KurdishDate (time component ignored)

**Example:**
```csharp
KurdishDate today = KurdishDate.Today;
```

### Instance Properties

```csharp
public int Year { get; }
public int Month { get; }
public int Day { get; }
public DayOfWeek DayOfWeek { get; }
public int DayOfYear { get; }
public bool IsLeapYear { get; }
```

**Returns:**
- `Year` — Kurdish year
- `Month` — Month (1-12)
- `Day` — Day of month
- `DayOfWeek` — Day of week (.NET DayOfWeek enum)
- `DayOfYear` — Day of year (1-365/366)
- `IsLeapYear` — Whether this is a leap year

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
Console.WriteLine(date.Year);        // 2725
Console.WriteLine(date.DayOfYear);   // 166
Console.WriteLine(date.IsLeapYear);  // false
```

### Static Methods

#### FromDateTime

```csharp
public static KurdishDate FromDateTime(DateTime gregorianDate)
```

**Parameters:**
- `gregorianDate` — Gregorian DateTime to convert

**Returns:** Corresponding KurdishDate

**Example:**
```csharp
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishDate kurdish = KurdishDate.FromDateTime(gregorian);
```

#### Parse

```csharp
public static KurdishDate Parse(string input, KurdishDialect dialect)
```

**Parameters:**
- `input` — Date string to parse
- `dialect` — Dialect used in the string

**Returns:** Parsed KurdishDate

**Exceptions:**
- `FormatException` — Invalid date string

**Example:**
```csharp
KurdishDate date = KurdishDate.Parse("15 Xakelêwe 2725", KurdishDialect.SoraniLatin);
```

#### TryParse

```csharp
public static bool TryParse(string input, KurdishDialect dialect, out KurdishDate result)
```

**Parameters:**
- `input` — Date string to parse
- `dialect` — Dialect used in the string
- `result` — Output parameter for parsed date

**Returns:** `true` if parsing succeeded; otherwise, `false`

**Example:**
```csharp
if (KurdishDate.TryParse("15/01/2725", KurdishDialect.SoraniLatin, out KurdishDate date))
{
  Console.WriteLine($"Parsed: {date}");
}
```

### Instance Methods

#### ToDateTime

```csharp
public DateTime ToDateTime()
```

**Returns:** Corresponding Gregorian DateTime

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);
DateTime gregorian = date.ToDateTime(); // 2025-03-21
```

#### ToAstronomical

```csharp
public KurdishAstronomicalDate ToAstronomical()
public KurdishAstronomicalDate ToAstronomicalRecalculated()
public KurdishAstronomicalDate ToAstronomical(double longitudeDegrees)
```

**Parameters:**
- `longitudeDegrees` — Geographic longitude for astronomical calculations

**Returns:** KurdishAstronomicalDate

**Notes:**
- `ToAstronomical()` — Lossless conversion (keeps same Y/M/D)
- `ToAstronomicalRecalculated()` — Recalculates via Gregorian (may differ by 1-2 days)

**Example:**
```csharp
KurdishDate standard = new KurdishDate(2725, 1, 1);
KurdishAstronomicalDate astro = standard.ToAstronomical();
```

#### AddDays

```csharp
public KurdishDate AddDays(int days)
```

**Parameters:**
- `days` — Number of days to add (can be negative)

**Returns:** New KurdishDate

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);
KurdishDate tomorrow = date.AddDays(1);
KurdishDate yesterday = date.AddDays(-1);
```

#### AddMonths

```csharp
public KurdishDate AddMonths(int months)
```

**Parameters:**
- `months` — Number of months to add (can be negative)

**Returns:** New KurdishDate

**Notes:** If resulting day exceeds month length, adjusts to last day of month

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 31);
KurdishDate nextMonth = date.AddMonths(1); // Still 31st (Gulan has 31 days)
```

#### AddYears

```csharp
public KurdishDate AddYears(int years)
```

**Parameters:**
- `years` — Number of years to add (can be negative)

**Returns:** New KurdishDate

**Notes:** If resulting day exceeds month length, adjusts to last day of month

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);
KurdishDate nextYear = date.AddYears(1);
```

#### DaysDifference

```csharp
public int DaysDifference(KurdishDate other)
```

**Parameters:**
- `other` — Date to compare with

**Returns:** Number of days difference (can be negative)

**Example:**
```csharp
KurdishDate start = new KurdishDate(2725, 1, 1);
KurdishDate end = new KurdishDate(2725, 12, 29);
int days = end.DaysDifference(start); // 364
```

#### ToString

```csharp
public override string ToString()
public string ToString(string? format)
public string ToString(string? format, KurdishDialect dialect)
public string ToString(string? format, KurdishDialect dialect, KurdishTextDirection? textDirection)
```

**Parameters:**
- `format` — Format string (see format patterns below)
- `dialect` — Dialect for formatting
- `textDirection` — Text direction override

**Returns:** Formatted date string

**Format Patterns:**
- `d` — Short date (dd/MM/yyyy)
- `D` — Long date (day monthName year)
- `M`, `m` — Month and day
- `Y`, `y` — Year and month
- Custom — Use format tokens (dd, MM, MMMM, yyyy, etc.)

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
string s1 = date.ToString("d", KurdishDialect.SoraniLatin);    // "15/06/2725"
string s2 = date.ToString("D", KurdishDialect.SoraniLatin);    // "15 Xermanan 2725"
string s3 = date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin); // "15 Xermanan 2725"
```

#### CompareTo

```csharp
public int CompareTo(KurdishDate other)
public int CompareTo(object? obj)
```

**Parameters:**
- `other` — Date to compare with
- `obj` — Object to compare with

**Returns:**
- `-1` if this date is earlier
- `0` if dates are equal
- `1` if this date is later

**Example:**
```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);
int result = date1.CompareTo(date2); // -1
```

### Operators

```csharp
public static bool operator ==(KurdishDate left, KurdishDate right)
public static bool operator !=(KurdishDate left, KurdishDate right)
public static bool operator <(KurdishDate left, KurdishDate right)
public static bool operator >(KurdishDate left, KurdishDate right)
public static bool operator <=(KurdishDate left, KurdishDate right)
public static bool operator >=(KurdishDate left, KurdishDate right)

public static implicit operator KurdishDate(DateTime dateTime)
public static explicit operator DateTime(KurdishDate kurdishDate)
```

**Example:**
```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);

bool equal = date1 == date2;  // false
bool less = date1 < date2;    // true

// Implicit conversion
DateTime dt = new DateTime(2025, 3, 21);
KurdishDate kd = dt;  // Allowed

// Explicit conversion
DateTime dt2 = (DateTime)kd;
```

## KurdishAstronomicalDate

Kurdish date type using astronomical precision (actual spring equinox calculation).

### Constants

```csharp
public const double DefaultLongitude = 44.0; // Erbil
public const double SulaymaniyahLongitude = 45.0;
public const double TehranLongitude = 52.5;
```

### Constructors

```csharp
public KurdishAstronomicalDate(int year, int month, int day)
public KurdishAstronomicalDate(DateTime gregorianDate)
```

**Notes:** Default constructor uses Erbil longitude (44.0°E)

### Static Factory Methods

```csharp
public static KurdishAstronomicalDate FromErbil(int year, int month, int day)
public static KurdishAstronomicalDate FromSulaymaniyah(int year, int month, int day)
public static KurdishAstronomicalDate FromTehran(int year, int month, int day)
public static KurdishAstronomicalDate FromUtc(int year, int month, int day)
public static KurdishAstronomicalDate FromLongitude(int year, int month, int day, double longitudeDegrees)

public static KurdishAstronomicalDate FromDateTime(DateTime gregorianDate)
public static KurdishAstronomicalDate FromDateTime(DateTime gregorianDate, double longitudeDegrees)
```

**Example:**
```csharp
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
var custom = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);
```

### Static Properties

```csharp
public static KurdishAstronomicalDate Today { get; }
public static KurdishAstronomicalDate Now { get; }
```

### Instance Properties

```csharp
public int Year { get; }
public int Month { get; }
public int Day { get; }
public double Longitude { get; }
public DayOfWeek DayOfWeek { get; }
public int DayOfYear { get; }
public bool IsLeapYear { get; }
```

**Notes:** `IsLeapYear` determined by astronomical calculation (366 days between consecutive Nowruz dates)

### Instance Methods

All methods similar to `KurdishDate`, plus:

#### ToStandardDate

```csharp
public KurdishDate ToStandardDate()
public KurdishDate ToStandardDateRecalculated()
```

**Returns:** KurdishDate

**Notes:**
- `ToStandardDate()` — Lossless conversion
- `ToStandardDateRecalculated()` — Recalculates via Gregorian

**Example:**
```csharp
KurdishAstronomicalDate astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
KurdishDate standard = astro.ToStandardDate();
```

### Static Methods

#### Parse

```csharp
public static KurdishAstronomicalDate Parse(string input, KurdishDialect dialect, double longitude = 44.0)
```

#### TryParse

```csharp
public static bool TryParse(string input, KurdishDialect dialect, out KurdishAstronomicalDate result)
public static bool TryParse(string input, KurdishDialect dialect, double longitude, out KurdishAstronomicalDate result)
```

#### ClearEquinoxCache

```csharp
public static void ClearEquinoxCache()
public static void ClearEquinoxCache(int year)
```

**Notes:** Clears cached equinox calculations for performance or testing

**Example:**
```csharp
KurdishAstronomicalDate.ClearEquinoxCache(2725);
```

## KurdishDialect

Enumeration of supported dialect/script combinations.

```csharp
public enum KurdishDialect
{
  SoraniLatin,
  SoraniArabic,
  SoraniGregorianLatin,
  SoraniGregorianArabic,
  KurmanjiLatin,
  KurmanjiArabic,
  KurmanjiGregorianLatin,
  KurmanjiGregorianArabic,
  HawramiLatin,
  HawramiArabic
}
```

## Formatters

### GregorianKurmanjiFormatter

Static class for formatting Gregorian dates with Kurmanji month names.

#### ScriptType

```csharp
public enum ScriptType
{
  Latin,
  Arabic
}
```

#### Methods

```csharp
public static string GetMonthName(int month, ScriptType script = ScriptType.Latin, bool abbreviated = false)
public static string Format(DateTime date, ScriptType script = ScriptType.Latin, string? format = null)
public static string FormatShort(DateTime date, ScriptType script = ScriptType.Latin)
public static string FormatLong(DateTime date, ScriptType script = ScriptType.Latin)
```

**Example:**
```csharp
DateTime dt = new DateTime(2025, 12, 2);
string formatted = GregorianKurmanjiFormatter.Format(dt, ScriptType.Latin, "dd MMMM yyyy");
// Output: "02 Kanûna Êkê 2025"
```

### GregorianSoraniFormatter

Static class for formatting Gregorian dates with Sorani month names.

Methods identical to `GregorianKurmanjiFormatter`.

## Parsers

### KurdishDateParser

Static class providing parsing services.

```csharp
public static class KurdishDateParser
{
  public static KurdishDate Parse(string input, KurdishDialect dialect)
  public static bool TryParse(string input, KurdishDialect dialect, out KurdishDate result)
  
  public static KurdishAstronomicalDate ParseAstronomical(string input, KurdishDialect dialect, double longitude = 44.0)
  public static bool TryParseAstronomical(string input, KurdishDialect dialect, double longitude, out KurdishAstronomicalDate result)
}
```

**Supported Formats:**
- Numeric: `dd/MM/yyyy`, `dd-MM-yyyy`, `dd MM yyyy`
- Long: `dd MonthName yyyy` or `yyyy MonthName dd`
- Arabic numerals: Converted automatically

**Example:**
```csharp
KurdishDate date = KurdishDateParser.Parse("15 Xakelêwe 2725", KurdishDialect.SoraniLatin);
```

## Extension Methods

### DateTimeKurmanjiExtensions

```csharp
public static string ToKurmanjiGregorian(this DateTime date, 
  GregorianKurmanjiFormatter.ScriptType script = ScriptType.Latin,
  string? format = null)

public static string ToKurmanjiGregorianShort(this DateTime date, 
  GregorianKurmanjiFormatter.ScriptType script = ScriptType.Latin)

public static string ToKurmanjiGregorianLong(this DateTime date, 
  GregorianKurmanjiFormatter.ScriptType script = ScriptType.Latin)

public static string GetKurmanjiMonthName(this DateTime date, 
  GregorianKurmanjiFormatter.ScriptType script = ScriptType.Latin,
  bool abbreviated = false)
```

### DateTimeSoraniExtensions

Identical methods for Sorani dialect.

**Example:**
```csharp
DateTime dt = new DateTime(2025, 12, 2);
string formatted = dt.ToKurmanjiGregorian();
// Output: "2 Kanûna Êkê 2025"
```

## Culture Information

### KurdishCultureInfo

Static class providing culture-specific information.

```csharp
public static class KurdishCultureInfo
{
  public static string GetMonthName(int month, KurdishDialect dialect, bool abbreviated = false)
  public static string GetDayName(DayOfWeek dayOfWeek, KurdishDialect dialect, bool abbreviated = false)
  public static bool IsArabicScript(KurdishDialect dialect)
  public static string FormatNumber(int number, KurdishDialect dialect)
}
```

**Example:**
```csharp
string monthName = KurdishCultureInfo.GetMonthName(1, KurdishDialect.SoraniLatin);
// Output: "Xakelêwe"

string dayName = KurdishCultureInfo.GetDayName(DayOfWeek.Friday, KurdishDialect.SoraniLatin);
// Output: "Hênî"

bool isArabic = KurdishCultureInfo.IsArabicScript(KurdishDialect.SoraniArabic);
// Output: true

string arabicNumber = KurdishCultureInfo.FormatNumber(2725, KurdishDialect.SoraniArabic);
// Output: "٢٧٢٥"
```

## Enumerations

### KurdishMonth

```csharp
public enum KurdishMonth
{
  Xakelêwe = 1,
  Gulan = 2,
  Cozerdan = 3,
  Pûşper = 4,
  Gelawêj = 5,
  Xermanan = 6,
  Rezber = 7,
  Gelarêzan = 8,
  Sermawez = 9,
  Befranbar = 10,
  Rêbendan = 11,
  Reşeme = 12
}
```

### KurmanjiGregorianMonth

```csharp
public enum KurmanjiGregorianMonth
{
  KanunaDuye = 1,    // January
  Subat = 2,         // February
  Adar = 3,          // March
  Nisan = 4,         // April
  Gulan = 5,         // May
  Heziran = 6,       // June
  Tirmeh = 7,        // July
  Tebax = 8,         // August
  Eylul = 9,         // September
  CiriyaEke = 10,    // October
  CiriyaDuye = 11,   // November
  KanunaEke = 12     // December
}
```

### SoraniGregorianMonth

```csharp
public enum SoraniGregorianMonth
{
  KanuniDuhem = 1,   // January
  Subat = 2,         // February
  Adar = 3,          // March
  Nisan = 4,         // April
  Ayar = 5,          // May
  Huzeyran = 6,      // June
  Temmuz = 7,        // July
  Ab = 8,            // August
  Eylul = 9,         // September
  TisriniYekem = 10, // October
  TisriniDuhem = 11, // November
  KanuniYekem = 12   // December
}
```

### KurdishTextDirection

```csharp
public enum KurdishTextDirection
{
  LeftToRight,
  RightToLeft
}
```

## Thread Safety

All date types (`KurdishDate`, `KurdishAstronomicalDate`) are immutable `readonly struct` types, making them inherently thread-safe.

The equinox cache in `KurdishAstronomicalDate` uses thread-safe dictionary operations.

## Performance Notes

- **KurdishDate**: ~5μs per date creation
- **KurdishAstronomicalDate** (uncached): ~50μs per date creation
- **KurdishAstronomicalDate** (cached): ~0.1μs per date creation
- Conversions (lossless): ~0.1μs

## See Also

- [Getting Started](getting-started.md)
- [Examples](examples.md)
- [Formatting and Parsing](formatting-and-parsing.md)
- [Astronomical Calculations](astronomical-calculations.md)

---

**بەختێکی باش! (Good luck!)**