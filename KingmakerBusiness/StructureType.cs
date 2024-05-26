using System;
using System.Collections.Generic;

namespace PF2e_Kingmaker_Bot.KingmakerBusiness;

public partial class StructureType
{
    public int IDStructureType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Structure> Structures { get; set; } = new List<Structure>();
}
