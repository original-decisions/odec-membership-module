using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using odec.CP.Server.Model.User.Membership.Simple.Contexts;
using odec.CP.Server.Model.User.Membership.Simple.Models;
using odec.Framework.Logging;
using odec.CP.User.Membership.DAL;
using odec.CP.User.Membership.DAL.Simple;
using User.DAL.Tests.Abstractions;
using UserE = odec.CP.Server.Model.User.Membership.Simple.Models.User;

namespace User.DAL.Tests.Membership.Simple
{
    public class UserExtMembershipTester : UserManagerTestBase<int, UserE, Role>
    {
        //private static RoleManager<Role> CreateRoleManager(IRoleStore<Role> roleStore)
        //{
        //    return MockHelpers.TestRoleManager(roleStore);
        //}
        private const string testPassword = "`12qweASD";
        public UserManager<UserE> UserManager { get; set; }

        //protected UserManager<UserE> GenerateUserManager(UserContextExt db)
        //{
        //    return new UserManager<UserE>(
        //       store: new UserStore<UserE, Role, UserContextExt, int>(db),
        //       optionsAccessor: null,
        //       passwordHasher: new PasswordHasher<UserE>(),
        //       userValidators: new List<UserValidator<UserE>>() { },
        //       passwordValidators: null,
        //       keyNormalizer: new UpperInvariantLookupNormalizer(),
        //       errors: new IdentityErrorDescriber(),
        //       services: null,
        //       logger: null
        //       );
        //}
        //protected UserManager<UserE> GenerateRoleManager(UserContextExt db)
        //{
        //    return new UserManager<UserE>(
        //       store: new UserStore<UserE, Role, UserContextExt, int>(db),
        //       optionsAccessor: null,
        //       passwordHasher: new PasswordHasher<UserE>(),
        //       userValidators: new List<UserValidator<UserE>>() { },
        //       passwordValidators: null,
        //       keyNormalizer: new UpperInvariantLookupNormalizer(),
        //       errors: new IdentityErrorDescriber(),
        //       services: null,
        //       logger: null
        //       );
        //}
        //protected SignInManager<UserE> GenerateSignManager(UserManager<UserE> userManager)
        //{
        //    return new SignInManager<UserE>(
        //        userManager,
        //        contextAccessor: new HttpContextAccessor(), 
        //        claimsFactory: null, 
        //        optionsAccessor:null,
        //        logger:null
        //        );
        //}
        public UserExtMembershipTester()
        {

        }
        [Test]
        public void SetUserManager()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                //UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));


                //Удаляем созданный объект
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void SetRoleManager()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                //UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => repository.SetRoleManager(manager));


                //Удаляем созданный объект
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void SetMembershipOptions()
        {
            try
            {
                
                var repository = new UserMembershipRepository();
                //UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => repository.Options = new MembershipOptions
                {
                    LockoutOnFailure = true,
                    AuthSessionLifetime = 30,
                    PersistCookie = true
                });
                Assert.True(repository.Options.AuthSessionLifetime ==30);
                Assert.True(repository.Options.LockoutOnFailure);
                Assert.True(repository.Options.PersistCookie);
                //Удаляем созданный объект
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void SetSetSignInManager()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                //UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => repository.SetSignInManager(manager));
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        protected override object CreateTestContext()
        {
            var options = Tester<UserContextExt>.CreateNewContextOptions();
            return new UserContextExt(options);
        }

        protected override void AddUserStore(IServiceCollection services, object context = null)
        {
            services.AddSingleton<IUserStore<UserE>>(new UserStore<UserE, Role, UserContextExt, int>((UserContextExt)context));
        }

        protected override void AddRoleStore(IServiceCollection services, object context = null)
        {
            services.AddSingleton<IRoleStore<Role>>(new RoleStore<Role, UserContextExt, int>((UserContextExt)context));
        }

        protected override void SetUserPasswordHash(UserE user, string hashedPassword)
        {
            user.PasswordHash = hashedPassword;
        }

        protected override UserE CreateTestUser(string namePrefix = "", string email = "", string phoneNumber = "", bool lockoutEnabled = false,
            DateTimeOffset? lockoutEnd = null, bool useNamePrefixAsUserName = false)
        {
            return new UserE
            {
                UserName = useNamePrefixAsUserName ? namePrefix : string.Format("{0}{1}", namePrefix, Guid.NewGuid()),
                Email = email,
                PhoneNumber = phoneNumber,
                LockoutEnabled = lockoutEnabled,
                LockoutEnd = lockoutEnd
            };
        }

        protected override Role CreateTestRole(string roleNamePrefix = "", bool useRoleNamePrefixAsRoleName = false)
        {
            var roleName = useRoleNamePrefixAsRoleName ? roleNamePrefix : string.Format("{0}{1}", roleNamePrefix, Guid.NewGuid());
            return new Role(roleName);
        }

        protected override Expression<Func<UserE, bool>> UserNameAreEqualsPredicate(string userName) => u => u.UserName == userName;

        protected override Expression<Func<UserE, bool>> UserNameStartsWithPredicate(string userName) => u => u.UserName.StartsWith(userName);

        protected override Expression<Func<Role, bool>> RoleNameAreEqualsPredicate(string roleName) => r => r.Name == roleName;

        protected override Expression<Func<Role, bool>> RoleNameStartsWithPredicate(string roleName) => r => r.Name.StartsWith(roleName);

        private static SignInManager<UserE> SetupSignInManager(UserManager<UserE> manager, HttpContext context, StringBuilder logStore = null, IdentityOptions identityOptions = null)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context);
            var roleManager = MockHelpers.MockRoleManager<Role>();
            identityOptions = identityOptions ?? new IdentityOptions();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(a => a.Value).Returns(identityOptions);
            var claimsFactory = new UserClaimsPrincipalFactory<UserE, Role>(manager, roleManager.Object, options.Object);
            var sm = new SignInManager<UserE>(manager, contextAccessor.Object, claimsFactory, options.Object,
                MockHelpers.MockILogger<SignInManager<UserE>>(logStore ?? new StringBuilder()).Object);
            return sm;
        }
        private static void SetupSignIn(Mock<AuthenticationManager> auth, int? userId = null, bool? isPersistent = null, string loginProvider = null)
        {
            auth.Setup(a => a.SignInAsync(new IdentityCookieOptions().ApplicationCookieAuthenticationScheme,
                It.Is<ClaimsPrincipal>(id =>
                    (userId == null || id.FindFirstValue(ClaimTypes.NameIdentifier) == userId.ToString()) &&
                    (loginProvider == null || id.FindFirstValue(ClaimTypes.AuthenticationMethod) == loginProvider)),
                It.Is<AuthenticationProperties>(v => isPersistent == null || v.IsPersistent == isPersistent))).Returns(Task.FromResult(0)).Verifiable();
        }
        [Test]
        public void LogIn1()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                
                Assert.DoesNotThrow(() => repository.RegisterUser(user, "`12qweASD"));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager= SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogIn(user, testPassword));
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void LogIn2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.RegisterUser(user, "`12qweASD"));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogIn(user.UserName, testPassword));
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void LogInAsync()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.RegisterUser(user, "`12qweASD"));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogInAsync(user, testPassword).Result);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void LogInAsync2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.RegisterUser(user, "`12qweASD"));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogInAsync(user.UserName, testPassword).Result);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void LogOff()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogInAsync(user.UserName, testPassword).Result);
                Assert.True(success);
                success = false;
                Assert.DoesNotThrow(() => success = repository.LogOff());
                //Удаляем созданный объект
                Assert.True(success);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void LogOffAsync()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.RegisterUser(user, "`12qweASD"));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => success = repository.LogInAsync(user.UserName, testPassword).Result);
                Assert.True(success);
                success = false;
                Assert.DoesNotThrow(() => success = repository.LogOff());
                //Удаляем созданный объект
                Assert.True(success);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Register()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUser(user, "`12qweASD"));
                Assert.True(result != null && result.Succeeded);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RegisterAsync()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void CreateAndLogin()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser(); 
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.CreateAndLogin(user, "`12qweASD"));
                Assert.True(result != null && result.Succeeded);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void CreateAndLoginAsync()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var repository = new UserMembershipRepository(db);
               
                var user = CreateTestUser();
                IdentityResult result = null;
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.CreateAndLoginAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }





        [Test]
        public void ChangePassword1()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => success = repository.ChangePassword(user, testPassword, "`12QWEasd"));
                Assert.True(success);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ChangePassword2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(() => success = repository.ChangePassword(user.Id, testPassword, "`12QWEasd"));
                Assert.True(success);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ChangePasswordIResult1()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                //сохраняем генерированный объект
                Assert.DoesNotThrow(
                        () => result = repository.ChangePasswordIResult(user.Id, testPassword, "`12QWEasd"));
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ChangePasswordIResult2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);

                Assert.DoesNotThrow(() => result = repository.ChangePasswordIResult(user, testPassword, "`12QWEasd"));
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ChangePasswordIResultAsync1()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
            

                Assert.DoesNotThrow(
                        () => result = repository.ChangePasswordIResultAsync(user, testPassword, "`12QWEasd").Result);
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ChangePasswordIResultAsync2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                var success = false;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);


                Assert.DoesNotThrow(
                        () => result = repository.ChangePasswordIResultAsync(user.Id, testPassword, "`12QWEasd").Result);
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void LoginAndSave()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                //сохраняем генерированный объект
                user.Patronymic = "LOL";
                Assert.DoesNotThrow(() => user = repository.LoginAndSave(user, testPassword, true));
                Assert.NotNull(user);
                Assert.True(user.Id > 0);
                Assert.True(user.Patronymic.Equals("LOL"));
                Assert.DoesNotThrow(() => repository.LogOff());
                Assert.DoesNotThrow(() => user = repository.LoginAndSave(user, testPassword, false));
                Assert.NotNull(user);
                Assert.True(user.Id > 0);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetAllUsersInRole()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult identityResult = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => identityResult = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(identityResult != null && identityResult.Succeeded);
                var roleName = "Crafter";

                Assert.DoesNotThrow(() => identityResult = roleManager.CreateAsync(new Role(roleName)).Result);
                Assert.DoesNotThrow(() => identityResult = manager.AddToRoleAsync(user, roleName).Result);
                IList<UserE> result = null;
                Assert.DoesNotThrow(() => result = repository.GetAllUsersInRole(roleName));
                Assert.True(result != null && result.Count == 1);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void IsInRole1()
        {
            try
            {

                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var roleName = "Crafter";

                Assert.DoesNotThrow(() => result = roleManager.CreateAsync(new Role(roleName)).Result);
                Assert.DoesNotThrow(() => result = manager.AddToRoleAsync(user, roleName).Result);

                var checkResult = false;
                Assert.DoesNotThrow(() => checkResult = repository.IsInRole(user.UserName, roleName));
                Assert.True(checkResult);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void IsInRole2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var signInManager = CreateSignInManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var roleName = "Crafter";

                Assert.DoesNotThrow(() => result = roleManager.CreateAsync(new Role(roleName)).Result);
                Assert.DoesNotThrow(() => result = manager.AddToRoleAsync(user, roleName).Result);

                var checkResult = false;
                Assert.DoesNotThrow(() => checkResult = repository.IsInRole(user, roleName));
                Assert.True(checkResult);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [Ignore("No way to Check it for now")]
        public void IsAuthenticated1()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                var checkResult = false;
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user));
                Assert.True(!checkResult);
                Assert.DoesNotThrow(() => repository.LogIn(user, testPassword));
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user));
                Assert.True(checkResult);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Ignore("No way to Check it for now")]
        [Test]
        public void IsAuthenticated2()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                var checkResult = false;
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user.Id));
                Assert.True(!checkResult);
                Assert.DoesNotThrow(() => repository.LogIn(user, testPassword));
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user.Id));
                Assert.True(checkResult);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Ignore("No way to Check it for now")]
        [Test]
        public void IsAuthenticated3()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                Assert.DoesNotThrow(() => result = repository.RegisterUserAsync(user, "`12qweASD").Result);
                Assert.True(result != null && result.Succeeded);
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                var checkResult = false;
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user.UserName));
                Assert.True(!checkResult);
                Assert.DoesNotThrow(() => repository.LogIn(user, testPassword));
                Assert.DoesNotThrow(() => checkResult = repository.IsAuthenticated(user.UserName));
                Assert.True(checkResult);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RegisterUserWithContactAndLogin()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                var contact = UserTestHelper.GenerateContact();
                IdentityResult result = null;
                Assert.DoesNotThrow(
                    () => result = repository.RegisterUserWithContactAndLogin(user, contact, testPassword));
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void RegisterUserWithContactAndLoginAsync()
        {
            try
            {
                var services = new ServiceCollection();
                var options = Tester<UserContextExt>.CreateNewContextOptions(services);
                var db = new UserContextExt(options);
                SetupIdentityServices(services, db);

                var manager = CreateManager(db, services);
                var roleManager = CreateRoleManager(db, services);
                var repository = new UserMembershipRepository(db);
                var user = CreateTestUser();
                IdentityResult result = null;
                Assert.DoesNotThrow(() => repository.SetUserManager(manager));
                Assert.DoesNotThrow(() => repository.SetRoleManager(roleManager));
                var context = new Mock<HttpContext>();
                var auth = new Mock<AuthenticationManager>();
                context.Setup(c => c.Authentication).Returns(auth.Object).Verifiable();
                SetupSignIn(auth, user.Id, false);
                var signInManager = SetupSignInManager(manager, context.Object);
                Assert.DoesNotThrow(() => repository.SetSignInManager(signInManager));
                var contact = UserTestHelper.GenerateContact();
                Assert.DoesNotThrow(
                    () =>
                        result = repository.RegisterUserWithContactAndLoginAsync(user, contact, testPassword).Result);
                Assert.NotNull(result);
                Assert.True(result.Succeeded);

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}
