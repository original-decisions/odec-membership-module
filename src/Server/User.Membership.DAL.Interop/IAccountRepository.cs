using System;

namespace odec.CP.User.Membership.DAL.Interop
{
    public interface IAccountRepository<TKey,TUser,TRole>:
        IAuthenticate<TUser,TKey> 
        where TKey : IEquatable<TKey> 
        where TUser : class
    {
        void AddUserToRole(TRole role, TUser user);
        void AddUserToRole(TKey roleId, TKey userId);
        void AddUserToRole(string roleName, string userName);
        void RemoveUserFromRole(TKey roleId, TKey userId);
        void RemoveUserFromRole(TRole roleId, TUser user);
        void RemoveUserFromRole(string roleName, string userName);
    }
}
