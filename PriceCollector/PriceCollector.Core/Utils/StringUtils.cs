using System.Text.RegularExpressions;

namespace PriceCollector.Core.Utils
{
    public static class StringUtils
    {
        public static string DeleteCurrencySymbolsAndTrim(this string str)
        {
            Regex reg = new Regex("[$€EUReur]");
            str = reg.Replace(str, string.Empty).Trim();
            return str;
        }

        public static string DeleteSymbolsAndTrim(this string str, params char[] symbols)
        {
            string symbolsString = string.Join(' ', symbols);
            Regex reg = new Regex($"[{symbolsString}]");
            str = reg.Replace(str, string.Empty).Trim();
            return str;
        }

        public static string ReplaceDotByComma(this string str)
        {
            Regex reg = new Regex("[.]");
            str = reg.Replace(str, ",").Trim();
            return str;
        }
    }
}