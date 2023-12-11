using Azure;
using Azure.AI.OpenAI;

public class OpenAiService
{
    // private readonly string _modelName = String.Empty;
    private readonly string _modelName = "chatmodel";
    private readonly OpenAIClient _client;

    private readonly string _systemPrompt = @"
    You are an AI assistant that helps people find information.
    Provide concise answers that are polite and professional." + Environment.NewLine;

    private readonly string _summarizePrompt = @"
    Summarize this prompt in one or two words to use as a label in a button on a web page.
    Do not use any punctuation." + Environment.NewLine;

    public OpenAiService(string endpoint, string key, string modelName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(modelName);
        ArgumentNullException.ThrowIfNullOrEmpty(endpoint);
        ArgumentNullException.ThrowIfNullOrEmpty(key);
        
        //_modelName = modelName;

        Uri uri = new(endpoint);
        AzureKeyCredential credential = new(key);
        
        _client = new(
            endpoint: uri,
            keyCredential: credential
        );
    }

    public async Task<(string completionText, int completionTokens)> GetChatCompletionAsync(string sessionId, string userPrompt)
    {
        Azure.AI.OpenAI.ChatRequestSystemMessage systemMessage = new(_systemPrompt);
        Azure.AI.OpenAI.ChatRequestUserMessage userMessage = new(userPrompt);

        ChatCompletionsOptions options = new()
        {
            DeploymentName = _modelName,
            Messages = {
                systemMessage,
                userMessage
            },
            User = sessionId,
            MaxTokens = 4000,
            Temperature = 0.3f,
            NucleusSamplingFactor = 0.5f,
            FrequencyPenalty = 0,
            PresencePenalty = 0
        };

        ChatCompletions completions = await _client.GetChatCompletionsAsync(options);

        return (
            completionText: completions.Choices[0].Message.Content,
            completionTokens: completions.Usage.CompletionTokens
        );
    }

    public async Task<string> SummarizeAsync(string sessionId, string conversationText)
    {
        Azure.AI.OpenAI.ChatRequestSystemMessage systemMessage = new(_summarizePrompt);
        Azure.AI.OpenAI.ChatRequestUserMessage userMessage = new(conversationText);

        ChatCompletionsOptions options = new()
        {
            DeploymentName = _modelName,
            Messages = {
                systemMessage,
                userMessage
            },
            User = sessionId,
            MaxTokens = 200,
            Temperature = 0.0f,
            NucleusSamplingFactor = 1.0f,
            FrequencyPenalty = 0,
            PresencePenalty = 0
        };

        ChatCompletions completions = await _client.GetChatCompletionsAsync(options);

        return completions.Choices[0].Message.Content;
    }
}
