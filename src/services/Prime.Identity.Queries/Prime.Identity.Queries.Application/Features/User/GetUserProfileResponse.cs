using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Queries.Application.Features.User;

public record GetUserProfileResponse(string UserId,string Email,string UserName);