using Application.Features.Members.CreateMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MembersController : ApiController
{
    public MembersController(ISender sender) : base(sender) { }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterMember(CreateMemberRequestDto request,CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(request);

        var result = await Sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);

        //return CreatedAtAction(
        //    nameof(GetMemberById),
        //    new { id = result.Value },
        //    result.Value);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> LoginMember(LoginMemberRequestDto request,CancellationToken cancellationToken)
    {
        var command = new LoginMemberCommand(request);

        var tokenResult = await Sender.Send(command,cancellationToken);

        if(tokenResult.IsFailure)
        {
            return HandleFailure(tokenResult);
        }

        return Ok(tokenResult.Value);
    }
}
