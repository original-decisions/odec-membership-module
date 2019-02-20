using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using odec.Server.Model.User.Generic.UnifiedKey;

namespace odec.Server.Model.User
{
    /// <summary>
    /// Server object - user
    /// </summary>
    public class User : UserGeneric<int>
    {        
        [DefaultValue(0)]
        public decimal Rating { get; set; }

        public bool LockoutEnabled { get; set; }
        public string PhoneNumber { get; set; }
    }

}