using odec.Framework.Generic;

namespace odec.Server.Model.User.Filters
{
    public class SkillFilter<TKey>: FilterBase
    {
        public string Code { get; set; }
        public TKey UserId { get; set; }
    }
}
