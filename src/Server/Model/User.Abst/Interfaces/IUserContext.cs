using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces
{
    public interface IUserContext<TKey, TUser, TRole,TUserRole, TEmailUrlRelation, TSex>
        where TEmailUrlRelation : class
        where TSex : class
        where TRole : class 
        where TUser : class
        where TUserRole : class
    {
        DbSet<TUser> Users { get; set; }
        DbSet<TRole> Roles { get; set; }
        DbSet<TUserRole> UserRoles { get; set; }
        DbSet<TSex> Sexes { get; set; }
        DbSet<TEmailUrlRelation> EmailUrlRelations { get; set; }
    }
}
