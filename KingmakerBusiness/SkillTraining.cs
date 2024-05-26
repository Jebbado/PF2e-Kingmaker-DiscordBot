using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class SkillTraining
{
    public int IDSkillTraining { get; set; }

    public int IDKingdom { get; set; }

    public int IDSkillTrainingType { get; set; }

    public int IDSkill { get; set; }

    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;

    public virtual Skill IDSkillNavigation { get; set; } = null!;

    public virtual SkillTrainingType IDSkillTrainingTypeNavigation { get; set; } = null!;
}
