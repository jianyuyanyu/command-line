using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotMake.CommandLine
{
    /// <summary>
    /// Provides CLI related <see cref="string" /> methods.
    /// </summary>
    public static class CliStringUtil
    {
        private static readonly Regex SplitWordsRegex = new Regex(@"
            (?<=[\p{Ll}\p{Lm}\p{Lo}])(?=[\p{Lu}\p{Lt}]) # split on what precedes is a lowercase or modifier or other letter
                                                        # and what follows is an uppercase or title-case letter

            |(?<=[\p{N}])(?=[\p{L}])                    # or split on what precedes is a numeric character
                                                        # and what follows is a letter (or vice versa)
            |(?<=[\p{L}])(?=[\p{N}])

            |([\p{Z}\p{P}]+)                            # or split on any kind of whitespace or invisible separator
                                                        # or on any kind of punctuation character.
        ", RegexOptions.IgnorePatternWhitespace);

        private static readonly Regex WordSpacesRegex = new Regex(
            @"^$|[\p{Z}\p{P}]+"
        );

        /// <summary>
        /// Splits a string in to words.
        /// </summary>
        /// <param name="value">A string instance.</param>
        /// <param name="keepSpaces">Whether to keep a single space between words if there were any whitespace or punctuation in the original.</param>
        /// <returns>An array of strings.</returns>
        public static string[] SplitWords(string value, bool keepSpaces = false)
        {
            var words = SplitWordsRegex.Split(value);

            if (keepSpaces)
            {
                var newWords = new List<string>();
                for (var index = 0; index < words.Length; index++)
                {
                    var word = words[index];

                    var nextIndex = index + 1;
                    if (WordSpacesRegex.IsMatch(word) && (nextIndex < words.Length))
                    {
                        var nextWord = words[nextIndex];
                        if (!WordSpacesRegex.IsMatch(nextWord) && newWords.Count > 0)
                            newWords.Add(" ");
                    }
                    else
                        newWords.Add(word);
                }

                if (newWords.Count > 0)
                {
                    var lastIndex = newWords.Count - 1;
                    var word = newWords[lastIndex];
                    if (WordSpacesRegex.IsMatch(word))
                        newWords.RemoveAt(lastIndex);
                }

                return newWords.ToArray();
            }

            return words.Where(w => !WordSpacesRegex.IsMatch(w)).ToArray();
        }

        /// <summary>
        /// Converts the string to a specific case.
        /// </summary>
        /// <param name="value">A string instance.</param>
        /// <param name="nameCasingConvention">The name casing convention to convert to.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules. If <paramref name="culture" /> is <see langword="null" />, the invariant culture is used.</param>
        /// <param name="keepSpaces">
        /// Whether to keep a single space between words if there were any whitespace or punctuation in the original.
        /// This works only for LowerCase, UpperCase, TitleCase which allows spaces.
        /// </param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string ToCase(string value, CliNameCasingConvention nameCasingConvention, CultureInfo culture = null, bool keepSpaces = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            culture ??= CultureInfo.InvariantCulture;
            var textInfo = culture.TextInfo;

            switch (nameCasingConvention)
            {
                case CliNameCasingConvention.LowerCase:
                    return string.Concat(
                        SplitWords(value, keepSpaces: keepSpaces)
                            .Select(word => textInfo.ToLower(word))
                    );
                case CliNameCasingConvention.UpperCase:
                    return string.Concat(
                        SplitWords(value, keepSpaces: keepSpaces)
                            .Select(word => textInfo.ToUpper(word))
                    );
                case CliNameCasingConvention.TitleCase:
                    return string.Concat(
                        SplitWords(value, keepSpaces: keepSpaces)
                            .Select(word => textInfo.ToTitleCase(textInfo.ToLower(word)))
                    );
                case CliNameCasingConvention.PascalCase:
                    return string.Concat(
                        SplitWords(value)
                            .Select(word => textInfo.ToTitleCase(textInfo.ToLower(word)))
                    );
                case CliNameCasingConvention.CamelCase:
                    return string.Concat(
                        SplitWords(value)
                            .Select((word, index) => (index == 0) ? textInfo.ToLower(word) : textInfo.ToTitleCase(textInfo.ToLower(word)))
                    );
                case CliNameCasingConvention.KebabCase:
                    return string.Join("-",
                        SplitWords(value)
                            .Select(word => textInfo.ToLower(word))
                    );
                case CliNameCasingConvention.SnakeCase:
                    return string.Join("_",
                        SplitWords(value)
                            .Select(word => textInfo.ToLower(word))
                    );
                default:
                    return value;
            }
        }

        /// <summary>
        /// Strips specific suffixes from the string.
        /// </summary>
        /// <param name="value">A string instance.</param>
        /// <param name="suffixes">Suffix strings to remove.</param>
        /// <param name="ignoreCase">Whether to ignore case.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string StripSuffixes(string value, IEnumerable<string> suffixes, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            foreach (var suffix in suffixes.OrderByDescending(s => s.Length))
            {
                if (string.IsNullOrEmpty(suffix)
                    || suffix.Length >= value.Length)
                    continue;

                if (value.EndsWith(suffix, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    return value.Substring(0, value.Length - suffix.Length);
            }

            return value;
        }


        /// <summary>
        /// Adds a specific prefix to the string.
        /// </summary>
        /// <param name="alias">A string instance.</param>
        /// <param name="namePrefixConvention">The prefix convention to use.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string AddPrefix(string alias, CliNamePrefixConvention namePrefixConvention)
        {
            if (namePrefixConvention == CliNamePrefixConvention.None)
                return alias;

            var prefixLength = GetPrefixLength(alias);

            if (prefixLength > 0) //Has prefix
                return alias;

            switch (namePrefixConvention)
            {
                case CliNamePrefixConvention.SingleHyphen:
                    return "-" + alias;
                default:
                case CliNamePrefixConvention.DoubleHyphen:
                    return "--" + alias;
                case CliNamePrefixConvention.ForwardSlash:
                    return "/" + alias;
            }
        }

        /// <summary>
        /// Removes prefixes (-, --, /) from the string.
        /// </summary>
        /// <param name="alias">A string instance.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string RemovePrefix(string alias)
        {
            var prefixLength = GetPrefixLength(alias);

            if (prefixLength > 0) //Has prefix
                return alias.Substring(prefixLength);

            return alias;
        }

        /// <summary>
        /// Gets the prefix (-, --, /) length from the string.
        /// </summary>
        /// <param name="alias">A string instance.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static int GetPrefixLength(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
                return 0;

            if (alias[0] == '-')
            {
                if (alias.Length > 1 && alias[1] == '-')
                    return 2;

                return 1;
            }

            if (alias[0] == '/')
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Splits prefixes (-, --, /) from the string.
        /// </summary>
        /// <param name="rawAlias">A string instance.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static (string Prefix, string Alias) SplitPrefix(string rawAlias)
        {
            if (rawAlias[0] == '/')
            {
                return ("/", rawAlias.Substring(1));
            }
            else if (rawAlias[0] == '-')
            {
                if (rawAlias.Length > 1 && rawAlias[1] == '-')
                {
                    return ("--", rawAlias.Substring(2));
                }

                return ("-", rawAlias.Substring(1));
            }

            return (null, rawAlias);
        }


        /// <summary>
        /// Formats a value as a printable string.
        /// </summary>
        /// <param name="value">An object value.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string FormatValue(object value)
        {
            if (value == null)
                return "null";

            if (value is string stringValue)
                return stringValue.ToLiteral();

            if (value is char charValue)
                return charValue.ToString().ToLiteral('\'');

            if (value is bool boolValue)
                return boolValue.ToString().ToLowerInvariant();

            if (value is IFormattable)
                return value.ToString();

            if (value is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToArray();

                return "[" + string.Join(", ", items.Select(FormatValue)) + "]";
            }

            if (value.ToString() != value.GetType().ToString())
                return value.ToString();

            return "object";
        }

        /// <summary>
        /// Gets an escaped string literal. Value is enclosed with double quotes and Unicode and ASCII non-printable characters are escaped.
        /// </summary>
        /// <param name="value">A string instance.</param>
        /// <param name="quoteChar">The enclosing quote char, by default it's double quotes.</param>
        /// <returns>A new <see cref="string" /> instance.</returns>
        public static string ToLiteral(this string value, char quoteChar = '"')
        {
            //https://stackoverflow.com/a/14087738

            var literal = new StringBuilder(value.Length + 2);

            literal.Append(quoteChar);

            foreach (var c in value)
            {
                switch (c)
                {
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        // ASCII printable character
                        if (c >= 0x20 && c <= 0x7e)
                        {
                            literal.Append(c);

                        }
                        // UTF16 control characters
                        else if (char.GetUnicodeCategory(c) == UnicodeCategory.Control)
                        {
                            literal.Append(@"\u");
                            literal.Append(((int)c).ToString("x4"));
                        }
                        else
                        {
                            literal.Append(c);
                        }
                        break;
                }
            }

            literal.Append(quoteChar);

            return literal.ToString();
        }
    }
}
