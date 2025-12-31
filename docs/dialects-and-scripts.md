---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Dialects and Scripts
---

# Dialects and Scripts

Complete guide to Kurdish dialects and script support in the Kurdish Calendar library.

## Overview

The library supports three major Kurdish dialects (Sorani, Kurmanji, and Hawrami) in both Latin and Arabic scripts, plus Gregorian calendar formatting with Kurdish Sorani and Kurdish Kurmanji month names.

## Supported Combinations

| Calendar | Dialect | Latin Script | Arabic Script |
|----------|---------|--------------|---------------|
| **Kurdish Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |
| | Hawrami | ✓ | ✓ |
| **Gregorian Calendar** | Sorani | ✓ | ✓ |
| | Kurmanji | ✓ | ✓ |

Total: 10 dialect/script combinations

## Kurdish Calendar Dialects

### Sorani

Sorani is spoken primarily in Iraqi Kurdistan and Iranian Kurdistan.

#### Month Names

**Latin Script:**
1. Xakelêwe
2. Gulan
3. Cozerdan
4. Pûşper
5. Gelawêj
6. Xermanan
7. Rezber
8. Gelarêzan
9. Sermawez
10. Befranbar
11. Rêbendan
12. Reşeme

**Arabic Script:**
1. خاکەلێوە
2. گوڵان
3. جۆزەردان
4. پووشپەڕ
5. گەلاوێژ
6. خەرمانان
7. ڕەزبەر
8. گەڵاڕێزان
9. سەرماوەز
10. بەفرانبار
11. ڕێبەندان
12. ڕەشەمە

#### Day Names

**Latin Script:**
- Yekşemme (Sunday)
- Duşemme (Monday)
- Sêşemme (Tuesday)
- Çwarşemme (Wednesday)
- Pêncşemme (Thursday)
- Hênî (Friday)
- Şemme (Saturday)

**Arabic Script:**
- یەکشەممە (Sunday)
- دووشەممە (Monday)
- سێشەممە (Tuesday)
- چوارشەممە (Wednesday)
- پێنجشەممە (Thursday)
- ھەینی (Friday)
- شەممە (Saturday)

**Usage:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Sorani Latin
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "1 Xakelêwe 2725"

// Sorani Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniArabic));
// Output: "١ خاکەلێوە ٢٧٢٥"
```

### Kurmanji

Kurmanji is spoken in Turkey, Syria, parts of Iraq, and diaspora communities.

#### Month Names

Same as Sorani for the Kurdish calendar (Xakelêwe, Gulan, etc.).

**Note:** Kurdish calendar month names are shared across Sorani and Kurmanji. The primary difference is in Gregorian calendar month names (see Gregorian Dialects section below).

**Usage:**
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);

// Kurmanji Latin
Console.WriteLine(date.ToString("D", KurdishDialect.KurmanjiLatin));
// Output: "15 Xermanan 2725"

// Kurmanji Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.KurmanjiArabic));
// Output: "١٥ خەرمانان ٢٧٢٥"
```

### Hawrami

Hawrami (Gorani) is spoken in parts of Iraqi and Iranian Kurdistan.

#### Month Names

**Source:** Zaniary.com (https://zaniary.com/blog/61ec1e25bfc4e)

**Latin Script:**
1. Newroz
2. Pajerej
3. Çêlkirr
4. Kopir
5. Gelawêj
6. Awewere
7. Tarazî
8. Gellaxezan
9. Kelleherz
10. Arga
11. Rabrân
12. Siyawkam

**Arabic Script:**
1. نەوروز
2. پاژەرەژ
3. چێڵکڕ
4. کۆپڕ
5. گەلاوێژ
6. ئاوەوەرە
7. تارازیێ
8. گەڵاخەزان
9. کەڵەهەرز
10. ئارگا
11. ڕابڕان
12. سیاوکام

#### Day Names

**Source:** D.N. MacKenzie, "The Dialect of Awroman (Hawrämän-i Luhön)" (1966), pages 21-22

**Latin Script:**
- Yekşem (Sunday)
- Duşem (Monday)
- Sêşem (Tuesday)
- Çwarşem (Wednesday)
- Pêncşem (Thursday)
- Hellîne (Friday) — Note: Distinct from Kurmanji În
- Şeme (Saturday)

**Arabic Script:**
- یەکشەم
- دووشەم
- سێشەم
- چوارشەم
- پێنجشەم
- هەڵڵینە (Friday — note doubled ڵ)
- شەمی

**Usage:**
```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

// Hawrami Latin
Console.WriteLine(date.ToString("D", KurdishDialect.HawramiLatin));
// Output: "1 Newroz 2725"

// Hawrami Arabic
Console.WriteLine(date.ToString("D", KurdishDialect.HawramiArabic));
// Output: "١ نەوروز ٢٧٢٥"
```

## Gregorian Calendar Dialects

### Sorani Gregorian

Used for formatting Gregorian dates with Sorani Kurdish month names.

**Source:** Kurdistan Regional Government (gov.krd) official publications

#### Month Names

**Latin Script:**
1. Kanûnî Duhem (January)
2. Şubat (February)
3. Adar (March)
4. Nîsan (April)
5. Ayar (May)
6. Hûzeyran (June)
7. Temmûz (July)
8. Ab (August)
9. Eylûl (September)
10. Tişrînî Yekem (October)
11. Tişrînî Duhem (November)
12. Kanûnî Yekem (December)

**Arabic Script:**
1. کانونی دووەم (January)
2. شوبات (February)
3. ئادار (March)
4. نیسان (April)
5. ئایار (May)
6. حوزەیران (June)
7. تەمووز (July)
8. ئاب (August)
9. ئەیلول (September)
10. تشرینی یەکەم (October)
11. تشرینی دووەم (November)
12. کانونی یەکەم (December)

**Usage:**
```csharp
DateTime gregorian = new DateTime(2025, 1, 15);

// Sorani Gregorian Latin
string formatted = gregorian.ToSoraniGregorian();
// Output: "15 Kanûnî Duhem 2025"

// Sorani Gregorian Arabic
string arabicFormatted = gregorian.ToSoraniGregorian(
  GregorianSoraniFormatter.ScriptType.Arabic
);
// Output: "١٥ کانونی دووەم ٢٠٢٥"
```

### Kurmanji Gregorian

Used for formatting Gregorian dates with Kurmanji Kurdish month names.

**Source:** Kurdistan Regional Government (gov.krd) official publications

#### Month Names

**Latin Script:**
1. Kanûna Duyê (January)
2. Şubat (February)
3. Adar (March)
4. Nîsan (April)
5. Gulan (May)
6. Hezîran (June)
7. Tîrmeh (July)
8. Tebax (August)
9. Eylûl (September)
10. Çiriya Êkê (October)
11. Çiriya Duyê (November)
12. Kanûna Êkê (December)

**Arabic Script:**
1. کانوونا دویێ (January)
2. شوبات (February)
3. ئادار (March)
4. نیسان (April)
5. گولان (May)
6. حەزیران (June)
7. تیرمەه (July)
8. تەباخ (August)
9. ئەیلووڵ (September)
10. چریا ێکێ (October)
11. چریا دویێ (November)
12. کانوونا ێکێ (December)

**Usage:**
```csharp
DateTime gregorian = new DateTime(2025, 5, 15);

// Kurmanji Gregorian Latin
string formatted = gregorian.ToKurmanjiGregorian();
// Output: "15 Gulan 2025"

// Kurmanji Gregorian Arabic
string arabicFormatted = gregorian.ToKurmanjiGregorian(
  GregorianKurmanjiFormatter.ScriptType.Arabic
);
// Output: "١٥ گولان ٢٠٢٥"
```

## Script Systems

### Latin Script

**Characteristics:**
- Left-to-Right (LTR) text direction
- Uses Western numerals (0-9)
- Extended Latin characters with diacritics (ê, î, û, ç, ş, etc.)

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "15 Xermanan 2725"
```

### Arabic Script

**Characteristics:**
- Right-to-Left (RTL) text direction for text
- Left-to-Right (LTR) for numeric formats
- Uses Eastern Arabic-Indic numerals (٠-٩)
- Kurdish-specific Arabic letters (گ, چ, پ, ڵ, ڕ, ێ, ۆ, وو, ە)

**Numeral System:**
- ٠ (0), ١ (1), ٢ (2), ٣ (3), ٤ (4)
- ٥ (5), ٦ (6), ٧ (7), ٨ (8), ٩ (9)

**Example:**
```csharp
KurdishDate date = new KurdishDate(2725, 6, 15);
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniArabic));
// Output: "٢٧٢٥ خەرمانان ١٥" (RTL: year month day)

Console.WriteLine(date.ToString("d", KurdishDialect.SoraniArabic));
// Output: "١٥/٠٦/٢٧٢٥" (LTR: day/month/year)
```

## Choosing the Right Dialect

### Based on Region

| Region | Recommended Dialect |
|--------|---------------------|
| Iraqi Kurdistan (Erbil, Sulaymaniyah) | Sorani |
| Iranian Kurdistan | Sorani |
| Turkish Kurdistan | Kurmanji |
| Syrian Kurdistan | Kurmanji |
| Diaspora (general) | Kurmanji or Sorani |
| Hawraman region | Hawrami |

### Based on Script Preference

| User Base | Recommended Script |
|-----------|-------------------|
| Iraq | Arabic or Latin (both common) |
| Iran | Arabic (primarily) |
| Turkey | Latin (primarily) |
| Syria | Arabic or Latin |
| Diaspora | Latin (primarily) |

### Based on Calendar

| Use Case | Recommended |
|----------|-------------|
| Cultural events, genealogy | Kurdish Calendar (KurdishDate) |
| Official documents (KRG) | Gregorian with Kurdish month names |
| International coordination | Gregorian calendar |

## Working with Multiple Dialects

### Display Same Date in All Dialects

```csharp
KurdishDate date = new KurdishDate(2725, 1, 1);

Console.WriteLine("Kurdish Calendar (Newroz 2725):");
Console.WriteLine($"Sorani Latin:    {date.ToString("D", KurdishDialect.SoraniLatin)}");
Console.WriteLine($"Sorani Arabic:   {date.ToString("D", KurdishDialect.SoraniArabic)}");
Console.WriteLine($"Kurmanji Latin:  {date.ToString("D", KurdishDialect.KurmanjiLatin)}");
Console.WriteLine($"Kurmanji Arabic: {date.ToString("D", KurdishDialect.KurmanjiArabic)}");
Console.WriteLine($"Hawrami Latin:   {date.ToString("D", KurdishDialect.HawramiLatin)}");
Console.WriteLine($"Hawrami Arabic:  {date.ToString("D", KurdishDialect.HawramiArabic)}");

// Output:
// Sorani Latin:    1 Xakelêwe 2725
// Sorani Arabic:   ١ خاکەلێوە ٢٧٢٥
// Kurmanji Latin:  1 Xakelêwe 2725
// Kurmanji Arabic: ١ خاکەلێوە ٢٧٢٥
// Hawrami Latin:   1 Newroz 2725
// Hawrami Arabic:  ١ نەوروز ٢٧٢٥
```

### Try Parsing with Multiple Dialects

```csharp
bool TryParseAnyDialect(string input, out KurdishDate result)
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
      Console.WriteLine($"Parsed as {dialect}");
      return true;
    }
  }

  result = default;
  return false;
}
```

## Regional Variations

### Sorani Variations

**Note:** Sorani has some regional spelling variations. The library uses transliterations verified against Kurdistan Regional Government publications.

### Kurmanji Variations

**Note:** Kurmanji has dialectal variations across Turkey, Syria, and Iraq. The library uses standard forms common in Iraqi Kurdistan.

### Hawrami Variations

**Note:** Hawrami month names sourced from Zaniary.com documentation. Day names from D.N. MacKenzie's linguistic research (1966). Regional variations may exist and should be verified with local speakers.

## Verifying Names for Your Region

To verify month and day names match your regional usage:

1. Check the names in actual usage
2. Consult with native speakers
3. Compare with official regional publications
4. Report discrepancies for future updates

**Sources Used:**
- **Sorani/Kurmanji Gregorian**: Kurdistan Regional Government (gov.krd)
- **Hawrami Days**: D.N. MacKenzie (1966)
- **Hawrami Months**: Zaniary.com

## Best Practices

### 1. Be Explicit About Dialect

```csharp
// Good - explicit dialect
string formatted = date.ToString("D", KurdishDialect.SoraniLatin);

// Avoid - relies on defaults
string formatted = date.ToString("D");
```

### 2. Store Dialect Preference

```csharp
public class UserPreferences
{
  public KurdishDialect PreferredDialect { get; set; }
  
  public string FormatDate(KurdishDate date)
  {
    return date.ToString("D", PreferredDialect);
  }
}
```

### 3. Provide Dialect Selection

```csharp
// Let users choose their preferred dialect and script
public enum PreferredLanguage
{
  SoraniLatin,
  SoraniArabic,
  KurmanjiLatin,
  KurmanjiArabic,
  HawramiLatin,
  HawramiArabic
}

public KurdishDialect ToDialect(PreferredLanguage pref)
{
  return pref switch
  {
    PreferredLanguage.SoraniLatin => KurdishDialect.SoraniLatin,
    PreferredLanguage.SoraniArabic => KurdishDialect.SoraniArabic,
    PreferredLanguage.KurmanjiLatin => KurdishDialect.KurmanjiLatin,
    PreferredLanguage.KurmanjiArabic => KurdishDialect.KurmanjiArabic,
    PreferredLanguage.HawramiLatin => KurdishDialect.HawramiLatin,
    PreferredLanguage.HawramiArabic => KurdishDialect.HawramiArabic,
    _ => KurdishDialect.SoraniLatin
  };
}
```

## See Also

- [Formatting and Parsing](formatting-and-parsing.md) — Format string details
- [API Reference](api-reference.md) — Complete API documentation
- [Examples](examples.md) — Practical code examples

---

**بەختێکی باش! (Good luck!)**