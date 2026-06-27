namespace Prime.Identity.Domain.Entities.Categories;

public readonly record struct CategoryId(Guid Value)
{
    public static CategoryId New() => new(Guid.NewGuid());

    public static bool TryParse(string? input,out CategoryId categoryId)
    {
        if(Guid.TryParse(input,out var guid))
        {
            categoryId = new CategoryId(guid);
            return true;
        }

        categoryId = default;
        return false;
    }
}