using System;
using System.ComponentModel.DataAnnotations;
using odec.Server.Model.Contact;
using odec.Server.Model.User.Generic.UnifiedKey;

namespace odec.Server.Model.User.Denormalized
{
    public class User : UserGeneric<int>,IEquatable<int>
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

        public bool LockoutEnabled { get; set; }

        public bool Equals(int other)
        {
            return Id == other;
        }
    }
}
