using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.User
{
    /// <summary>
    /// связь между пользователем и его друзьями
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// индентификатор пользователя-друга
        /// </summary>
        
        public int FriendId { get; set; }
        /// <summary>
        /// Серверный объект пользователь(друг)
        /// </summary>
        [ForeignKey("FriendId")]
       // [NotMapped]
        public User FriendUsr { get; set; }

        /// <summary>
        /// идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// серверный объект-пользователь
        /// </summary>
        public User User { get; set; }
    }
}
