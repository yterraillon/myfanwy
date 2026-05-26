using System.Text.Json.Serialization;

namespace EnBref.Infrastructure.ClaudeAgents;

public class ClaudeRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "claude-sonnet-4-6";

    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 1024;

    [JsonPropertyName("system")]
    public string? System { get; set; }

    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = [];

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; } = 0.3;

    [JsonPropertyName("tools")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Tool>? Tools { get; set; }

    [JsonPropertyName("tool_choice")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ToolChoice? ToolChoiceConfig { get; set; }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    public class Tool
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("input_schema")]
        public object InputSchema { get; set; } = new();
    }

    public class ToolChoice
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "tool";

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
