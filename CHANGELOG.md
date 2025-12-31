# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0] - 2025-12-31

### Added
- Initial release
- `KurdishDate` type for standard calendar calculations
- `KurdishAstronomicalDate` type for astronomical precision
- Support for Sorani, Kurmanji, and Hawrami dialects
- Latin and Arabic script support
- Date arithmetic (AddDays, AddMonths, AddYears)
- Date parsing from multiple formats
- Customisable date formatting
- Gregorian date formatting with Kurdish month names (Sorani and Kurmanji)
- Astronomical equinox calculation using Jean Meeus algorithms
- Comprehensive test suite (150+ tests)
- Validation against Fred Espenak's ephemeris data (2000-2030)

### Validated
- Astronomical calculations accurate to ±1 minute for years 1800-2200
- Gregorian month names (Sorani and Kurmanji) verified against Kurdistan Regional Government publications
- Hawrami day names from D.N. MacKenzie (1966), month names from Zaniary.com
- Round-trip conversions (Kurdish ↔ Gregorian) tested

### Known Limitations
- Standard dates use fixed 21 March for Nowruz (actual equinox varies 19-22 March)
- Historical dates before 1800 CE calculated but not validated
- All month/day names sourced from documented references (KRG, MacKenzie, Zaniary.com) but regional variations may exist
- No time-of-day support (astronomical calculator provides equinox moment for date determination only)

