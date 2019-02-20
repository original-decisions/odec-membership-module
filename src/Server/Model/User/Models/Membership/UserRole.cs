using System.ComponentModel.DataAnnotations.Schema;
#if NETCOREAPP2_1
using Microsoft.AspNetCore.Identity;
#endif
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace odec.Server.Model.User
{
    /// <summary>
    /// Серверный объект - связь пользователя и роли
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
    }
}
