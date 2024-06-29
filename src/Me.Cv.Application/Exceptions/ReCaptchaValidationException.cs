using Me.Cv.Common.Exceptions;

namespace Me.Cv.Application.Exceptions;

public class ReCaptchaValidationException : BadRequestException
{
    public ReCaptchaValidationException(string message) : base(message)
    {
    }

    public ReCaptchaValidationException(string message, string details) : base(message, details)
    {
    }
}
