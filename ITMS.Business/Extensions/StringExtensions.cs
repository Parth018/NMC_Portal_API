using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMS.Business.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveEnd(this string s, string suffix)
        {
            return s.EndsWith(suffix) ? s.Substring(0, s.Length - suffix.Length) : s;
        }

        public static string RemoveStart(this string s, string prefix)
        {
            return s.StartsWith(prefix) ? s.Substring(prefix.Length) : s;
        }

        public static string PrefixWith(this string s, string prefix, string seperator = "-")
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(prefix))
            {
                return s;
            }
            return $"{prefix}{seperator}{s}";
        }

        public static string PrefixWith(this int s, string prefix, string seperator = "-")
        {
            return PrefixWith(s.ToString(), prefix, seperator);
        }

        public static string PrefixWith(this int? s, string prefix, string seperator = "-")
        {
            return !s.HasValue ? null : PrefixWith(s.ToString(), prefix, seperator);
        }

        public static string ToJoinedList(this IEnumerable<string> s, JoinedListStyle listStyle = JoinedListStyle.CommaSeperated)
        {
            switch (listStyle)
            {
                case JoinedListStyle.CommaSeperatedWithAnd:
                case JoinedListStyle.CommaSeperatedWithAmpersand:
                    if (s != null)
                    {
                        var list = s.Distinct().ToArray();
                        if (list.Length > 1)
                        {
                            return string.Join(", ", list, 0, list.Length - 1) + " " + (listStyle == JoinedListStyle.CommaSeperatedWithAnd ? "en" : "&") + " " + list[list.Length - 1];
                        }
                        return list.Length == 1 ? list[0] : null;
                    }
                    return null;
                default:
                    return string.Join(", ", s);
            }
        }

        public static string ReplaceLast(this string s, string oldValue, string newValue)
        {
            var place = s.LastIndexOf(oldValue, StringComparison.Ordinal);
            return place == -1 ? s : s.Remove(place, oldValue.Length).Insert(place, newValue);
        }

        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var chars = s.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }
    }

    public enum JoinedListStyle
    {
        CommaSeperated,
        CommaSeperatedWithAnd,
        CommaSeperatedWithAmpersand,
    }
}
