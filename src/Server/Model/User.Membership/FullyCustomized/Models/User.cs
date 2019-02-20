using System.ComponentModel;
using odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Generic.UnifiedKey;
using odec.Server.Model.User.Generic.UnifiedKey;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models
{
    /// <summary>
    /// Server object - user
    /// </summary>
    public class User : UserGeneric<int, UserLogin, UserRole, UserClaim>
    {        
        [DefaultValue(0)]
        public decimal Rating { get; set; }
    }

}