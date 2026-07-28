using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Application.Features.User.GetUserById;

public record GetUserByIdResponse(Guid UserId,string Email,string UserName,GetOwnerStoreResponse? GetOwnerStoreResponse);
