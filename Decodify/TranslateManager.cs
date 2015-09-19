using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Decodify
{
    public static class TranslateManager
    {
        private static IEnumerable<string> _differentKeys;
        private static readonly GoogleTranslate GoogleTranslator = new GoogleTranslate();
        private static readonly BingTranslate BingTranslator = new BingTranslate();
        private static IEnumerable<string> _missingKeys;
        private static ResourceBundle _englishBundle;

        /// <summary>
        /// The Base path where all the resources are.
        /// </summary>
        public static string BasePath { get; set; }
        /// <summary>
        /// The name of the resource bundle, including language codes
        /// </summary>
        public static string BundleName { get; set; }

        public static void TranslateAll()
        {
            if (string.IsNullOrWhiteSpace(BasePath) || string.IsNullOrWhiteSpace(BundleName))
            {
                throw new Exception("Can't translate anything if you haven't set the Base Path or Bundle Names");
            }
            var referenceBundle = ResourceBundle.Parse("english_reference.properties");
            _englishBundle = ResourceBundle.Parse(BasePath + "\\" + BundleName + "_en_EN.properties");
            Console.WriteLine("Finished Loading Reference Files");
            _differentKeys = _englishBundle.DifferentKeys(referenceBundle);
            _missingKeys = referenceBundle.MissingKeys(_englishBundle);
            Console.WriteLine("Translating Normal Languages\n----------------------------------");
            TranslateLanguages();
            Console.WriteLine("\nTranslating Silly Languages\n----------------------------------");
            TranslateSillyLanguages();
            ResourceBundle.Save(_englishBundle, "english_reference.properties");
        }

        private static void TranslateSillyLanguages()
        {
            var completed = 0;
            foreach (var language in SillyLanguages.Language)
            {
                Console.WriteLine("Starting " + language.Key);
                var keysToTranslate = _differentKeys.Concat(_missingKeys);
                ResourceBundle oldBundle;
                try
                {
                    oldBundle = ResourceBundle.Parse(PathGenerator(language.Value));
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No properties file found for language " + language.Key);
                    keysToTranslate = _englishBundle.Entries.Keys;
                    oldBundle = new ResourceBundle();
                }
                var i = 0.0;
                var toTranslate = keysToTranslate as string[] ?? keysToTranslate.ToArray();
                var length = toTranslate.Count();

                foreach (var key in toTranslate)
                {
                    Console.Write($"\r{Math.Round(i / length * 100, 2)}% Complete,   \t" +
                                  $"{Math.Round(((i / length + completed) / SillyLanguages.Language.Count) * 100, 2)}% Overall            ");
                    var englishWords = _englishBundle[key];
                    var translated = language.Value.Translate(englishWords);
                    translated = Regex.Replace(translated, @"[^\x00-\x7F]", c => $@"\u{(int)c.Value[0]:x4}");
                    oldBundle[key] = translated;
                    i++;
                }
                Console.Write("\r100% Complete                ");
                ResourceBundle.Save(oldBundle, PathGenerator(language.Value));
                Console.WriteLine("\nFinished " + language.Key);
                completed++;
            }
        }

        private static void TranslateLanguages()
        {
            var completed = 0;
            foreach (var language in Languages.Language)
            {
                Console.WriteLine("Starting " + language.Key);
                var defaultCode = language.Value.DefaultCode()[0];
                var defaultLanguage = language.Value.DefaultCode()[1];
                var keysToTranslate = _differentKeys.Concat(_missingKeys);
                ResourceBundle oldBundle;
                try
                {
                    oldBundle = ResourceBundle.Parse(PathGenerator(language.Value));
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No properties file found for language " + language.Key);
                    keysToTranslate = _englishBundle.Entries.Keys;
                    oldBundle = new ResourceBundle();
                }
                var i = 0.0;
                var toTranslate = keysToTranslate as string[] ?? keysToTranslate.ToArray();
                var length = toTranslate.Count();

                foreach (var key in toTranslate)
                {
                    var percent = i/length;
                    Console.Write($"\r{Math.Round(percent * 100, 2)}% Complete,   \t" +
                                  $"{Math.Round(((percent + completed) / Languages.Language.Count) * 100, 2)}% Overall      ");
                    var englishWords = _englishBundle[key];
                    string translated;
                    if (defaultLanguage.Equals("Google"))
                    {
                        translated = GoogleTranslator.Translate(englishWords, defaultCode);
                        if (translated.Equals(englishWords))
                        {
                            translated = BingTranslator.Translate(englishWords, language.Value.Bing);
                        }
                    }
                    else
                    {
                        translated = BingTranslator.Translate(englishWords, defaultCode);
                    }
                    translated = Regex.Replace(translated, @"[^\x00-\x7F]", c => $@"\u{(int) c.Value[0]:x4}");
                    oldBundle[key] = translated;
                    i++;
                }
                Console.Write("\r100% Complete    \t                ");
                ResourceBundle.Save(oldBundle, PathGenerator(language.Value));
                Console.WriteLine("\nFinished " + language.Key);
                completed++;
            }
        }

        private static string PathGenerator(LanguageCodeBase language)
        {
            return BasePath + "\\" + BundleName + "_" + language.LowerCode + "_" + language.UpperCode + ".properties";
        }
    }
}