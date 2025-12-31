# KurdishCalendar.Examples

Comprehensive examples demonstrating the use of the Kurdish Calendar library.

## Overview

This project contains working examples for all major features of the KurdishCalendar.Core library, organised by category and complexity.

## Running the Examples

### Prerequisites

- **.NET 10.0 SDK** or higher

### Build and Run

```bash
# Clone repository
git clone https://github.com/57951/KurdishCalendar.git
cd KurdishCalendar/KurdishCalendar.Examples

# Restore dependencies
dotnet restore

# Build
dotnet build

# Run specific example
dotnet run -- <example-name>

# List all examples
dotnet run -- list
```

## Example Categories

### 1. Basic Operations

**BasicDateCreation.cs** — Creating dates in different ways
```csharp
// From year, month, day
KurdishDate date1 = new KurdishDate(2725, 1, 1);

// From Gregorian DateTime
DateTime gregorian = new DateTime(2025, 3, 21);
KurdishDate date2 = KurdishDate.FromDateTime(gregorian);

// Today and Now
KurdishDate today = KurdishDate.Today;
KurdishDate now = KurdishDate.Now;
```

**DateConversion.cs** — Converting between Kurdish and Gregorian
```csharp
// Kurdish to Gregorian
KurdishDate kurdish = new KurdishDate(2725, 6, 15);
DateTime gregorian = kurdish.ToDateTime();

// Gregorian to Kurdish
DateTime dt = new DateTime(2025, 9, 6);
KurdishDate kd = KurdishDate.FromDateTime(dt);
```

**DateProperties.cs** — Accessing date properties
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
Console.WriteLine($"Year: {date.Year}");
Console.WriteLine($"Month: {date.Month}");
Console.WriteLine($"Day: {date.Day}");
Console.WriteLine($"Day of Week: {date.DayOfWeek}");
Console.WriteLine($"Day of Year: {date.DayOfYear}");
Console.WriteLine($"Is Leap Year: {date.IsLeapYear}");
```

### 2. Date Arithmetic

**AddingDays.cs** — Adding and subtracting days
```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);
KurdishDate tomorrow = date.AddDays(1);
KurdishDate yesterday = date.AddDays(-1);
KurdishDate nextWeek = date.AddDays(7);
```

**AddingMonths.cs** — Adding and subtracting months
```csharp
KurdishDate date = new KurdishDate(2725, 1, 15);
KurdishDate nextMonth = date.AddMonths(1);
KurdishDate lastMonth = date.AddMonths(-1);
KurdishDate sixMonthsLater = date.AddMonths(6);
```

**CalculatingDifferences.cs** — Computing date differences
```csharp
KurdishDate start = new KurdishDate(2725, 1, 1);
KurdishDate end = new KurdishDate(2725, 12, 29);
int daysDifference = end.DaysDifference(start);
Console.WriteLine($"Days between: {daysDifference}");
```

### 3. Formatting

**BasicFormatting.cs** — Standard format strings
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Short date
Console.WriteLine(date.ToString("d", KurdishDialect.SoraniLatin));
// Output: "15/06/2725"

// Long date
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "15 Xermanan 2725"
```

**CustomFormatting.cs** — Custom format patterns
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

Console.WriteLine(date.ToString("dd MMMM yyyy", KurdishDialect.SoraniLatin));
// Output: "15 Xermanan 2725"

Console.WriteLine(date.ToString("dddd, d MMM yy", KurdishDialect.SoraniLatin));
// Output: "Hênî, 15 Xer 25"
```

**MultiDialectFormatting.cs** — Formatting in different dialects
```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "1 Xakelêwe 2725"

Console.WriteLine(date.ToString("D", KurdishDialect.SoraniArabic));
// Output: "١ خاکەلێوە ٢٧٢٥"

Console.WriteLine(date.ToString("D", KurdishDialect.HawramiLatin));
// Output: "1 Newroz 2725"
```

**ArabicScriptFormatting.cs** — Arabic script with Eastern Arabic-Indic numerals
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

Console.WriteLine(date.ToString("D", KurdishDialect.SoraniArabic));
// Output: "٢٧٢٥ خەرمانان ١٥" (RTL)

Console.WriteLine(date.ToString("d", KurdishDialect.SoraniArabic));
// Output: "١٥/٠٦/٢٧٢٥" (LTR numeric)
```

### 4. Parsing

**BasicParsing.cs** — Parsing date strings
```csharp
// Long format
KurdishDate date1 = KurdishDate.Parse(
  "15 Xermanan 2725", 
  KurdishDialect.SoraniLatin
);

// Short format
KurdishDate date2 = KurdishDate.Parse(
  "15/06/2725", 
  KurdishDialect.SoraniLatin
);
```

**SafeParsing.cs** — Using TryParse for safe parsing
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
```

**ParsingArabicScript.cs** — Parsing Arabic script dates
```csharp
KurdishDate date = KurdishDate.Parse(
  "١٥ خەرمانان ٢٧٢٥",
  KurdishDialect.SoraniArabic
);
```

### 5. Astronomical Dates

**AstronomicalBasics.cs** — Creating astronomical dates
```csharp
// Default Erbil
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

// Tehran meridian
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);

// Custom longitude
var custom = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.3661);
```

**EquinoxCalculation.cs** — Finding exact equinox moments
```csharp
KurdishAstronomicalDate newroz = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
DateTime equinoxMoment = newroz.ToDateTime();
Console.WriteLine($"Newroz 2726: {equinoxMoment:yyyy-MM-dd HH:mm:ss} UTC");
```

**LocationComparison.cs** — Comparing equinox across locations
```csharp
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
var london = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, -0.1276);

Console.WriteLine($"Erbil:  {erbil.ToDateTime():yyyy-MM-dd HH:mm:ss}");
Console.WriteLine($"Tehran: {tehran.ToDateTime():yyyy-MM-dd HH:mm:ss}");
Console.WriteLine($"London: {london.ToDateTime():yyyy-MM-dd HH:mm:ss}");
```

### 6. Gregorian Formatting

**GregorianSorani.cs** — Formatting Gregorian dates with Sorani names
```csharp
DateTime gregorian = new DateTime(2025, 1, 28);

string formatted = gregorian.ToSoraniGregorian();
Console.WriteLine(formatted);
// Output: "28 Kanûnî Duhem 2025"

string arabicFormatted = gregorian.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Arabic
);
Console.WriteLine(arabicFormatted);
// Output: "٢٨ کانونی دووەم ٢٠٢٥"
```

**GregorianKurmanji.cs** — Formatting Gregorian dates with Kurmanji names
```csharp
DateTime gregorian = new DateTime(2025, 5, 15);

string formatted = gregorian.ToKurmanjiGregorian();
Console.WriteLine(formatted);
// Output: "15 Gulan 2025"
```

### 7. Practical Applications

**BirthdayReminder.cs** — Birthday tracking system
```csharp
public void CheckBirthdays(List<Person> people)
{
  KurdishDate today = KurdishDate.Today;
  
  foreach (var person in people)
  {
    if (person.Birthday.Month == today.Month && 
        person.Birthday.Day == today.Day)
    {
      int age = today.Year - person.Birthday.Year;
      Console.WriteLine($"{person.Name} turns {age} today!");
    }
  }
}
```

**EventCalendar.cs** — Cultural event calendar
```csharp
public class CulturalEvent
{
  public string Name { get; set; }
  public KurdishDate Date { get; set; }
  public string Description { get; set; }
}

var events = new List<CulturalEvent>
{
  new CulturalEvent 
  { 
    Name = "Newroz", 
    Date = new KurdishDate(2725, 1, 1),
    Description = "Kurdish New Year" 
  }
};
```

**AgriculturalPlanner.cs** — Agricultural planning system
```csharp
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
```

**DocumentGenerator.cs** — Generating formatted documents
```csharp
public string GenerateDocumentHeader(DateTime issueDate, bool useSorani)
{
  string formatted = useSorani
    ? issueDate.ToSoraniGregorian()
    : issueDate.ToKurmanjiGregorian();
  
  return $"Document issued on: {formatted}";
}
```

### 8. Comparison and Sorting

**DateComparison.cs** — Comparing dates
```csharp
KurdishDate date1 = new KurdishDate(2725, 1, 1);
KurdishDate date2 = new KurdishDate(2725, 6, 15);

bool equal = date1 == date2;
bool lessThan = date1 < date2;
int comparison = date1.CompareTo(date2);
```

**DateSorting.cs** — Sorting date collections
```csharp
List<KurdishDate> dates = new List<KurdishDate>
{
  new KurdishDate(2725, 6, 15),
  new KurdishDate(2725, 1, 1),
  new KurdishDate(2724, 12, 29)
};

dates.Sort();
// Result: Chronological order
```

### 9. Edge Cases

**LeapYearHandling.cs** — Working with leap years
```csharp
KurdishDate leap = new KurdishDate(2725, 12, 30);    // Leap year
KurdishDate nonLeap = new KurdishDate(2724, 12, 29); // Non-leap year

Console.WriteLine($"2725 is leap: {leap.IsLeapYear}");
Console.WriteLine($"2724 is leap: {nonLeap.IsLeapYear}");
```

**MonthBoundaries.cs** — Month transition handling
```csharp
KurdishDate endOfMonth = new KurdishDate(2725, 1, 31);
KurdishDate nextDay = endOfMonth.AddDays(1);
Console.WriteLine(nextDay); // 1/2/2725
```

**YearBoundaries.cs** — Year transition handling
```csharp
KurdishDate endOfYear = new KurdishDate(2724, 12, 29);
KurdishDate nextDay = endOfYear.AddDays(1);
Console.WriteLine(nextDay); // 1/1/2725 (Newroz)
```

## Building the Examples

### Debug Build

```bash
dotnet build
```

### Release Build

```bash
dotnet build -c Release
```

### Run Without Building

```bash
dotnet run --no-build -- <example-name>
```

## Contributing Examples

We welcome new examples. Please:

1. **Follow existing structure** — Place in appropriate category
2. **Include documentation** — Explain what the example demonstrates
3. **Add to README** — Update this file with your example
4. **Test thoroughly** — Ensure example runs correctly
5. **Keep it simple** — Focus on one concept per example

See [CONTRIBUTING.md](../../docs/contributing.md) for full guidelines.

## Requirements

- .NET 10.0 SDK or higher
- KurdishCalendar.Core package (referenced automatically)

## See Also

- [Getting Started Guide](../../docs/getting-started.md)
- [API Reference](../../docs/api-reference.md)
- [Examples Documentation](../../docs/examples.md)
- [FAQ](../../docs/faq.md)

## Licence

MIT Licence — See [LICENSE.md](LICENSE.md) for details.

---

**بەختێکی باش! (Good luck!)**