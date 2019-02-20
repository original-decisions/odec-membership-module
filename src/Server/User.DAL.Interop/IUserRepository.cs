using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using odec.Entity.DAL.Interop;

namespace odec.User.DAL.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser">user class</typeparam>
    /// <typeparam name="TKey">user identity type</typeparam>
    /// <typeparam name="TContact">user contact</typeparam>
    /// <typeparam name="TAddress"></typeparam>
    public interface IUserRepository<TUser,TKey, TContact, TAddress> :
        IActivatableEntity<TKey, TUser>,
        IEntityOperations<TKey,TUser> 
        where TUser : class
        where TKey : struct, IEquatable<TKey>
    {
        TUser GetUserByUsername(string username);
        IEnumerable<TAddress> GetUserAddresses(TUser user);
        IEnumerable<TAddress> GetUserAddresses(TKey userKey);
        KeyValuePair<TContact, TUser> SaveUserWithContact(TUser user, TContact contact);
       
        //TODO:Rename to link a contact
        [Obsolete]
        TContact SaveContact(TKey userKey, TContact contact);
        //TODO:Rename to link a contact
        [Obsolete]
        TContact SaveContact(TUser user, TContact contact);
        //TODO:Rename to link a contact
        [Obsolete]
        Task<TContact> SaveContactAsync(TKey userKey,TContact contact);
        //TODO:Rename to link a contact
        [Obsolete]
        Task<TContact> SaveContactAsync(TUser user, TContact contact);
    }
}
