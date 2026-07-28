namespace Application.Features.User.GetAllUsers;

public sealed record GetAllUsersRequest(
    string? After,
    string? Search,
    string? SortBy,
    bool Descending,
    int Size = 50);
