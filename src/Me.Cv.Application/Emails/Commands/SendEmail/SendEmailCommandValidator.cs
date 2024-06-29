namespace Me.Cv.Application.Emails.Commands.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotNull().WithMessage("Email can't be null");

        RuleFor(c => c.Email.To)
            .NotNull().WithMessage("Email.To can't be null")
            .NotEmpty().WithMessage("Email.To can't be empty")
            .EmailAddress().WithMessage("Email.To must be a valid email address");

        RuleFor(c => c.Email.Subject)
            .NotNull().WithMessage("Email.Subject can't be null")
            .NotEmpty().WithMessage("Email.Subject can't be empty")
            .MinimumLength(3).WithMessage("Email.Subject must be at least 3 characters");

        RuleFor(c => c.Email.Body)
            .NotNull().WithMessage("Email.Body can't be null")
            .NotEmpty().WithMessage("Email.Body can't be empty")
            .MinimumLength(3).WithMessage("Email.Body must be at least 3 characters");

        RuleFor(c => c.Email.ReCaptcha)
            .NotNull().WithMessage("Email.ReCaptcha can't be null")
            .NotEmpty().WithMessage("Email.ReCaptcha can't be empty");
    }
}
