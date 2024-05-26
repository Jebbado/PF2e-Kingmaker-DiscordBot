using System;
using System.Collections.Generic;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Models;

public partial class Kingdom
{
    public int IDKingdom { get; set; }

    public string Name { get; set; } = null!;

    public int? IDCharter { get; set; }

    public int? IDHeartland { get; set; }

    public int? IDGovernment { get; set; }

    public int CultureModifier { get; set; }

    public int EconomyModifier { get; set; }

    public int LoyaltyModifier { get; set; }

    public int StabilityModifier { get; set; }

    public int ExperiencePoints { get; set; }

    public int KingdomLevel { get; set; }

    public int PlayersLevel { get; set; }

    public int FamePoints { get; set; }

    public int? IDFameType { get; set; }

    public int UnrestPoints { get; set; }

    public int Food { get; set; }

    public int Lumber { get; set; }

    public int Stone { get; set; }

    public int Luxuries { get; set; }

    public int CorruptionScore { get; set; }

    public int CorruptionModifier { get; set; }

    public int CrimeScore { get; set; }

    public int CrimeModifier { get; set; }

    public int StrifeScore { get; set; }

    public int StrifeModifier { get; set; }

    public int DecayScore { get; set; }

    public int DecayModifier { get; set; }

    public int TurnNumber { get; set; }

    public bool IsAnarchy { get; set; }

    public bool IsTreasuryTapped { get; set; }

    public virtual ICollection<Hex> Hexes { get; set; } = new List<Hex>();

    public virtual Charter? IDCharterNavigation { get; set; }

    public virtual Government? IDGovernmentNavigation { get; set; }

    public virtual Heartland? IDHeartlandNavigation { get; set; }

    public virtual ICollection<Leader> Leaders { get; set; } = new List<Leader>();

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();

    public virtual ICollection<SkillTraining> SkillTrainings { get; set; } = new List<SkillTraining>();
}
