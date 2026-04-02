using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentication.RegisterUser;
public sealed record RegisterUserRequest(string Email,string Password,string ConfirmPassword);



