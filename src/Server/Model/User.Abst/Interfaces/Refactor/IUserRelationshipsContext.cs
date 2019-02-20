using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// cпользователя и отношений
    /// </summary>
    /// <typeparam name="TUserRelationship">тип отношения</typeparam>
    public interface IUserRelationshipsContext<TUserRelationship> where TUserRelationship : class
    {
        /// <summary>
        /// Таблица связи пользователя и отношений
        /// </summary>
        DbSet<TUserRelationship> UserRelationships { get; set; }
    }
}
