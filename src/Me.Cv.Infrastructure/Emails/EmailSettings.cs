namespace Me.Cv.Infrastructure.Emails;

public record EmailSettings
{
    public EmailSettings()
    {
    }

    public string Smpt { get; init; }
    public int Port { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
}
