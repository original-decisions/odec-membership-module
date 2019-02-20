using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace odec.Server.Model.User.Refactor
{
    /// <summary>
    /// серверный объект - категория видеоролика
    /// </summary>
    public class MovieCategory
    {
        /// <summary>
        /// идентификатор видеоролика
        /// </summary>
        [Key,Column(Order = 0)]
        public int MovieId { get; set; }

        /// <summary>
        /// серверный объект - видеоролик 
        /// </summary>
        public Movie Movie { get; set; }
        
        /// <summary>
        /// идентификатор категории
        /// </summary>
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }

        /// <summary>
        /// серверный объект - категория
        /// </summary>
        public Category.Category Category { get; set; }

    }
}
