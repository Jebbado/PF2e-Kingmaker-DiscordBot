using System.Data;
using System.Xml.Linq;

public enum EnumCharter
{
    None,
    Conquest,
    Expansion,
    Exploration,
    Grant,
    Open
}

public enum EnumHeartland
{
    None,
    Forest,
    Swamp,
    Hill,
    Plain,
    Lake,
    River,
    Mountain,
    Ruin
}

public enum EnumGovernment
{
    None,
    Despotism,
    Feudalism,
    Oligarchy,
    Republic,
    Thaumocracy,
    Yeomanry
}

public enum EnumRuinCategory
{
    Corruption,
    Crime,
    Strife,
    Decay
}

public enum EnumFameAspiration
{
    None,
    Fame,
    Infamy
}

public enum EnumXPMilestone
{
    FirstLandmark,  //40XP
    FirstRefuge,    //40XP
    FirstVillage,   //40XP
    KingdomSize10,  //40XP
    FirstDiplomatic,//60XP
    FirstTown,      //60XP
    AllLeaders,     //60XP
    KingdomSize25,  //60XP
    FirstTrade,     //80XP
    FirstCity,      //80XP
    KingdomSize50,  //80XP
    Spend100RP,     //80XP
    FirstMetropolis,//120XP
    KingdomSize100  //120XP
}

public enum EnumKingdomSize
{
    Territory,
    Province,
    State,
    Country,
    Dominion
}

public class Kingdom
{
    private string KingdomName;    

    private EnumCharter Charter;
    private EnumHeartland Heartland;
    private EnumGovernment Government;

    private int ExperiencePoints;
    public int KingdomLevel { get; private set; }
    private int PlayersLevel;

    public Dictionary<EnumAbilityScore, Ability> Abilities { get; }

    private int FamePoints;
    private EnumFameAspiration FameAspiration = EnumFameAspiration.None;
    private int UnrestPoints;

    private int RessourcePoints;
    private Dictionary<EnumCommodity, Commodity> Commodities;

    public Dictionary<EnumLeaderRole, Leader> Leaders { get; }

    private List<Settlement> Settlements;

    private Dictionary<string, Hex> WorldMap = new Dictionary<string, Hex>();
    
    private Dictionary<EnumRuinCategory, int> RuinScore;
    private Dictionary<EnumRuinCategory, int> RuinThreshold;
    public Dictionary<EnumRuinCategory, int> RuinItemPenalty { get; }
    public Dictionary<EnumSkills, EnumSkillTraining> SkillTrainings { get; }

    public List<EnumFeats> Feats { get; }
    public List<EnumXPMilestone> Milestones { get; }

    public int ThisTurn = 0;
    public Dictionary<int, Turn> Turns = new Dictionary<int, Turn>();

    private bool InAnarchy = false;
    private bool TreasuryTapped = false;

    public Kingdom(string name)
    {
        KingdomName = name;

        Charter = EnumCharter.None;
        Heartland = EnumHeartland.None;
        Government = EnumGovernment.None;

        KingdomLevel = 1;

        UnrestPoints = 0;

        Abilities = new Dictionary<EnumAbilityScore, Ability>();
        Abilities[EnumAbilityScore.Culture] = new Ability(EnumAbilityScore.Culture);
        Abilities[EnumAbilityScore.Economy] = new Ability(EnumAbilityScore.Economy);
        Abilities[EnumAbilityScore.Loyalty] = new Ability(EnumAbilityScore.Loyalty);
        Abilities[EnumAbilityScore.Stability] = new Ability(EnumAbilityScore.Stability);

        Leaders = new Dictionary<EnumLeaderRole, Leader>();

        Settlements = new List<Settlement>();     

        SkillTrainings = new Dictionary<EnumSkills, EnumSkillTraining>();

        RuinThreshold = new Dictionary<EnumRuinCategory, int>();
        RuinScore = new Dictionary<EnumRuinCategory, int>();
        RuinItemPenalty = new Dictionary<EnumRuinCategory, int>();

        Feats = new List<EnumFeats>();
        Milestones = new List<EnumXPMilestone>();

        Commodities = new Dictionary<EnumCommodity, Commodity>();
        Commodities[EnumCommodity.Food] = new Commodity(EnumCommodity.Food);
        Commodities[EnumCommodity.Lumber] = new Commodity(EnumCommodity.Lumber);
        Commodities[EnumCommodity.Stone] = new Commodity(EnumCommodity.Stone);
        Commodities[EnumCommodity.Ore] = new Commodity(EnumCommodity.Ore);
        Commodities[EnumCommodity.Luxuries] = new Commodity(EnumCommodity.Luxuries);

        Turns[ThisTurn] = new Turn(ThisTurn, EnumPhase.Creation, EnumStep.ChooseCharter);
    }  
    
    public Dictionary<string, Hex> Territory()
    {
        Dictionary<string, Hex> Territory = new Dictionary<string, Hex>();
        foreach (Hex forHex in WorldMap.Values)
        {
            if (forHex.InTerritory)
            {
                Territory.Add(forHex.Key(), forHex);
            }
        }
        return Territory;
    }

    public EnumKingdomSize KingdomSize()
    {
        if      (1 <= Territory().Count || Territory().Count <= 9) { return EnumKingdomSize.Territory; }
        else if (10 < Territory().Count || Territory().Count < 24) { return EnumKingdomSize.Province; }
        else if (25 < Territory().Count || Territory().Count < 49) { return EnumKingdomSize.State; }
        else if (50 < Territory().Count || Territory().Count < 99) { return EnumKingdomSize.Country; }
        else if (Territory().Count > 100)                          { return EnumKingdomSize.Dominion; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    public void AssignCharter(EnumCharter charter)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You can't change your Charter after Kingdom creation."); }
        if (CurrentTurn().Step != EnumStep.ChooseCharter) { throw new Exception("You're not at the 'Select a Charter' step right now."); }

        if (Charter != EnumCharter.None)
        {
            throw new Exception("Charter already chosen. You can't change your charter.");
        }

        if (charter == EnumCharter.None)
        {
            throw new Exception("You must choose a valid charter.");
        }

        Charter = charter;

        CurrentTurn().Step = EnumStep.ChooseHeartland;
    }

    public void AssignHeartland(EnumHeartland heartland)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You can't change your Heartland after Kingdom creation."); }
        if (CurrentTurn().Step != EnumStep.ChooseHeartland) { throw new Exception("You're not at the 'Choose a Heartland' step right now."); }

        if (Heartland != EnumHeartland.None)
        {
            throw new Exception("Heartland already chosen. You can't change your heartland.");
        }

        if (heartland == EnumHeartland.None)
        {
            throw new Exception("You must choose a valid heartland.");
        }

        Heartland = heartland;
        CurrentTurn().Step = EnumStep.ChooseGovernment;
    }

    public void AssignGovernment(EnumGovernment government)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You can't change Government after Kingdom creation."); }
        if (CurrentTurn().Step != EnumStep.ChooseGovernment) { throw new Exception("You're not at the 'Choose a Government' step right now."); }

        if (Government != EnumGovernment.None)
        {
            throw new Exception("Government already chosen. You can't change your government.");
        }

        if (government == EnumGovernment.None)
        {
            throw new Exception("You must choose a valid government.");
        }

        Government = government;

        switch(Government) 
        {
            case EnumGovernment.Despotism:
                TrainSkill(EnumSkills.Intrigue);
                TrainSkill(EnumSkills.Warfare);
                Feats.Add(EnumFeats.CrushDissent);
                break;
            case EnumGovernment.Feudalism:
                TrainSkill(EnumSkills.Defense);
                TrainSkill(EnumSkills.Trade);
                Feats.Add(EnumFeats.FortifiedFiefs);
                break;
            case EnumGovernment.Oligarchy:
                TrainSkill(EnumSkills.Arts);
                TrainSkill(EnumSkills.Industry);
                Feats.Add(EnumFeats.InsiderTrading);
                break;
            case EnumGovernment.Republic:
                TrainSkill(EnumSkills.Engineering);
                TrainSkill(EnumSkills.Politics);
                Feats.Add(EnumFeats.PullTogether);
                break;
            case EnumGovernment.Thaumocracy:
                TrainSkill(EnumSkills.Folklore);
                TrainSkill(EnumSkills.Magic);
                Feats.Add(EnumFeats.PracticalMagic);
                break;
            case EnumGovernment.Yeomanry:
                TrainSkill(EnumSkills.Agriculture);
                TrainSkill(EnumSkills.Wilderness);
                Feats.Add(EnumFeats.MuddleThrough);
                break;
        }
        CurrentTurn().Step = EnumStep.FinalizeAbilityScores;
    }

    public void AssignFirstLeaderSkill(EnumSkills addedSkill)
    {
        if(SkillTrainings.Count > 2)
        {
            throw new Exception("First leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignSecondLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 3)
        {
            throw new Exception("Second leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignThirdLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 4)
        {
            throw new Exception("Third leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignFourthLeaderSkill(EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 5)
        {
            throw new Exception("Fourth leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void FinalizeAbilityScore(EnumAbilityScore charterChoice, EnumAbilityScore governmentChoice,
                                        EnumAbilityScore freeChoice1, EnumAbilityScore freeChoice2)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You can't change Ability Scores after Kingdom creation."); }
        if (CurrentTurn().Step != EnumStep.FinalizeAbilityScores) { throw new Exception("You're not at the 'Finalize Ability Score' step right now."); }
        
        switch (Charter)
        {
            case EnumCharter.Conquest:
                if (charterChoice == EnumAbilityScore.Stability || charterChoice == EnumAbilityScore.Economy)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility(true);
                break;
            case EnumCharter.Expansion:
                if (charterChoice == EnumAbilityScore.Loyalty || charterChoice == EnumAbilityScore.Economy)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility(true);
                break;
            case EnumCharter.Exploration:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Loyalty)
                {
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility(true);
                break;
            case EnumCharter.Grant:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Stability)
                {                
                    Abilities[charterChoice].BoostAbility();
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility(true);
                break;
            case EnumCharter.Open:
                Abilities[charterChoice].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom charter is not valid.");
        }

        switch (Heartland)
        {
            case EnumHeartland.Forest:
            case EnumHeartland.Swamp:
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                break;
            case EnumHeartland.Hill:
            case EnumHeartland.Plain:
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            case EnumHeartland.Lake:
            case EnumHeartland.River:
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumHeartland.Mountain:
            case EnumHeartland.Ruin:
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom heartland is not valid.");
        }

        switch (Government)
        {
            case EnumGovernment.Despotism:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Loyalty)
                {                
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Feudalism:
                if (governmentChoice != EnumAbilityScore.Economy && governmentChoice != EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                break;
            case EnumGovernment.Oligarchy:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Stability)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Republic:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            case EnumGovernment.Thaumocracy:
                if (governmentChoice != EnumAbilityScore.Stability && governmentChoice != EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[EnumAbilityScore.Stability].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Economy].BoostAbility();
                break;
            case EnumGovernment.Yeomanry:
                if (governmentChoice != EnumAbilityScore.Stability && governmentChoice != EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                Abilities[governmentChoice].BoostAbility();
                Abilities[EnumAbilityScore.Culture].BoostAbility();
                Abilities[EnumAbilityScore.Loyalty].BoostAbility();
                break;
            default:
                throw new Exception("The Kingdom government is not valid.");
        }

        if (freeChoice1 == freeChoice2)
        {
            throw new Exception("You must choose two different additional ability boosts.");
        }
        Abilities[freeChoice1].BoostAbility();
        Abilities[freeChoice2].BoostAbility();

        CurrentTurn().Step = EnumStep.ChooseLeaders;
    }

    public void ChooseFame(EnumFameAspiration fame)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (CurrentTurn().Step != EnumStep.ChooseFameInfamy) { throw new Exception("You're not at the 'Fame or Infamy ?' step."); }

        if (FameAspiration != EnumFameAspiration.None) { throw new Exception("You can't change your Fame Aspiration after Kingdom Creation."); }
        if (fame == EnumFameAspiration.None) { throw new Exception("You must choose a valid Fame Aspiration."); }

        FameAspiration = fame;

        EndTurn();
    }

    public void AddSettlement(string settlementName, int posX = 0, int posY = 0, bool isCapital = false)
    {
        if(isCapital)
        {
            Hex SettlementHex = new Hex(posX, posY, Heartland);
            Territory()[SettlementHex.Key()] = SettlementHex;
        }

        if( ! Milestones.Contains(EnumXPMilestone.FirstVillage) && Settlements.Count == 0)
        {
            Milestones.Add(EnumXPMilestone.FirstVillage);
            AddXP(40);
        }

        Settlements.Add(new Settlement(this, settlementName, Territory()[posX+":"+posY], isCapital));        
    }

    public void CreateCapital(string name, EnumStructure initialStructure = EnumStructure.None)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (CurrentTurn().Step != EnumStep.FirstVillage) { throw new Exception("You're not at the 'Forst Village' step."); }

        AddSettlement(name, 0, 0, true);
        if (initialStructure != EnumStructure.None)
        {
            CapitalSettlement().PlaceStructure(initialStructure, 1);
        }

        CurrentTurn().Step = EnumStep.ChooseFameInfamy;
    }

    public void AddLeader(string name, EnumLeaderRole role, bool invested, bool isPC)
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You can't add Leaders after Kingdom creation. You should use the 'New Leadership' Activity."); }
        if (CurrentTurn().Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose a Government' step right now."); }        
        
        if (Leaders.ContainsKey(role)) throw new Exception("There is already a leader in this Role.");

        if (invested && InvestedLeadersCount() >= 4) throw new Exception("There are already 4 invested Leaders.");

        Leaders[role] = new Leader(name, role, invested, isPC);
    }

    public void EndLeaderStep()
    {
        if (CurrentTurn().Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (CurrentTurn().Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose Leaders' step."); }

        if (InvestedLeadersCount() < 4) { throw new Exception("You must have 4 Invested Leaders before ending"); }

        CurrentTurn().Step = EnumStep.FirstVillage;
    }

    public void setPlayersLevel(int level)
    { 
        this.PlayersLevel = level; 
    }

    public void RemoveLeader(EnumLeaderRole removedLeader) 
    { 
        Leaders.Remove(removedLeader); 
    }

    public int ControlDC()
    {
        int returnedDC;

        switch (KingdomLevel) 
        {
            case 1: returnedDC = 14; break;
            case 2: returnedDC = 15; break;
            case 3: returnedDC = 16; break;
            case 4: returnedDC = 18; break;
            case 5: returnedDC = 20; break;
            case 6: returnedDC = 22; break;
            case 7: returnedDC = 23; break;
            case 8: returnedDC = 24; break;
            case 9: returnedDC = 26; break;
            case 10: returnedDC = 27; break;
            case 11: returnedDC = 28; break;
            case 12: returnedDC = 30; break;
            case 13: returnedDC = 31; break;
            case 14: returnedDC = 32; break;
            case 15: returnedDC = 34; break;
            case 16: returnedDC = 35; break;
            case 17: returnedDC = 36; break;
            case 18: returnedDC = 38; break;
            case 19: returnedDC = 39; break;
            case 20: returnedDC = 40; break;
            default: throw new ArgumentOutOfRangeException("The Kingdom Level must be between 1 and 20.");
        }

        returnedDC += KingdomSizeControlDCModifier();

        if (Leaders[EnumLeaderRole.Ruler].IsVacant && ! CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Ruler))
        {
            returnedDC += 2;
        }

        return returnedDC;
    }

    public int RessourceDiceSize()
    {
        switch(KingdomSize())
        {
            case EnumKingdomSize.Territory: return 4;
            case EnumKingdomSize.Province: return 6;
            case EnumKingdomSize.State: return 8;
            case EnumKingdomSize.Country: return 10;
            case EnumKingdomSize.Dominion: return 12;
            default: throw new ArgumentOutOfRangeException();
        }
        
    }

    public int KingdomSizeControlDCModifier()
    {
        switch (KingdomSize())
        {
            case EnumKingdomSize.Territory: return 0;
            case EnumKingdomSize.Province: return 1;
            case EnumKingdomSize.State: return 2;
            case EnumKingdomSize.Country: return 3;
            case EnumKingdomSize.Dominion: return 4;
            default: throw new ArgumentOutOfRangeException();
        }       
    }  

    public int RessourceDiceAmount()
    {
        int amount = KingdomLevel + 4;

        if (Feats.Contains(EnumFeats.InsiderTrading))
        { amount++; }

        //TODO : Any reduction goes here.

        return Math.Max(amount, 0);
    }

    public int Consumption()
    {
        int totalConsumption = 0;

        foreach (Settlement settlement in Settlements)
        {
            switch(settlement.SettlementType)
            {
                case EnumSettlementType.Village:
                    totalConsumption += 1;
                    break;
                case EnumSettlementType.Town:
                    totalConsumption += 2;
                    break;
                case EnumSettlementType.City:
                    totalConsumption += 4;
                    break;
                case EnumSettlementType.Metropolis:
                    totalConsumption += 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("The consumption can't be calculated because this settlement type doesn't exist.");
            }            
        }
        
        foreach (Hex hex in Territory().Values)
        {
            if(hex.TerrainFeature == EnumTerrainFeature.Farmland) //TODO : And is influenced
            {
                totalConsumption--;
            }
        }

        return Math.Max(totalConsumption, 0);
    }

    public void EarnFame()
    {
        int FameThreshold = 3;

        if(Feats.Contains(EnumFeats.EnvyOfTheWorld))
        {
            FameThreshold++;
        }

        if (FamePoints >= FameThreshold)
        {
            return;
        }

        FamePoints += 1;        
    }

    public EnumCheckResult KingdomCheck(int DC, int modifier = 0) 
    {
        EnumCheckResult returnedResult = Check.MakeCheck(DC, modifier);

        if (InAnarchy)
        {
            returnedResult = Check.WorsenCheckResult(returnedResult);
        }

        if(returnedResult == EnumCheckResult.CritSuccess) { EarnFame(); }

        return returnedResult;
    }    
    
    public void TrainSkill(EnumSkills enumSkill, EnumSkillTraining skillTraining = EnumSkillTraining.Trained)
    {
        
        if(SkillTrainings.ContainsKey(enumSkill) && SkillTrainings[enumSkill] == skillTraining)
        { 
            throw new Exception("This skill is already at ." + skillTraining.ToString());
        }

        SkillTrainings[enumSkill] = skillTraining;
    }

    public bool SkillHasStatusBonusFromInvestedLeader(EnumSkills enumSkill)
    {
        foreach (Leader forLeader in Leaders.Values)
        {
            if(forLeader.IsInvested && forLeader.KeyAbility() == Skill.SkillList()[enumSkill].KeyAbility)
            {
                return true;
            }
        }

        return false;
    }

    public int UnrestStatusPenalty()
    {
        if (UnrestPoints < 0) throw new Exception("Unrest score can't be negative.");

        if(UnrestPoints >= 15)
        {
            return 4;
        }
        if (UnrestPoints >= 10)
        {
            return 3;
        }
        if (UnrestPoints >= 5)
        {
            return 2;
        }
        if (UnrestPoints >= 1)
        {
            return 1;
        }
        return 0;
    }

    public void ReduceUnrest(int amount)
    {
        UnrestPoints = Math.Max(UnrestPoints - amount, 0);
    }

    public void AddRuin(int ruinAmount, EnumRuinCategory ruinCategory)
    {
        RuinScore[ruinCategory] += ruinAmount;
        if(RuinScore[ruinCategory] > RuinThreshold[ruinCategory])
        {
            RuinScore[ruinCategory] -= RuinThreshold[ruinCategory];
            RuinItemPenalty[ruinCategory] += 1;
        }
    }

    public void RemoveRuin(int ruinAmount, EnumRuinCategory ruinCategory)
    {
        if (RuinScore[ruinCategory] == 0 && RuinItemPenalty[ruinCategory] > 0)
        {
            if (Check.MakeFlatCheck(16))
            {
                RuinItemPenalty[ruinCategory] -= 1;
            }
        }
        else
        {
            RuinScore[ruinCategory] = Math.Max(RuinScore[ruinCategory] - ruinAmount, 0);
        }
    }

    public static EnumRuinCategory RuinCategoryByAbility(EnumAbilityScore enumAbility)
    {
        switch(enumAbility)
        {
            case EnumAbilityScore.Culture:
                return EnumRuinCategory.Corruption;
            case EnumAbilityScore.Economy:
                return EnumRuinCategory.Crime;
            case EnumAbilityScore.Loyalty:
                return EnumRuinCategory.Strife;
            case EnumAbilityScore.Stability:
                return EnumRuinCategory.Decay;
            default: throw new NotImplementedException("This ability score doesn't exist.");
        }
    }

    public void AddXP(int xpAmount)
    {
        ExperiencePoints += xpAmount;
    }

    public void LevelUp()
    {
        if (ExperiencePoints < 1000)
        {
            return;
        }

        if(KingdomLevel >= PlayersLevel)
        {
            return;
        }

        ExperiencePoints -= 1000;
        KingdomLevel += 1;

        switch(KingdomLevel)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                Feats.Add(EnumFeats.ExpansionExpert);
                Feats.Add(EnumFeats.FineLiving);
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                Feats.Add(EnumFeats.ExperiencedLeadership);
                break;
            case 9:
                Feats.Add(EnumFeats.ExpansionExpertPlus);
                break;
            case 10:
                Feats.Add(EnumFeats.LifeOfLuxury);
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                Feats.Add(EnumFeats.ExperiencedLeadershipPlus);
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                Feats.Add(EnumFeats.EnvyOfTheWorld);
                break;
            case 21:
                KingdomLevel = 20;
                throw new NotSupportedException("Level over 20 is not supported. Kingdom has been put back to 20.");
        }
                
    }

    public int InvestedLeadersCount() 
    {
        int amount = 0;
        foreach (Leader forLeader in Leaders.Values)
        {
            if(forLeader.IsInvested)
            {
                amount++;
            }
        }
        return amount; 
    }

    public Settlement CapitalSettlement()
    {
        foreach (Settlement forSettlement in Settlements)
        {
            if(forSettlement.IsCapital)
            {
                return forSettlement;
            }
        }

        throw new NotSupportedException("There is no capital. Something went wrong.");
    }

    public void ResetFame()
    {
        FamePoints = 1;
    }

    public void NewLeadership(EnumSkills skill, EnumLeaderRole role, string name, bool invested, bool isPC)
    {
        
        if (CurrentTurn().Step != EnumStep.AssignLeadership && Leaders[role].IsVacant) 
        {   
            throw new Exception("You can only attempt 'New Leadership' in the 'Asssign Leadership' step, unless there is an empty role.");
        }

        if (invested && InvestedLeadersCount() >= 4) throw new Exception("There are already 4 invested Leaders.");

        if(LastTurn().RejectedNewLeaders.Contains(role)) 
        {
            CurrentTurn().RejectedNewLeadersRetried.Add(role);
        }

        List<Bonus> bonus = new List<Bonus>();       
        EnumActivity chosenActivity;
        switch(skill)
        {
            case EnumSkills.Intrigue:
                if(role == EnumLeaderRole.Emissary || role == EnumLeaderRole.Treasurer) 
                { 
                    bonus.Add(new Bonus(EnumBonusType.CircumstanceBonus, 2)); 
                }
                chosenActivity = EnumActivity.NewLeadershipIntrigue; 
                break;
            case EnumSkills.Politics:
                if (role == EnumLeaderRole.Counselor || role == EnumLeaderRole.Ruler)
                {
                    bonus.Add(new Bonus(EnumBonusType.CircumstanceBonus, 2));
                }
                chosenActivity = EnumActivity.NewLeadershipPolitics; break;
            case EnumSkills.Statecraft:
                if (role == EnumLeaderRole.Magister || role == EnumLeaderRole.Viceroy)
                {
                    bonus.Add(new Bonus(EnumBonusType.CircumstanceBonus, 2));
                }
                chosenActivity = EnumActivity.NewLeadershipStatecraft; break;
            case EnumSkills.Warfare:
                if (role == EnumLeaderRole.General || role == EnumLeaderRole.Warden)
                {
                    bonus.Add(new Bonus(EnumBonusType.CircumstanceBonus, 2));
                }
                chosenActivity = EnumActivity.NewLeadershipWarfare; break;
            default : throw new Exception("You can't attempt 'New Leadership' with this skill.");
        }

        if (role == EnumLeaderRole.Ruler)
        {
            bonus.Add(new Bonus(EnumBonusType.CircumstancePenalty, 4));
        }

        EnumCheckResult result = UseActivity(chosenActivity, bonus);
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                Leaders[role] = new Leader(name, role, invested, isPC, true);
                CurrentTurn().LovedNewLeaders.Add(role);
                break;
            case EnumCheckResult.Success:
                Leaders[role] = new Leader(name, role, invested, isPC, true);
                break;
            case EnumCheckResult.Failure:
                Leaders[role] = new Leader(name, role, invested, isPC, false);
                break;
            case EnumCheckResult.CritFailure:
                CurrentTurn().RejectedNewLeaders.Add(role);
                Leaders[role].IsVacant = true;
                UnrestPoints++;
                break;
            default: throw new Exception("Unknown check result.");
        }
        if(result != EnumCheckResult.CritSuccess && role == EnumLeaderRole.Ruler)
        {
            UnrestPoints++;
        }
    }

    public void MakeLeaderVacant(EnumLeaderRole role, bool sacrificeActivity)
    {
        if (CurrentTurn().Phase != EnumPhase.Upkeep) throw new Exception("You're not in the 'Upkeep' phase.");
        if (CurrentTurn().Step != EnumStep.AssignLeadership) throw new Exception("You're not at the 'Assign Leadership Roles' step.");

        //This means players decided 'Upkeep Step 1 Assign Leadership' is finished
        if (role == EnumLeaderRole.None)
        {
            foreach(EnumLeaderRole forRole in LastTurn().RejectedNewLeaders)
            {
                if( ! CurrentTurn().RejectedNewLeadersRetried.Contains(forRole))
                {
                    throw new Exception("You must try at least once to reassign a new " + forRole.ToString() + 
                                        " since you critically failed a 'New Leadership' activity for this role last Turn.");
                }
            }

            CurrentTurn().Step = EnumStep.AdjustUnrest;
            AdjustUnrest();
            return;
        }

        Leaders[role].IsVacant = true;

        if (sacrificeActivity)
        {
            CurrentTurn().LeadersGivingUpActivity.Add(role);
        }
    }

    public Turn CurrentTurn() 
    {
        return Turns[ThisTurn];
    }

    public Turn LastTurn()
    {
        return Turns[ThisTurn - 1];
    }

    public void EndTurn()
    {
        if(IsGameOver())
        {
            CurrentTurn().Phase = EnumPhase.GameOver;
            return;
        }

        if(ExperiencePoints >= 1000 && ! CurrentTurn().LeveledUp)
        {
            LevelUp();
        }
        else
        {
            if(LeadersNotAllIngratiated() && ! CurrentTurn().IngratiateEnded)
            { 
                CurrentTurn().Pause(EnumPausedReason.Ingratiate);
                return;                
            }
            ThisTurn++;
            Turns[ThisTurn] = new Turn(ThisTurn);
            ResetFame();
        }
    }

    public void Ingratiate(EnumLeaderRole role, EnumSkills skill)
    {
        if (CurrentTurn().PausedReason != EnumPausedReason.Ingratiate) throw new Exception("You can only ingratiate a leader at the end of the turn.");
        if (Leaders[role].IsIngratiated) throw new Exception("This leader is already ingratiated.");        
        if (CurrentTurn().NewLeadersIngratiateTried.Contains(role)) throw new Exception("You already tried to ingratiate this leader this turn.");

        //Players decided not to try to ingratiate any other Leader
        if (role == EnumLeaderRole.None)
        {
            CurrentTurn().IngratiateEnded = true;
            CurrentTurn().Unpause();
            EndTurn();
        }

        EnumActivity activity;
        switch(skill)
        {
            case EnumSkills.Intrigue:
                activity = EnumActivity.IngratiateIntrigue; break;
            case EnumSkills.Politics:
                activity = EnumActivity.IngratiatePolitics; break;
            case EnumSkills.Statecraft:
                activity = EnumActivity.IngratiateStatecraft; break;
            case EnumSkills.Warfare:
                activity = EnumActivity.IngratiateWarfare; break;
            default : throw new Exception("You must use a Loyalty-based skill to ingratiate a leader.");
        }
        
        EnumCheckResult result = UseActivity(activity);
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                Leaders[role].IsIngratiated = true;
                break;
            case EnumCheckResult.Success:
                Leaders[role].IsIngratiated = true;
                break;
            case EnumCheckResult.Failure:
                break;
            case EnumCheckResult.CritFailure:
                CurrentTurn().RejectedNewLeaders.Add(role);
                Leaders[role].IsVacant = true;
                UnrestPoints++;
                break;
            default: throw new Exception("Unknown check result.");
        }
    }

    public bool LeadersNotAllIngratiated()
    {
        foreach (Leader forLeader in Leaders.Values)
        {
            if (!forLeader.IsIngratiated)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsGameOver()
    {
        if(CurrentTurn().Phase == EnumPhase.GameOver)
        {
            return true;
        }

        if(LastTurn().WentWithoutHex && ! LastTurn().CapturedHex )
        {
            if(CurrentTurn().CapturedHex)
            {
                foreach(Hex forHex in Territory().Values)
                {
                    if(forHex.TerrainFeature == EnumTerrainFeature.Settlement)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public EnumCheckResult UseActivity(EnumActivity usedActivityParam, List<Bonus>? paramBonusList = null, int DCModifier = 0, EnumLeaderRole role = EnumLeaderRole.None)
    {       
        Activity usedActivity = Activity.ActivityList()[usedActivityParam];
        Skill usedSkill = Skill.SkillList()[usedActivity.RequiredSkill];

        if (InAnarchy)
        {
            if (usedActivity.ActivityName != EnumActivity.QuellUnrestArts
            && usedActivity.ActivityName  != EnumActivity.QuellUnrestFolklore
            && usedActivity.ActivityName  != EnumActivity.QuellUnrestIntrigue
            && usedActivity.ActivityName  != EnumActivity.QuellUnrestMagic
            && usedActivity.ActivityName  != EnumActivity.QuellUnrestPolitics
            && usedActivity.ActivityName  != EnumActivity.QuellUnrestWarfare)
            {
                throw new Exception("You can only do 'Quell Unrest' while in Anarchy.");
            }
        }

        if ((usedActivity.Phase != CurrentTurn().Phase || usedActivity.Step != CurrentTurn().Step) && usedActivity.Phase != EnumPhase.None && usedActivity.Step != EnumStep.None)
        {
            throw new Exception("This activity must be used during " + usedActivity.Phase + "Phase and " + usedActivity.Step + " Step.");
        }

        if (usedActivity.Step == EnumStep.Leadership)
        {
            if(role == EnumLeaderRole.None)
            {
                throw new ArgumentException("The leader role attempting the activity must be specified for Leadership activities.");
            }
            LeadershipActivityAllowed(usedActivity.ActivityName, role);
        }

        int totalModifier = Abilities[usedSkill.KeyAbility].Modifier();

        //Skill Training - Proficency Bonus
        if (SkillTrainings.ContainsKey(usedActivity.RequiredSkill))
        {
            totalModifier += Skill.TrainingBonus(SkillTrainings[usedActivity.RequiredSkill]) + KingdomLevel;
        }

        BonusManager BonusList = new BonusManager();
        if (paramBonusList != null)
        {
            foreach (Bonus forBonus in paramBonusList)
            {
                BonusList.AddBonus(forBonus);
            }            
        }

        //Invested leaders
        if (SkillHasStatusBonusFromInvestedLeader(usedActivity.RequiredSkill))
        {
            int bonusFromInvested = 1;
            if (Feats.Contains(EnumFeats.ExperiencedLeadership)) bonusFromInvested = 2;
            if (Feats.Contains(EnumFeats.ExperiencedLeadershipPlus)) bonusFromInvested = 3;

            BonusList.AddBonus(EnumBonusType.StatusBonus, bonusFromInvested);
        }

        //Vacancy Penalities
        if (Leaders[EnumLeaderRole.Ruler].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Ruler) == false)
        {
            BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
        }
        if (Leaders[EnumLeaderRole.Counselor].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Counselor) == false)
        {
            if (usedSkill.KeyAbility == EnumAbilityScore.Culture)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Leaders[EnumLeaderRole.Emissary].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Emissary) == false)
        {
            if (usedSkill.KeyAbility == EnumAbilityScore.Loyalty)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Leaders[EnumLeaderRole.Treasurer].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Treasurer) == false)
        {
            if (usedSkill.KeyAbility == EnumAbilityScore.Economy)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Leaders[EnumLeaderRole.Viceroy].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Viceroy) == false)
        {
            if (usedSkill.KeyAbility == EnumAbilityScore.Stability)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Leaders[EnumLeaderRole.Warden].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Warden) == false)
        {
            if (usedActivity.Step == EnumStep.Region)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }
        if (Leaders[EnumLeaderRole.General].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.General) == false)
        {
            if (usedActivity.Phase == EnumPhase.Warfare)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }
        if (Leaders[EnumLeaderRole.Magister].IsVacant && CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Magister) == false)
        {
            if (usedActivity.Phase == EnumPhase.Warfare)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }

        //Ruin
        BonusList.AddBonus(EnumBonusType.ItemPenalty, RuinItemPenalty[RuinCategoryByAbility(usedSkill.KeyAbility)]);
        
        //Collect Taxes Bonus
        if(CurrentTurn().CollectedTaxesBonus > 0 && usedSkill.KeyAbility == EnumAbilityScore.Economy)
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, 2);
        }

        //Improve Lifestyle
        if(CurrentTurn().ImprovedLifestyleBonus != 0 && usedSkill.KeyAbility == EnumAbilityScore.Culture)
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, CurrentTurn().ImprovedLifestyleBonus);
        }
        if (CurrentTurn().ImprovedLifestyleMalus != 0 && usedSkill.KeyAbility == EnumAbilityScore.Economy)
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, CurrentTurn().ImprovedLifestyleMalus);
        }

        //Tap treasury
        if (CurrentTurn().TapTreasurySuccessMalus && usedSkill.KeyAbility == EnumAbilityScore.Economy)
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, CurrentTurn().ImprovedLifestyleBonus);
        }
        if (CurrentTurn().TapTreasuryFailureMalus && (usedSkill.KeyAbility == EnumAbilityScore.Economy || usedSkill.KeyAbility == EnumAbilityScore.Loyalty))
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, CurrentTurn().ImprovedLifestyleMalus);
        }

        //Captured Landmark Bonus
        if (CurrentTurn().CapturedLandmark || LastTurn().CapturedLandmark)
        {
            if(usedSkill.KeyAbility == EnumAbilityScore.Economy || usedSkill.KeyAbility == EnumAbilityScore.Culture)
            {
                BonusList.AddBonus(EnumBonusType.CircumstanceBonus, 2);
            }
        }

        //Captured Refuge Bonus
        if (CurrentTurn().CapturedRefuge || LastTurn().CapturedRefuge)
        {
            if (usedSkill.KeyAbility == EnumAbilityScore.Loyalty || usedSkill.KeyAbility == EnumAbilityScore.Stability)
            {
                BonusList.AddBonus(EnumBonusType.CircumstanceBonus, 2);
            }
        }       

        //New Leadership
        if(CurrentTurn().LovedNewLeaders.Contains(role) || LastTurn().LovedNewLeaders.Contains(role))
        {
            BonusList.AddBonus(EnumBonusType.CircumstanceBonus, 1);
        }
        if ( ! Leaders[role].IsIngratiated)
        {
            BonusList.AddBonus(EnumBonusType.CircumstancePenalty, 1);
        }




        totalModifier += BonusList.TotalBonus();

        return KingdomCheck(ControlDC() + DCModifier, totalModifier);
    }

    public void AdjustUnrest()
    {
        if (ThisTurn == 1)
        {
            UpkeepCollectRessources();
        }

        if (Leaders[EnumLeaderRole.Ruler].IsVacant && ! CurrentTurn().LeadersGivingUpActivity.Contains(EnumLeaderRole.Ruler))
        {
            UnrestPoints += DiceRoller.RollDice(1, 4);
        }

        foreach (Settlement forSettlement in Settlements)
        {
            if(forSettlement.IsOvercrowded())
            {
                UnrestPoints++;
            }
        }

        //TODO
        //if(AtWar)
        //{
        //    UnrestPoints++;
        //}

        if(UnrestPoints  >= 10)
        {
            if(UnrestPoints >= 20)
            {
                InAnarchy = true;
            }
            CurrentTurn().Step = EnumStep.AdjustUnrest;
            CurrentTurn().UpkeepUnrestRuinPoints = DiceRoller.RollDice(1, 10);
            CurrentTurn().UpkeepUnrestLostHex = !Check.MakeFlatCheck(11);
        }
        else
        {
            UpkeepCollectRessources();
        }
    }

    public void DistributeUnrestRuin(int amount, EnumRuinCategory ruin)
    {
        if (CurrentTurn().Phase != EnumPhase.Upkeep) throw new Exception("You're not in the 'Upkeep' phase.");
        if (CurrentTurn().Step != EnumStep.AdjustUnrest) throw new Exception("You're not in the 'Adjust Unrest' step.");

        if (amount > CurrentTurn().UpkeepUnrestRuinPoints) throw new Exception("You are placing more Ruin than required.");

        AddRuin(amount, ruin);

        if (CurrentTurn().UpkeepUnrestRuinPoints <= 0 && CurrentTurn().UpkeepUnrestLostHex == false)
        {
            UpkeepCollectRessources();
        }
    }

    public void LoseUnrestHex(string hexPostion)
    {
        if (CurrentTurn().Phase != EnumPhase.Upkeep) throw new Exception("You're not in the 'Upkeep' phase.");
        if (CurrentTurn().Step != EnumStep.AdjustUnrest) throw new Exception("You're not in the 'Adjust Unrest' step.");

        RemoveHex(hexPostion);

        if (CurrentTurn().UpkeepUnrestRuinPoints <= 0 && CurrentTurn().UpkeepUnrestLostHex == false)
        {
            UpkeepCollectRessources();
        }
    }

    public void AddHex(Hex hex)
    {
        WorldMap.Add(hex.Key(), hex);
        if (hex.InTerritory)
        {
            return;
        }

        CurrentTurn().CapturedHex = true;

        //Captured a Landmark
        if (hex.TerrainFeature == EnumTerrainFeature.Landmark)
        {
            ReduceUnrest(DiceRoller.RollDice(1, 4));
            CurrentTurn().CapturedLandmark = true;
            if ( ! Milestones.Contains(EnumXPMilestone.FirstLandmark))
            {
                Milestones.Add(EnumXPMilestone.FirstLandmark);
                AddXP(40);
            }
        }

        //Captured a Refuge
        if (hex.TerrainFeature == EnumTerrainFeature.Refuge)
        {
            CurrentTurn().Pause(EnumPausedReason.RuinDown);
            CurrentTurn().CapturedRefuge = true;
            if ( ! Milestones.Contains(EnumXPMilestone.FirstRefuge))
            {
                Milestones.Add(EnumXPMilestone.FirstRefuge);
                AddXP(40);
            }
        }
    }

    public void RemoveHex(string hexPostion)
    {
        WorldMap[hexPostion].InTerritory = false;
        if(Territory().Count() == 0)
        {
            CurrentTurn().WentWithoutHex = true;
        }
    }


    public void UpkeepCollectRessources()
    {
        if (CurrentTurn().Phase != EnumPhase.Upkeep) throw new Exception("You're not in the 'Upkeep' phase.");

        RessourcePoints = DiceRoller.RollDice(RessourceDiceAmount(), RessourceDiceSize());

        foreach(Hex forHex in Territory().Values)
        {
            if(forHex.TerrainFeature == EnumTerrainFeature.WorkSite)
            {
                AddCommodity(1, forHex.Ressource);
            }
            else if (forHex.TerrainFeature == EnumTerrainFeature.RessourceWorksite)
            {
                AddCommodity(2, forHex.Ressource);
            }
        }

        CurrentTurn().Phase = EnumPhase.Upkeep;
        CurrentTurn().Step = EnumStep.PayConsumption;
    }

    public void AddCommodity(int addedAmount, EnumCommodity commodity)
    {
        Commodities[commodity].Amount = Math.Min(Commodities[commodity].Amount + addedAmount, CommodityStorage(commodity));
    }

    public int CommodityStorage(EnumCommodity commodity)
    {
        int returnedStorage;

        returnedStorage = KingdomSizeCommodityStorageModifier();

        //TODO : Amount of storage from buildings affecting the current commodity

        return returnedStorage;
    }

    public int KingdomSizeCommodityStorageModifier()
    {
        switch (KingdomSize())
        {
            case EnumKingdomSize.Territory: return 4;
            case EnumKingdomSize.Province: return 8;
            case EnumKingdomSize.State: return 12;
            case EnumKingdomSize.Country: return 16;
            case EnumKingdomSize.Dominion: return 20;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void UpkeepPayConsumption(bool spendForFood)
    {
        if (ThisTurn == 1)
        {
            CurrentTurn().Phase = EnumPhase.Commerce;
            CurrentTurn().Step = EnumStep.CollectTaxes;
        }

        if (CurrentTurn().Phase != EnumPhase.Upkeep) { throw new Exception("You're not in the 'Upkeep' Phase."); }
        if (CurrentTurn().Step != EnumStep.PayConsumption) { throw new Exception("You're not in the 'Pay Consumption' Step."); }

        int initialFood = Commodities[EnumCommodity.Food].Amount;
        int amountWillEat = Math.Min(Consumption(), initialFood);
        int amountMissingFood = Consumption() - initialFood;

        if (spendForFood)
        {
            if(amountMissingFood > 0)
            {
                if (RessourcePoints < amountMissingFood * 5)
                {
                    throw new Exception("You don't have enough Ressource Points to pay for Consumption.");
                }
                RessourcePoints -= amountMissingFood * 5;                
            }
            else
            {
                if (RessourcePoints < Consumption() * 5)
                {
                    throw new Exception("You don't have enough Ressource Points to pay for Consumption.");
                }
                RessourcePoints -= Consumption() * 5;
            }
        }
        else
        {            
            if(amountMissingFood > 0)
            {
                UnrestPoints += DiceRoller.RollDice(1, 4);
            }
        }

        Commodities[EnumCommodity.Food].Amount = Math.Max(initialFood - Consumption(), 0);

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.CollectTaxes;
    }

    public void CollectTaxes(bool collect)
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.CollectTaxes) { throw new Exception("You're not in the 'Collect Taxes' Step."); }

        if (collect)
        {
            CurrentTurn().CollectedTaxes = true;

            EnumCheckResult result = UseActivity(EnumActivity.CollectTaxes);
            switch (result)
            {
                case EnumCheckResult.CritSuccess:
                    CurrentTurn().CollectedTaxesBonus = 2;
                    CurrentTurn().Phase = EnumPhase.Commerce;
                    CurrentTurn().Step = EnumStep.ApproveExpenses;
                    break;
                case EnumCheckResult.Success:
                    CurrentTurn().CollectedTaxesBonus = 1;
                    if(LastTurn().CollectedTaxes)
                    {
                        UnrestPoints += 1;
                    }
                    CurrentTurn().Phase = EnumPhase.Commerce;
                    CurrentTurn().Step = EnumStep.ApproveExpenses;
                    break;
                case EnumCheckResult.Failure:
                    CurrentTurn().CollectedTaxesBonus = 1;
                    UnrestPoints += 1;
                    if (LastTurn().CollectedTaxes)
                    {
                        UnrestPoints += 1;
                    }
                    CurrentTurn().Phase = EnumPhase.Commerce;
                    CurrentTurn().Step = EnumStep.ApproveExpenses;
                    break;
                case EnumCheckResult.CritFailure:
                    UnrestPoints += 2;
                    CurrentTurn().Phase = EnumPhase.Commerce;
                    CurrentTurn().Step = EnumStep.CollectTaxesRuin;
                    break;
                default: throw new Exception("Unknown check result.");
            }
        }
        else
        {
            if (Check.MakeFlatCheck(11))
            {
                ReduceUnrest(1);
            }
        }
    }

    public void RuinFromCollectTaxes(EnumRuinCategory ruin)
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.CollectTaxesRuin) { throw new Exception("You're not in the 'Collect Taxes' Step."); }

        AddRuin(1, ruin);

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.ApproveExpenses;
    }

    public void ImproveLifestyle()
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.ApproveExpenses) { throw new Exception("You're not in the 'Approve Expenses' Step."); }

        EnumCheckResult result = UseActivity(EnumActivity.ImproveLifestyle);
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                CurrentTurn().ImprovedLifestyleBonus = 2;
                break;
            case EnumCheckResult.Success:
                CurrentTurn().ImprovedLifestyleBonus = 1;
                break;
            case EnumCheckResult.Failure:
                CurrentTurn().ImprovedLifestyleBonus = 1;
                CurrentTurn().ImprovedLifestyleMalus = 1;
                break;
            case EnumCheckResult.CritFailure:
                CurrentTurn().ImprovedLifestyleMalus = 1;
                UnrestPoints += 1;
                break;
            default: throw new Exception("Unknown check result.");
        }        
            
        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.TapCommodities;
        if(result == EnumCheckResult.CritFailure)
        {
            CurrentTurn().Pause(EnumPausedReason.RuinUp);
        }
    }

    public void TapTreasury()
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.ApproveExpenses) { throw new Exception("You're not in the 'Approve Expenses' Step."); }

        EnumCheckResult result = UseActivity(EnumActivity.TapTreasury);
        if(TreasuryTapped)
        {
            result = Check.WorsenCheckResult(Check.WorsenCheckResult(result));
        }
        switch (result)
        {
            case EnumCheckResult.CritSuccess:                
                break;
            case EnumCheckResult.Success:
                CurrentTurn().TapTreasurySuccessMalus = true;
                break;
            case EnumCheckResult.Failure:
                CurrentTurn().TapTreasuryFailureMalus = true;
                break;
            case EnumCheckResult.CritFailure:
                CurrentTurn().TapTreasuryFailureMalus = true;
                UnrestPoints += 1;
                break;
            default: throw new Exception("Unknown check result.");
        }

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.TapCommodities;
        if (result == EnumCheckResult.CritFailure)
        {
            CurrentTurn().Pause(EnumPausedReason.RuinUp);
        }
    }

    public void TradeCommodities(int amount, EnumCommodity commodity, bool withDiplomaticRelation)
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.TapCommodities) { throw new Exception("You're not in the 'Tap Commodities' Step."); }

        if (amount > 4) { throw new Exception("You can't trade more than 4 commodities at a time."); }
        if (amount <= 0) { throw new Exception("You must trade at least 1 commodity."); }
        if (commodity == EnumCommodity.None) { throw new Exception("You must choose a valid commodity."); }
        if (amount > Commodities[commodity].Amount) { throw new Exception("You can't trade more commodity than you have."); }

        Commodities[commodity].Amount -= amount;
        CurrentTurn().TradedCommodity = true;
        
        List<Bonus> bonus = new List<Bonus>();
        if (withDiplomaticRelation)
        {
            bonus.Add(new Bonus(EnumBonusType.CircumstanceBonus, 1));
        }

        EnumCheckResult result = UseActivity(EnumActivity.TapTreasury, bonus);   
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                CurrentTurn().NextTurnBonusDice += amount * 2;
                break;
            case EnumCheckResult.Success:
                CurrentTurn().NextTurnBonusDice += amount;
                break;
            case EnumCheckResult.Failure:
                CurrentTurn().NextTurnBonusDice += 1;
                break;
            case EnumCheckResult.CritFailure:
                if(LastTurn().TradedCommodity)
                {
                    UnrestPoints += 1;
                }
                break;
            default: throw new Exception("Unknown check result.");
        }

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.ManageTradeAgreements;
    }

    public void StopTapCommodities()
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.TapCommodities) { throw new Exception("You're not in the 'Tap Commodities' Step."); }

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.ManageTradeAgreements;
    }

    public void ManageTradeAgreement(int amount)
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.ManageTradeAgreements) { throw new Exception("You're not in the 'Manage Trade Agreements' Step."); }
        
        if(LastTurn().CritFailedManagedTradeAgreement)
        {
            { throw new Exception("You can't 'Manage Trade Agreement' since you critically failed last turn."); }
        }

        SpendRP(amount * 2);

        int dcModifier = 0;
        if(LastTurn().ManagedTradeAgreement)
        {
            dcModifier = 5;
        }

        CurrentTurn().ManagedTradeAgreement = true;
        CurrentTurn().ManagedTradeAgreementAmount = amount;

        EnumCheckResult result = UseActivity(EnumActivity.ManageTradeAgreements, DCModifier: dcModifier);
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                CurrentTurn().NextTurnBonusDice += amount;
                CurrentTurn().NextTurnBonusCommodity += amount;
                CurrentTurn().NextTurnBonusCommodityMaxLuxury += amount / 2;
                break;
            case EnumCheckResult.Success:
                
                break;
            case EnumCheckResult.Failure:
                CurrentTurn().NextTurnBonusRP += amount;
                break;
            case EnumCheckResult.CritFailure:
                CurrentTurn().CritFailedManagedTradeAgreement = true;
                break;
            default: throw new Exception("Unknown check result.");
        }

        CurrentTurn().Phase = EnumPhase.Activity;
        CurrentTurn().Step = EnumStep.Leadership;
        if(result == EnumCheckResult.Success)
        { 
            CurrentTurn().Pause(EnumPausedReason.ChoiceFromManageTrade);
        }
    }

    public void ChoiceManageTradeAgreement(bool choice)
    {
        if(CurrentTurn().PausedReason != EnumPausedReason.ChoiceFromManageTrade)
        {
            throw new Exception("You don't have to make a 'Manage Trade Agreement' choice.");
        }

        if(choice)
        {
            CurrentTurn().NextTurnBonusDice += CurrentTurn().ManagedTradeAgreementAmount;
        }
        else
        {
            CurrentTurn().NextTurnBonusCommodity += CurrentTurn().ManagedTradeAgreementAmount;
            CurrentTurn().NextTurnBonusCommodityMaxLuxury += CurrentTurn().ManagedTradeAgreementAmount / 2;
        }
    }

    public void CapitalInvestment(bool refund)
    {
        if(refund && !TreasuryTapped)
        {
            throw new Exception("There is no Tapped Treasury to refund.");
        }


    }

    public void SpendRP(int amount, bool forced = false)
    {        
        if(RessourcePoints - amount < 0)
        {
            if (forced)
            {
                CurrentTurn().Pause(EnumPausedReason.RuinUp);
            }
            else
            {
                throw new Exception("Can't spend that much Ressource Points.");
            }
        }
        
        RessourcePoints = Math.Max(RessourcePoints - amount, 0);
    }

    public void ChooseRuinToRemove(EnumRuinCategory ruin)
    {
        if (CurrentTurn().PausedReason != EnumPausedReason.RuinDown)
        {
            throw new Exception("You don't need to remove ruin right now.");
        }
        RemoveRuin(1, ruin);
        CurrentTurn().Unpause();
    }

    public void AddRuinPaused(int amount, EnumRuinCategory ruin)
    {
        if (CurrentTurn().PausedReason != EnumPausedReason.RuinUp)
        {
            throw new Exception("You don't need to add ruin.");
        }
        AddRuin(amount, ruin);
        CurrentTurn().Unpause();
    }

    public void LeadershipActivityAllowed(EnumActivity activity, EnumLeaderRole role)
    {
        if( ! CurrentTurn().LeadershipActivities.ContainsKey(role))
        {
            throw new Exception("This role doesn't have activities this turn.");
        }

        int activityPerLeader = 2;
        if(CapitalSettlement().ContainsStructure(EnumStructure.Castle) 
        || CapitalSettlement().ContainsStructure(EnumStructure.Palace) 
        || CapitalSettlement().ContainsStructure(EnumStructure.TownHall))
        {
            activityPerLeader = 3;
        }

        if(CurrentTurn().LeadershipActivities[role].Contains(activity))
        {
            //TODO : But not for exception cases to be added here.
            throw new Exception("This activity can't be attempted twice in a turn by the same leader.");
        }

        if(CurrentTurn().LeadershipActivities[role].Count() >= activityPerLeader) 
        {
            throw new Exception("This leader already attempted all its activities.");
        }
    }

    public void ReplaceInvestedLeader(EnumLeaderRole role1, EnumLeaderRole role2)
    {
        if (CurrentTurn().Step != EnumStep.AssignLeadership) { throw new Exception("You can only switch invested roles at the 'Asssign Leadership' step."); }
        if (role1 == role2) { throw new Exception("You must choose 2 different roles to exchange."); }
        if (Leaders[role1].IsInvested) { throw new Exception("Your Leader to uninvest must be invested."); }
        if ( ! Leaders[role2].IsInvested) { throw new Exception("Your Leader to invest must not be already invested."); }

        Leaders[role1].IsInvested = false;
        Leaders[role2].IsInvested = true;
    }


    public void IsOnlyATemplate()
    {
        if (CurrentTurn().Phase != EnumPhase.Commerce) { throw new Exception("You're not in the 'Commerce' Phase."); }
        if (CurrentTurn().Step != EnumStep.CollectTaxes) { throw new Exception("You're not in the 'Collect Taxes' Step."); }

        CurrentTurn().Phase = EnumPhase.Commerce;
        CurrentTurn().Step = EnumStep.ApproveExpenses;

        EnumCheckResult result = EnumCheckResult.Success;
        switch (result)
        {
            case EnumCheckResult.CritSuccess:
                break;
            case EnumCheckResult.Success:
                break;
            case EnumCheckResult.Failure:
                break;
            case EnumCheckResult.CritFailure:
                break;
            default: throw new Exception("Unknown check result.");
        }
    }
}

