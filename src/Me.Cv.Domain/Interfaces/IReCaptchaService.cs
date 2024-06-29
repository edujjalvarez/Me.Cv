namespace Me.Cv.Domain.Interfaces;

public interface IReCaptchaService
{
    Task<bool> ValidateAsync(string reCaptcha);
}
