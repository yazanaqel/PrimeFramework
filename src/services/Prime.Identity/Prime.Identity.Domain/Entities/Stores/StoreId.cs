namespace Prime.Identity.Domain.Entities.Stores;

public readonly record struct StoreId(Guid Value)
{
    public static StoreId New() => new(Guid.NewGuid());

    public static bool TryParse(string? input,out StoreId storeId)
    {
        if(Guid.TryParse(input,out var guid))
        {
            storeId = new StoreId(guid);
            return true;
        }

        storeId = default;
        return false;
    }
}