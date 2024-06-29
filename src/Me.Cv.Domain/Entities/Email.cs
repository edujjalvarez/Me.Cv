namespace Me.Cv.Domain.Entities;

public class Email
{
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
    public string ReCaptcha { get; set; } = default!;
}
