using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class Heartland
{
    public int IDHeartland { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Hex> Hexes { get; set; } = new List<Hex>();

    public virtual ICollection<Kingdom> Kingdoms { get; set; } = new List<Kingdom>();
}
