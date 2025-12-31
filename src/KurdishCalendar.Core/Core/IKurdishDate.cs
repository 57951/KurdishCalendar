using System;

namespace KurdishCalendar.Core
{
  /// <summary>
  /// Defines the common interface for Kurdish date types.
  /// </summary>
  public interface IKurdishDate : IComparable<IKurdishDate>, IEquatable<IKurdishDate>
  {
    /// <summary>
    /// Gets the year component of the date.
    /// </summary>
    int Year { get; }

    /// <summary>
    /// Gets the month component of the date (1-12).
    /// </summary>
    int Month { get; }

    /// <summary>
    /// Gets the day component of the date.
    /// </summary>
    int Day { get; }

    /// <summary>
    /// Gets the day of the week for this date.
    /// </summary>
    DayOfWeek DayOfWeek { get; }

    /// <summary>
    /// Gets the day of the year (1-365 or 1-366 in leap years).
    /// </summary>
    int DayOfYear { get; }

    /// <summary>
    /// Gets a value indicating whether this year is a leap year.
    /// </summary>
    bool IsLeapYear { get; }

    /// <summary>
    /// Converts this Kurdish date to Gregorian DateTime.
    /// </summary>
    /// <returns>The corresponding Gregorian DateTime.</returns>
    DateTime ToDateTime();

    /// <summary>
    /// Returns a string representation of this date.
    /// </summary>
    /// <returns>A string representation of the date.</returns>
    string ToString();
  }
}