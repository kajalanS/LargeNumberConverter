using System.Globalization;

namespace Ksoftm.LargeNumberConverter
{
    using System;
    using System.Numerics;
    using UnityEngine;

    /// <summary>
    /// Represents a serializable large number using BigInteger under the hood,
    /// with support for Unity Inspector editing and formatted output.
    /// </summary>
    [Serializable]
    public class LargeNumber
    {
        #region Properties

        [SerializeField, LargeNumberSelector(true)]
        private string valueString = "0";

        [NonSerialized] private BigInteger _cachedValue = BigInteger.Zero;

        public BigInteger Value
        {
            get => _cachedValue;
            set
            {
                _cachedValue = value;
                valueString = value.ToString("R", CultureInfo.InvariantCulture).Parse().ToShortString();
            }
        }

        /// <summary>
        /// Returns a static zero value.
        /// </summary>
        public static LargeNumber Zero => new("0");

        #endregion

        #region Constructors

        public LargeNumber()
        {
            Value = BigInteger.Zero;
        }

        public LargeNumber(string value)
        {
            Value = TryParse(value, out var result) ? result : BigInteger.Zero;
        }

        public LargeNumber(BigInteger value)
        {
            Value = value;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Attempts to parse a string to BigInteger safely.
        /// </summary>
        private static bool TryParse(string input, out BigInteger result)
        {
            try
            {
                result = input.Parse();
                return true;
            }
            catch
            {
                result = BigInteger.Zero;
                return false;
            }
        }

        /// <summary>
        /// Returns the formatted short string representation.
        /// </summary>
        public string ToShortString()
        {
            return Value.ToShortString();
        }

        /// <summary>
        /// Shorthand to get short string.
        /// </summary>
        public string Short() => ToShortString();

        public override string ToString() => ToShortString();

        #endregion

        #region Implicit Conversions (Optional - uncomment if needed)

        public static implicit operator LargeNumber(string value) => new(value);
        public static implicit operator LargeNumber(BigInteger value) => new(value);
        public static implicit operator BigInteger(LargeNumber largeNumber) => largeNumber.Value;
        // public static implicit operator string(LargeNumber largeNumber) => largeNumber.ToShortString();

        #endregion
    }
}