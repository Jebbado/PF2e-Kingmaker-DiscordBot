using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Settlement
{
    public int IDSettlement { get; set; }

    public int IDKingdom { get; set; }

    public int IDSettlementType { get; set; }

    public bool IsCapital { get; set; }

    public int IDHex { get; set; }

    public virtual Hex IDHexNavigation { get; set; } = null!;

    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;

    public virtual SettlementType IDSettlementTypeNavigation { get; set; } = null!;

    public virtual ICollection<Structure> Structures { get; set; } = new List<Structure>();
}
