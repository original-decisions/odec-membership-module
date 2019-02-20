using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Прокси объект для контекста пользователя и альбома
    /// </summary>
    /// <typeparam name="TUserAlbum">тип альбома</typeparam>
    public interface IUserAlbumContext<TUserAlbum> where TUserAlbum : class
    {
        /// <summary>
        /// Таблица связи пользователя и альбомов
        /// </summary>
        DbSet<TUserAlbum> UserAlbums { get; set; } 
    }

}
