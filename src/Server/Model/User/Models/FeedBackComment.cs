using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using odec.Framework.Generic;

namespace odec.Server.Model.User
{
    /// <summary>
    /// серверный объект - комментарии обратной связи
    /// </summary>
    public class FeedBackComment:CommentTemplate<Int32,Int32,User>
    {
        /// <summary>
        /// рейтинг
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public Decimal Rate { get; set; }

    }
}