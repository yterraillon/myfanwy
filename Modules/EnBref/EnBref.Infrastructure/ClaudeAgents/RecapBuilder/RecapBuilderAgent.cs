using Application.AiAgents;
using Infrastructure.HttpClients;

namespace EnBref.Infrastructure.ClaudeAgents.RecapBuilder;

public class RecapBuilderAgent : IAiAgent<IEnumerable<string>, string>
{
    public string SystemPrompt { get; } = string.Empty;
    public string UserPrompt { get; }

    private readonly HttpClient _client;

    public RecapBuilderAgent(HttpClient client, Settings settings)
    {
        _client = client;
        _client.DefaultRequestHeaders.Add("x-api-key", settings.ClaudeApiKey ?? "");
        _client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
        UserPrompt = RecapBuilderPrompts.UserPrompt;
    }

    public async Task<string> Handle(IEnumerable<string> argument)
    {
        var prompt = UserPrompt + FormatTitlesForPrompt(argument);
        var request = new ClaudeRequest
        {
            Messages = [new ClaudeRequest.Message { Role = "user", Content = prompt }]
        };

        var result = await _client.PostRequest<ClaudeRequest, ClaudeResponse>(_client.BaseAddress!.ToString(), request);
        return result?.Content.FirstOrDefault(c => c.Type == "text")?.Text ?? string.Empty;
    }

    private static string FormatTitlesForPrompt(IEnumerable<string> titles) =>
        titles.Aggregate(string.Empty, (current, title) => current + $"\n- {title}");
}
