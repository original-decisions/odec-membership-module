using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Categ = odec.Server.Model.Category.Category;
namespace odec.Server.Model.User
{
    public class UserSkill
    {
        //[Key,Column(Order = 1)]
        public int UserId { get; set; }
        //public User User { get; set; }
      //  [Key,Column(Order = 0)]
        public int SkillId { get; set; }

        public Categ Skill { get; set; }
    }
}
