using System;
using System.Numerics;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// Provides methods to parse and format large numbers using short-scale suffixes and "illion" names.
/// </summary>
public static class LargeNumberConverter
{
  // Extended suffix list for short-scale and Conway–Guy "illion" names (preserved from original)
  private static readonly IReadOnlyList<string> Suffixes = new List<string>
    {
        "", "k", "M", "B", "T", "Q", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
        "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
        "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx", "cy", "cz",
        // Continue as needed...
    };

  private static readonly Dictionary<string, int> SuffixExponents;

  // Illion names up to centillion
  private static readonly IReadOnlyList<string> IllionNames = new[] {
        "", "thousand", "million", "billion", "trillion", "quadrillion",
        "quintillion", "sextillion", "septillion", "octillion", "nonillion",
        "decillion", "undecillion", "duodecillion", "tredecillion", "quattuordecillion",
        "quindecillion", "sexdecillion", "septendecillion", "octodecillion", "novemdecillion",
        "vigintillion", /* ... up to centillion */ "centillion"
    };

  static LargeNumberConverter()
  {
    SuffixExponents = new Dictionary<string, int>(Suffixes.Count);
    for (int i = 0; i < Suffixes.Count; i++)
    {
      SuffixExponents[Suffixes[i]] = i * 3;
    }

    // Ensure that our IllionNames list is covered by suffixes
    if (IllionNames.Count > Suffixes.Count)
    {
      Debug.LogWarning($"[LargeNumberConverter] Warning: Suffixes count ({Suffixes.Count}) is less than IllionNames count ({IllionNames.Count}). Consider extending the Suffixes list.");
    }
  }

  /// <summary>
  /// Parses a string like "12.5 aa" or "3.2 M" into a BigInteger.
  /// </summary>
  public static BigInteger Parse(string input)
  {
    if (string.IsNullOrWhiteSpace(input))
      throw new ArgumentException("Input cannot be empty.", nameof(input));

    var parts = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    if (!decimal.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
      throw new FormatException($"Invalid numeric value: '{parts[0]}'");

    string suffix = parts.Length > 1 && SuffixExponents.ContainsKey(parts[1]) ? parts[1] : string.Empty;
    int exponent = SuffixExponents.TryGetValue(suffix, out var exp) ? exp : 0;

    var scaled = number * (decimal)BigInteger.Pow(10, exponent);
    return new BigInteger(scaled);
  }

  /// <summary>
  /// Formats a BigInteger into short form (e.g. 1250 → "1.25k").
  /// </summary>
  public static string ToShortString(BigInteger value)
  {
    if (value.IsZero)
      return "0";

    var abs = BigInteger.Abs(value);
    double log10 = BigInteger.Log10(abs);
    int group = Math.Min((int)(log10 / 3), Suffixes.Count - 1);

    BigInteger divisor = BigInteger.Pow(10, group * 3);
    decimal truncated = (decimal)abs / (decimal)divisor;

    string formatted = truncated.ToString("0.###", CultureInfo.InvariantCulture);
    return (value.Sign < 0 ? "-" : string.Empty) + formatted + Suffixes[group];
  }

  /// <summary>
  /// Converts a BigInteger into words using "illion" names.
  /// Groups beyond predefined names use "10^###" notation.
  /// </summary>
  public static string ToIllionText(BigInteger value)
  {
    if (value.IsZero)
      return "zero";

    double log10 = BigInteger.Log10(BigInteger.Abs(value));
    int group = (int)(log10 / 3);
    BigInteger divisor = BigInteger.Pow(10, group * 3);
    decimal truncated = (decimal)value / (decimal)divisor;

    string name = group < IllionNames.Count ? IllionNames[group] : $"10^{group * 3}";
    string numberText = truncated.ToString("0.###", CultureInfo.InvariantCulture);
    return string.IsNullOrEmpty(name) ? numberText : $"{numberText} {name}";
  }
}
