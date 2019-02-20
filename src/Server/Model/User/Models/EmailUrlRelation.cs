using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace odec.Server.Model.User
{
    /// <summary>
    /// серверный объект-связь эл.почты и ссылок на основные почтовые ресурсы (исп. для восстановления доступа)
    /// </summary>
    public class EmailUrlRelation
    {
         public EmailUrlRelation()
        {
            IsActive = true;
        }

        /// <summary>
         /// постфикс адреса почты (@gmail.com, @ yandex.ru и проч.)
        /// </summary>
        [Key]
        [Required]
        [StringLength(40)]
        public string Postfix { get; set; }

        /// <summary>
        /// ссылка на почтовые ресурсы
        /// </summary>
        [Required]
        [StringLength(255)]
        public string EmailUrl { get; set; }

        /// <summary>
        /// активность этой записи в БД
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
