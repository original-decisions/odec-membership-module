using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace User.DAL.Tests.Abstractions
{
    //public abstract class UserManagerTestBase<TKey, TUser, TRole> : UserManagerTestBase< TUser, TRole, int>
    //   where TUser : class
    //   where TRole : class
    //{ }

    public abstract class UserManagerTestBase<TKey, TUser, TRole>
        where TUser : class
        where TRole : class
        where TKey : IEquatable<TKey>
    {

        protected virtual void SetupIdentityServices(IServiceCollection services, object context = null)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //var cts = services.BuildServiceProvider().GetService<IHttpContextAccessor>();
            //cts.HttpContext = new DefaultHttpContext();
            services.AddIdentity<TUser, TRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = null;
            }).AddDefaultTokenProviders();
            AddUserStore(services, context);
            AddRoleStore(services, context);
            services.AddLogging();
            services.AddSingleton<ILogger<UserManager<TUser>>>(new TestLogger<UserManager<TUser>>());
            services.AddSingleton<ILogger<RoleManager<TRole>>>(new TestLogger<RoleManager<TRole>>());
        }

        protected virtual UserManager<TUser> CreateManager(object context = null, IServiceCollection services = null, Action<IServiceCollection> configureServices = null)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            if (context == null)
            {
                context = CreateTestContext();
            }
            SetupIdentityServices(services, context);

            configureServices?.Invoke(services);

            return services.BuildServiceProvider().GetService<UserManager<TUser>>();
        }

        protected RoleManager<TRole> CreateRoleManager(object context = null, IServiceCollection services = null)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            if (context == null)
            {
                context = CreateTestContext();
            }
            SetupIdentityServices(services, context);
            return services.BuildServiceProvider().GetService<RoleManager<TRole>>();
        }

        protected SignInManager<TUser> CreateSignInManager(object context = null, IServiceCollection services = null)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            if (context == null)
            {
                context = CreateTestContext();
            }
            SetupIdentityServices(services, context);
            return services.BuildServiceProvider().GetService<SignInManager<TUser>>();
        }

        protected abstract object CreateTestContext();

        protected abstract void AddUserStore(IServiceCollection services, object context = null);
        protected abstract void AddRoleStore(IServiceCollection services, object context = null);

        protected abstract void SetUserPasswordHash(TUser user, string hashedPassword);

        protected abstract TUser CreateTestUser(string namePrefix = "", string email = "", string phoneNumber = "",
            bool lockoutEnabled = false, DateTimeOffset? lockoutEnd = null, bool useNamePrefixAsUserName = false);

        protected abstract TRole CreateTestRole(string roleNamePrefix = "", bool useRoleNamePrefixAsRoleName = false);

        protected abstract Expression<Func<TUser, bool>> UserNameAreEqualsPredicate(string userName);
        protected abstract Expression<Func<TUser, bool>> UserNameStartsWithPredicate(string userName);

        protected abstract Expression<Func<TRole, bool>> RoleNameAreEqualsPredicate(string roleName);
        protected abstract Expression<Func<TRole, bool>> RoleNameStartsWithPredicate(string roleName);

        private class AlwaysBadValidator : IUserValidator<TUser>, IRoleValidator<TRole>,
            IPasswordValidator<TUser>
        {
            public static readonly IdentityError ErrorMessage = new IdentityError { Description = "I'm Bad.", Code = "BadValidator" };

            public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
            {
                return Task.FromResult(IdentityResult.Failed(ErrorMessage));
            }

            public Task<IdentityResult> ValidateAsync(RoleManager<TRole> manager, TRole role)
            {
                return Task.FromResult(IdentityResult.Failed(ErrorMessage));
            }

            public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
            {
                return Task.FromResult(IdentityResult.Failed(ErrorMessage));
            }
        }


        public List<TUser> GenerateUsers(string userNamePrefix, int count)
        {
            var users = new List<TUser>(count);
            for (var i = 0; i < count; i++)
            {
                users.Add(CreateTestUser(userNamePrefix + i));
            }
            return users;
        }

        public List<TRole> GenerateRoles(string namePrefix, int count)
        {
            var roles = new List<TRole>(count);
            for (var i = 0; i < count; i++)
            {
                roles.Add(CreateTestRole(namePrefix + i));
            }
            return roles;
        }
    }
}
