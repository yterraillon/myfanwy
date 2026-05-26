using Application.AiAgents;
using Infrastructure.HttpClients;

namespace EnBref.Infrastructure.ClaudeAgents.RecapFormatter;

public class RecapFormatterAgent : IAiAgent<string, Recap>
{
    public string SystemPrompt { get; } = string.Empty;
    public string UserPrompt { get; } = "Voici le texte brut à structurer :\n";

    private readonly HttpClient _client;

    private static readonly ClaudeRequest.Tool RecapTool = new()
    {
        Name = "create_recap",
        Description = "Crée un récap structuré à partir d'un texte d'actualités.",
        InputSchema = new
        {
            type = "object",
            properties = new
            {
                sections = new
                {
                    type = "array",
                    items = new
                    {
                        type = "object",
                        properties = new
                        {
                            Title = new { type = "string", description = "Titre de la section (ex: Politique, Économie)" },
                            Text = new { type = "string", description = "Contenu de la section, texte brut avec tirets" }
                        },
                        required = new[] { "Title", "Text" }
                    }
                }
            },
            required = new[] { "sections" }
        }
    };

    public RecapFormatterAgent(HttpClient client, Settings settings)
    {
        _client = client;
        _client.DefaultRequestHeaders.Add("x-api-key", settings.ClaudeApiKey ?? "");
        _client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    }

    public async Task<Recap> Handle(string argument)
    {
        var request = new ClaudeRequest
        {
            Temperature = 0.2,
            Messages = [new ClaudeRequest.Message { Role = "user", Content = UserPrompt + argument }],
            Tools = [RecapTool],
            ToolChoiceConfig = new ClaudeRequest.ToolChoice { Type = "tool", Name = "create_recap" }
        };

        var result = await _client.PostRequest<ClaudeRequest, ClaudeResponse>(_client.BaseAddress!.ToString(), request);
        var toolBlock = result?.Content.FirstOrDefault(c => c.Type == "tool_use");

        if (toolBlock?.Input is not { } input)
            return Recap.EmptyRecap;

        var sections = JsonSerializer.Deserialize<List<Section>>(
            input.GetProperty("sections").GetRawText()) ?? [];

        return new Recap
        {
            Title = BuildRecapTitle(),
            Sections = sections
        };
    }

    private static string BuildRecapTitle()
    {
        var today = DateTime.Now.ToString("dd MMMM", new System.Globalization.CultureInfo("fr-FR"));
        return $"Récap du {today}";
    }
}
