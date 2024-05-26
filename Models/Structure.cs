using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Structure
{
    public int IDStructure { get; set; }

    public int IDStructureType { get; set; }

    public int IDSettlement { get; set; }

    public virtual Settlement IDSettlementNavigation { get; set; } = null!;

    public virtual StructureType IDStructureTypeNavigation { get; set; } = null!;
}
