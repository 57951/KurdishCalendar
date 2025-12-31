# Kurdish Astronomical Calendar - Reference

## When to Use What?

```
Standard (KurdishDate)           Astronomical (KurdishAstronomicalDate)
├─ 99.9% of use cases            ├─ Cultural ceremony timing
├─ Fast & predictable            ├─ Legal/official documents
├─ 21 March Nowruz               ├─ Academic research
└─ Simple applications           └─ Cross-calendar sync
```

## Creating Dates

```csharp
// Standard
var date = new KurdishDate(2725, 1, 1);

// Astronomical - Erbil (default)
var astro = new KurdishAstronomicalDate(2725, 1, 1);
var astro = KurdishAstronomicalDate.FromErbil(2725, 1, 1);

// Astronomical - Other locations
var slemani = KurdishAstronomicalDate.FromSulaymaniyah(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);
var utc = KurdishAstronomicalDate.FromUtc(2725, 1, 1);
var custom = KurdishAstronomicalDate.FromLongitude(2725, 1, 1, 44.0);
```

## Converting

```csharp
// Standard → Astronomical
KurdishAstronomicalDate a1 = standard.ToAstronomical();             // Lossless
KurdishAstronomicalDate a2 = standard.ToAstronomicalRecalculated(); // Informational

// Astronomical → Standard
KurdishDate s1 = astro.ToSimplifiedDate();            // Lossless
KurdishDate s2 = astro.ToStandardDateRecalculated();  // Informational

// Both → Gregorian
DateTime gregorian = date.ToDateTime();
```

## Lossless vs Informational

**Lossless**: Keeps same year/month/day values, just changes type
```csharp
var std = new KurdishDate(2725, 1, 1);
var astro = std.ToAstronomical();
// astro is still 2725/1/1, but uses astronomical equinox internally
```

**Informational**: Recalculates via Gregorian, accounting for equinox differences
```csharp
var std = new KurdishDate(2725, 1, 1);  // Assumes 21 March
var astro = std.ToAstronomicalRecalculated();
// astro might be 2724/12/30 if actual equinox was 20 March
```

## Reference Longitudes

| Location | Longitude | Usage |
|----------|-----------|-------|
| Erbil | 44.0°E | Default, capital of Kurdistan Region |
| Sulaymaniyah | 45.0°E | Second city, cultural center |
| Tehran | 52.5°E | Matches Iranian calendar |
| UTC | 0.0° | International standard |
| Custom | Any | `FromLongitude(y, m, d, longitude)` |

## Cache Management

```csharp
// Clear all cached equinoxes (testing)
KurdishAstronomicalDate.ClearEquinoxCache();

// Clear specific year/location
KurdishAstronomicalDate.ClearEquinoxCache(2725, 44.0);
```

## Performance

- Standard date creation: **~5μs**
- Astronomical (uncached): **~50μs**
- Astronomical (cached): **~0.1μs**
- Conversions: **~0.1μs** (lossless)

## Common Patterns

### Find exact Nowroz moment
```csharp
var newroz = KurdishAstronomicalDate.FromErbil(2726, 1, 1);
DateTime moment = newroz.ToDateTime();
Console.WriteLine($"Nowroz 2726: {moment:yyyy-MM-dd HH:mm:ss} UTC");
```

### Compare locations
```csharp
var erbil = KurdishAstronomicalDate.FromErbil(2725, 1, 1);
var tehran = KurdishAstronomicalDate.FromTehran(2725, 1, 1);

if (erbil.ToDateTime().Date == tehran.ToDateTime().Date)
  Console.WriteLine("Same day!");
```

### Historical conversion
```csharp
DateTime historical = new DateTime(1945, 5, 8);  // VE Day
var kurdish = KurdishAstronomicalDate.FromDateTime(historical);
Console.WriteLine($"{kurdish.Year}/{kurdish.Month}/{kurdish.Day}");
```

### Interface usage
```csharp
void ProcessDate(IKurdishDate date)
{
  Console.WriteLine($"{date.Year}/{date.Month}/{date.Day}");
}

// Works with both types!
ProcessDate(new KurdishDate(2725, 1, 1));
ProcessDate(new KurdishAstronomicalDate(2725, 1, 1));
```

## Gotchas

❌ **Don't** create astronomical dates in tight loops without caching consideration
✅ **Do** cache equinoxes automatically handles this

❌ **Don't** assume Erbil and Tehran dates are always the same
✅ **Do** test for your specific years and locations

❌ **Don't** use astronomical dates for performance-critical paths
✅ **Do** use standard dates unless you need astronomical precision

❌ **Don't** forget longitude affects which day Nowroz falls on
✅ **Do** specify the correct reference location for your use case

## Algorithm Details

- **Jean Meeus** algorithm from "Astronomical Algorithms" (1991/1998)
- **Accuracy**: ±1 minute for years 1800-2200
- **Method**: Calculates Julian Ephemeris Day with periodic terms
- **Legal**: Open source, freely usable, no restrictions

## Need Help?

Read `astronomical-calculations.md` for a comprehensive guide.

**Start simple, add complexity only when needed!**