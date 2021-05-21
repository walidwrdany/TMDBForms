using System.Text.RegularExpressions;

namespace TMDBForms
{
    public static class MyStringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toSearch"></param>
        /// <param name="toFind"></param>
        /// <returns></returns>
        public static bool Like(this string toSearch, string toFind)
        {
            return Regex.Replace(Regex.Replace(toSearch.ToLower(), @"[.()\:\!\-\[\]]", " ").Trim(), @"\s+", " ") == toFind.ToLower() ? true : false;
        }

        public static string GetString(this int number)
        {
            return number.ToString();
        }
    }
}
