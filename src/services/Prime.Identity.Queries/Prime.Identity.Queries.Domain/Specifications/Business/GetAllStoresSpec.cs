using Ardalis.Specification;
using Prime.Identity.Queries.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Domain.Specifications.Business;

public sealed class GetAllStoresSpec: Specification<Store>
{
    public GetAllStoresSpec(string? search)
    {
        if(!string.IsNullOrWhiteSpace(search))
        {
            Query.Where(u =>
                u.Name.Contains(search) ||
                u.Description.Contains(search));
        }


    }
}
