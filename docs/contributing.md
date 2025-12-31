---
layout: default  # ← Uses the THEME's default layout
title: Kurdish Calendar - Contributing
---

# Contributing

Thank you for considering contributing to the Kurdish Calendar library!

## Ways to Contribute

### 1. Report Bugs

Found a bug? Please report it via GitHub Issues:

**Include:**
- Library version
- .NET version
- Operating system
- Minimal code to reproduce
- Expected vs actual behaviour

**Example:**
```
**Version**: 1.0.0
**Environment**: .NET 10.0, Ubuntu 22.04

**Code**:
```csharp
var date = KurdishDate.Parse("invalid", KurdishDialect.SoraniLatin);
```

**Expected**: FormatException thrown
**Actual**: Application crashes
```

### 2. Suggest Features

Have an idea? Open a GitHub Issue with:

- **Use case**: Why is this needed?
- **Proposed API**: How should it work?
- **Examples**: Sample code showing usage
- **Alternatives**: Other approaches you've considered

### 3. Improve Documentation

Documentation improvements are welcome:

- Add examples for common scenarios
- Translate examples to other languages

## Development Setup

### Prerequisites

- **.NET 10.0 SDK** or higher
- **Git** for version control
- **IDE**: Visual Studio Code, Visual Studio, or JetBrains Rider

### Clone Repository

```bash
git clone https://github.com/57951/KurdishCalendar.git
cd KurdishCalendar
```

### Install Dependencies

```bash
dotnet restore
```

### Build Project

```bash
dotnet build
```

### Run Tests

```bash
cd KurdishCalendar.Tests
dotnet test
```

Expected output: All tests passing

## Code Guidelines

### 1. Code Style

**Follow Existing Conventions:**

```csharp
// Good - British English in comments
/// <summary>
/// Converts this colourful Kurdish date to Gregorian DateTime.
/// </summary>

// Avoid - American English
/// <summary>
/// Converts this colorful Kurdish date to Gregorian DateTime.
/// </summary>

// Good - Explicit types, no 'var'
KurdishDate date = new KurdishDate(2725, 1, 1);
DateTime gregorian = date.ToDateTime();

// Avoid - Use of 'var'
var date = new KurdishDate(2725, 1, 1);
var gregorian = date.ToDateTime();

// Good - 2-space indentation
public void Method()
{
  if (condition)
  {
    DoSomething();
  }
}

// Avoid - 4-space indentation
public void Method()
{
    if (condition)
    {
        DoSomething();
    }
}
```

### 2. Documentation

**XML Documentation Required:**

```csharp
/// <summary>
/// Adds the specified number of days to this date.
/// </summary>
/// <param name="days">The number of days to add (can be negative).</param>
/// <returns>A new <see cref="KurdishDate"/> representing the result.</returns>
/// <example>
/// <code>
/// KurdishDate date = new KurdishDate(2725, 1, 15);
/// KurdishDate tomorrow = date.AddDays(1);
/// </code>
/// </example>
public KurdishDate AddDays(int days)
{
  // Implementation
}
```

### 3. Testing

**All new code must include tests:**

```csharp
[Test]
public void AddDays_PositiveValue_AddsCorrectly()
{
  // Arrange
  KurdishDate date = new KurdishDate(2725, 1, 15);
  
  // Act
  KurdishDate result = date.AddDays(5);
  
  // Assert
  Assert.AreEqual(2725, result.Year);
  Assert.AreEqual(1, result.Month);
  Assert.AreEqual(20, result.Day);
}

[Test]
public void AddDays_NegativeValue_SubtractsCorrectly()
{
  // Arrange
  KurdishDate date = new KurdishDate(2725, 1, 15);
  
  // Act
  KurdishDate result = date.AddDays(-5);
  
  // Assert
  Assert.AreEqual(2725, result.Year);
  Assert.AreEqual(1, result.Month);
  Assert.AreEqual(10, result.Day);
}

[Test]
public void AddDays_CrossesMonthBoundary()
{
  // Arrange
  KurdishDate date = new KurdishDate(2725, 1, 30);
  
  // Act
  KurdishDate result = date.AddDays(2);
  
  // Assert
  Assert.AreEqual(2725, result.Year);
  Assert.AreEqual(2, result.Month);
  Assert.AreEqual(1, result.Day);
}
```

### 4. Error Handling

**Use appropriate exceptions:**

```csharp
// Good - Specific exception with helpful message
public KurdishDate(int year, int month, int day)
{
  if (year < 1)
  {
    throw new ArgumentOutOfRangeException(
      nameof(year), 
      "Year must be 1 or greater."
    );
  }
  
  if (month < 1 || month > 12)
  {
    throw new ArgumentOutOfRangeException(
      nameof(month), 
      "Month must be between 1 and 12."
    );
  }
  
  // Validation continues...
}
```

### 5. Immutability

**Date types are immutable:**

```csharp
// Good - Return new instance
public KurdishDate AddDays(int days)
{
  DateTime gregorian = ToDateTime().AddDays(days);
  return new KurdishDate(gregorian);
}

// Avoid - Modifying this instance
public void AddDays(int days)
{
  this.Day += days; // Error: readonly struct
}
```

### 6. Thread Safety

**Ensure thread-safe code:**

```csharp
// Good - Use thread-safe dictionary operations
private static readonly Dictionary<int, DateTime> _cache = new();

public static DateTime GetCached(int year)
{
  lock (_cache)
  {
    if (_cache.TryGetValue(year, out DateTime value))
    {
      return value;
    }
    
    DateTime result = Calculate(year);
    _cache[year] = result;
    return result;
  }
}
```

## Submission Process

### 1. Fork Repository

```bash
# On GitHub, click "Fork" button
# Clone your fork
git clone https://github.com/57951/KurdishCalendar.git
```

### 2. Create Branch

```bash
git checkout -b feature/your-feature-name
```

**Branch naming:**
- `feature/` — New features
- `bugfix/` — Bug fixes
- `docs/` — Documentation updates
- `refactor/` — Code refactoring

### 3. Make Changes

```bash
# Edit files
# Add tests
# Update documentation
```

### 4. Run Tests

```bash
dotnet test
```

**Ensure:**
- ✅ All existing tests pass
- ✅ New tests added for new code
- ✅ Code coverage maintained (>95%)

### 5. Commit Changes

```bash
git add .
git commit -m "Add feature: description of change"
```

**Commit message guidelines:**
- Use imperative mood ("Add feature" not "Added feature")
- First line: brief summary (<50 characters)
- Blank line, then detailed description if needed
- Reference issue numbers if applicable

**Example:**
```
Add astronomical date parsing with custom longitude

- Implement Parse method for KurdishAstronomicalDate
- Add support for custom longitude parameter
- Include tests for all dialects
- Update documentation with examples

Fixes #123
```

### 6. Push to Fork

```bash
git push origin feature/your-feature-name
```

### 7. Create Pull Request

On GitHub:
1. Navigate to your fork
2. Click "Pull Request"
3. Select your branch
4. Fill in PR template
5. Submit

**PR Description Should Include:**
- What changes were made
- Why changes were needed
- How to test the changes
- Related issue numbers

## Code Review Process

### What We Look For

1. **Correctness** — Does it work as intended?
2. **Tests** — Are there adequate tests?
3. **Documentation** — Is it well documented?
4. **Style** — Follows coding conventions?
5. **Performance** — No performance regressions?

### Feedback and Iteration

- Reviewers may request changes
- Address feedback in new commits
- Discussion and iteration are normal
- Be patient and respectful
- Avoid political, religious, or racial commentary

## Linguistic Contributions

**Sources to Check:**
1. Kurdistan Regional Government (gov.krd) publications
2. Academic linguistic research
3. Community consensus in Kurdish regions
4. Published dictionaries and references

### Adding New Dialects

To propose a new dialect:

1. **Provide complete month and day names** (Both Latin and Arabic scripts)
2. **Cite authoritative sources**
3. **Explain geographic/linguistic scope**
4. **Provide native speaker validation**
5. **Include example formatting**

```markdown
**Dialect**: Laki
**Region**: Khorramabad, Iran
**Script**: Latin

**Current**: Xakelêwe
**Suggested**: Sîbat (confirmed correct)
**Source**: KRG official calendar 2024
```

## Testing Contributions

### Writing Good Tests

**Test Naming:**
```csharp
[Test]
public void MethodName_Scenario_ExpectedOutcome()
{
  // Example:
  // AddDays_PositiveValue_AddsCorrectly
  // Parse_InvalidFormat_ThrowsException
}
```

**Test Structure (AAA Pattern):**
```csharp
[Test]
public void Method_Scenario_Outcome()
{
  // Arrange - Set up test data
  KurdishDate date = new KurdishDate(2725, 1, 1);
  
  // Act - Execute the method
  KurdishDate result = date.AddDays(1);
  
  // Assert - Verify the outcome
  Assert.AreEqual(2, result.Day);
}
```

### Test Categories

Use test categories for organisation:

```csharp
[Test]
[Category("Conversion")]
public void ToDateTime_Newroz_ConvertsToMarch21()
{
  // Test implementation
}

[Test]
[Category("Astronomical")]
public void CalculateEquinox_2025_WithinOneMinute()
{
  // Test implementation
}
```

## Documentation Contributions

### Documentation Structure

```
docs/
├── index.md                    # Documentation homepage
├── getting-started.md          # Tutorial
├── examples.md                 # Code examples
├── api-reference.md            # API documentation
├── formatting-and-parsing.md   # Formatting guide
├── dialects-and-scripts.md     # Dialect details
├── astronomical-calculations.md # Astronomy guide
├── gregorian-formatting.md     # Gregorian guide
├── testing.md                  # Testing guide
├── contributing.md             # This file
└── faq.md                      # FAQ
```

### Documentation Style

**Use British English:**
```markdown
<!-- Good -->
The library provides localised date formatting.

<!-- Avoid -->
The library provides localized date formatting.
```

**Code Examples:**
```markdown
<!-- Good - Complete, runnable examples -->
```csharp
using KurdishCalendar.Core;

KurdishDate date = new KurdishDate(2725, 1, 1);
Console.WriteLine(date.ToString("D", KurdishDialect.SoraniLatin));
// Output: "1 Xakelêwe 2725"
```

<!-- Avoid - Incomplete snippets -->
```csharp
var date = ...
date.ToString(...);
```
```

## Release Process

1. Update version in `.csproj`
2. Update `CHANGELOG.md`
3. Create git tag: `git tag v1.x.x`
4. Push tag: `git push --tags`
5. GitHub Actions builds and publishes NuGet package

## Communication

### GitHub Issues

- **Questions**: Use Discussions
- **Bugs**: Create detailed issue
- **Features**: Propose and discuss first
- **Security**: Email privately (see SECURITY.md)

### Code of Conduct

Be respectful, inclusive, and constructive:
- Welcome newcomers
- Be patient with questions
- Provide constructive feedback
- Respect different perspectives
- Avoid political, religious, and racial commentary

## Recognition

Contributors are recognised in:
- `README.md` acknowledgements
- Release notes
- Git commit history

## Questions?

- **Documentation**: Check existing docs first
- **Issues**: Search existing issues
- **Discussions**: Start a discussion on GitHub
- **Email**: For sensitive matters

## Licence

By contributing, you agree that your contributions will be licenced under the MIT Licence.

---

**بەختێکی باش! (Good luck!)**