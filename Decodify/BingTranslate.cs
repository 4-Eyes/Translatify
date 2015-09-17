using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Decodify
{
    public class BingTranslate : Translator
    {
        private string _appId;

        private const string BingAppIdUrl = "http://www.bing.com/translator/dynamic/223578/js/LandingPage.js";
        private const string BingTranslateUrl = "http://api.microsofttranslator.com/v2/ajax.svc/TranslateArray2?appId={0}&texts=[\"{1}\"]&from=en&to={2}";

        public BingTranslate()
        {
            SetUpAppId();
        }

        private void SetUpAppId()
        {
            try
            {
                Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:42.0) Gecko/20100101 Firefox/42.0");
                var javascript = Client.GetStringAsync(BingAppIdUrl).Result;
                var match = Regex.Match(javascript, "rttAppId:\"(?<id>[^\"]+)\"");
                if (!string.IsNullOrWhiteSpace(match.Groups["id"].Value))
                {
                    _appId = match.Groups["id"].Value;
                }
                else
                {
                    throw new Exception("Failed to get app id from the JavaScript file. No Bing translations will work");
                }
            }
            catch (WebException)
            {
                Console.WriteLine("Failed to get a Bing app Id");
            }
        }

        public override string Translate(string words, string languageCode)
        {
            try
            {
                var translation =
                    Client.GetStringAsync(string.Format(BingTranslateUrl, _appId, Regex.Replace(words, "\\s+", "+"),
                        languageCode)).Result;
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
            var m = Regex.Match(response, "\"TranslatedText\":\"(?<trans>[^\"]+)\"");
            return !string.IsNullOrWhiteSpace(m.Groups["trans"].Value) ? m.Groups["trans"].Value : null;
        }
    }
}