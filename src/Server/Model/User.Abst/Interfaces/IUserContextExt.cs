using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces
{
    /// <summary>
    /// User Context Extended proxy.
    /// </summary>
    /// <typeparam name="TKey">Identity Key Type</typeparam>
    /// <typeparam name="TUser">User Type</typeparam>
    /// <typeparam name="TContact">Contact Type</typeparam>
    /// <typeparam name="TPhone">Phone Type</typeparam>
    /// <typeparam name="TPhoneType">Phone Type(mobile,work, etc) Type</typeparam>
    /// <typeparam name="TContactPhone">Link Entity "ContactPhone" Type</typeparam>
    /// <typeparam name="TFeedBack">Feedback Type</typeparam>
    /// <typeparam name="TEmailUrlRelation">The type that describes link between url of the smtp server and the e-mail service provider.
    /// Used for fast redirect user for workflow proceedure. 
    /// Ex: google - mail.google.com/... </typeparam>
    /// <typeparam name="TSex">Sex type</typeparam>
    /// <typeparam name="TRole">Role type</typeparam>
    /// <typeparam name="THobby">Hobby type</typeparam>
    /// <typeparam name="TUserHobby">Link Entity between user and hobby type</typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    public interface IUserContextExt<TKey, TUser,TRole, TUserRole, TContact, TPhone, TPhoneType, TContactPhone, TFeedBack, TEmailUrlRelation, TSex, THobby, TUserHobby>:
        IUserContext<TKey, TUser, TRole, TUserRole, TEmailUrlRelation, TSex>
        where TUser : class
        where TContact : class
        where TEmailUrlRelation : class
        where TFeedBack : class
        where TContactPhone : class
        where TPhoneType : class
        where TPhone : class
        where TSex : class
        where TUserRole : class
        where TRole :class 
        where THobby : class 
        where TUserHobby : class
    {

        #region User relatve Entities
        DbSet<THobby> Hobbies { get; set; }

        DbSet<TUserHobby> UserHobbies { get; set; }

        DbSet<TPhone> Phones { get; set; }

        DbSet<TPhoneType> PhoneTypes { get; set; }

        DbSet<TContactPhone> ContactPhones { get; set; }

        DbSet<TFeedBack> FeedBacks { get; set; }

        DbSet<TContact> Contacts { get; set; }

        #endregion

    }


}
