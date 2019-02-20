using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Entity.DAL;
using odec.Entity.DAL.Interop;
using odec.Framework.Extensions;
using odec.Framework.Logging;
using odec.Server.Model.Category;
using odec.Server.Model.User;
using odec.Server.Model.User.Filters;
using odec.User.DAL.Interop;

namespace odec.User.DAL
{
    public class UserSkillRepository : OrmEntityOperationsRepository<int, Category, DbContext>, 
        IUserSkillRepository<int, DbContext, Category, Server.Model.User.User, SkillFilter<int?>>, 
        IContextRepository<DbContext>
    {
        public UserSkillRepository()
        {
        }
        public UserSkillRepository(DbContext db)
        {
            Db = db;
        }
        public void SetConnection(string connection)
        {
        }

        public void SetContext(DbContext db)
        {
            Db = db;
        }

        public IEnumerable<Category> Get(SkillFilter<int?> filter)
        {
            try
            {
                var query = Db.Set<UserSkill>().AsQueryable();
                if (filter.UserId.HasValue)
                    query = query.Where(it => it.UserId == filter.UserId);

                if (!string.IsNullOrEmpty(filter.Code))
                    query = query.Where(it => it.Skill.Code.Equals(filter.Code));

                query = filter.Sord.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(filter.Sidx)
                : query.OrderBy(filter.Sidx);

                return query.Include(it => it.Skill).Select(it => it.Skill).Skip(filter.Rows * (filter.Page - 1))
                        .Take(filter.Rows)
                        .Distinct();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void AddUserSkill(Category skill, Server.Model.User.User user)
        {
            try
            {
                if (skill.Id == 0 && !string.IsNullOrEmpty(skill.Code))
                    if (Exists(skill, it => it.Code.Equals(skill.Code)))
                        skill = Db.Set<Category>().Single(it => it.Code.Equals(skill.Code));
                    else
                        Db.Set<Category>().Add(skill);
                var userSkill = new UserSkill
                {
                    UserId = user.Id,
                    SkillId = skill.Id
                };
                Db.Set<UserSkill>().Add(userSkill);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public IEnumerable<Category> GetUserSkills(Server.Model.User.User user)
        {
            return GetUserSkills(user.Id);
        }

        public IEnumerable<Category> GetUserSkills(int userId)
        {
            try
            {
                return Db.Set<UserSkill>().Include(it => it.Skill).Where(it => it.UserId == userId).Select(it => it.Skill);
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void RemoveUserSkill(Server.Model.User.User user, Category skill)
        {
            RemoveUserSkill(user.Id, skill.Id);
        }

        public void RemoveUserSkill(int userId, int skillId)
        {
            try
            {
                var uSkill = Db.Set<UserSkill>().SingleOrDefault(it => it.SkillId == skillId && it.UserId == userId);
                if (uSkill == null) return;
                Db.Set<UserSkill>().Remove(uSkill);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public void ClearUserSkills(Server.Model.User.User user)
        {
            ClearUserSkills(user.Id);
        }

        public void ClearUserSkills(int userId)
        {
            try
            {
                var victims = Db.Set<UserSkill>().Where(it => it.UserId == userId);
                if (!victims.Any()) return;
                Db.Set<UserSkill>().RemoveRange(victims);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
