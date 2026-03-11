using Application.Abstractions.Messaging;

namespace Application.Features.User.GetUserById;


public sealed record GetUserByIdQuery(Guid UserId,CancellationToken cancellationToken) : IQuery<GetUserByIdResponse>;