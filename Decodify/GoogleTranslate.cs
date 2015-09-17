using System;
using System.Text.RegularExpressions;

namespace Decodify
{
    public class GoogleTranslate : Translator
    {

        private readonly string _googleRequestUrl = "https://translate.google.com/translate_a/single?client=t&sl=en&tl={0}&dt=t&ie=UTF-8&oe=UTF-8&q={1}";

        public override string Translate(string words, string languageCode)
        {
            try
            {
                var translation =
                    Client.GetStringAsync(string.Format(_googleRequestUrl, languageCode,
                        words))
                        .Result;
                var parsed = ParseResponse(translation);
                return parsed ?? words;
            }
            catch (Exception)
            {
                Console.WriteLine("Bad request for words " + words);
                return words;
            }
        }

        private static string ParseResponse(string response)
        {
            var m = Regex.Match(response, "\\[\\[\\[\"(?<trans>[^\"]+)\"");
            return !string.IsNullOrWhiteSpace(m.Groups["trans"].Value) ? m.Groups["trans"].Value : null;
        }
    }
}