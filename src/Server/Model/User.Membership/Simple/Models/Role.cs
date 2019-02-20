using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.CP.Server.Model.User.Membership.Simple.Models
{
    /// <summary>
    /// серверный объект - роль
    /// </summary>
    //[Table("AspNetRoles",Schema = "users")]
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
        }
        public Role(string roleName) : base(roleName) { }
        public int? InRoleId { get; set; }
        public Role InRole { get; set; }
        public string Scope { get; set; }
    }
}
