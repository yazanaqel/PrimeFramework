using Application.Abstractions.Messaging;

namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery() : IQuery<IEnumerable<GetAllUsersResponse>>;
