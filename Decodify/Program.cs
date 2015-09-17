namespace Decodify
{
    class Program
    {
        static void Main(string[] args)
        {
            TranslateManager.BundleName = "words";
            TranslateManager.BasePath = "..\\src\\main\\resources\\sws\\murcs\\languages";
            TranslateManager.TranslateAll();
        }
    }
}
