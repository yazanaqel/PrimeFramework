using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Store.Create;

public sealed record CreateStoreCommand(CreateStoreRequest Request,CancellationToken ct) : ICommand<bool>;