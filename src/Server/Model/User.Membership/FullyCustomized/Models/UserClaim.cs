using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models
{
    /// <summary>
    /// Серверный объект - связь пользователя и требования (запроса)
    /// </summary>
    public class UserClaim :IdentityUserClaim<int>
    {
        /// <summary>
        /// идентификатор требования (запроса)
        /// </summary>
        [Key]
        public override int Id { get; set; }
    }
}
