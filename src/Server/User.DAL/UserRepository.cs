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
using odec.Server.Model.Location;
using odec.Server.Model.User;
using odec.User.DAL.Interop;

namespace odec.User.DAL
{
    public class UserRepository : IUserRepository<Server.Model.User.User, int, Contact, Address>, IContextRepository<DbContext>
    {


        public UserRepository()
        {
        }
        public UserRepository(DbContext db)
        {
            Db = db;
        }

        public UserRepository(DbContext db, UserManager<Server.Model.User.User> manager, int authSessionLifetime)
        {
            Db = db;
        }


        public IEnumerable<Server.Model.User.User> GetTableData(bool all = false)
        {
            return Db.Set<Server.Model.User.User>().Where(it => all || (it.LockoutEnabled && !all));
        }

        public Task<IEnumerable<Server.Model.User.User>> GetTableDataAsync(bool all = false)
        {
            return Task<IEnumerable<Server.Model.User.User>>.Factory.StartNew(() => GetTableData(all));
        }

        public Server.Model.User.User GetById(int id)
        {
            return Db.Set<Server.Model.User.User>().SingleOrDefault(it => it.Id == id);
        }

        public void Save(Server.Model.User.User entity)
        {
            if (!Db.Set<Server.Model.User.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
            }
            Db.SaveChanges();

        }

        public void SaveById(Server.Model.User.User entity)
        {
            Save(entity);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            Delete(user);
        }

        public Server.Model.User.User Save(Server.Model.User.User entity, out int id)
        {
            if (!Db.Set<Server.Model.User.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
            }
            Db.SaveChanges();
            id = entity.Id;
            return entity;
        }



        public void Delete(Server.Model.User.User entity)
        {
            try
            {
                Db.Set<Server.Model.User.User>().Remove(entity);
                Db.SaveChanges();
            }
            catch (Exception e)
            {
                LogEventManager.Logger.Error(e.Message, e);
            }
        }

        public Task DeleteAsync(Server.Model.User.User entity)
        {
            return Task.Factory.StartNew(() => Delete(entity));
        }

        public Task SaveAsync(Server.Model.User.User entity)
        {
            return Task.Factory.StartNew(() => Save(entity));
        }

        public Task<KeyValuePair<int, Server.Model.User.User>> SaveReturnIdAsync(Server.Model.User.User entity)
        {
            return Task<KeyValuePair<int, Server.Model.User.User>>.Factory.StartNew(() => SaveReturnId(entity));
        }
        public KeyValuePair<int, Server.Model.User.User> SaveReturnId(Server.Model.User.User entity)
        {

            if (!Db.Set<Server.Model.User.User>().Any(usr => usr.Id == entity.Id))
            {
                Db.Set<Server.Model.User.User>().Add(entity);
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
            }
            Db.SaveChanges();
            KeyValuePair<int, Server.Model.User.User> pair = new KeyValuePair<int, Server.Model.User.User>(entity.Id, entity);
            return pair;
        }

        public Server.Model.User.User GetUserByUsername(string username)
        {
            return Db.Set<Server.Model.User.User>().First(usr => usr.UserName == username);
        }

        public IEnumerable<Address> GetUserAddresses(Server.Model.User.User user)
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

        public KeyValuePair<Contact, Server.Model.User.User> SaveUserWithContact(Server.Model.User.User user, Contact contact)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
                try
                {


                    Save(user);
                    if (!Db.Set<Contact>().Any(it => it.Id == contact.Id))
                        Db.Set<Contact>().Add(contact);
                    else
                        Db.Entry(contact).State = EntityState.Modified;

                    if (!Db.Set<UserContact>().Any(it => it.ContactId == contact.Id && it.UserId == user.Id))
                    {
                        Db.Set<UserContact>().Add(new UserContact
                        {
                            UserId = user.Id,
                            ContactId = contact.Id
                        });
                    }
                    Db.SaveChanges();
              //      tran.Commit();
                    return new KeyValuePair<Contact, Server.Model.User.User>(contact, user);
                }
                catch (Exception ex)
                {
               //     tran.Rollback();
                    LogEventManager.Logger.Error(ex.Message, ex);
                    throw;
                }
          //  }
        }

        public Contact SaveContact(int userId, Contact contact)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
                try
                {
                    if (!Db.Set<Contact>().Any(it => it.Id == contact.Id))
                        Db.Set<Contact>().Add(contact);
                    else
                        Db.Entry(contact).State = EntityState.Modified;

                    if (!Db.Set<UserContact>().Any(it => it.ContactId == contact.Id && it.UserId == userId))
                    {
                        Db.Set<UserContact>().Add(new UserContact
                        {
                            UserId = userId,
                            ContactId = contact.Id
                        });
                    }
                    Db.SaveChanges();
                   // tran.Commit();
                    return contact;
                }
                catch (Exception ex)
                {
                 //   tran.Rollback();
                    LogEventManager.Logger.Error(ex.Message, ex);
                    throw;
                }
           // }
        }


        public Contact SaveContact(Server.Model.User.User user, Contact contact)
        {
            return SaveContact((int)user.Id, contact);
        }

        public Task<Contact> SaveContactAsync(int userId, Contact contact)
        {
            return Task<Contact>.Factory.StartNew(() => SaveContact(userId, contact));
        }

        public Task<Contact> SaveContactAsync(Server.Model.User.User user, Contact contact)
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

        public Server.Model.User.User Deactivate(int id)
        {
            var user = GetById(id);
            Deactivate(user);
            return user;
        }

        public void Deactivate(Server.Model.User.User entity)
        {
            entity.LockoutEnabled = true;
            Db.SaveChanges();
        }

        public Server.Model.User.User Activate(int id)
        {
            var user = GetById(id);
            Activate(user);
            return user;
        }

        public void Activate(Server.Model.User.User entity)
        {
            entity.LockoutEnabled = false;
            Db.SaveChanges();
        }
    }
}
