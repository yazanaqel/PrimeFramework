namespace InsideMarket.Web.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginRequest loginRequest);
    Task LogoutAsync();
    Task<UserProfileInfo> GetUserProfileAsync();


    Task<CursorPageResponse<GetAllUsersResponse>> GetAllUsersAsync();
    Task<GetUserByIdResponse> GetUserByIdAsync(string userId);


    Task<IEnumerable<Store>> GetAllStores();
    Task<Store> GetStoreById(Guid storeId);
    Task<bool> ChangeStoreStatus(StoreChangeStatus storeChangeStatus);
}
public class GetAllUsersResponse
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }
}
public record GetUserByIdResponse
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }
    public Store? GetOwnerStoreResponse { get; set; }
}
public sealed class CursorPageResponse<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    public string? NextCursor { get; set; }
    public bool HasMore { get; set; }
}

public class Store
{
    public string StoreId { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageCover { get; set; } = string.Empty;
    public Stream ImageCoverStream { get; set; } = Stream.Null;
    public string ImageCoverFileName { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public Stream ImageStream { get; set; } = Stream.Null;
    public string ImageFileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsShippingAvailable { get; set; }
    public City City { get; set; } = City.Damascus;
    public StoreStatus StoreStatus { get; set; }

}
public class StoreChangeStatus
{
    public required string StoreId { get; set; }
    public required StoreStatus StoreStatus { get; set; }

}

public enum City
{
    Damascus = 1,
    Aleppo = 2,
    Homs = 3,
    Hama = 4,
    Latakia = 5,
    Tartous = 6,
    DeirEzzor = 7,
    Raqqa = 8,
    Daraa = 9,
    Quneitra = 10,
    Qamishli = 11,
    Hassakeh = 12,
    Idlib = 13,
    Suweida = 14,
}

public enum StoreStatus
{
    Active = 1,
    Suspended = 2,
    Draft = 3,
}
