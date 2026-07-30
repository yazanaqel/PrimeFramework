using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Application.Features.User.GetAllUsers;

public record GetAllUsersResponse(Guid UserId,string UserName,string Email,string PhoneNumber,bool EmailConfirmed,DateTime CreatedAt);
