using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Decodify
{
    public class ResourceBundle
    {
        public string LanguageName { get; set; }
        public Languages.LanguageCodes LanguageCode { get; set; }

        public Dictionary<string, string> Entries { get; private set; }

        public ResourceBundle()
        {
            Entries = new Dictionary<string, string>();
        }

        /// <summary>
        /// Loads a translation from a file.
        /// </summary>
        /// <param name="filePath">The file</param>
        /// <returns>The translation</returns>
        public static ResourceBundle Parse(string filePath)
        {
            var translation = new ResourceBundle();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var pair = line.Split('=');
                translation.Entries.Add(pair[0].Trim(), pair[1].Trim());
            }

            return translation;
        }

        /// <summary>
        /// Lets you save a translation to a file.
        /// </summary>
        /// <param name="translation">The translation</param>
        /// <param name="path">The file path</param>
        public static void Save(ResourceBundle translation, string path)
        {
            var lines = translation.Entries.Select(pair => $"{pair.Key} = {pair.Value}").ToList();
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// Gets or sets the value of a specific entry. Will also add keys if they don't exist
        /// and will return null on keys that don't exist
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value of that entry</returns>
        public string this[string key]
        {
            get { return Entries.ContainsKey(key) ? Entries[key] : null; }
            set
            {
                if (Entries.ContainsKey(key))
                {
                    Entries[key] = value;
                }
                else
                {
                    Entries.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Gets all the keys that are missing from this translation,
        /// given a master translation.
        /// </summary>
        /// <param name="master">The master translation</param>
        public IEnumerable<string> MissingKeys(ResourceBundle master)
        {
            return (from key in master.Entries.Keys where !Entries.ContainsKey(key) select key);
        }

        /// <summary>
        /// Gets all the keys that have changed. This method should be called on your new
        /// set of keys. new.DifferentKeys(old) and will give you a list of the keys that have changed.
        /// </summary>
        /// <param name="old">The old set of keys</param>
        /// <returns>The changed keys (all if on a different language)</returns>
        public IEnumerable<string> DifferentKeys(ResourceBundle old)
        {
            return from key in Entries.Keys where !old.Entries.ContainsKey(key) || this[key] != old[key] select key;
        }
    }
}