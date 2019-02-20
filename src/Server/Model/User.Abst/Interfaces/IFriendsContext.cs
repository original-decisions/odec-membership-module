using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces
{
    /// <summary>
    /// Прокси объект контекста друзеЙ
    /// </summary>
    /// <typeparam name="TFriend">тип друга</typeparam>
    /// <typeparam name="TFriendGroup">тип группы друга</typeparam>
    public interface IFriendsContext<TFriend, TFriendGroup> where TFriend : class where TFriendGroup : class
    {
        /// <summary>
        /// таблица связи друзей
        /// </summary>
        DbSet<TFriend> Friends { get; set; }

        /// <summary>
        /// таблица связи групп друга
        /// </summary>
        DbSet<TFriendGroup> FriendGroups { get; set; }

    }
}
