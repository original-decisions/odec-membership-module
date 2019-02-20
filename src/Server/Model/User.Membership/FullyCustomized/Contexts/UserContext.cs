using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using odec.CP.Server.Model.User.Membership.FullyCustomized.Models;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using odec.Server.Model.User.Abst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Role = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.Role;
using UserClaim = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.UserClaim;
using UserE = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.User;
using UserLogin = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.UserLogin;
using UserRole = odec.CP.Server.Model.User.Membership.FullyCustomized.Models.UserRole;

namespace odec.CP.Server.Model.User.Membership.FullyCustomized.Contexts
{
    public class UserContext : IdentityDbContext<UserE, Role, int, UserClaim, UserRole, UserLogin, IdentityRoleClaim<int>, UserToken>,
        IUserContext<int, UserE, Role, UserRole, EmailUrlRelation, Sex>,
        IMembershipContext<UserE, Role, UserLogin, UserRole, UserClaim, RoleClaim, UserToken>

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
            // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserE>().ToTable("Users", MembershipScheme);
            modelBuilder.Entity<Role>().ToTable("Roles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)modelBuilder.Entity<UserRole>(),
                "UserRoles", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)modelBuilder.Entity<UserClaim>(),
                "UserClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)modelBuilder.Entity<UserLogin>(),
                "UserLogins", MembershipScheme);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", MembershipScheme);
            RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)modelBuilder.Entity<UserToken>(),
                "UserTokens", MembershipScheme);
            modelBuilder.Entity<Sex>()
                .ToTable("Sexes", UserScheme);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<RoleClaim> RoleClaims { get; set; }
    }


    //public class ApplicationClaimsPrincipal : ClaimsPrincipal
    //{
    //    public ApplicationClaimsPrincipal(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal) { }
    //    public Int32 UserId { get { return Int32.Parse(this.FindFirst(ClaimTypes.Sid).Value); } }
    //}

    #if NETCOREAPP2_1
    #region Stores
        
       


    public class MyUserStore<TContext> :
        UserStore<Models.User, Role, DbContext, int, UserClaim, UserRole, UserLogin, UserToken,RoleClaim>,
        IUserStore<Models.User>
        where TContext : DbContext
    {
        public MyUserStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
        //public iUserStore(UserContext context) : base(context) { }
        protected override UserRole CreateUserRole(Models.User user, Role role)
        {
            if (!Context.Set<UserRole>().Any(it => it.UserId == user.Id && role.Id == it.RoleId))
                return Context.Set<UserRole>().Single(it => it.UserId == user.Id && role.Id == it.RoleId);
            var usrRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            Context.Set<UserRole>().Add(usrRole);
            Context.SaveChanges();
            return usrRole;


        }

        protected override UserClaim CreateUserClaim(Models.User user, Claim claim)
        {

            var usrClaim = new UserClaim
            {
                ClaimValue = claim.Value,
                ClaimType = claim.Type,
                UserId = user.Id
            };
            Context.Set<UserClaim>().Add(usrClaim);
            Context.SaveChanges();
            return usrClaim;
        }

        protected override UserLogin CreateUserLogin(Models.User user, UserLoginInfo login)
        {
            if (!Context.Set<UserLogin>().Any(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider))
                return Context.Set<UserLogin>().Single(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider);
            var usrLogin = new UserLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            };
            Context.Set<UserLogin>().Add(usrLogin);
            Context.SaveChanges();
            return usrLogin;
        }

        protected override UserToken CreateUserToken(Models.User user, string loginProvider, string name, string value)
        {
            //if (!Context.Set<UserToken>().Any(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider))
            //    return Context.Set<UserToken>().Single(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider);
            var usrToken = new UserToken
            {
                LoginProvider = loginProvider,
                Value = value,
                Name = name,
                UserId = user.Id
            };
            Context.Set<UserToken>().Add(usrToken);
            Context.SaveChanges();
            return usrToken;
        }
    }
    public class MyRoleStore :
        RoleStore<Role, DbContext, Int32, UserRole, IdentityRoleClaim<int>>,
        IRoleStore<Role>
    {
        public MyRoleStore(UserContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
        protected override IdentityRoleClaim<int> CreateRoleClaim(Role role, Claim claim)
        {
            var roleClaim = new RoleClaim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                RoleId = role.Id
            };
            Context.Set<RoleClaim>().Add(roleClaim);
            Context.SaveChanges();
            return roleClaim;
        }
    }

    #endregion


    public class UserManager : UserManager<Models.User>
    {

        public UserManager(
            MyUserStore<DbContext> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Models.User> passwordHasher,
            IEnumerable<IUserValidator<Models.User>> userValidators,
            IEnumerable<IPasswordValidator<Models.User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Models.User>> logger) : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }
    }
    public class RoleManager : RoleManager<Role>
    {
         public RoleManager(
            MyRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger) { }
    }

    public class SignInManager : SignInManager<Models.User>
    {
        //  IAuthenticationSchemeProvider schemes
        public SignInManager(
            UserManager<Models.User> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<Models.User> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<Models.User>> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger,schemes)
        {
        }
    }
    #endif
#if !NETCOREAPP2_1
    #region Stores
        
       


    public class MyUserStore<TContext> :
        UserStore<Models.User, Role, DbContext, int, UserClaim, UserRole, UserLogin, UserToken>,
        IUserStore<Models.User>
        where TContext : DbContext
    {
        public MyUserStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
        //public iUserStore(UserContext context) : base(context) { }
        protected override UserRole CreateUserRole(Models.User user, Role role)
        {
            if (!Context.Set<UserRole>().Any(it => it.UserId == user.Id && role.Id == it.RoleId))
                return Context.Set<UserRole>().Single(it => it.UserId == user.Id && role.Id == it.RoleId);
            var usrRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            Context.Set<UserRole>().Add(usrRole);
            Context.SaveChanges();
            return usrRole;


        }

        protected override UserClaim CreateUserClaim(Models.User user, Claim claim)
        {

            var usrClaim = new UserClaim
            {
                ClaimValue = claim.Value,
                ClaimType = claim.Type,
                UserId = user.Id
            };
            Context.Set<UserClaim>().Add(usrClaim);
            Context.SaveChanges();
            return usrClaim;
        }

        protected override UserLogin CreateUserLogin(Models.User user, UserLoginInfo login)
        {
            if (!Context.Set<UserLogin>().Any(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider))
                return Context.Set<UserLogin>().Single(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider);
            var usrLogin = new UserLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            };
            Context.Set<UserLogin>().Add(usrLogin);
            Context.SaveChanges();
            return usrLogin;
        }

        protected override UserToken CreateUserToken(Models.User user, string loginProvider, string name, string value)
        {
            //if (!Context.Set<UserToken>().Any(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider))
            //    return Context.Set<UserToken>().Single(it => it.UserId == user.Id && login.ProviderKey == it.ProviderKey && login.LoginProvider == it.LoginProvider);
            var usrToken = new UserToken
            {
                LoginProvider = loginProvider,
                Value = value,
                Name = name,
                UserId = user.Id
            };
            Context.Set<UserToken>().Add(usrToken);
            Context.SaveChanges();
            return usrToken;
        }
    }
    public class MyRoleStore :
        RoleStore<Role, DbContext, Int32, UserRole, IdentityRoleClaim<int>>,
        IRoleStore<Role>
    {
        public MyRoleStore(UserContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
        protected override IdentityRoleClaim<int> CreateRoleClaim(Role role, Claim claim)
        {
            var roleClaim = new RoleClaim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                RoleId = role.Id
            };
            Context.Set<RoleClaim>().Add(roleClaim);
            Context.SaveChanges();
            return roleClaim;
        }
    }

    #endregion


    public class UserManager : UserManager<Models.User>
    {

        public UserManager(
            MyUserStore<DbContext> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Models.User> passwordHasher,
            IEnumerable<IUserValidator<Models.User>> userValidators,
            IEnumerable<IPasswordValidator<Models.User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Models.User>> logger) : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }
    }
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(
            MyRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger,
            IHttpContextAccessor contextAccessor) : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor) { }
    }

    public class SignInManager : SignInManager<Models.User>
    {
        public SignInManager(UserManager<Models.User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Models.User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<Models.User>> logger) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }
    }
    #endif
}