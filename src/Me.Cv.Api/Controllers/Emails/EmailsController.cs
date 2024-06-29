using Mapster;
using Me.Cv.Api.Controllers.Emails.SendEmail;
using Me.Cv.Application.Emails.Commands.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Me.Cv.Api.Controllers.Emails;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EmailsController(
    ISender sender,
    ILogger<EmailsController> logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Send")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Send(SendEmailRequest request)
    {
        var command = request.Adapt<SendEmailCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<SendEmailResponse>();
        return Ok(response);
    }
}
