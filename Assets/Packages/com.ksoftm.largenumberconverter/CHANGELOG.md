# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

- Initial setup.

## [1.0.0] - 2025-06-22

- First release of Large Number Converter package.
- Added core conversion logic.
- Added basic documentation.
- Added CustomPropertyAttribute.
- Added LargeNumberSelectorAttribute.

## [1.1.0] - 2025-06-23
- Improved performance of number conversion logic.
- Fixed rounding errors in edge cases.
- Updated documentation with usage examples.

## [1.2.0] - 2025-06-25
- Add LargeNumberConverter package with support for large number handling.
- Implemented `LargeNumber` class for serializable large number representation using `BigInteger`.
- Added `LargeNumberAttribute` for marking numeric string fields in the Unity Inspector.
- Created extension methods for `BigInteger` to facilitate parsing, formatting, and arithmetic operations.
- Enhanced `LargeNumberConverter` with methods for parsing and formatting large numbers using short-scale suffixes and "illion" names.
- Developed example scripts demonstrating the usage of `LargeNumber` and `LargeNumberSelector` in Unity.
- In Sample, Updated `PlayerManager` to utilize `LargeNumber` for managing player coin values.
