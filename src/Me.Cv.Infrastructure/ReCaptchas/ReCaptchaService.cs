using Me.Cv.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Me.Cv.Infrastructure.ReCaptchas;

public class ReCaptchaService : IReCaptchaService
{
    private readonly ReCaptchaSettings _reCaptchaSettings;
    private readonly ILogger<ReCaptchaService> _logger;
    private readonly HttpClient _httpClient;

    public ReCaptchaService(
        ReCaptchaSettings reCaptchaSettings,
        ILogger<ReCaptchaService> logger)
    {
        _reCaptchaSettings = reCaptchaSettings;
        _logger = logger;
        _httpClient = new HttpClient
        {
            MaxResponseContentBufferSize = 1024 * 1024 * 25 // 25Mb
        };
    }

    public async Task<bool> ValidateAsync(string reCaptcha)
    {
        var verificationUrl = _reCaptchaSettings.VerificationUrl;
        var secretKey = _reCaptchaSettings.SecretKey;
        var endpoint = $"{verificationUrl}?secret={secretKey}&response={reCaptcha}";
        var uri = new Uri(endpoint);
        var response = await _httpClient.GetAsync(uri);
        var responseStr = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new ReCaptchaValidationInfraException($"An or more error has occurred: {response.StatusCode} ({(int)response.StatusCode}). Detail: {responseStr}");
        var reCaptchaResponse = JsonSerializer.Deserialize<ReCaptchaResponse>(responseStr);
        if (reCaptchaResponse is null)
            throw new ReCaptchaValidationInfraException($"An or more error has occurred: Response body can't be serialized.");
        if (!reCaptchaResponse.Success)
        {
            var errorCodes = string.Join(", ", reCaptchaResponse.ErrorCodes);
            throw new ReCaptchaValidationInfraException($"An or more error has occurred: {errorCodes}");
        }
        return true;
    }
}
