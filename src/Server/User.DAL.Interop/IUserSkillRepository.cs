using System.Collections.Generic;
using odec.Entity.DAL.Interop;

namespace odec.User.DAL.Interop
{
    public interface IUserSkillRepository<in TKey,TContext,TSkill, in TUser, in TSkillFilter>:IContextRepository<TContext>,IEntityOperations<TKey,TSkill> where TKey : struct
    {
        IEnumerable<TSkill> Get(TSkillFilter filter);
        void AddUserSkill(TSkill skill,TUser user);
        IEnumerable<TSkill> GetUserSkills(TUser user);
        IEnumerable<TSkill> GetUserSkills(TKey userId);
        void RemoveUserSkill(TUser user,TSkill skill);
        void RemoveUserSkill(TKey userId,TKey skillId);
        void ClearUserSkills(TUser user);
        void ClearUserSkills(TKey userId);

    }
}