using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Category.Create;

public sealed record CreateCategoryCommand(CreateCategoryRequest Request,CancellationToken ct) : ICommand<bool>;