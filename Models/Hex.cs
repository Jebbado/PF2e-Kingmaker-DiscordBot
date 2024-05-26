using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Hex
{
    public int IDHex { get; set; }

    public int IDKingdom { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int IDHeartland { get; set; }

    public int IDTerrainFeature { get; set; }

    public int? IDRessourceType { get; set; }

    public bool IsTerritory { get; set; }

    public virtual Heartland IDHeartlandNavigation { get; set; } = null!;

    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;

    public virtual RessourceType? IDRessourceTypeNavigation { get; set; }

    public virtual TerrainFeature IDTerrainFeatureNavigation { get; set; } = null!;

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();
}
