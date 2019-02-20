using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using Role = odec.CP.Server.Model.User.Membership.Simple.Models.Role;

namespace odec.CP.Server.Model.User.Membership.Simple.Contexts
{
    public class UserContext : IdentityDbContext<Models.User,Role,int>,
        IMembershipContext<Models.User,Role, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>,IdentityRoleClaim<int>, IdentityUserToken<int>>,
        IUserContext<int, Models.User, Role, IdentityUserRole<int>, EmailUrlRelation, Sex>
    {
        private string MembershipScheme = "AspNet";
        private string UserScheme = "users";
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }
        public DbSet<Sex> Sexes { get; set; }
        //TODO: refactor this property should be moved somewhere except user. Because it couldnt be used for user
        public DbSet<EmailUrlRelation> EmailUrlRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Models.User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", MembershipScheme);
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", MembershipScheme);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", MembershipScheme);

            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);
            //  RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserToken>(), "UserTokens", MembershipScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }

}