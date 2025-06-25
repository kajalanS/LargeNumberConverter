namespace Ksoftm.LargeNumberConverter
{
    using System;
    using System.Numerics;
    using System.Collections.Generic;
    using System.Globalization;
    using UnityEngine;

    /// <summary>
    /// Provides methods to parse and format large numbers using short-scale suffixes and "illion" names.
    /// MySQL datatype: DECIMAL(65,30) for BigInteger
    /// </summary>
    public static partial class LargeNumberConverter
    {
        // Extended suffix list for short-scale and Conway–Guy "illion" names (preserved from original)
        public static readonly IReadOnlyList<string> Suffixes = new List<string>
        {
            "", "k", "M", "B", "T", "Q", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am",
            "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az",
            "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br",
            "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz",
            "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr",
            "cs", "ct", "cu", "cv", "cw", "cx", "cy", "cz",
        };

        private static readonly Dictionary<string, int> SuffixExponents;

        // Illion names up to centillion
        public static readonly IReadOnlyList<string> IllionNames = new[]
        {
            "", "thousand", "million", "billion", "trillion", "quadrillion",
            "quintillion", "sextillion", "septillion", "octillion", "nonillion",
            "decillion", "undecillion", "duodecillion", "tredecillion", "quattuordecillion",
            "quindecillion", "sexdecillion", "septendecillion", "octodecillion", "novemdecillion",
            "vigintillion", "centillion"
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
                Debug.LogWarning(
                    $"[LargeNumberConverter] Warning: Suffixes count ({Suffixes.Count}) is less than IllionNames count ({IllionNames.Count}). Consider extending the Suffixes list.");
            }
        }

        /// <summary>
        /// Parses a string like "12.5 aa" or "3.2 M" into a BigInteger.
        /// </summary>
        public static BigInteger Parse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be empty.", nameof(input));

            var parts = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!BigInteger.TryParse(parts[0], out var number))
                throw new FormatException($"Invalid numeric value: '{parts[0]}'");

            var suffix = parts.Length > 1 && SuffixExponents.ContainsKey(parts[1]) ? parts[1] : string.Empty;
            var exponent = SuffixExponents.GetValueOrDefault(suffix, 0);

            // var scaled = new BigInteger(number) * BigInteger.Pow(10, exponent);
            var scaled = number * BigInteger.Pow(10, exponent);
            return scaled;
        }

        public static string ToShortString(this BigInteger value)
        {
            if (value.IsZero)
                return "0";

            var abs = BigInteger.Abs(value);
            var log10 = BigInteger.Log10(abs);
            var group = Math.Min((int)(log10 / 3), Suffixes.Count - 1);

            var divisor = BigInteger.Pow(10, group * 3);
            var truncated = (abs / divisor) + (abs % divisor) / divisor;

            var formatted = truncated.ToString("0.###", CultureInfo.InvariantCulture);
            var suffix = Suffixes[group];

            return (value.Sign < 0 ? "-" : string.Empty) + formatted +
                   (string.IsNullOrEmpty(suffix) ? string.Empty : " " + suffix);
        }


        /// <summary>
        /// Converts a BigInteger into words using "illion" names.
        /// Groups beyond predefined names use "10^###" notation.
        /// </summary>
        public static string ToIllionText(this BigInteger value)
        {
            if (value.IsZero)
                return "zero";

            var log10 = BigInteger.Log10(BigInteger.Abs(value));
            var group = (int)(log10 / 3);
            var divisor = BigInteger.Pow(10, group * 3);
            var truncated = value / divisor;

            var name = group < IllionNames.Count ? IllionNames[group] : $"10^{group * 3}";
            var numberText = truncated.ToString("0.###", CultureInfo.InvariantCulture);
            return string.IsNullOrEmpty(name) ? numberText : $"{numberText} {name}";
        }

        public static bool TryGetSuffixExponent(string suffix, out int exponent)
        {
            return SuffixExponents.TryGetValue(suffix, out exponent);
        }
    }
}