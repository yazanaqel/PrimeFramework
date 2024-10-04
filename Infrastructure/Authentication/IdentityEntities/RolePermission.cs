using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.IdentityEntities;
public class RolePermission : IdentityRoleClaim<int>
{
    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}
