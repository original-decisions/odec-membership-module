using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using odec.CP.Server.Model.Location;
using odec.Entity.DAL.Interop;
using odec.Framework.Logging;
using odec.Server.Model.Contact;
using odec.Server.Model.User;
using odec.User.DAL.Interop;

namespace odec.User.DAL.Denormalized
{
    public class UserRepository : IUserRepository<Server.Model.User.Denormalized.User, int, Contact, Address>, IContextRepository<DbContext>
    {

        private UserManager<Server.Model.User.Denormalized.User> _manager;
        private SignInManager<Server.Model.User.Denormalized.User> _sManager;
        private string _xsrfKey = "XsrfId";
        private readonly int _authSessionLifetime;


        public UserRepository(DbContext db, UserManager<Server.Model.User.Denormalized.User> manager, SignInManager<Server.Model.User.Denormalized.User> sManager, int authSessionLifetime)
        {
            _manager = manager;
            _sManager = sManager;
            _authSessionLifetime = authSessionLifetime;
            Db = db;
        }

        public string xsrfKey
        {
            get { return _xsrfKey; }
            set { _xsrfKey = value; }
        }

        public void SetUserManager(UserManager<Server.Model.User.Denormalized.User> userManager)
        {
            _manager = userManager;
        }

        public void SetSignInManager(SignInManager<Server.Model.User.Denormalized.User> signInManager)
        {
            _sManager = signInManager;
        }



        public bool LogIn(Server.Model.User.Denormalized.User user, string password, bool persistCookie = true)
        {
            return LogIn((string)user.UserName, password, persistCookie);
        }

        public bool LogIn(string userName, string password, bool persistCookie = true)
        {
            var task = _sManager.PasswordSignInAsync(userName, password, persistCookie, lockoutOnFailure: false);
            task.Wait();
            return false;
        }

        public Task<bool> LogInAsync(string userName, string password, bool persistCookie = true)
        {
            return Task<bool>.Factory.StartNew(() => LogIn(userName, password, persistCookie));
        }

        public IdentityResult RegisterUser(Server.Model.User.Denormalized.User user, string password)
        {
            var task = _manager.CreateAsync(user, password);
            return task.Result;
        }

        public Task<IdentityResult> RegisterUserAsync(Server.Model.User.Denormalized.User user, string password)
        {
            return Task<IdentityResult>.Factory.StartNew(() => RegisterUser(user, password));
        }

        public IdentityResult CreateAndLogin(Server.Model.User.Denormalized.User user, string password)
        {
            var res = RegisterUser(user, password);
            LogIn(user, password);
            return res;
        }

        public Task<IdentityResult> CreateAndLoginAsync(Server.Model.User.Denormalized.User user, string password)
        {
            return Task<IdentityResult>.Factory.StartNew(() => CreateAndLogin(user, password));
        }

        public Task<bool> LogInAsync(Server.Model.User.Denormalized.User user, string password, bool persistCookie = true)
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

        public bool ChangePassword(Server.Model.User.Denormalized.User user, string oldPassword, string newPassword)
        {
            return ChangePassword((int)user.Id, oldPassword, newPassword);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            return ChangePasswordIResult(userId, oldPassword, newPassword).Succeeded;
        }

        public IdentityResult ChangePasswordIResult(Server.Model.User.Denormalized.User user, string oldPassword, string newPassword)
        {
            return ChangePasswordIResult((int)user.Id, oldPassword, newPassword);
        }

        public IdentityResult ChangePasswordIResult(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangePasswordIResultAsync(Server.Model.User.Denormalized.User user, string oldPassword, string newPassword)
        {
            return Task<IdentityResult>.Factory.StartNew(() => ChangePasswordIResult(user, oldPassword, newPassword));
        }

        public Task<IdentityResult> ChangePasswordIResultAsync(int userId, string oldPassword, string newPassword)
        {
            return Task<IdentityResult>.Factory.StartNew(() => ChangePasswordIResult(userId, oldPassword, newPassword));
        }

        public Server.Model.User.Denormalized.User LoginAndSave(Server.Model.User.Denormalized.User user, string password, bool persistCookie = true)
        {
            throw new NotImplementedException();
        }

        public Server.Model.User.Denormalized.User OAuthLogin(Server.Model.User.Denormalized.User user, bool persistCookie = true)
        {
            throw new NotImplementedException();
        }

        public Server.Model.User.Denormalized.User SaveOathExtraData(Server.Model.User.Denormalized.User user)
        {
            throw new NotImplementedException();
        }

        public bool HasLocalLogin(Server.Model.User.Denormalized.User user)
        {
            return HasPassword(user);
        }

        public bool HasLocalLogin(int userId)
        {
            throw new NotImplementedException();
        }

        public bool HasPassword(Server.Model.User.Denormalized.User user)
        {
            var task = _manager.HasPasswordAsync(user);
            task.Wait();
            return task.Result;

        }

        public bool HasPassword(int userId)
        {
            throw new NotImplementedException();

        }

        public bool IsOath(Server.Model.User.Denormalized.User user)
        {
            var task = _manager.GetLoginsAsync(user);
            task.Wait();

            return task.Result.Count > 0;
        }

        public bool IsOath(int userId)
        {
            throw new NotImplementedException();
        }

        public IList<Server.Model.User.Denormalized.User> GetAllUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(Server.Model.User.Denormalized.User user, string roleName)
        {
            var task = _manager.IsInRoleAsync(user, roleName);
            task.Wait();
            return task.Result;
        }

        public bool IsAuthenticated(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated(Server.Model.User.Denormalized.User user)
        {
            return IsAuthenticated((string)user.UserName);
        }

        public bool IsAuthenticated(int userId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Server.Model.User.Denormalized.User> GetTableData(bool all = false)
        {
            return Db.Set<Server.Model.User.Denormalized.User>().Where(it => all || (it.LockoutEnabled && !all));
        }

        public Task<IEnumerable<Server.Model.User.Denormalized.User>> GetTableDataAsync(bool all = false)
        {
            return Task<IEnumerable<Server.Model.User.Denormalized.User>>.Factory.StartNew(() => GetTableData(all));
        }

        public Server.Model.User.Denormalized.User GetById(int id)
        {
            return Db.Set<Server.Model.User.Denormalized.User>().Single(it => it.Id == id);
        }

        public void Save(Server.Model.User.Denormalized.User entity)
        {
            if (!Db.Set<Server.Model.User.Denormalized.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.Denormalized.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State= EntityState.Modified;
            }
            
            Db.SaveChanges();

        }

        public void SaveById(Server.Model.User.Denormalized.User entity)
        {
            Save(entity);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            Delete(user);
        }

        public Server.Model.User.Denormalized.User Save(Server.Model.User.Denormalized.User entity, out int id)
        {
            if (!Db.Set<Server.Model.User.Denormalized.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.Denormalized.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
            }

            Db.SaveChanges();
            id = entity.Id;
            return entity;
        }



        public void Delete(Server.Model.User.Denormalized.User entity)
        {
            try
            {
                Db.Set<Server.Model.User.Denormalized.User>().Remove(entity);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                LogEventManager.Logger.Error(e.Message, e);
            }
        }

        public Task DeleteAsync(Server.Model.User.Denormalized.User entity)
        {
            return Task.Factory.StartNew(() => Delete(entity));
        }

        public Task SaveAsync(Server.Model.User.Denormalized.User entity)
        {
            return Task.Factory.StartNew(() => Save(entity));
        }

        public Task<KeyValuePair<int, Server.Model.User.Denormalized.User>> SaveReturnIdAsync(Server.Model.User.Denormalized.User entity)
        {
            return Task<KeyValuePair<int, Server.Model.User.Denormalized.User>>.Factory.StartNew(() => SaveReturnId(entity));
        }
        public KeyValuePair<int, Server.Model.User.Denormalized.User> SaveReturnId(Server.Model.User.Denormalized.User entity)
        {

            if (!Db.Set<Server.Model.User.Denormalized.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.Denormalized.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
            }

            Db.SaveChanges();
            KeyValuePair<int, Server.Model.User.Denormalized.User> pair = new KeyValuePair<int, Server.Model.User.Denormalized.User>(entity.Id, entity);
            return pair;
        }

        public Server.Model.User.Denormalized.User GetUserByUsername(string username)
        {
            return Db.Set<Server.Model.User.Denormalized.User>().First(usr => usr.UserName == username);
        }

        public IEnumerable<Address> GetUserAddresses(Server.Model.User.Denormalized.User user)
        {
            return from userContact in Db.Set<UserContact>()
                   join contactAddress in Db.Set<ContactAddress>() on userContact.ContactId equals contactAddress.ContactId
                   join address in Db.Set<Address>() on contactAddress.AddressId equals address.Id
                   select address;
        }

        public IEnumerable<Address> GetUserAddresses(int userId)
        {
            return from userContact in Db.Set<UserContact>()
                   join contactAddress in Db.Set<ContactAddress>() on userContact.ContactId equals contactAddress.ContactId
                   join address in Db.Set<Address>() on contactAddress.AddressId equals address.Id
                   where userId == userContact.UserId
                   select address;
        }

        public KeyValuePair<Contact, Server.Model.User.Denormalized.User> SaveUserWithContact(Server.Model.User.Denormalized.User user, Contact contact)
        {
            if (!Db.Set<Server.Model.User.Denormalized.User>().Any(usr => usr.Id == user.Id))
            {
                Db.Set<Server.Model.User.Denormalized.User>().Add(user);
            }
            else
            {
                Db.Entry(user).State = EntityState.Modified;
            }
            var uC = new UserContact
            {
                UserId = user.Id,
                ContactId = contact.Id
            };
            if (!Db.Set<UserContact>().Any(usr => usr.UserId == user.Id && usr.ContactId == contact.Id))
            {
                Db.Set<UserContact>().Add(uC);
            }
            else
            {
                Db.Entry(uC).State = EntityState.Modified;
            }
            Db.SaveChanges();
            return new KeyValuePair<Contact, Server.Model.User.Denormalized.User>(contact, user);
        }

        public IdentityResult RegisterUserWithContactAndLogin(Server.Model.User.Denormalized.User user, Contact contact, string password)
        {
            using (var tran = Db.Database.BeginTransaction())
            {
                try
                {
                    var res = RegisterUser(user, password: password);
                    if (res.Succeeded)
                    {
                        if (!Db.Set<Contact>().Any(c => c.Id == contact.Id))
                        {
                            Db.Set<Contact>().Add(contact);
                        }
                        else
                        {
                            Db.Entry(contact).State = EntityState.Modified;
                        }
                        var uC = new UserContact
                        {
                            UserId = user.Id,
                            ContactId = contact.Id
                        };
                        if (!Db.Set<UserContact>().Any(usr => usr.UserId == user.Id && usr.ContactId == contact.Id))
                        {
                            Db.Set<UserContact>().Add(uC);
                        }
                        else
                        {
                            Db.Entry(uC).State = EntityState.Modified;
                        }

                        Db.SaveChanges();
                        tran.Commit();
                        LogIn(user, password);

                    }
                    else
                        tran.Rollback();

                    return res;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public Task<IdentityResult> RegisterUserWithContactAndLoginAsync(Server.Model.User.Denormalized.User user, Contact contact, string password)
        {
            return Task<IdentityResult>.Factory.StartNew(() => RegisterUserWithContactAndLogin(user, contact, password));
        }

        public Contact SaveContact(int userId, Contact contact)
        {
            if (!Db.Set<Contact>().Any(c => c.Id == contact.Id))
            {
                Db.Set<Contact>().Add(contact);
            }
            else
            {
                Db.Entry(contact).State = EntityState.Modified;
            }
            var uC = new UserContact
            {
                UserId = userId,
                ContactId = contact.Id
            };
            if (!Db.Set<UserContact>().Any(usr => usr.UserId == userId && usr.ContactId == contact.Id))
            {
                Db.Set<UserContact>().Add(uC);
            }
            else
            {
                Db.Entry(uC).State = EntityState.Modified;
            }
            Db.SaveChanges();
            return contact;
        }


        public Contact SaveContact(Server.Model.User.Denormalized.User user, Contact contact)
        {
            return SaveContact((int)user.Id, contact);
        }

        public Task<Contact> SaveContactAsync(int userId, Contact contact)
        {
            return Task<Contact>.Factory.StartNew(() => SaveContact(userId, contact));
        }

        public Task<Contact> SaveContactAsync(Server.Model.User.Denormalized.User user, Contact contact)
        {
            return SaveContactAsync((int)user.Id, contact);
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

        public Server.Model.User.Denormalized.User Deactivate(int id)
        {
            var user = GetById(id);
            Deactivate(user);
            return user;
        }

        public void Deactivate(Server.Model.User.Denormalized.User entity)
        {
            entity.LockoutEnabled = true;
            Db.SaveChanges();
        }

        public Server.Model.User.Denormalized.User Activate(int id)
        {
            var user = GetById(id);
            Activate(user);
            return user;
        }

        public void Activate(Server.Model.User.Denormalized.User entity)
        {
            entity.LockoutEnabled = false;
            Db.SaveChanges();
        }
    }
}
