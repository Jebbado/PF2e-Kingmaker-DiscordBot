using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Leader
{
    public int IDLeader { get; set; }

    public int IDKingdom { get; set; }

    public int IDLeaderType { get; set; }

    public string Name { get; set; } = null!;

    public bool IsInvested { get; set; }

    public bool? IsPlayer { get; set; }

    public bool IsVacant { get; set; }

    public bool IsIngratiated { get; set; }

    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;

    public virtual LeaderType IDLeaderTypeNavigation { get; set; } = null!;
}
