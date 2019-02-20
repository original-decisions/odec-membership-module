using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using odec.CP.Server.Model.Location;
using odec.Entity.DAL.Interop;
using odec.Framework.Logging;
using odec.Server.Model.Contact;
using odec.Server.Model.Location;
using odec.Server.Model.User;
using odec.Server.Model.User.Contexts;
using odec.User.DAL;
using odec.User.DAL.Interop;
using UserM = odec.Server.Model.User.User;

namespace User.DAL.Tests
{

    public class UserExtRepositoryTester : Tester<UserContextExt>
    {
 //        public IUserRepository<UserM, int, Contact, Address> repository { get; set; }
       // private HttpSimulator simulator = new HttpSimulator();

        public UserExtRepositoryTester()
        {
  //          Db = new UserContextExt(Effort.DbConnectionFactory.CreateTransient());
         //   simulator.SimulateRequest();
            //Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
            //            {
            //                {"owin.RequestBody", null}
            //            };
            //HttpContext.Current.Items.Add("owin.Environment", owinEnvironment);
            //AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        }

        public SignInManager<UserM> SignInManager { get; set; }
        public UserManager<UserM> UserManager { get; set; }
        [Test]
        public void InitContext()
        {
            var options = CreateNewContextOptions();
          
            using (var db = new UserContextExt(options))
            {
              //  host.Services.
              //  var userManager = new UserManager<UserM>(new UserStore<>());
                 var repository = new UserRepository(db);//new UserRepository(db,,new SignInManager<UserM>() );
                
                var ctx = (repository as IContextRepository<DbContext>);
                Assert.NotNull(ctx);
                Assert.DoesNotThrow(() => ctx.SetContext(db));
            }
        }


        /// <summary>
        /// Сохраняем серверный объект сообщения
        /// для корректной работы необходимо, чтобы отрабатывало удаление элемента
        /// </summary>
        [Test]//атрибут тест -это атрибут из фреймворка nUnit http://www.nunit.org/
        public void Save()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //проверяем, что он сохранился(присвоился новый идентификатор в базе)
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Удаление серверного объекта сообщений
        /// Для корректной работы теста необходимо, чтобы отрабатывало сохранение
        /// </summary>
        [Test]
        public void Delete()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест удаления серверного объекта  сообщений по его идентификатору 
        /// </summary>
        [Test]
        public void DeleteById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item.Id));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест деактивации записи серверного объекта сообщений
        /// </summary>
        [Test]
        public void Deactivate()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов деактивации серверного объекта
                    Assert.DoesNotThrow(() => repository.Deactivate(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что деактивация сработала
                    Assert.IsFalse(!item.LockoutEnabled);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест деактивации записи серверного объекта сообщений по его идентификатору 
        /// </summary>
        [Test]
        public void DeactivateById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов деактивации серверного объекта
                    Assert.DoesNotThrow(() => item = repository.Deactivate(item.Id));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что деактивация сработала
                    Assert.IsFalse(!item.LockoutEnabled);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест активации записи серверного объекта сообщений по его идентификатору 
        /// </summary>
        [Test]
        public void Activate()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов активации серверного объекта
                    Assert.DoesNotThrow(() => repository.Activate(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что активация сработала
                    Assert.IsTrue(!item.LockoutEnabled);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест активации записи серверного объекта сообщения по его идентификатору 
        /// </summary>
        [Test]
        public void ActivateById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    item.LockoutEnabled = true;
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов активации серверного объекта
                    Assert.DoesNotThrow(() => item = repository.Activate(item.Id));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что активация сработала
                    Assert.IsTrue(!item.LockoutEnabled);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Тест проверяющий возможность получения по идентификатору сущности
        /// (для удачного прохождения теста необходимо, чтобы объект сохранялся и удалялся)
        /// </summary>
        [Test]
        public void GetById()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));

                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetUserByUsername()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => item = repository.GetUserByUsername(item.UserName));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        [TestCase("Andrew",ExpectedResult = true)]
        public bool GetUserAddresses1(string userName)
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = db.Set<UserM>().Single(it=>it.UserName == userName);
                    IEnumerable<Address> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetUserAddresses(item));
                    Assert.NotNull(result);
                    return result.Any();
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        [TestCase("Andrew", ExpectedResult = true)]
        public bool GetUserAddresses2(string userName)
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = db.Set<UserM>().Single(it => it.UserName == userName);
                    IEnumerable<Address> result = null;

                    Assert.DoesNotThrow(() => result = repository.GetUserAddresses(item.Id));
                                        Assert.NotNull(result);
                    return result.Any();
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void SaveUserWithContact()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    var contact = UserTestHelper.GenerateContact();
                    var result = new KeyValuePair<Contact, UserM>(contact, item);
                    Assert.DoesNotThrow(() => result = repository.SaveUserWithContact(item, contact));
                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(result.Key);
                    Assert.NotNull(result.Value);
                    Assert.Greater(result.Key.Id, 0);
                    Assert.Greater(result.Value.Id, 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void SaveContact1()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    var contact = UserTestHelper.GenerateContact();

                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => contact = repository.SaveContact(item, contact));

                    Assert.NotNull(contact);
                    Assert.True(contact.Id > 0);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void SaveContact2()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    var contact = UserTestHelper.GenerateContact();

                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => contact = repository.SaveContact(item.Id, contact));

                    Assert.NotNull(contact);
                    Assert.True(contact.Id > 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void SaveContactAsync1()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    var contact = UserTestHelper.GenerateContact();

                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => contact = repository.SaveContactAsync(item, contact).Result);

                    Assert.NotNull(contact);
                    Assert.True(contact.Id > 0);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void SaveContactAsync2()
        {
            try
            {
                var options = CreateNewContextOptions();using (var db = new UserContextExt(options))
                {
                    var repository = new UserRepository(db);
                    UserTestHelper.PopulateDefaultMenuDataUserCtx(db);
                    var item = UserTestHelper.GenerateModel();
                    var contact = UserTestHelper.GenerateContact();

                    Assert.DoesNotThrow(() => repository.Save(item));
                    Assert.DoesNotThrow(() => contact = repository.SaveContactAsync(item.Id, contact).Result);

                    Assert.NotNull(contact);
                    Assert.True(contact.Id > 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}
