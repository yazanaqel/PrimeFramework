using Ardalis.Specification;
using Domain.Entities.User;

namespace Domain.Specifications.User;

public sealed class UsersCursorSpecification : Specification<AppUser>
{
    public UsersCursorSpecification(
        UserCursor? after,
        int size,
        string? search,
        string? sortBy,
        bool descending)
    {
        // 1. Filtering
        if(!string.IsNullOrWhiteSpace(search))
        {
            Query.Where(u =>
                u.Email.Contains(search) ||
                u.UserName.Contains(search));
        }

        // 2. Sorting (user-defined)
        if(!string.IsNullOrWhiteSpace(sortBy))
        {
            if(sortBy.Equals("email",StringComparison.OrdinalIgnoreCase))
            {
                if(descending)
                    Query.OrderByDescending(u => u.Email);
                else
                    Query.OrderBy(u => u.Email);
            }
            else if(sortBy.Equals("username",StringComparison.OrdinalIgnoreCase))
            {
                if(descending)
                    Query.OrderByDescending(u => u.UserName);
                else
                    Query.OrderBy(u => u.UserName);
            }
        }

        // 3. Cursor ordering (always applied last)
        //if(OrderBy is null && OrderByDescending is null)
        //    ApplyOrderBy(u => u.CreatedAt);
        //else
        //    ApplyThenBy(u => u.CreatedAt);

        //ApplyThenBy(u => u.Id);

        // 4. Cursor filter
        if(after is not null)
        {
            Query.Where(u =>
                u.CreatedAt > after.CreatedAt ||
                (u.CreatedAt == after.CreatedAt && u.Id > after.Id));
        }

        // 5. Page size
        Query.Take(size + 1);
    }
}





