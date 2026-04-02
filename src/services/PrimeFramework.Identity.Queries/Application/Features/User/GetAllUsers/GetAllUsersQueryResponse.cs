namespace Application.Features.User.GetAllUsers;

public record GetAllUsersQueryResponse(Guid UserId,string Email,DateTime CreatedAt);