using _03_homework.models;

namespace _03_homework;

using System.Text;
using System.Text.Json;

public class TranslatorService {

    private readonly AzureTranslatorConfig _config;
    private readonly HttpClient _httpClient;

    public TranslatorService(AzureTranslatorConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        
        // Validate configuration
        if (string.IsNullOrEmpty(_config.Key))
            throw new ArgumentException("Azure Translator Key is required", nameof(config));
        if (string.IsNullOrEmpty(_config.Endpoint))
            throw new ArgumentException("Azure Translator Endpoint is required", nameof(config));
        if (string.IsNullOrEmpty(_config.Region))
            throw new ArgumentException("Azure Translator Region is required", nameof(config));
            
        _httpClient = new HttpClient();
    }

    public async Task<string> TranslateTextAsync(string text, string toLanguage)
    {
        if (string.IsNullOrEmpty(text))
            return "Error: Text to translate cannot be empty";
            
        if (string.IsNullOrEmpty(toLanguage))
            return "Error: Target language cannot be empty";

        try
        {
            string route = $"/translate?api-version=3.0&to={toLanguage}";
            string uri = _config.Endpoint + route;

            var requestBody = new object[] { new { Text = text } };
            var requestBodyJson = JsonSerializer.Serialize(requestBody);
            using var requestContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.Key);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", _config.Region);

            var response = await _httpClient.PostAsync(uri, requestContent);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"API Error: {response.StatusCode} - {responseBody}";
            }

            var result = JsonSerializer.Deserialize<List<TranslatorResponse>>(responseBody);
            return result?.FirstOrDefault()?.Translations?.FirstOrDefault()?.Text ?? "Translation failed - no translation found in response";
        }
        catch (HttpRequestException ex)
        {
            return $"Network error: {ex.Message}";
        }
        catch (JsonException ex)
        {
            return $"JSON parsing error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Unexpected error: {ex.Message}";
        }
    }
}