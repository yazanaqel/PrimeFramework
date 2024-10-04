using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Repositories.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MembersController : ApiController
{
    private readonly IUserRepository _memberRepository;

    public MembersController(ISender sender, IUserRepository memberRepository) : base(sender)
    {
        _memberRepository = memberRepository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterMember(string email, string password, CancellationToken cancellationToken)
    {
        var member = new User
        {
            Email = email,
            UserName = email,
        };

        var result = await _memberRepository.RegisterAsync(member);

        return Ok(result);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> LoginMember(string email, string password, CancellationToken cancellationToken)
    {
        var result = await _memberRepository.LoginAsync(email, password);

        return Ok(result);
    }
}
