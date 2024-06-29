namespace Me.Cv.Infrastructure.ReCaptchas;

public record ReCaptchaSettings
{
    public ReCaptchaSettings()
    {

    }

    public string VerificationUrl { get; init; }
    public string SecretKey { get; init; }
}
