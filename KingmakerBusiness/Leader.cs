﻿
using PF2e_Kingmaker_Bot.KingmakerBusiness;

public enum EnumLeaderRole
{
    None,
    Ruler,
    Warden,
    Viceroy,
    Treasurer,
    Magister,
    Emissary,
    General,
    Counselor
}

public class Leader
{
    public int IDLeader { get; set; }
    public int IDKingdom { get; set; }
    public int IDLeaderType { get; set; }
    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;
    public virtual LeaderType IDLeaderTypeNavigation { get; set; } = null!;
    public string Name { get; set; }
    public EnumLeaderRole Role { get; set; }
    public bool IsInvested { get; set; }
    public bool IsPlayerCharacter { get; set; }
    public bool IsVacant { get; set; }
    public bool IsIngratiated { get; set; }

    public Leader(string name, EnumLeaderRole role, bool invested, bool isPlayerCharacter = true, bool ingratiated = true)
    {
        if (name == null || name == "") throw new Exception("Name must not be empty.");

        if (role == EnumLeaderRole.None && invested) throw new Exception("A leader without a Role can't be Invested.");
        
        Name = name;
        Role = role;
        IsInvested = invested;
        IsPlayerCharacter = isPlayerCharacter;
        IsVacant = false;
        IsIngratiated = ingratiated;
    }     

    public static EnumAbilityScore KeyAbilityForRole(EnumLeaderRole role)
    {
        switch(role) 
        {
            case EnumLeaderRole.Counselor:
            case EnumLeaderRole.Magister:
                return EnumAbilityScore.Culture;
            case EnumLeaderRole.Treasurer:
            case EnumLeaderRole.Viceroy:
                return EnumAbilityScore.Economy;
            case EnumLeaderRole.Ruler:
            case EnumLeaderRole.Emissary:
                return EnumAbilityScore.Loyalty;
            case EnumLeaderRole.General:
            case EnumLeaderRole.Warden:
                return EnumAbilityScore.Stability;
            default: 
                throw new Exception("This leader's Role has no Key Ability.");
        }
    }

    public EnumAbilityScore KeyAbility()
    {
        return KeyAbilityForRole(Role);
    }

}