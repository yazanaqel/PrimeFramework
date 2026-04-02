using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentication.LoginUser;
public sealed record LoginUserRequest(string Email, string Password);



