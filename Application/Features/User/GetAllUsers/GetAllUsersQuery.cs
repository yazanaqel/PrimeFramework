using Application.Abstractions.Messaging;

namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery(CancellationToken cancellationToken) : IQuery<IEnumerable<GetAllUsersResponse>>;
