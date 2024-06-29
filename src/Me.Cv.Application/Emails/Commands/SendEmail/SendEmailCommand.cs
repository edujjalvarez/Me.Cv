namespace Me.Cv.Application.Emails.Commands.SendEmail;

public record SendEmailCommand(EmailDto Email) : ICommand<SendEmailResult>;
