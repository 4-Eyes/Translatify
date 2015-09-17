using System.Net.Http;

namespace Decodify
{
    public abstract class Translator
    {
        public abstract string Translate(string words, string languageCode);
        protected HttpClient Client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = true});
    }
}