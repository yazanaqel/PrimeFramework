using Application.Features.User.RefreshToken;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Application.Features.User.LoginUser;

public record LoginUserResponse(UserId UserId,string UserName,TokenResponse TokenResponse);
