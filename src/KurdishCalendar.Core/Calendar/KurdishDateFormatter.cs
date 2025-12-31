using System;
using System.Text;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Provides formatting services for Kurdish dates.
  /// </summary>
  public static class KurdishDateFormatter
  {
    /// <summary>
    /// Formats a Kurdish date using the specified format, dialect, and text direction.
    /// This method works with both KurdishDate and KurdishAstronomicalDate.
    /// </summary>
    /// <param name="date">The Kurdish date to format.</param>
    /// <param name="format">The format string (null for default short format).</param>
    /// <param name="dialect">The Kurdish dialect for localisation.</param>
    /// <param name="textDirection">The text direction (null for default based on script).</param>
    /// <returns>A formatted string representation of the date.</returns>
    public static string Format(IKurdishDate date, string? format, KurdishDialect dialect, KurdishTextDirection? textDirection)
    {
      if (string.IsNullOrEmpty(format))
      {
        format = "d";
      }

      // Determine default direction based on script if not specified
      KurdishTextDirection direction = textDirection ?? GetDefaultTextDirection(dialect);

      string formattedDate = format switch
      {
        "d" => FormatShortDate(date, dialect, direction),
        "D" => FormatLongDate(date, dialect, direction),
        "M" or "m" => FormatMonthDay(date, dialect, direction),
        "Y" or "y" => FormatYearMonth(date, dialect, direction),
        "f" => FormatFullDateShortTime(date, dialect, direction),
        "F" => FormatFullDateTime(date, dialect, direction),
        _ => FormatCustom(date, format, dialect, direction)
      };

      return formattedDate;
    }

    private static KurdishTextDirection GetDefaultTextDirection(KurdishDialect dialect)
    {
      // Arabic script defaults to RTL, Latin to LTR
      return KurdishCultureInfo.IsArabicScript(dialect)
        ? KurdishTextDirection.RightToLeft
        : KurdishTextDirection.LeftToRight;
    }

    private static string FormatShortDate(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      string day = KurdishCultureInfo.FormatNumber(date.Day, dialect);
      string month = KurdishCultureInfo.FormatNumber(date.Month, dialect);
      string year = KurdishCultureInfo.FormatNumber(date.Year, dialect);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        // RTL: year/month/day
        return $"{year}/{month}/{day}";
      }

      // LTR: day/month/year
      if (KurdishCultureInfo.IsArabicScript(dialect))
      {
        return $"{day}/{month}/{year}";
      }

      return $"{date.Day:D2}/{date.Month:D2}/{date.Year}";
    }

    private static string FormatLongDate(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      string day = KurdishCultureInfo.FormatNumber(date.Day, dialect);
      string monthName = KurdishCultureInfo.GetMonthName(date.Month, dialect);
      string year = KurdishCultureInfo.FormatNumber(date.Year, dialect);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        // RTL: year month day
        if (KurdishCultureInfo.IsArabicScript(dialect))
        {
          return $"{year} {monthName} {day}";
        }
        return $"{date.Year} {monthName} {date.Day}";
      }

      // LTR: day month year
      if (KurdishCultureInfo.IsArabicScript(dialect))
      {
        return $"{day} {monthName} {year}";
      }

      return $"{date.Day} {monthName} {date.Year}";
    }

    private static string FormatMonthDay(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      string day = KurdishCultureInfo.FormatNumber(date.Day, dialect);
      string monthName = KurdishCultureInfo.GetMonthName(date.Month, dialect);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        // RTL: month day
        if (KurdishCultureInfo.IsArabicScript(dialect))
        {
          return $"{monthName} {day}";
        }
        return $"{monthName} {date.Day}";
      }

      // LTR: day month
      if (KurdishCultureInfo.IsArabicScript(dialect))
      {
        return $"{day} {monthName}";
      }

      return $"{date.Day} {monthName}";
    }

    private static string FormatYearMonth(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      string monthName = KurdishCultureInfo.GetMonthName(date.Month, dialect);
      string year = KurdishCultureInfo.FormatNumber(date.Year, dialect);

      // Year-month is same order in both directions
      if (KurdishCultureInfo.IsArabicScript(dialect))
      {
        return $"{monthName} {year}";
      }

      return $"{monthName} {date.Year}";
    }

    private static string FormatFullDateShortTime(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      string dayName = KurdishCultureInfo.GetDayName(date.DayOfWeek, dialect);
      string longDate = FormatLongDate(date, dialect, direction);

      if (direction == KurdishTextDirection.RightToLeft)
      {
        return $"{longDate}، {dayName}";  // Arabic comma
      }

      return $"{dayName}, {longDate}";
    }

    private static string FormatFullDateTime(IKurdishDate date, KurdishDialect dialect, KurdishTextDirection direction)
    {
      return FormatFullDateShortTime(date, dialect, direction);
    }

    private static string FormatCustom(IKurdishDate date, string format, KurdishDialect dialect, KurdishTextDirection direction)
    {
      StringBuilder result = new StringBuilder();
      int i = 0;

      while (i < format.Length)
      {
        char currentChar = format[i];
        int count = 1;

        // Count consecutive occurrences of the same character
        while (i + count < format.Length && format[i + count] == currentChar)
        {
          count++;
        }

        string token = new string(currentChar, count);

        switch (currentChar)
        {
          case 'd':
            result.Append(FormatDayToken(date, count, dialect));
            break;
          case 'M':
            result.Append(FormatMonthToken(date, count, dialect));
            break;
          case 'y':
            result.Append(FormatYearToken(date, count, dialect));
            break;
          case '\'':
          case '\"':
            // Handle quoted literals
            int endQuote = format.IndexOf(currentChar, i + 1);
            if (endQuote > i)
            {
              result.Append(format.Substring(i + 1, endQuote - i - 1));
              count = endQuote - i + 1;
            }
            else
            {
              result.Append(currentChar);
            }
            break;
          case '\\':
            // Escape character
            if (i + 1 < format.Length)
            {
              result.Append(format[i + 1]);
              count = 2;
            }
            else
            {
              result.Append(currentChar);
            }
            break;
          default:
            result.Append(token);
            break;
        }

        i += count;
      }

      return result.ToString();
    }

    private static string FormatDayToken(IKurdishDate date, int count, KurdishDialect dialect)
    {
      switch (count)
      {
        case 1:
          return KurdishCultureInfo.IsArabicScript(dialect)
            ? KurdishCultureInfo.FormatNumber(date.Day, dialect)
            : date.Day.ToString();
        case 2:
          return KurdishCultureInfo.IsArabicScript(dialect)
            ? KurdishCultureInfo.FormatNumber(date.Day, dialect)
            : date.Day.ToString("D2");
        case 3:
          return KurdishCultureInfo.GetDayName(date.DayOfWeek, dialect, abbreviated: true);
        case 4:
        default:
          return KurdishCultureInfo.GetDayName(date.DayOfWeek, dialect, abbreviated: false);
      }
    }

    private static string FormatMonthToken(IKurdishDate date, int count, KurdishDialect dialect)
    {
      switch (count)
      {
        case 1:
          return KurdishCultureInfo.IsArabicScript(dialect)
            ? KurdishCultureInfo.FormatNumber(date.Month, dialect)
            : date.Month.ToString();
        case 2:
          return KurdishCultureInfo.IsArabicScript(dialect)
            ? KurdishCultureInfo.FormatNumber(date.Month, dialect)
            : date.Month.ToString("D2");
        case 3:
          return KurdishCultureInfo.GetMonthName(date.Month, dialect, abbreviated: true);
        case 4:
        default:
          return KurdishCultureInfo.GetMonthName(date.Month, dialect, abbreviated: false);
      }
    }

    private static string FormatYearToken(IKurdishDate date, int count, KurdishDialect dialect)
    {
      if (count <= 2)
      {
        // Two-digit year
        int twoDigitYear = date.Year % 100;
        return KurdishCultureInfo.IsArabicScript(dialect)
          ? KurdishCultureInfo.FormatNumber(twoDigitYear, dialect)
          : twoDigitYear.ToString("D2");
      }
      else
      {
        // Four-digit year
        return KurdishCultureInfo.IsArabicScript(dialect)
          ? KurdishCultureInfo.FormatNumber(date.Year, dialect)
          : date.Year.ToString();
      }
    }
  }
}