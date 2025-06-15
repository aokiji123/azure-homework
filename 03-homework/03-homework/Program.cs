using _03_homework;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var translatorConfig = configuration.GetSection("AzureTranslator").Get<AzureTranslatorConfig>();

            if (translatorConfig == null)
            {
                Console.WriteLine("Error: AzureTranslator configuration section not found in appsettings.json");
                return;
            }

            var translatorService = new TranslatorService(translatorConfig);

            Console.WriteLine("Enter text for translation:");
            var text = Console.ReadLine();

            Console.WriteLine("Enter language code to translate('en', 'de', 'fr'):");
            var toLanguage = Console.ReadLine();

            var translation = await translatorService.TranslateTextAsync(text, toLanguage);
            Console.WriteLine($"Translation: {translation}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: appsettings.json file not found. Please create it with your Azure Translator configuration.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Configuration Error: {ex.Message}");
            Console.WriteLine("Please check your appsettings.json file and ensure all Azure Translator settings are provided:");
            Console.WriteLine("- Key: Your Azure Translator subscription key");
            Console.WriteLine("- Endpoint: https://api.cognitive.microsofttranslator.com");
            Console.WriteLine("- Region: Your Azure resource region");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}