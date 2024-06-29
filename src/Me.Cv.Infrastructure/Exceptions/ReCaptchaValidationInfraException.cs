using Me.Cv.Common.Exceptions;

namespace Me.Cv.Infrastructure.Exceptions;

public class ReCaptchaValidationInfraException : BadRequestException
{
    public ReCaptchaValidationInfraException(string message) : base(message)
    {
    }

    public ReCaptchaValidationInfraException(string message, string details) : base(message, details)
    {
    }
}
