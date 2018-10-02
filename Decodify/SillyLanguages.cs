using System;
using System.Collections.Generic;

namespace Decodify
{
    public static class SillyLanguages
    {
        public class LanguageCodes : LanguageCodeBase
        {
            public Func<string, string> Translate { get; set; }
            private readonly string _code;
            public override string LowerCode => _code;
            public override string UpperCode => _code;

            public LanguageCodes(string code)
            {
                _code = code;
            }
        }

        public static readonly Dictionary<string, LanguageCodes> Language = new Dictionary<string, LanguageCodes>()
        {
            {"Hodor", new LanguageCodes("ho") {Translate = TranslateHodor}},
            {"Programmer", new LanguageCodes("pr") {Translate = TranslateProgrammer}},
            {"Foo", new LanguageCodes("fo") {Translate = TranslateFoo}},
            {"Groot", new LanguageCodes("gr") {Translate = TranslateGroot}},
            {"White Space", new LanguageCodes("wi") {Translate = TranslateWhiteSpace}},
            {"Random", new LanguageCodes("rx") {Translate = TranslateRandom}}
        };

        private static string TranslateWhiteSpace(string arg)
        {
            return " ";
        }

        private static readonly Random Rand = new Random();

        private static string TranslateHodor(string english)
        {
            if (string.IsNullOrWhiteSpace(english))
            {
                return english;
            }

            var spl = english.Split(' ');
            for (var i = 0; i < spl.Length; i++)
            {
                if (spl[i].Length < 5 && Rand.Next(3) == 1)
                {
                    spl[i] = "hodor";
                }
                else
                {
                    var olen = spl[i].Length - 4 + Rand.Next(10) - Rand.Next(8);
                    if (olen <= 0) olen = 1;
                    spl[i] = "h" + new string('o', olen) + "dor";
                }
            }

            english = string.Join(" ", spl);
            var h = Rand.Next(english.Length);
            for (var i = 0; i < h; i++)
            {
                var ind = Rand.Next(english.Length);
                var beginning = english.Substring(0, ind);
                var c = english[ind];
                c = char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c);
                var end = english.Substring(ind + 1);
                english = beginning + c + end;
            }

            return english;
        }

        private static string TranslateProgrammer(string english)
        {
            string[] words =
            {
                "Build",
                "Compile",
                "Refactor",
                "Git",
                "Maven",
                "Java",
                "C#",
                "Python",
                "C",
                "C++",
                "Ruby",
                "Lisp",
                "Fix",
                "Bug",
                "Issues",
                "Agilefant",
                "Breakpoint",
                "Pipes",
                "NullPointerException",
                "void",
                "StackOverflow",
                "heap",
                "queue",
                "CLI",
                "GUI",
                "Revert",
                "Push",
                "Pull",
                "IntelliJ",
                "Agile",
                "Client",
                "Documentation",
                "It worked on my machine",
                "Did you break it?",
                "Fail",
                "derp",
            };

            var spl = english.Split(' ');
            for (var i = 0; i < spl.Length; i++)
            {
                spl[i] = words[Rand.Next(words.Length)];
            }
            return string.Join(" ", spl);
        }

        private static string TranslateFoo(string english)
        {
            switch (Rand.Next(5))
            {
                case 0:
                    return "Foo";
                case 1:
                    return "Bar";
                case 2:
                    return "Baar";
                case 3:
                    return "Foooo";
                case 4:
                    return "FooBar";
                default:
                    return "Foo?";
            }
        }

        private static string TranslateGroot(string english)
        {
            string[] words = {"I", "am", "groot"};

            var spl = english.Split(' ');
            for (var i = 0; i < spl.Length; i++)
            {
                spl[i] = words[Rand.Next(words.Length)];
            }
            return string.Join(" ", spl);
        }

        private static string TranslateRandom(string english)
        {
            return Rand.Next(english.Length).ToString();
        }
    }
}
