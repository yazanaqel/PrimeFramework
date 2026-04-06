namespace Application.Features.User.GetAllUsers;

public record GetAllUsersResponse(Guid UserId,string Email,DateTime CreatedAt);