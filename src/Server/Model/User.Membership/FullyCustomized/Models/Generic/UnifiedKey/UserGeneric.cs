using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Generic.UnifiedKey
{
    //TODO:Analize this class
    /// <summary>
    /// Generic class for DB representation of user
    /// </summary>
    /// <typeparam name="TKey">indentity type</typeparam>
    /// <typeparam name="TUserLogin">Type of link between User and External Logins</typeparam>
    /// <typeparam name="TUserRole">Type of link between User and Roles</typeparam>
    /// <typeparam name="TUserClaim">Type of link between User and claims of External Logins</typeparam>
    public class UserGeneric<TKey, TUserLogin, TUserRole, TUserClaim> :
#if NETCOREAPP2_1
        IdentityUser<TKey>
#endif
#if !NETCOREAPP2_1
        IdentityUser<TKey,TUserClaim,TUserRole,TUserLogin>
#endif

        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
        //IUser<Int32>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserGeneric()
        {
            DateRegistration = DateTime.Now;
        }
        /// <summary>
        /// path to profile picture
        /// </summary>
        public string ProfilePicturePath { get; set; }
        /// <summary>
        /// Person first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Person last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Person patronymic
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Date updated
        /// </summary>
        public DateTime? DateUpdated { get; set; }


        /// <summary>
        /// Last activity date
        /// </summary>
        public DateTime? LastActivityDate { get; set; }
        /// <summary>
        /// Last login date
        /// </summary>
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// private field for Date registration
        /// </summary>
        private DateTime _dateRegistration;
        /// <summary>
        /// if we should remind after some days
        /// </summary>
        [Required]
        [DefaultValue(15)]
        public int RemindInDays { get; set; }
        /// <summary>
        /// Date registration
        /// </summary>
        [Required]
        public DateTime DateRegistration
        {
            get { return _dateRegistration; }
            set { _dateRegistration = value; }
        }
        /// <summary>
        /// Short information about a user
        /// </summary>
        public string Description { get; set; }
    }

}