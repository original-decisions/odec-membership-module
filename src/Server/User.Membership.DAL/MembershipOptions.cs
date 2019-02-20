using odec.CP.User.Membership.DAL.Interop;
using odec.Server.Model.User.Abst.Interfaces;

namespace odec.CP.User.Membership.DAL
{
    public class MembershipOptions:IMembershipOptions
    {
        public bool PersistCookie { get; set; }
        public bool LockoutOnFailure { get; set; }
        public int AuthSessionLifetime { get; set; }
    }
}
