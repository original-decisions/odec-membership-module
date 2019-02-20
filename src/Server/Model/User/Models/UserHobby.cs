using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.User
{
    /// <summary>
    /// таблица-связь хобби пользователя
    /// </summary>
    public class UserHobby
    {
        /// <summary>
        /// идентификатор пользователя
        /// </summary>
     //   [Key, Column(Order = 0)]
        public int UserId { get; set; }
        ///// <summary>
        ///// серверный объект - пользователь
        ///// </summary>
        //public User User { get; set; }

        /// <summary>
        /// идентификатор увлечения, хобби
        /// </summary>
      //  [Key,Column(Order = 1)]
        public int HobbyId { get; set; }

        /// <summary>
        /// серверный объект - увлечения, хобби
        /// </summary>
        public Hobby Hobby { get; set; }
    }
}
