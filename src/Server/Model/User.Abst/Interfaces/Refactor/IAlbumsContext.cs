using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Прокси объект контекста альбома
    /// </summary>
    /// <typeparam name="TAlbum">тип альбома</typeparam>
    /// <typeparam name="TFotoRelation">тип отношение фотографии</typeparam>
    /// <typeparam name="TImage">тип изображения</typeparam>
    public interface IAlbumsContext<TAlbum, TFotoRelation, TImage>
        where TAlbum : class
        where TFotoRelation : class
        where TImage : class
    {
        /// <summary>
        /// таблица саязи альбомов
        /// </summary>
        DbSet<TAlbum> Albums { get; set; }
        /// <summary>
        /// таблица связей фотографий
        /// </summary>
        DbSet<TFotoRelation> FotoRelations { get; set; }
        /// <summary>
        /// Таблица фото
        /// </summary>
        DbSet<TImage> Fotos { get; set; }

    }
}
