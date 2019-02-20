using System;
using System.ComponentModel.DataAnnotations;
using odec.Server.Model.Contact;
using odec.Server.Model.User.Generic.UnifiedKey;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Denormalized
{
    public class User : Generic.UnifiedKey.UserGeneric<int, UserLogin, UserRole, UserClaim>,IEquatable<int>
    {

        public int? SexId { get; set; }
        public Sex Sex { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string AddressLine1 { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string AddressLine2 { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string MainPhone { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string AdditionalPhone { get; set; }

        public bool Equals(int other)
        {
            return Id == other;
        }
    }
}
