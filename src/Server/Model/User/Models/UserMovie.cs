using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Server.Model.User.Refactor;

namespace odec.Server.Model.User
{
    /// <summary>
    /// серверный объект - связь пользователя и фильма
    /// </summary>
    public class UserMovie
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        [Key,Column(Order = 0)]
        public int UserId { get; set; }

        ///// <summary>
        ///// серверный объект - пользователь
        ///// </summary>
        //public User User { get; set; }
        
        /// <summary>
        /// идентификатор фильма
        /// </summary>
        [Key, Column(Order = 1)]
        public int MovieId { get; set; }

        /// <summary>
        /// серверный объект - фильм
        /// </summary>
        public Movie Movie { get; set; }
    }
}
