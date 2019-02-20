using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.Server.Model.User
{
    /// <summary>
    /// серверный объект - роль
    /// </summary>
    //[Table("AspNetRoles",Schema = "users")]
    public class Role
    {
        //
        // Summary:
        //     A random value that should change whenever a role is persisted to the store
        public virtual string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key for this role.
        public virtual int Id { get; set; }
        //
        // Summary:
        //     Gets or sets the name for this role.
        public virtual string Name { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized name for this role.
        public virtual string NormalizedName { get; set; }
        public int? InRoleId { get; set; }
        public Role InRole { get; set; }
        public string Scope { get; set; }
    }
}
