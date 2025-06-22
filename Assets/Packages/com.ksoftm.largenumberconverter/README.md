# Large Number Converter

![Unity Version Compatibility](https://img.shields.io/badge/Unity-2020.3%20%E2%80%94%20Latest-brightgreen)

A Unity package for converting and formatting large numbers into human-readable strings (e.g., 10000000 → 10M).

## Features

- Converts large numbers to abbreviated formats (K, M, B, T, etc.)
- Supports custom formatting and localization
- Easy integration with Unity UI

## Installation

1. Copy the `com.ksoftm.largenumberconverter` folder into your project's `Assets/Packages/` directory.
2. In Unity, the package will be available for use in your scripts.

## Usage

```csharp
using KSoftM.LargeNumberConverter;
using UnityEngine;

public class LargeNumberExample : MonoBehaviour
{
  [LargeNumber(showShortForm: true, showIllionForm: true)]
  public string ValueText = "1.23 M";

  void Start()
  {
    var value = LargeNumberConverter.Parse(ValueText);
    Debug.Log($"Parsed value: {value}");

    var shortStr = LargeNumberConverter.ToShortString(value);
    Debug.Log($"Short format: {shortStr}");

    var wordStr = LargeNumberConverter.ToIllionText(value);
    Debug.Log($"In words: {wordStr}");
  }
}
```

### Custom Formatting

```csharp
var value = LargeNumberConverter.Parse("987654321");
string shortFormat = LargeNumberConverter.ToShortString(value); // "987.654 M"
string illionText = LargeNumberConverter.ToIllionText(value);   // "987.654 million"
```

## API Reference

| Method                              | Description                                      |
|--------------------------------------|--------------------------------------------------|
| `Parse(string input)`                | Parses a formatted string to a numeric value.     |
| `ToShortString(BigInteger value)`    | Formats a number as an abbreviated string.        |
| `ToIllionText(BigInteger value)`     | Formats a number in words (illion form).          |

## Supported Abbreviations

- K (Thousand)
- M (Million)
- B (Billion)
- T (Trillion)
- Q (Quadrillion)
- ...and more

## License

MIT License. See [LICENSE](./LICENSE) for details.

## Author

KSoftM
