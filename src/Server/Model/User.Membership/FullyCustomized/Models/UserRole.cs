using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Models
{
    /// <summary>
    /// Серверный объект - связь пользователя и роли
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
    }
}
