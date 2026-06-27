using Domain.Primitives;

namespace Prime.Identity.Domain.Entities.Categories;

public sealed class Category : Entity<CategoryId>, IAuditableEntity
{
    private Category() { }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }

    //Navigation properties
    public Category? ParentCategory { get; private set; }
    public CategoryId? ParentCategoryId { get; private set; }

    private readonly List<Category> _subCategories = new();
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();



    public static Category Create(string name,string description,CategoryId? parentCategoryId = null)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.",nameof(name));

        return new Category
        {
            Id = CategoryId.New(),
            Name = name.Trim(),
            Description = description,
            ParentCategoryId = parentCategoryId
        };
    }

    public void Rename(string newName)
    {
        if(string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Category name is required.",nameof(newName));

        Name = newName.Trim();
        Touch();
    }

    public void ChangeDescription(string description)
    {
        Description = description;
        Touch();
    }

    public void SetParent(CategoryId? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
        Touch();
    }

    public void AddSubCategory(Category subCategory)
    {
        if(subCategory is null)
            throw new ArgumentNullException(nameof(subCategory));

        if(subCategory.Id == Id)
            throw new InvalidOperationException("Category cannot be its own parent.");

        if(!_subCategories.Contains(subCategory))
        {
            _subCategories.Add(subCategory);
        }
    }

    private void Touch()
    {
        ModifiedAt = DateTime.UtcNow;
    }
}