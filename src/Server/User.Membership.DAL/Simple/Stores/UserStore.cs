using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using odec.CP.Server.Model.User.Membership.Simple.Contexts;
using odec.CP.Server.Model.User.Membership.Simple.Models;

namespace odec.CP.User.Membership.DAL.Simple.Stores
{
    public class UserStore:UserStore<Server.Model.User.Membership.Simple.Models.User,Role,UserContextExt,int>
    {
        public UserStore(UserContextExt context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}