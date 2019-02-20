using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.Server.Model.User.Generic
{
    public class UserGeneric<TKey, TSexId, TSex, TExtendedDataId, TOrder, TUserContact, TWish, TBasketGood,
        TDiscountCard>
    {
        public UserGeneric()
        {
            DateRegistration = DateTime.Now;
        }
        public TKey Id { get; set; }
        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public virtual string UserName { get; set; }


        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime? DateUpdateInfo { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(255)]
        public string OldPassword { get; set; }


        public DateTime? LastActivityDate { get; set; }

        public DateTime? LastLogin { get; set; }


        public TExtendedDataId ExtendedUserDataId { get; set; }
        //public ExtendedUserData ExtendedUserData { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Email { get; set; }

        [Required]
        [DefaultValue(15)]
        public int RemindInDays { get; set; }

        [Required]
        public DateTime DateRegistration { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [Required]
        public TSexId SexId { get; set; }


        public TSex Sex { get; set; }

        public virtual ICollection<TOrder> Orders { get; set; }

        public virtual ICollection<TUserContact> UserContacts { get; set; }


        // public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }

        public virtual ICollection<TWish> Wishes { get; set; }
        public virtual ICollection<TBasketGood> BasketGoods { get; set; }

        public virtual ICollection<TDiscountCard> DiscountCards { get; set; }
    }

}