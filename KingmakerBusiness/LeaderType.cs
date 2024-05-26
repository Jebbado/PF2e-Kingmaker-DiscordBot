using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class LeaderType
{
    public int IDLeaderType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Leader> Leaders { get; set; } = new List<Leader>();
}
