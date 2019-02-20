using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.Server.Model.User.Generic.UnifiedKey
{
    /// <summary>
    /// Generic class for DB representation of user
    /// </summary>
    /// <typeparam name="TKey">indentity type</typeparam>
    public class UserGeneric<TKey> 
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserGeneric()
        {
            DateRegistration = DateTime.Now;
        }

        public TKey Id { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        public virtual string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        public virtual string UserName { get; set; }
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
        /// if we should remind after some days
        /// </summary>
        [Required]
        [DefaultValue(15)]
        public int RemindInDays { get; set; }
        /// <summary>
        /// Date registration
        /// </summary>
        [Required]
        public DateTime DateRegistration { get; set; }

        /// <summary>
        /// Short information about a user
        /// </summary>
        public string Description { get; set; }
    }

}