namespace Me.Cv.Domain.Interfaces;

public interface IEmailService
{
    Task Send(Email email);
}
