using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces
{
    public interface IMembershipContext<TUser, TRole, TUserLogin, TUserRole, TUserClaim,TRoleClaim,TUserToken> 
        where TUser : class 
        where TRole : class 
        where TUserRole : class
        where TUserLogin : class 
        where TUserClaim : class 
        where TRoleClaim : class
        where TUserToken : class
    {
        DbSet<TUser> Users { get; set; }
        DbSet<TRole> Roles { get; set; }
        DbSet<TUserRole> UserRoles { get; set; }
        DbSet<TUserLogin> UserLogins { get; set; }
        DbSet<TUserClaim> UserClaims { get; set; }
        DbSet<TRoleClaim> RoleClaims { get; set; }
        DbSet<TUserToken> UserTokens { get; set; }
    }
}