using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Application.Features.User.GetUserById;

public record GetUserByIdResponse(Guid UserId,string UserName,string Email,string PhoneNumber,bool EmailConfirmed,DateTime CreatedAt,GetOwnerStoreResponse? GetOwnerStoreResponse);
