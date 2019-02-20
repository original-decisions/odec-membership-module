using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Server.Model.User.Refactor;

namespace odec.Server.Model.User
{
    /// <summary>
    /// серверный объект - связь пользователя и альбома
    /// </summary>
    public class UserAlbum
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
        /// идентификатор альбомов
        /// </summary>
        [Key, Column(Order = 1)]
       public int AlbumId { get; set; }
        
        /// <summary>
        /// серверный объект - альбом
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// путь к изображению обложки альбома
        /// </summary>
        public string CoverFoto { get; set; }


    }
}
