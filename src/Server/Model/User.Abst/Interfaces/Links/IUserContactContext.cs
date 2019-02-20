
using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Links
{
    /// <summary>
    /// Прокси объект для контекста пользователя и контакта
    /// </summary>
    /// <typeparam name="TUserContact">тип контактов</typeparam>
    public interface IUserContactContext<TUserContact> where TUserContact : class
    {
        /// <summary>
        /// Таблица связи пользователя и контактов
        /// </summary>
        DbSet<TUserContact> UserContacts { get; set; }
    }
}
