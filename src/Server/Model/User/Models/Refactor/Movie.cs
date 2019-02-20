using odec.Framework.Generic;

namespace odec.Server.Model.User.Refactor
{
    /// <summary>
    /// серверный объект - видеозапись
    /// </summary>
    public class Movie:Glossary<int>
    {
        /// <summary>
        /// содержание видеоролика
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// расширение
        /// </summary>
        public string Extension { get; set; }
    }
}
