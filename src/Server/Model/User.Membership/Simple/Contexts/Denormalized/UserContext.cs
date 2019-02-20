using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using Role = odec.CP.Server.Model.User.Membership.Simple.Models.Role;

namespace odec.CP.Server.Model.User.Membership.Simple.Contexts.Denormalized
{
    public class UserContext : IdentityDbContext<Models.Denormalized.User, Role, int>,
        IMembershipContext<Models.Denormalized.User, Role, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>,
        IUserContext<int, Models.Denormalized.User, Role, IdentityUserRole<int>, EmailUrlRelation, Sex>
    {
        private string MembershipScheme = "AspNet";

        private string UserScheme = "users";
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<EmailUrlRelation> EmailUrlRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Models.Denormalized.User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", MembershipScheme);
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", MembershipScheme);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", MembershipScheme);
            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
