using System;

namespace odec.CP.User.Membership.DAL.Interop
{
    public interface IOauthAuthentication<TUser, TKey> :
        IAuthenticate<TUser,TKey> 
        where TUser : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsOath(TUser user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        bool IsOath(TKey userKey);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        TUser SaveOathExtraData(TUser user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        TUser OAuthLogin(TUser user, bool persistCookie = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool HasLocalLogin(TUser user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        bool HasLocalLogin(TKey userKey);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool HasPassword(TUser user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        bool HasPassword(TKey userKey);
    }
}