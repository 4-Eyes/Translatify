using System.Collections.Generic;

namespace Decodify
{
    public static class Languages
    {
        public class LanguageCodes : LanguageCodeBase
        {
            public string Google { get; set; }
            public string Bing { get; set; }

            public string[] DefaultCode()
            {
                return Google != null ? new[] {Google, "Google"} : new[] {Bing, "Bing"};
            }

            public string FallbackCode { set; private get; }
            public override string LowerCode => FallbackCode ?? GetDefaultBaseCode().ToLower();
            public override string UpperCode => GetDefaultBaseCode().ToUpper();

            private string GetDefaultBaseCode()
            {
                return DefaultCode()[0].ToLower().Replace("-", "");
            }
        }

        public static readonly Dictionary<string, LanguageCodes> Language = new Dictionary<string, LanguageCodes>()
        {
            {"Afrikaans", new LanguageCodes {Google="af", Bing=null}},
            {"Albanian", new LanguageCodes {Google="sq", Bing=null}},
            {"Arabic", new LanguageCodes {Google="ar", Bing="ar"}},
            {"Armenian", new LanguageCodes {Google="hy", Bing=null}},
            {"Azerbaijani", new LanguageCodes {Google="az", Bing=null}},
            {"Basque", new LanguageCodes {Google="eu", Bing=null}},
            {"Belarusian", new LanguageCodes {Google="be", Bing=null}},
            {"Bengali", new LanguageCodes {Google="bn", Bing=null}},
            {"Bosnian", new LanguageCodes {Google="bs", Bing="bs-Latn"}},
            {"Bulgarian", new LanguageCodes {Google="bg", Bing="bg"}},
            {"Catalan", new LanguageCodes {Google="ca", Bing="ca"}},
            {"Cebuano", new LanguageCodes {Google="ceb", Bing=null}},
            {"Chichewa", new LanguageCodes {Google="ny", Bing=null}},
            {"Chinese Simplified", new LanguageCodes {Google="zh-CN", Bing="zh-CHS"}},
            {"Chinese Traditional", new LanguageCodes {Google="zh-TW", Bing="zh-CHT"}},
            {"Croatian", new LanguageCodes {Google="hr", Bing="hr"}},
            {"Czech", new LanguageCodes {Google="cs", Bing="cs"}},
            {"Danish", new LanguageCodes {Google="da", Bing="da"}},
            {"Dutch", new LanguageCodes {Google="nl", Bing="nl"}},
            {"Esperanto", new LanguageCodes {Google="eo", Bing=null}},
            {"Estonian", new LanguageCodes {Google="et", Bing="et"}},
            {"Filipino", new LanguageCodes {Google="tl", Bing=null}},
            {"Finnish", new LanguageCodes {Google="fi", Bing="fi"}},
            {"French", new LanguageCodes {Google="fr", Bing="fr"}},
            {"Galician", new LanguageCodes {Google="gl", Bing=null}},
            {"Georgian", new LanguageCodes {Google="ka", Bing=null}},
            {"German", new LanguageCodes {Google="de", Bing="de"}},
            {"Greek", new LanguageCodes {Google="el", Bing="el"}},
            {"Gujarati", new LanguageCodes {Google="gu", Bing=null}},
            {"Haitian Creole", new LanguageCodes {Google="ht", Bing="ht"}},
            {"Hausa", new LanguageCodes {Google="ha", Bing=null}},
            {"Hebrew", new LanguageCodes {Google="iw", Bing="he"}},
            {"Hindi", new LanguageCodes {Google="hi", Bing="hi"}},
            {"Hmong", new LanguageCodes {Google="hmn", Bing="mww"}},
            {"Hungarian", new LanguageCodes {Google="hu", Bing="hu"}},
            {"Icelandic", new LanguageCodes {Google="is", Bing=null}},
            {"Igbo", new LanguageCodes {Google="ig", Bing=null}},
            {"Indonesian", new LanguageCodes {Google="id", Bing="id", FallbackCode = "in"}},
            {"Irish", new LanguageCodes {Google="ga", Bing=null}},
            {"Italian", new LanguageCodes {Google="it", Bing="it"}},
            {"Japanese", new LanguageCodes {Google="ja", Bing="ja"}},
            {"Javanese", new LanguageCodes {Google="jw", Bing=null}},
            {"Kannada", new LanguageCodes {Google="kn", Bing=null}},
            {"Kazakh", new LanguageCodes {Google="kk", Bing=null}},
            {"Khmer", new LanguageCodes {Google="km", Bing=null}},
            {"Klingon", new LanguageCodes {Google=null, Bing="tlh"}},
            {"Klingon IpIqaD", new LanguageCodes {Google=null, Bing="tlh-Qaak"}},
            {"Korean", new LanguageCodes {Google="ko", Bing="ko"}},
            {"Lao", new LanguageCodes {Google="lo", Bing=null}},
            {"Latin", new LanguageCodes {Google="la", Bing=null}},
            {"Latvian", new LanguageCodes {Google="lv", Bing="lv"}},
            {"Lithuanian", new LanguageCodes {Google="lt", Bing="lt"}},
            {"Macedonian", new LanguageCodes {Google="mk", Bing=null}},
            {"Malagasy", new LanguageCodes {Google="mg", Bing=null}},
            {"Malay", new LanguageCodes {Google="ms", Bing="ms"}},
            {"Malayalam", new LanguageCodes {Google="ml", Bing=null}},
            {"Maltese", new LanguageCodes {Google="mt", Bing="mt"}},
            {"Maori", new LanguageCodes {Google="mi", Bing=null}},
            {"Marathi", new LanguageCodes {Google="mr", Bing=null}},
            {"Mongolian", new LanguageCodes {Google="mn", Bing=null}},
            {"Myanmar (Burmese)", new LanguageCodes {Google="my", Bing=null}},
            {"Nepali", new LanguageCodes {Google="ne", Bing=null}},
            {"Norwegian", new LanguageCodes {Google="no", Bing="no"}},
            {"Persian", new LanguageCodes {Google="fa", Bing="fa"}},
            {"Polish", new LanguageCodes {Google="pl", Bing="pl"}},
            {"Portuguese", new LanguageCodes {Google="pt", Bing="pt"}},
            {"Punjabi", new LanguageCodes {Google="ma", Bing=null}},
            {"Queretaro Otomi", new LanguageCodes {Google=null, Bing="otq"}},
            {"Romanian", new LanguageCodes {Google="ro", Bing="ro"}},
            {"Russian", new LanguageCodes {Google="ru", Bing="ru"}},
            {"Serbian", new LanguageCodes {Google="sr", Bing="sr-Cyrl"}},
            {"Serbian (Latin)", new LanguageCodes {Google=null, Bing="sr-Latn"}},
            {"Sesotho", new LanguageCodes {Google="st", Bing=null}},
            {"Sinhala", new LanguageCodes {Google="si", Bing=null}},
            {"Slovak", new LanguageCodes {Google="sk", Bing="sk"}},
            {"Slovenian", new LanguageCodes {Google="sl", Bing="sl"}},
            {"Somali", new LanguageCodes {Google="so", Bing=null}},
            {"Spanish", new LanguageCodes {Google="es", Bing="es"}},
            {"Sudanese", new LanguageCodes {Google="su", Bing=null}},
            {"Swahili", new LanguageCodes {Google="sw", Bing=null}},
            {"Swedish", new LanguageCodes {Google="sv", Bing="sv"}},
            {"Tajik", new LanguageCodes {Google="tg", Bing=null}},
            {"Tamil", new LanguageCodes {Google="ta", Bing=null}},
            {"Telugu", new LanguageCodes {Google="te", Bing=null}},
            {"Thai", new LanguageCodes {Google="th", Bing="th"}},
            {"Turkish", new LanguageCodes {Google="tr", Bing="tr"}},
            {"Ukrainian", new LanguageCodes {Google="uk", Bing="uk"}},
            {"Urdu", new LanguageCodes {Google="ur", Bing="ur"}},
            {"Uzbek", new LanguageCodes {Google="uz", Bing=null}},
            {"Vietnamese", new LanguageCodes {Google="vi", Bing="vi"}},
            {"Welsh", new LanguageCodes {Google="cy", Bing="cy"}},
            {"Yiddish", new LanguageCodes {Google="yi", Bing=null, FallbackCode = "ji"}},
            {"Yoruba", new LanguageCodes {Google="yo", Bing=null}},
            {"Yucatec Maya", new LanguageCodes {Google=null, Bing="yua"}},
            {"Zulu", new LanguageCodes {Google="zu", Bing=null}},
        };
    }
}