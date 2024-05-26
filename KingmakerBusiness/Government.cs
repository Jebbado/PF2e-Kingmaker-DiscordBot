using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class Government
{
    public int IDGovernment { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Kingdom> Kingdoms { get; set; } = new List<Kingdom>();
}
