using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace odec.CP.User.Membership.DAL.Interop
{
    public interface IMembershipContact<TUser,TContact>
    {
        //TODO: Might be better place Somewhere.
        IdentityResult RegisterUserWithContactAndLogin(TUser user, TContact contact, string password);
        Task<IdentityResult> RegisterUserWithContactAndLoginAsync(TUser user, TContact contact, string password);
    }
}