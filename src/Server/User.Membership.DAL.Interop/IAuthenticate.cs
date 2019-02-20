using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace odec.CP.User.Membership.DAL.Interop
{
    public interface IAuthenticate<TUser, TKey>  
        where TUser : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Options to operate with membership
        /// </summary>
        IMembershipOptions Options { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string xsrfKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        void SetUserManager(UserManager<TUser> userManager);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInManager"></param>
        void SetSignInManager(SignInManager<TUser> signInManager);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        bool LogIn(TUser user, string password, bool persistCookie = true);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        bool LogIn(string userName, string password, bool persistCookie = true);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        Task<bool> LogInAsync(string userName, string password, bool persistCookie = true);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IdentityResult RegisterUser(TUser user,string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterUserAsync(TUser user,string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IdentityResult CreateAndLogin(TUser user,string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateAndLoginAsync(TUser user,string password);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool LogOff();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        Task<bool> LogInAsync(TUser user,string password, bool persistCookie = true);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> LogOffAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(TUser user,string oldPassword,string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(TKey userKey, string oldPassword, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        IdentityResult ChangePasswordIResult(TUser user, string oldPassword, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        IdentityResult ChangePasswordIResult(TKey userKey, string oldPassword, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordIResultAsync(TUser user, string oldPassword, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordIResultAsync(TKey userKey, string oldPassword, string newPassword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        //TODO:Remove
        [Obsolete]
        TUser LoginAndSave(TUser user,string password, bool persistCookie = true);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        //TODO: Move to another interface
        [Obsolete]
        IList<TUser> GetAllUsersInRole(string roleName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
         //TODO: Move to another interface
        [Obsolete]
        bool IsInRole(string userName, string roleName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
         //TODO: Move to another interface
        [Obsolete]
        bool IsInRole(TUser user,string roleName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool IsAuthenticated(string username);
        bool IsAuthenticated(TUser user);
        bool IsAuthenticated(TKey userKey);
    }
}
