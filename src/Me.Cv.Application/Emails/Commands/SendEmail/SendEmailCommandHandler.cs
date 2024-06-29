namespace Me.Cv.Application.Emails.Commands.SendEmail;

public class SendEmailCommandHandler(
    IReCaptchaService reCaptchaService,
    IEmailService emailService) : ICommandHandler<SendEmailCommand, SendEmailResult>
{
    public async Task<SendEmailResult> Handle(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var isCaptchaValid = await reCaptchaService.ValidateAsync(command.Email.ReCaptcha);
        if (!isCaptchaValid)
            throw new ReCaptchaValidationException("Invalid reCaptcha token.");
        var email = command.Email.Adapt<Domain.Entities.Email>();
        await emailService.Send(email);
        return new SendEmailResult(true);
    }
}
