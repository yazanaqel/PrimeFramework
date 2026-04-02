namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQueryRequest(
    string? After,
    int Size,
    string? Search,
    string? SortBy,
    bool Descending,
    CancellationToken cancellationToken);
