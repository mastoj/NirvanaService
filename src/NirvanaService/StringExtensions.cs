using System;
using System.Text.RegularExpressions;

namespace NirvanaService
{
    public static class StringExtensions
    {
        public static string ResolveEnvVariables(this string text)
        {
            var regexp = new Regex(".*?(%.+?%).*?");
            var matches = regexp.Matches(text);
            foreach (Match match in matches)
            {
                var unescapedKey = match.Groups[1].Value;
                var key = unescapedKey.Substring(1).Substring(0, unescapedKey.Length - 2);
                var environmentVariable = Environment.GetEnvironmentVariable(key);
                if (!string.IsNullOrWhiteSpace(environmentVariable))
                {
                    text = text.Replace(unescapedKey, environmentVariable);
                }
            }
            return text;
        }
    }
}