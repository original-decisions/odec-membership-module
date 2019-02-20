using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using odec.CP.Server.Model.User.Membership.FullyCustomized.Models;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using RoleE = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Role;
using UserRole = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.UserRole;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Contexts.Denormalized
{
    public class UserContext:IdentityDbContext<Models.Denormalized.User, RoleE, int,UserClaim,UserRole,UserLogin,IdentityRoleClaim<int>,UserToken>,
        IUserContext<int, Models.Denormalized.User, RoleE, UserRole, EmailUrlRelation, Sex>
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
            modelBuilder.Entity<Models.User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<RoleE>().ToTable("Roles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserRole>(),
                "UserRoles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserClaim>(),
                "UserClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserLogin>(),
                "UserLogins", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<RoleClaim>(),
                "RoleClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserToken>(),
                "UserTokens", MembershipScheme);

            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
