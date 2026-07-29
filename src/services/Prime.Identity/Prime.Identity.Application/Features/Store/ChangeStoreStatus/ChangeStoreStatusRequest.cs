using Prime.Identity.Domain.Entities.Enums;

namespace Prime.Identity.Application.Features.Store.ChangeStoreStatus;

public record ChangeStoreStatusRequest(
    string StoreId,
    StoreStatus StoreStatus);