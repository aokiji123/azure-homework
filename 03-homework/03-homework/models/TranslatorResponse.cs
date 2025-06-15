using System.Text.Json.Serialization;

namespace _03_homework.models;

public class TranslatorResponse
{
    [JsonPropertyName("detectedLanguage")]
    public DetectedLanguage DetectedLanguage { get; set; }
    
    [JsonPropertyName("translations")]
    public List<Translation> Translations { get; set; }
}

public class DetectedLanguage
{
    [JsonPropertyName("language")]
    public string Language { get; set; }
    
    [JsonPropertyName("score")]
    public double Score { get; set; }
}

public class Translation
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("to")]
    public string To { get; set; }
}