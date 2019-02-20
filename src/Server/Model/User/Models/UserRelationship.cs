using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using odec.Server.Model.User.Refactor;

namespace odec.Server.Model.User
{
    /// <summary>
    /// Таблица связей отношения- пользователи
    /// </summary>
    public class UserRelationship
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        /// <summary>
        /// Объект пользователя
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Идентификатор отношения
        /// </summary>
        [Key, Column(Order = 1)]
        public int RelationshipId { get; set; }
        /// <summary>
        /// Объект -Отношение
        /// </summary>
        public Relationship Relationship { get; set; }
        /// <summary>
        /// Идентификатор пользователя, с которым  пользователь в отношениях
        /// </summary>
        [Key, Column(Order = 2)]
        public int RelativeWithId { get; set; }
        /// <summary>
        /// Объект пользователя, с которым  пользователь в отношениях
        /// </summary>
        public User RelativeWith { get; set; }
    }
}
