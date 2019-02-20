using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using odec.CP.Server.Model.Location;
using odec.Server.Model.Contact;
using odec.Server.Model.Location;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using Cont = odec.Server.Model.Contact.Contact;
using Role = odec.CP.Server.Model.User.Membership.Simple.Models.Role;

namespace odec.CP.Server.Model.User.Membership.Simple.Contexts
{
    public class UserContextExt :
        IdentityDbContext<Models.User,Role,int>,
            IMembershipContext<Models.User, Role, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>,
        IUserContextExt<int,Models.User,Role,IdentityUserRole<int>,  Cont, Phone,PhoneType,ContactPhone,FeedBackComment,EmailUrlRelation,Sex,Hobby,UserHobby>//,
    //    IUserStore<Models.User>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.User>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", MembershipScheme);
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", MembershipScheme);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", MembershipScheme);
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", MembershipScheme);

            modelBuilder.Entity<UserSkill>()
                .ToTable("UserSkills", UserScheme)
                .HasKey(it=> new {it.UserId,it.SkillId});
            modelBuilder.Entity<ContactPhone>()
                .ToTable("ContactPhones", UserScheme)
                .HasKey(it => new { it.ContactId, it.PhoneId});
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
            
        }
    }
}
