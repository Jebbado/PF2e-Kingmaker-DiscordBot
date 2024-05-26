using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class SkillTrainingType
{
    public int IDSkillTrainingType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SkillTraining> SkillTrainings { get; set; } = new List<SkillTraining>();
}
