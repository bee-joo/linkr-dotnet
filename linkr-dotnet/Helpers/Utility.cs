using System.Text.RegularExpressions;

namespace linkr_dotnet.Helpers
{
    public static class Utility
    {
        private readonly static Random _random = new();

        public static string GenerateString(int length)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(it => alphabet[_random.Next(0, alphabet.Length)]);

            return new string(chars.ToArray());
        }

        public static bool CheckURLValid(string url) =>
            Regex.IsMatch(url, @"(http|https)://(([www\.])?|([\da-z-\.]+))\.([a-z\.]{2,3})$");
    }
}
