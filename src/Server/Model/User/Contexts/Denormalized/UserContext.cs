using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Contact;
using odec.Server.Model.User.Abst.Interfaces;
#if NETCOREAPP2_1
using Microsoft.AspNetCore.Identity;
#endif
namespace odec.Server.Model.User.Contexts.Denormalized
{
    public class UserContext:DbContext,
        IUserContext<int, User,Role,UserRole, EmailUrlRelation, Sex>
    {
        private string MembershipScheme = "AspNet";
        private string UserScheme = "users";
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<EmailUrlRelation> EmailUrlRelations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: updata schema mapping
            modelBuilder.Entity<User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRoles", MembershipScheme)
                .HasKey(it => new { it.UserId, it.RoleId });
            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles", MembershipScheme);
            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
