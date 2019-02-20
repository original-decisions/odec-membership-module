using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using odec.CP.Server.Model.User.Membership.Simple.Contexts;
using odec.CP.Server.Model.User.Membership.Simple.Models;

namespace odec.CP.User.Membership.DAL.Simple.Stores
{
    public class RoleStore:RoleStore<Role,UserContextExt,int>
    {
        public RoleStore(UserContextExt context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
