using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.User.Generic
{
    public class UserContactGeneric<TUserId, TContactId, TUser, TContact>
    {
        [Key, Column(Order = 0)]
        public TUserId UserId { get; set; }

        public TUser User { get; set; }

        [Key, Column(Order = 1)]
        public TContactId ContactId { get; set; }

        public TContact Contact { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsAccountBased { get; set; }



    }
}
