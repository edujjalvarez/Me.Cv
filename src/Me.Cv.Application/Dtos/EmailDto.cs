namespace Me.Cv.Application.Dtos;

public record EmailDto(
    string To,
    string Subject,
    string Body,
    string ReCaptcha);
