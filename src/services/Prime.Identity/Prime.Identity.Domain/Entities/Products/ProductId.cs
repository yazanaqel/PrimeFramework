namespace Prime.Identity.Domain.Entities.Products;

public readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());

    public static bool TryParse(string? input,out ProductId productId)
    {
        if(Guid.TryParse(input,out var guid))
        {
            productId = new ProductId(guid);
            return true;
        }

        productId = default;
        return false;
    }
}