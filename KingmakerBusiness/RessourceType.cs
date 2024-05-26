using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class RessourceType
{
    public int IDRessourceType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Hex> Hexes { get; set; } = new List<Hex>();
}
