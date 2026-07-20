using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;

namespace Prime.Identity.Queries.Application.Features.Product;

public record GetCategorizedProductsResponse(string CategoryId,string CategoryName,List<GetStoreProductsResponse> Products);
