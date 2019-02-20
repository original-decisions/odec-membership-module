using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.CP.Server.Model.User.Membership.Simple.Models.Denormalized
{
    public class User : IdentityUser<int>
    {

        public User()
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
        /// <summary>
        /// User Overall Rating
        /// </summary>
        [DefaultValue(0)]
        public decimal Rating { get; set; }


    }
}
