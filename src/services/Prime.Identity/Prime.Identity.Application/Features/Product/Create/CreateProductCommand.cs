using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Product.Create;

public sealed record CreateProductCommand(CreateProductRequest Request,CancellationToken ct) : ICommand<bool>;