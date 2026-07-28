using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Application.Features.User.GetAllUsers;

public record GetAllUsersResponse(Guid UserId,string Email,DateTime CreatedAt);