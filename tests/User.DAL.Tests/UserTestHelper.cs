using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using odec.CP.Server.Model.Location;
using odec.Server.Model.Contact;
using odec.Server.Model.Location;
using odec.Server.Model.User;

namespace User.DAL.Tests
{
    public static class UserTestHelper
    {
        internal static IWebHost HostServer()
        {
            var host = new WebHostBuilder()
               // .UseNowin()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                //.UseStartup<Startup>()
                .Build();
            host.Run();
            return host;
        }
        internal static void ShutServer()
        {

        }
        internal static void PopulateDefaultMenuDataUserCtx(DbContext db)
        {
            try
            {
                var crafterRole = new Role
                {
                  //  Id = 1,
                    Name = "Crafter"

                };
                db.Set<Role>().Add(crafterRole);
                var userRole = new Role
                {
                    Id = 2,
                    Name = "User",
                };
                db.Set<Role>().Add(userRole);
                var andrew = new odec.Server.Model.User.User
                {
                  //  Id = 1,
                    UserName = "Andrew",

                };
                db.Set<odec.Server.Model.User.User>().Add(andrew);
                var alex = new odec.Server.Model.User.User
                {
                    // Id = 2,
                    UserName = "Alex",
                };
                db.Set<odec.Server.Model.User.User>().Add(alex);
                var male = new Sex
                {
                  //  Id = 1,
                    Name = "Male",
                    Code = "Male",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };
                db.Set<Sex>().Add(male);
                db.Set<UserRole>().Add(new UserRole {RoleId = crafterRole.Id,UserId = andrew.Id});
                var contact = new Contact
                {
                    SexId= male.Id,
                    Email = "pirmosk@gmail.com",
                    BirthdayDate = DateTime.Today,
                    Code = "pirmosk@gmail.com" + DateTime.Now.ToString(),
                    SendNews = true,
                    FirstName = "Alex",
                    LastName = "Pirogov",
                    Patronymic = "Leonidovich",
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    AddressDenormolized = "Litovsky Street 1 flat 512",
                    PhoneNumberDenormolized = "9686698356",
                    Name = "MainAddress",
                    SortOrder = 0
                };
                db.Set<Contact>().Add(contact);
               db.Set<UserContact>().Add(new UserContact
                {
                    UserId = andrew.Id,
                    ContactId = contact.Id
                });
                var russia = new Country
                {
                    Name = "Russia",
                    Code = "Russia",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };
                
                var moscow = new City
                {
                    Name = "Moscow",
                    Code = "Moscow",
                    Region = "Moscow",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0,
                    Genitive = "Moscow",
                    Prepositional = "Moscow"

                };
                var yasenevo = new Subway
                {
                    Name = "Yasenevo",
                    Code = "Yasenevo",
                    City = moscow,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0,
                };
                var litovsky = new Street
                {
                    Name = "Litovsky Bulevard",
                    Code = "LitovskyBulevard",
                    CityId = moscow.Id,
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0,
                };
                var houseOne = new House
                {
                    Name = "1",
                    Code = "ONE",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0,
                    StreetId = litovsky.Id
                };
                var flat512 = new Flat
                {
                    House = houseOne,

                    Name = "512",
                    Code = "512",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0,
                };
              //  var noHousing = new Housing();
                var address1 = new Address
                {
                    Name = "MainAddress",

                    CityId = moscow.Id,
                    CountryId = russia.Id,
                    StreetId = litovsky.Id,
                    FlatId = flat512.Id,
                    House = houseOne,
                    Subway = yasenevo,
                    Code = "MainAddress",
                    IsActive = true,
                    SortOrder = 0,
                    StringRepresentation = String.Empty

                };
                db.Set<Address>().Add(address1);
                db.Set<ContactAddress>().Add(new ContactAddress
                {
                    ContactId = contact.Id,
                    AddressId = address1.Id
                });
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Генерирует тестовый серверный объект - сообщения
        /// </summary>
        /// <returns>тестовый серверный объект - сообщения</returns>
        internal static odec.Server.Model.User.User GenerateModel()
        {
            return new odec.Server.Model.User.User
            {
                UserName = "pirmosk@gmail.com",
                LockoutEnabled = false,
                Email = "pirmosk@gmail.com",
                FirstName = "Alex",
                LastName = "Pirogov"
            };
        }

        internal static Contact GenerateContact()
        {
            return new Contact
            {
                SexId = 1,
                Email = "pirmosk@gmail.com",
                BirthdayDate = DateTime.Today,
                Code = "pirmosk@gmail.com" + DateTime.Now.ToString(),
                SendNews = true,
                FirstName = "Alex",
                LastName = "Pirogov",
                Patronymic = "Leonidovich",
                IsActive = true,
                DateCreated = DateTime.Now,
                AddressDenormolized = "Litovsky Street 1 flat 512",
                PhoneNumberDenormolized = "9686698356",
                Name = "MainAddress",
                SortOrder = 0
            };
        }
    }
}
