using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Generic
{

    public class UserGeneric<TKey, TSexId, TSex, TExtendedDataId, TOrder, TUserContact, TWish, TBasketGood,
        TDiscountCard> :
#if NETCOREAPP2_1
         IdentityUser<TKey>
#endif
#if !NETCOREAPP2_1
         IdentityUser<TKey, IdentityUserLogin<TKey>, IdentityUserRole<TKey>, IdentityUserClaim<TKey>>
#endif

        where TKey : IEquatable<TKey>
        //IUser<Int32>
    {
        public UserGeneric()
        {
            DateRegistration = DateTime.Now;
        }



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

        private DateTime _dateRegistration;

        [Required]
        [DefaultValue(15)]
        public int RemindInDays { get; set; }

        [Required]
        public DateTime DateRegistration
        {
            get { return _dateRegistration; }
            set { _dateRegistration = value; }
        }

        [StringLength(50)]
#warning reason to store that field here
        public string CardNumber { get; set; }

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