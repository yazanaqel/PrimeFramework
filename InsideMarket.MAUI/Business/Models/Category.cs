namespace InsideMarket.MAUI.Business.Models;

public class Category
{
    public string CategoryId { get; set; } =string.Empty;
    public string? ParentCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category? ParentCategory { get; set; }
    private readonly List<Category> _subCategories = new();
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();


}