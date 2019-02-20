using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models
{
    /// <summary>
    /// серверный объект - роль
    /// </summary>
    //[Table("AspNetRoles",Schema = "users")]
#if NETCOREAPP2_1
    public class Role : IdentityRole<int>
    {
        public int? InRoleId { get; set; }
        public Role InRole { get; set; }
        public string Scope { get; set; }
    }
#endif
#if !NETCOREAPP2_1
    public class Role : IdentityRole<int, UserRole, IdentityRoleClaim<int>>
    {
        public int? InRoleId { get; set; }
        public Role InRole { get; set; }
        public string Scope { get; set; }
    }
#endif
}
