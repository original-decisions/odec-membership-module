namespace odec.CP.User.Membership.DAL.Interop
{
    public interface IMembershipOptions
    {
        /// <summary>
        /// Should logins persist cookies
        /// </summary>
        bool PersistCookie { get; set; }
        /// <summary>
        /// Should we lock User out on failure
        /// </summary>
        bool LockoutOnFailure { get; set; }
                
        /// <summary>
        /// Authentication Session value
        /// </summary>
        int AuthSessionLifetime { get; set; }
    }
}