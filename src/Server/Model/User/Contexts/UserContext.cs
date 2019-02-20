using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.Server.Model.Contact;
using odec.Server.Model.User.Abst.Interfaces;

namespace odec.Server.Model.User.Contexts
{
    public class UserContext : DbContext,//IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, IdentityRoleClaim<int>, UserToken>, 
        IUserContext<int, User,Role,UserRole, EmailUrlRelation, Sex>
    {
        private string MembershipScheme = "AspNet";
        private string UserScheme = "users";
        //private string ContactScheme = "contact";
        //private string LocationScheme = "location";
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
            // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRoles", MembershipScheme)
                .HasKey(it => new { it.UserId, it.RoleId });
            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles", MembershipScheme);
            modelBuilder.Entity<IdentityRoleClaim<int>>()
                .ToTable("RoleClaims", MembershipScheme);

            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme); ;
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }


    //public class ApplicationClaimsPrincipal : ClaimsPrincipal
    //{
    //    public ApplicationClaimsPrincipal(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal) { }
    //    public Int32 UserId { get { return Int32.Parse(this.FindFirst(ClaimTypes.Sid).Value); } }
    //}
}