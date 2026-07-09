using InsideMarket.MAUI.Auth;
using InsideMarket.MAUI.Business.Models;

namespace InsideMarket.MAUI.Business.Services;

public interface IStoreService
{
    Task<Store> GetOwnerStore();
    Task<IEnumerable<Store>> GetAllStores(GetAllStoresRequest getAllStoresRequest);
    Task<bool> CreateStore(Store store);

}
