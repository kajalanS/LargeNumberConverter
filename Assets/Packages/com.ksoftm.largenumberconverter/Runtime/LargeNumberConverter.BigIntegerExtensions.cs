namespace Ksoftm.LargeNumberConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public static partial class LargeNumberConverter
    {
        #region Math Helpers

        /// <summary>
        /// Compute the integer square root of a BigInteger.
        /// </summary>
        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n < 0) throw new ArgumentException("Cannot compute sqrt of negative number.");
            if (n == 0) return 0;

            var bitLength = (int)Math.Ceiling(BigInteger.Log(n, 2));
            var x = BigInteger.One << (bitLength / 2);

            while (true)
            {
                var y = (x + n / x) >> 1;
                if (y == x || y == x - 1) return y;
                x = y;
            }
        }

        /// <summary>
        /// Greatest Common Divisor (GCD) of two BigIntegers.
        /// </summary>
        public static BigInteger Gcd(this BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var r = a % b;
                a = b;
                b = r;
            }

            return BigInteger.Abs(a);
        }

        /// <summary>
        /// Least Common Multiple (LCM) of two BigIntegers.
        /// </summary>
        public static BigInteger Lcm(this BigInteger a, BigInteger b)
        {
            if (a.IsZero || b.IsZero) return BigInteger.Zero;
            return BigInteger.Abs(a / a.Gcd(b) * b);
        }

        #endregion

        #region Conversion Helpers

        public static BigInteger ToBigInteger(this long value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this ulong value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this int value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this uint value) => new BigInteger((ulong)value);
        public static BigInteger ToBigInteger(this short value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this ushort value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this byte value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this sbyte value) => new BigInteger(value);
        public static BigInteger ToBigInteger(this double value) => new BigInteger((decimal)value);

        #endregion

        #region Formatting Helpers

        /// <summary>
        /// Formats BigInteger into your custom short form using LargeNumberConverter.
        /// </summary>
        public static string ToShortForm(this BigInteger value) => value.ToShortString();

        /// <summary>
        /// Formats BigInteger into "illion" text form.
        /// </summary>
        public static string ToIllionForm(this BigInteger value)
            => value.ToIllionText();

        #endregion

        #region Parsing & Arithmetic with Suffix-Strings

        /// <summary>
        /// Parses the string with suffix into a BigInteger.
        /// </summary>
        public static BigInteger ParseWithSuffix(this string input) => input.Parse();

        /// <summary>
        /// Adds a BigInteger value to a suffix-formatted string and returns the sum as BigInteger.
        /// </summary>
        public static BigInteger Add(this string inputWithSuffix, BigInteger toAdd)
        {
            var baseValue = inputWithSuffix.ParseWithSuffix();
            return BigInteger.Add(baseValue, toAdd);
        }

        /// <summary>
        /// Adds a long value to a suffix-formatted string, returning a BigInteger.
        /// </summary>
        public static BigInteger Add(this string inputWithSuffix, long toAdd)
            => inputWithSuffix.Add(new BigInteger(toAdd));

        /// <summary>
        /// Adds a numeric value to a suffix-formatted string and returns the result as short-form string.
        /// </summary>
        public static string AddToShortForm(this string inputWithSuffix, BigInteger toAdd)
        {
            var sum = inputWithSuffix.Add(toAdd);
            return sum.ToShortString();
        }

        /// <summary>
        /// Adds a numeric value to a suffix-formatted string and returns the result as short-form string.
        /// </summary>
        public static string AddToShortForm(this string inputWithSuffix, string toAdd)
        {
            var sum = inputWithSuffix.Add(toAdd.Parse());
            return sum.ToShortString();
        }

        /// <summary>
        /// Parses, adds, and returns Illion-text formatted result.
        /// </summary>
        public static string AddToIllionForm(this string inputWithSuffix, BigInteger toAdd)
        {
            var sum = inputWithSuffix.Add(toAdd);
            return sum.ToIllionText();
        }

        /// <summary>
        /// Adds two suffix-formatted strings and returns the sum as short-form.
        /// </summary>
        public static string Add(this string a, string b)
        {
            var sum = a.ParseWithSuffix() + b.ParseWithSuffix();
            return sum.ToShortString();
        }

        /// <summary>
        /// Adds two suffix-formatted strings and returns the sum as short-form.
        /// </summary>
        public static BigInteger Add(this BigInteger a, string b)
        {
            return a + b.ParseWithSuffix();
        }

        /// <summary>
        /// Subtracts a BigInteger from a suffix-formatted string and returns short-form.
        /// </summary>
        public static string Subtract(this string inputWithSuffix, string toSubtract)
        {
            var result = inputWithSuffix.ParseWithSuffix() - toSubtract.ParseWithSuffix();
            return result.ToShortForm();
        }

        /// <summary>
        /// Subtracts a BigInteger from a suffix-formatted string and returns short-form.
        /// </summary>
        public static string Subtract(this string inputWithSuffix, BigInteger toSubtract)
        {
            var result = inputWithSuffix.ParseWithSuffix() - toSubtract;
            return result.ToShortForm();
        }

        /// <summary>
        /// Multiply suffix-formatted string by a BigInteger and return short-form.
        /// </summary>
        public static string Multiply(this string inputWithSuffix, string multiplier)
        {
            var result = inputWithSuffix.ParseWithSuffix() * multiplier.ParseWithSuffix();
            return result.ToShortForm();
        }

        /// <summary>
        /// Multiply suffix-formatted string by a BigInteger and return short-form.
        /// </summary>
        public static string Multiply(this string inputWithSuffix, BigInteger multiplier)
        {
            var result = inputWithSuffix.ParseWithSuffix() * multiplier;
            return result.ToShortForm();
        }

        /// <summary>
        /// Tries to parse a suffix-formatted string into a BigInteger.
        /// Returns true on success, false on failure.
        /// </summary>
        public static bool TryParseWithSuffix(this string input, out BigInteger result)
        {
            result = BigInteger.Zero;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var parts = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return false;

            if (!decimal.TryParse(parts[0], System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var number))
                return false;

            var suffix = (parts.Length > 1) ? parts[1] : "";
            if (!LargeNumberConverter.TryGetSuffixExponent(suffix, out int exponent))
                return false;

            try
            {
                var scaled = number * (decimal)BigInteger.Pow(10, exponent);
                result = new BigInteger(scaled);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}