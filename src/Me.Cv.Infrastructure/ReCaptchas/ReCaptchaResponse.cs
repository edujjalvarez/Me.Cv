using System.Text.Json.Serialization;

namespace Me.Cv.Infrastructure.ReCaptchas;

public record ReCaptchaResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; init; } = default!;

    [JsonPropertyName("challenge_ts")]
    public DateTime? ChallengeTs { get; init; }

    [JsonPropertyName("hostname")]
    public string Hostname { get; init; } = default!;

    [JsonPropertyName("error-codes")]
    public List<string> ErrorCodes { get; init; } = [];
}
