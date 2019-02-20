using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Прокси объект контекста отношения
    /// </summary>
    /// <typeparam name="TRelationship">тип отношения</typeparam>
    public interface IRelationshipContext<TRelationship> where TRelationship : class
    {
        /// <summary>
        /// Таблица отношений
        /// </summary>
        DbSet<TRelationship> Relationships { get; set; }

    }
}
