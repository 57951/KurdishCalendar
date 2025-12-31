using System;
using System.Collections.Generic;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides culture-specific information for Kurdish calendar formatting.
  /// </summary>
  public static class KurdishCultureInfo
  {
    private static readonly Dictionary<KurdishDialect, MonthNames> MonthNamesByDialect;
    private static readonly Dictionary<KurdishDialect, DayNames> DayNamesByDialect;

    static KurdishCultureInfo()
    {
      // Initialise month names for all dialects
      MonthNamesByDialect = new Dictionary<KurdishDialect, MonthNames>
      {
        {
          // Faithful Transliteration
          // Long vowels (û, î) reflect Sorani pronunciation and common academic/KRG transliteration.
          // Kanûnî / Tişrînî mirrors the Sorani genitive ـى.
          // “Duhem / Yekem” are Sorani ordinal forms (not Arabic thānī / awwal).
          KurdishDialect.SoraniGregorianLatin,
          new MonthNames(
            new[] { "Kanûnî Duhem", "Şubat", "Adar", "Nîsan", "Ayar", "Hûzeyran", "Temmûz", "Ab", "Eylûl", "Tişrînî Yekem", "Tişrînî Duhem", "Kanûnî Yekem" },
            new[] { "Kan2", "Şub", "Ada", "Nîs", "Aya", "Hûz", "Tem", "Ab", "Eyl", "Tiş1", "Tiş2", "Kan1" }
          )
        },
        {
          // Notes:
          // 1. The ـى in كانونى / تشرينى is deliberate and matches KRG usage.
          // 2. Numerals are spelled out (يەكەم / دووەم), not digits.
          // 3. Abbreviations are not published by KRG; these are conservative, UI-safe shortenings.
          KurdishDialect.SoraniGregorianArabic,
          new MonthNames(
            new[] { "كانونى دووەم", "شوبات", "ئادار", "نيسان", "ئايار", "حوزەيران", "تەمووز", "ئاب", "ئەيلول", "تشرينى يەكەم", "تشرينى دووەم", "كانونى يەكەم" },
            new[] { "كان٢", "شوب", "ئادا", "نيس", "ئاي", "حوز", "تەم", "ئاب", "ئەي", "تشر١", "تشر٢", "كان١" }
          )
        },
        {
          KurdishDialect.SoraniLatin,
          new MonthNames(
            new[] { "Xakelêwe", "Gulan", "Cozerdan", "Pûşper", "Gelawêj", "Xermanan", "Rezber", "Gelarêzan", "Sermawez", "Befranbar", "Rêbendan", "Reşeme" },
            new[] { "Xak", "Gul", "Coz", "Pûş", "Gel", "Xer", "Rez", "Gea", "Ser", "Bef", "Rêb", "Reş" }
          )
        },
        {
          KurdishDialect.SoraniArabic,
          new MonthNames(
            new[] { "خاکەلێوە", "گوڵان", "جۆزەردان", "پووشپەڕ", "گەلاوێژ", "خەرمانان", "ڕەزبەر", "گەڵاڕێزان", "سەرماوەز", "بەفرانبار", "ڕێبەندان", "ڕەشەمە" },
            new[] { "خاک", "گوڵ", "جۆز", "پوش", "گەل", "خەر", "ڕەز", "گەڵ", "سەر", "بەف", "ڕێب", "ڕەش" }
          )
        },
        {
          // Kurmanji Gregorian Calendar Month Names (Latin Script)
          // These are the Syriac/Aramaic-origin month names used with the Gregorian calendar
          // in Kurdistan Region of Iraq and other Kurmanji-speaking areas.
          // Authoritive Source: Kurdistan Regional Government (gov.krd) publications.
          KurdishDialect.KurmanjiGregorianLatin,
          new MonthNames(
            new[] { "Kanûna Duyê", "Şubat", "Adar", "Nîsan", "Gulan", "Hezîran", "Tîrmeh", "Tebax", "Eylûl", "Çiriya Êkê", "Çiriya Duyê", "Kanûna Êkê" },
            new[] { "Kan Duy", "Şub", "Ada", "Nîs", "Gul", "Hez", "Tîr", "Teb", "Eyl", "Çir Êk", "Çir Duy", "Kan Êk" }
          )
        },
        {
          // Kurmanji Gregorian Calendar Month Names (Arabic Script)
          // Arabic script rendering of the Gregorian month names for Kurmanji speakers.
          // Authoritive Source: Kurdistan Regional Government (gov.krd) publications.
          KurdishDialect.KurmanjiGregorianArabic,
          new MonthNames(
            new[] { "کانوونا دویێ", "شوبات", "ئادار", "نیسان", "گولان", "حەزیران", "تیرمەه", "تەباخ", "ئەیلوول", "چریا ێکێ", "چریا دویێ", "کانوونا ێکێ" },
            new[] { "کان دوی", "شوب", "ئادا", "نیس", "گول", "حەز", "تیر", "تەب", "ئەیل", "چر ێک", "چر دوی", "کان ێک" }
          )
        },
        {
          KurdishDialect.KurmanjiLatin,
          new MonthNames(
            new[] { "Xakelêwe", "Gulan", "Cozerdan", "Pûşper", "Gelawêj", "Xermanan", "Rezber", "Gelarêzan", "Sermawez", "Befranbar", "Rêbendan", "Reşeme" },
            new[] { "Xak", "Gul", "Coz", "Pûş", "Gel", "Xer", "Rez", "Gea", "Ser", "Bef", "Rêb", "Reş" }
          )
        },
        {
          KurdishDialect.KurmanjiArabic,
          new MonthNames(
            new[] { "خاکەلێوە", "گوڵان", "جۆزەردان", "پووشپەڕ", "گەلاوێژ", "خەرمانان", "ڕەزبەر", "گەڵاڕێزان", "سەرماوەز", "بەفرانبار", "ڕێبەندان", "ڕەشەمە" },
            new[] { "خاک", "گوڵ", "جۆز", "پوش", "گەل", "خەر", "ڕەز", "گەڵ", "سەر", "بەف", "ڕێب", "ڕەش" }
          )
        },
        {
          // For Hawrami month names, we're using a source that explicitly maps Sorani→Hawrami equivalents.
          // Consistent, Hawrami-respecting set of 3-letter abbreviations based strictly on phonological salience, not Persian/Turkish conventions and not Sorani bias.
          // https://zaniary.com/blog/61ec1e25bfc4e
          KurdishDialect.HawramiLatin,
          new MonthNames(
            new[] { "Newroz", "Pajerej", "Çêlkirr", "Kopir", "Gelawêj", "Awewere", "Tarazî", "Gellaxezan", "Kelleherz", "Arga", "Rabrân", "Siyawkam" },
            new[] { "New", "Paj", "Çêl", "Kop", "Gel1", "Awe", "Tar", "Gel2", "Kel", "Arg", "Rab", "Siy" }
          )
        },
        {
          // For Hawrami month names, we're using a source that explicitly maps Sorani→Hawrami equivalents.
          // Consistent, Hawrami-respecting set of 3-letter abbreviations based strictly on phonological salience, not Persian/Turkish conventions and not Sorani bias.
          // https://zaniary.com/blog/61ec1e25bfc4e
          KurdishDialect.HawramiArabic,
          new MonthNames(
            new[] { "نەورۆز", "پاژەرەژ", "چێڵکڕ", "کۆپڕ", "گەلاوێژ", "ئاوەوەرە", "ترازیێ", "گەڵاخەزان", "کەڵەهەرز", "ئارگا", "ڕابڕان", "سیاوکام" },
            new[] { "نەو", "پاژ", "چێڵ", "کۆپ", "گەل١", "ئاو", "ترا", "گەڵ٢", "کەڵ", "ئار", "ڕاب", "سیا" }
          )
        }
      };

      // Initialise day names for all dialects
      DayNamesByDialect = new Dictionary<KurdishDialect, DayNames>
      {
        // Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
        {
          // KRG (gov.krd) uses یەکشەممە/دووشەممە/.../هەینی/شەممە
          // Some sources show alternate spellings, such as Yekşem, Duşem, Sêşem, Çwarşem, Pêncşem, Hênî, Şem.
          // No official abbreviations shown on gov.krd; these are safe/obvious shortenings.
          KurdishDialect.SoraniLatin,
          new DayNames(
            new[] { "Yekşemme", "Duşemme", "Sêşemme", "Çwarşemme", "Pêncşemme", "Hênî", "Şemme" },
            new[] { "Yek", "Du", "Sê", "Çwa", "Pên", "Hên", "Şem" }
          )
        },
        {
          KurdishDialect.SoraniArabic,
          new DayNames(
            new[] { "یەکشەممە", "دووشەممە", "سێشەممە", "چوارشەممە", "پێنجشەممە", "هەینی", "شەممە" },
            // Again: gov.krd doesn’t publish short forms; these are straightforward shortenings
            new[] { "یەک", "دوو", "سێ", "چوا", "پێن", "هەین", "شەم" }
          )
        },
        {
          KurdishDialect.KurmanjiLatin,
          new DayNames(
            new[] { "Yekşem", "Duşem", "Sêşem", "Çarşem", "Pêncşem", "În", "Şemî" },
            new[] { "Yek", "Du", "Sê", "Çar", "Pên", "În", "Şem" }
          )
        },
        {
          KurdishDialect.KurmanjiArabic,
          new DayNames(
            new[] { "یەکشەم", "دووشەم", "سێشەم", "چارشەم", "پێنجشەم", "ئین", "شەمی" },
            new[] { "یەک", "دوو", "سێ", "چار", "پێن", "ئین", "شەم" }
          )
        },
        {
          // D.N. MacKenzie's "The Dialect of Awroman (Hawrämän-i Luhön)" from 1966,
          // Page 21-22, section 13 "Adjectives preceding the noun."
          KurdishDialect.HawramiLatin,
          new DayNames(
            new[] { "Yekşem", "Duşem", "Sêşem", "Çwarşem", "Pêncşem", "Hellîne", "Şeme" },
            new[] { "Yek", "Du", "Sê", "Çwa", "Pên", "Hel", "Şem" }
          )
        },
        {
          // D.N. MacKenzie's "The Dialect of Awroman (Hawrämän-i Luhön)" from 1966,
          // Page 21-22, section 13 "Adjectives preceding the noun."
          // Note on Friday: MacKenzie clearly documented hellîne (هەڵڵینە) as the Hawrami form,
          // which is distinct from the Kurmanji În (ئین). The doubled ڵ (ll) is important in
          // the transcription.
          KurdishDialect.HawramiArabic,
          new DayNames(
            new[] { "یەکشەم", "دووشەم", "سێشەم", "چوارشەم", "پێنجشەم", "هەڵڵینە", "شەمی" },
            new[] { "یەک", "دوو", "سێ", "چوا", "پێن", "ھەڵ", "شەم" }
          )
        }
      };
    }

    /// <summary>
    /// Gets the full month name for the specified month in the given dialect.
    /// </summary>
    public static string GetMonthName(int month, KurdishDialect dialect, bool abbreviated = false)
    {
      if (month < 1 || month > 12)
      {
        throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
      }

      return abbreviated
        ? MonthNamesByDialect[dialect].AbbreviatedNames[month - 1]
        : MonthNamesByDialect[dialect].FullNames[month - 1];
    }

    /// <summary>
    /// Gets the day name for the specified day of the week in the given dialect.
    /// </summary>
    public static string GetDayName(DayOfWeek dayOfWeek, KurdishDialect dialect, bool abbreviated = false)
    {
      return abbreviated
        ? DayNamesByDialect[dialect].AbbreviatedNames[(int)dayOfWeek]
        : DayNamesByDialect[dialect].FullNames[(int)dayOfWeek];
    }

    /// <summary>
    /// Determines if the dialect uses Arabic script.
    /// </summary>
    public static bool IsArabicScript(KurdishDialect dialect)
    {
      return dialect == KurdishDialect.SoraniArabic ||
             dialect == KurdishDialect.SoraniGregorianArabic ||
             dialect == KurdishDialect.KurmanjiArabic ||
             dialect == KurdishDialect.KurmanjiGregorianArabic ||
             dialect == KurdishDialect.HawramiArabic;
    }

    /// <summary>
    /// Formats a number according to the dialect's script.
    /// </summary>
    public static string FormatNumber(int number, KurdishDialect dialect)
    {
      if (!IsArabicScript(dialect))
      {
        return number.ToString();
      }

      // Convert to Arabic-Indic numerals (٠-٩) - U+0660 to U+0669
      string westernNumber = number.ToString();
      string arabicNumber = string.Empty;

      foreach (char c in westernNumber)
      {
        if (char.IsDigit(c))
        {
          int digit = c - '0';
          arabicNumber += (char)(0x0660 + digit); // Arabic-Indic digits (٠-٩)
        }
        else
        {
          arabicNumber += c;
        }
      }

      return arabicNumber;
    }

    private class MonthNames
    {
      public string[] FullNames { get; }
      public string[] AbbreviatedNames { get; }

      public MonthNames(string[] fullNames, string[] abbreviatedNames)
      {
        FullNames = fullNames;
        AbbreviatedNames = abbreviatedNames;
      }
    }

    private class DayNames
    {
      public string[] FullNames { get; }
      public string[] AbbreviatedNames { get; }

      public DayNames(string[] fullNames, string[] abbreviatedNames)
      {
        FullNames = fullNames;
        AbbreviatedNames = abbreviatedNames;
      }
    }
  }
}