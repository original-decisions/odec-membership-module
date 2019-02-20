using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Таблица связи контекста и видео
    /// </summary>
    /// <typeparam name="TMovie">тип видео</typeparam>
    /// <typeparam name="TMovieCategories">тип связи видео и категорий</typeparam>
    /// <typeparam name="TMovieCategory">тип связи видео и категории</typeparam>
    public interface IMoviesContext<TMovie, TMovieCategories, TMovieCategory> 
        where TMovie : class
        where TMovieCategories : class
        where TMovieCategory : class
    {
        /// <summary>
        /// Таблица видео
        /// </summary>
        DbSet<TMovie> Movies { get; set; }

        /// <summary>
        /// Таблица связи видео и категорий
        /// </summary>
        DbSet<TMovieCategories> MovieCategories { get; set; }

        /// <summary>
        /// Таблица категорий
        /// </summary>
        DbSet<TMovieCategory> Categories { get; set; }
    }
}
