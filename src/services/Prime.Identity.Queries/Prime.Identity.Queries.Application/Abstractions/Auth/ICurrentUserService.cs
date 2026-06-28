using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    string? UserId { get; }
    bool IsAuthenticated { get; }
}

