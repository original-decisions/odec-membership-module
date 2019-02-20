using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Прокси объект для контекста пользователя и видео
    /// </summary>
    /// <typeparam name="TUserMovie">тип видео</typeparam>
    public interface IUserMovieContext<TUserMovie> 
        where TUserMovie : class
    {
        /// <summary>
        /// Таблица связи пользователя и видео
        /// </summary>
        DbSet<TUserMovie> UserMovies { get; set; }
    }
}
