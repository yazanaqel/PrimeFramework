namespace InsideMarket.MAUI.Business.Models;


public class Category {
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } =string.Empty;
    public List<ChildrenResponse> ChildrenResponses { get; set; } = new List<ChildrenResponse>();
}
public class ChildrenResponse {
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ParentCategoryId { get; set; }
}