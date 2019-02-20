using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using odec.CP.User.Membership.DAL.Interop;
using odec.Entity.DAL.Interop;
using odec.Framework.Logging;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Role = odec.CP.Server.Model.User.Membership.Simple.Models.Role;
using UserE = odec.CP.Server.Model.User.Membership.Simple.Models.User;

namespace odec.CP.User.Membership.DAL.Simple
{
    public class UserMembershipRepository : IContextRepository<DbContext>
    {
        private UserManager<UserE> _manager;
        private SignInManager<UserE> _sManager;
        private RoleManager<Role> _roleManager;
        public IMembershipOptions Options { get; set; }

        public UserMembershipRepository()
        {
        }

        public UserMembershipRepository(DbContext db)
        {
            Db = db;
        }

        public UserMembershipRepository(DbContext db, UserManager<UserE> manager, IMembershipOptions options)
        {
            Db = db;
            _manager = manager;
            Options = options;
        }

        public UserMembershipRepository(DbContext db, UserManager<UserE> manager, SignInManager<UserE> signManager,
            IMembershipOptions options)
        {
            Db = db;
            _manager = manager;
            _sManager = signManager;
            Options = options;
        }

        public UserMembershipRepository(DbContext db, UserManager<UserE> manager, SignInManager<UserE> signManager,
            RoleManager<Role> roleManager, IMembershipOptions options)
        {
            Db = db;
            _manager = manager;
            _sManager = signManager;
            _roleManager = roleManager;
            Options = options;
        }

        public IdentityResult RegisterUserWithContactAndLogin(UserE user, Contact contact, string password)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
            try
            {
                var res = RegisterUser(user, password: password);
                if (res.Succeeded)
                {

                    if (!Queryable.Any<Contact>(Db.Set<Contact>(), it => it.Id == contact.Id))
                        Db.Set<Contact>().Add(contact);
                    else
                        Db.Entry(contact).State = EntityState.Modified;

                    if (
                        !Queryable.Any<UserContact>(Db.Set<UserContact>(),
                            it => it.ContactId == contact.Id && it.UserId == user.Id))
                    {
                        Db.Set<UserContact>().Add(new UserContact
                        {
                            UserId = user.Id,
                            ContactId = contact.Id
                        });
                    }
                    Db.SaveChanges();
                    //     tran.Commit();
                    LogIn(user, password);
                }
                //else
                //    tran.Rollback();

                return res;
            }
            catch (Exception ex)
            {
                //    tran.Rollback();
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            //   }
        }

        public void SetUserManager(UserManager<UserE> userManager)
        {
            _manager = userManager;
        }

        public void SetSignInManager(SignInManager<UserE> signInManager)
        {
            _sManager = signInManager;
        }

        public bool LogIn(UserE user, string password, bool persistCookie = true)
        {
            return LogIn(user.UserName, password, persistCookie);
        }

        public bool LogIn(string userName, string password, bool persistCookie = true)
        {
            try
            {
                var task = _sManager.PasswordSignInAsync(userName, password, persistCookie, lockoutOnFailure: false);
                task.Wait();
                return true;
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }

        }

        public Task<bool> LogInAsync(string userName, string password, bool persistCookie = true)
        {
            return Task<bool>.Factory.StartNew(() => LogIn(userName, password, persistCookie));
        }

        public IdentityResult RegisterUser(UserE user, string password)
        {
            var task = _manager.CreateAsync(user, password);
            task.Wait();
            return task.Result;
        }

        public Task<IdentityResult> RegisterUserAsync(UserE user, string password)
        {
            return _manager.CreateAsync(user, password);
        }

        public IdentityResult CreateAndLogin(UserE user, string password)
        {
            var res = RegisterUser(user, password);
            LogIn(user, password);
            return res;
        }

        public Task<IdentityResult> CreateAndLoginAsync(UserE user, string password)
        {
            return Task<IdentityResult>.Factory.StartNew(() => CreateAndLogin(user, password));
        }

        public Task<bool> LogInAsync(UserE user, string password, bool persistCookie = true)
        {
            return Task<bool>.Factory.StartNew(() => LogIn(user, password, persistCookie));
        }

        public bool LogOff()
        {
            try
            {
                _sManager.SignOutAsync().Wait();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Task<bool> LogOffAsync()
        {
            return Task<bool>.Factory.StartNew(LogOff);
        }

        public bool ChangePassword(UserE user, string oldPassword, string newPassword)
        {
            return ChangePassword((int)user.Id, oldPassword, newPassword);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            return ChangePasswordIResult(userId, oldPassword, newPassword).Succeeded;
        }

        public IdentityResult ChangePasswordIResult(UserE user, string oldPassword, string newPassword)
        {
            var task = _manager.ChangePasswordAsync(user, oldPassword, newPassword);
            task.Wait();
            return task.Result;
        }

        public IdentityResult ChangePasswordIResult(int userId, string oldPassword, string newPassword)
        {
            var user = _manager.Users.Single(it => it.Id == userId);
            var task = _manager.ChangePasswordAsync(user, oldPassword, newPassword);
            task.Wait();
            return task.Result;
        }

        public Task<IdentityResult> ChangePasswordIResultAsync(UserE user, string oldPassword, string newPassword)
        {
            return Task<IdentityResult>.Factory.StartNew(() => ChangePasswordIResult(user, oldPassword, newPassword));
        }

        public Task<IdentityResult> ChangePasswordIResultAsync(int userId, string oldPassword, string newPassword)
        {
            return Task<IdentityResult>.Factory.StartNew(() => ChangePasswordIResult(userId, oldPassword, newPassword));
        }

        public UserE LoginAndSave(UserE user, string password, bool persistCookie = true)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
            try
            {
                user.DateUpdated = DateTime.Now;
                user.LastActivityDate = DateTime.Now;
                user.LastLogin = DateTime.Now;
                Db.SaveChanges();
                user = _manager.Users.Single(it => user.UserName == it.UserName);
                _sManager.SignOutAsync().Wait();
#if !NETCOREAPP2_1
_sManager.SignInAsync(user, new AuthenticationProperties {IsPersistent = persistCookie}).Wait();
#endif
#if NETCOREAPP2_1
                _sManager.SignInAsync(user, persistCookie).Wait();
#endif
                return user;
            }
            catch (Exception ex)
            {
                //    tran.Rollback();
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
            //  }
        }

        public UserE OAuthLogin(UserE user, bool persistCookie = true)
        {
            throw new NotImplementedException();
        }

        public UserE SaveOathExtraData(UserE user)
        {
            throw new NotImplementedException();
        }

        public bool HasLocalLogin(UserE user)
        {
            return HasPassword(user);
        }

        public bool HasLocalLogin(int userId)
        {
            return HasPassword(userId);
        }

        public bool HasPassword(UserE user)
        {
            var task = _manager.HasPasswordAsync(user);
            task.Wait();
            return task.Result;

        }

        public bool HasPassword(int userId)
        {

            var user = _manager.Users.Single(it => it.Id == userId);
            var task = _manager.HasPasswordAsync(user);
            task.Wait();
            return task.Result;
        }

        public bool IsOath(UserE user)
        {
            var task = _manager.GetLoginsAsync(user);
            task.Wait();

            return task.Result.Count > 0;
        }

        public bool IsOath(int userId)
        {
            var user = _manager.Users.Single(it => it.Id == userId);
            var task = _manager.GetLoginsAsync(user);
            task.Wait();

            return task.Result.Count > 0;
        }

        public IList<UserE> GetAllUsersInRole(string roleName)
        {
            return _manager.GetUsersInRoleAsync(roleName).Result;
        }

        public bool IsInRole(string userName, string roleName)
        {
            var user = _manager.Users.Single(it => it.UserName == userName);
            var result = _manager.IsInRoleAsync(user, roleName).Result;
            return result;
        }

        public bool IsInRole(UserE user, string roleName)
        {
            var result = _manager.IsInRoleAsync(user, roleName).Result;
            return result;
        }

        public bool IsAuthenticated(string username)
        {
            var user = _manager.Users.Single(it => it.UserName == username);
            var userClaims = _manager.GetClaimsAsync(user).Result;
            return _sManager.IsSignedIn(new ClaimsPrincipal(userClaims.Select(it => it.Subject)));
        }

        public bool IsAuthenticated(UserE user)
        {
            // var userLogins = _manager.GetLoginsAsync(user).Result;
            var userClaims = _manager.GetClaimsAsync(user).Result;
            return _sManager.IsSignedIn(new ClaimsPrincipal(userClaims.Select(it => it.Subject)));
        }

        public bool IsAuthenticated(int userId)
        {
            var user = _manager.Users.Single(it => it.Id == userId);
            var userClaims = _manager.GetClaimsAsync(user).Result;
            return _sManager.IsSignedIn(new ClaimsPrincipal(userClaims.Select(it => it.Subject)));

        }
        public Task<IdentityResult> RegisterUserWithContactAndLoginAsync(UserE user, Contact contact, string password)
        {
            return Task<IdentityResult>.Factory.StartNew(() => RegisterUserWithContactAndLogin(user, contact, password));
        }

        public DbContext Db { get; set; }
        public void SetConnection(string connection)
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public void SetRoleManager(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}