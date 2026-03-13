using Application.Abstractions.Messaging;
using Domain.Entities.Users;
using MediatR;

namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery(CancellationToken cancellationToken) : IQuery<IEnumerable<AppUser>>;
