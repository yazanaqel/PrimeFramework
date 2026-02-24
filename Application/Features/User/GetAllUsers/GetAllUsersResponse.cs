using Domain.Entities.Users;

namespace Application.Features.User.GetAllUsers;

public record GetAllUsersResponse(Guid UserId, string Email, string UserName);