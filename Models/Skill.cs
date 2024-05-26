using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Skill
{
    public int IDSkill { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SkillTraining> SkillTrainings { get; set; } = new List<SkillTraining>();
}
