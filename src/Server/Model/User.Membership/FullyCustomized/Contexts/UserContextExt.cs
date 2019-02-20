using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using odec.CP.Server.Model.Location;
using odec.CP.Server.Model.User.Membership.FullyCustomized.Models;
using odec.Server.Model.Contact;
using odec.Server.Model.Location;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using Cont = odec.Server.Model.Contact.Contact;
using RoleE = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Role;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Contexts
{
    public class UserContextExt :
        IdentityDbContext<Models.User, RoleE, int,UserClaim, Models.UserRole, UserLogin,IdentityRoleClaim<int>,UserToken>,
        IUserContextExt<int,Models.User, RoleE, Models.UserRole, Cont, Phone,PhoneType,ContactPhone,FeedBackComment,EmailUrlRelation,Sex,Hobby,UserHobby>
    {
        private string MembershipScheme = "AspNet";
        private string UserScheme = "users";

        private string LocationScheme = "location";

        public UserContextExt(DbContextOptions<UserContextExt> options)
            : base(options)
        {

        }

        public DbSet<Sex> Sexes { get; set; }
        public DbSet<EmailUrlRelation> EmailUrlRelations { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<ContactPhone> ContactPhones { get; set; }
        public DbSet<FeedBackComment> FeedBacks { get; set; }
        public DbSet<Cont> Contacts { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo: how to remove cascade delete
            //   modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //   modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Models.User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<RoleE>().ToTable("Roles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<Models.UserRole>(), "UserRoles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserClaim>(), "UserClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserLogin>(), "UserLogins", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<RoleClaim>(), "RoleClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder) modelBuilder.Entity<UserToken>(), "UserTokens", MembershipScheme);

            modelBuilder.Entity<UserSkill>()
                .ToTable("UserSkills", UserScheme)
                .HasKey(it => new { it.UserId, it.SkillId });
            modelBuilder.Entity<ContactPhone>()
                .ToTable("ContactPhones", UserScheme)
                .HasKey(it => new { it.ContactId, it.PhoneId });
            modelBuilder.Entity<UserHobby>()
                .ToTable("UserHobbies", UserScheme)
                .HasKey(it => new { it.UserId, it.HobbyId });
            modelBuilder.Entity<Hobby>()
                .ToTable("Hobbies", UserScheme);
            modelBuilder.Entity<Phone>()
                .ToTable("Phones", UserScheme);
            modelBuilder.Entity<PhoneType>()
                .ToTable("PhoneTypes", UserScheme);
            modelBuilder.Entity<Cont>()
                .ToTable("Contacts", UserScheme);
            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);
            modelBuilder.Entity<UserContact>()
                .ToTable("UserContacts", UserScheme)
                .HasKey(it => new { it.UserId, it.ContactId });
            modelBuilder.Entity<ContactAddress>()
                .ToTable("ContactAddresses", UserScheme)
                .HasKey(it => new { it.ContactId, it.AddressId });
            modelBuilder.Entity<FeedBackComment>()
                .ToTable("FeedBacks", UserScheme);


            modelBuilder.Entity<Address>()
                .ToTable("Addresses", LocationScheme);
            modelBuilder.Entity<Subway>()
                .ToTable("Subways", LocationScheme);
            modelBuilder.Entity<House>()
                .ToTable("Houses", LocationScheme);
            modelBuilder.Entity<Country>()
                .ToTable("Countries", LocationScheme);
            modelBuilder.Entity<City>()
                .ToTable("Cities", LocationScheme);
            modelBuilder.Entity<Street>()
                .ToTable("Streets", LocationScheme);
            modelBuilder.Entity<Flat>()
                .ToTable("Flats", LocationScheme);
            modelBuilder.Entity<Housing>()
                .ToTable("Housings", LocationScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }
    }
}
