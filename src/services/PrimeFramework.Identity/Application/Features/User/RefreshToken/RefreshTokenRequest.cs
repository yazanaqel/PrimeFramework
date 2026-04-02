using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.RefreshToken;

public sealed record RefreshTokenRequest(string AccessToken,string RefreshToken);