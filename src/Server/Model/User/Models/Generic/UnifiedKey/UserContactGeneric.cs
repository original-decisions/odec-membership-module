using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.User.Generic.UnifiedKey
{
    /// <summary>
    /// Обобщенный класс связи контакта и пользователя
    /// </summary>
    /// <typeparam name="TKey">Тип идентификтора</typeparam>
    /// <typeparam name="TUser">Тип пользователя</typeparam>
    /// <typeparam name="TContact">Тип контакта</typeparam>
    public class UserContactGeneric<TKey, TContact>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
    //    [Key, Column(Order = 0)]
        public TKey UserId { get; set; }
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
    //    [Key, Column(Order = 1)]
        public TKey ContactId { get; set; }
        /// <summary>
        /// контакт
        /// </summary>
        public TContact Contact { get; set; }
        /// <summary>
        /// Флаг - является ли основным контактом для акаунта
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool IsAccountBased { get; set; }
    }
}
