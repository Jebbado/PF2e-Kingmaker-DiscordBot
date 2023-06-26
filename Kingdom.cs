using System;

public class Kingdom
{
    private string KingdomName;
    private string CapitolName;

    private EnumCharter Charter;
    private EnumHeartland Heartland;
    private EnumGovernment Government;

    private int ExperiencePoints;
    private int KingdomLevel;
    private int PlayersLevel;
    private int RessourcePoints;
    //private int KingdomSize;

    private int CultureAbilityScore;    
    private int EconomyAbilityScore;
    private int LoyaltyAbilityScore;
    private int StabilityAbilityScore;

    private int FamePoints;
    private int UnrestPoints;

    private int FoodCommodities;
    private int LumberCommodities;
    private int LuxuriesCommodities;
    private int OreCommodities;
    private int StoneCommodities;

    private List<Leader> Leaders;

    private List<Settlement> Settlements;

    private List<Hex> Territory;

    //private List<EnumRuinCategory> RuinResistances;
    private Dictionary<EnumRuinCategory, int> RuinScore;
    private Dictionary<EnumRuinCategory, int> RuinThreshold;
    private Dictionary<EnumRuinCategory, int> RuinItemPenalty;    

    private Dictionary<Skill.EnumSkills, Skill.EnumSkillTraining> SkillTrainings;
    private Dictionary<Feat.EnumFeats, string> Feats;

    public enum EnumAbilityScore
    {
        None,
        Culture,
        Economy,
        Loyalty,
        Stability
    }

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

   

    public Kingdom(string name, string capitolName)
    {
        KingdomName = name;
        CapitolName = capitolName;

        Charter = EnumCharter.None;
        Heartland = EnumHeartland.None;
        Government = EnumGovernment.None;

        CultureAbilityScore = 10;
        EconomyAbilityScore = 10;
        LoyaltyAbilityScore = 10;
        StabilityAbilityScore = 10;

        KingdomLevel = 1;

        UnrestPoints = 0;

        Leaders = new List<Leader>();
        Settlements = new List<Settlement>();
        Settlements.Add(new Settlement(capitolName, new Hex(1, 1, Heartland))); //TODO : Correct None type.
        Territory = new List<Hex>();        
        SkillTrainings = new Dictionary<Skill.EnumSkills, Skill.EnumSkillTraining>();
        RuinThreshold = new Dictionary<EnumRuinCategory, int>();
        RuinScore = new Dictionary<EnumRuinCategory, int>();
        RuinItemPenalty = new Dictionary<EnumRuinCategory, int>();
        Feats = new Dictionary<Feat.EnumFeats, string>();
}

    public void BoostAbility(EnumAbilityScore ability, bool isFlaw = false)  
    {
        switch (ability)
        {
            case EnumAbilityScore.Culture:
                CultureAbilityScore += 2 * (isFlaw ? -1 : 1);
                break;
            case EnumAbilityScore.Economy:
                EconomyAbilityScore += 2 * (isFlaw ? -1 : 1);
                break;
            case EnumAbilityScore.Loyalty:
                LoyaltyAbilityScore += 2 * (isFlaw ? -1 : 1);
                break;
            case EnumAbilityScore.Stability:
                StabilityAbilityScore += 2 * (isFlaw ? -1 : 1);
                break;
            default:
                Console.WriteLine("Invalid Ability.");
                break;
        }
    }

    public int CultureModifier()
    {
        return (CultureAbilityScore - 10) / 2;
    }

    public int EconomyModifier()
    {
        return (EconomyAbilityScore - 10) / 2;
    }

    public int LoyaltyModifier()
    {
        return (LoyaltyAbilityScore - 10) / 2;
    }

    public int StabilityModifier()
    {
        return (StabilityAbilityScore - 10) / 2;
    }

    public int AbilityModifier(EnumAbilityScore abilityScore)
    {
        switch(abilityScore) 
        {
            case EnumAbilityScore.Economy:
                return EconomyModifier();
            case EnumAbilityScore.Loyalty:
                return LoyaltyModifier();
            case EnumAbilityScore.Stability:
                return StabilityModifier();
            case EnumAbilityScore.Culture:
                return CultureModifier();
            default: 
                throw new NotImplementedException();
        }
    }

   // public int RuinThreshold(EnumRuinCategory enumRuinCategory) 
   // {
   //     int returnedValue = 10;
   //
   //     foreach (EnumRuinCategory resistance in RuinResistances)
   //     {
   //         if(resistance == enumRuinCategory)
   //         {
   //             returnedValue += 2;
   //         }
   //     }
   //
   //     return returnedValue;
   //
   // }

    //public int KingdomLevel()
    //{
    //    return Math.Min(((ExperiencePoints / 1000) + 1), PlayersLevel);
    //}

    public int KingdomSize()
    {
        return Territory.Count;
    }

    public void AssignCharter(EnumCharter charter)
    {
        if (Charter != EnumCharter.None)
        {
            throw new Exception("Charter already chosen. You can't change your charter.");
        }

        if (charter == EnumCharter.None)
        {
            throw new Exception("You must choose a valid charter.");
        }

        Charter = charter;
    }

    public void AssignHeartland(EnumHeartland heartland)
    {
        if (Heartland != EnumHeartland.None)
        {
            throw new Exception("Heartland already chosen. You can't change your heartland.");
        }

        if (heartland == EnumHeartland.None)
        {
            throw new Exception("You must choose a valid heartland.");
        }

        Heartland = heartland;
    }

    public void AssignGovernment(EnumGovernment government)
    {
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
                TrainSkill(Skill.EnumSkills.Intrigue);
                TrainSkill(Skill.EnumSkills.Warfare);
                break;
            case EnumGovernment.Feudalism:
                TrainSkill(Skill.EnumSkills.Defense);
                TrainSkill(Skill.EnumSkills.Trade);
                break;
            case EnumGovernment.Oligarchy:
                TrainSkill(Skill.EnumSkills.Arts);
                TrainSkill(Skill.EnumSkills.Industry);
                break;
            case EnumGovernment.Republic:
                TrainSkill(Skill.EnumSkills.Engineering);
                TrainSkill(Skill.EnumSkills.Politics);
                break;
            case EnumGovernment.Thaumocracy:
                TrainSkill(Skill.EnumSkills.Folklore);
                TrainSkill(Skill.EnumSkills.Magic);
                break;
            case EnumGovernment.Yeomanry:
                TrainSkill(Skill.EnumSkills.Agriculture);
                TrainSkill(Skill.EnumSkills.Wilderness);
                break;
        }
    }

    public void AssignFirstLeaderSkill(Skill.EnumSkills addedSkill)
    {
        if(SkillTrainings.Count > 2)
        {
            throw new Exception("First leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignSecondLeaderSkill(Skill.EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 3)
        {
            throw new Exception("Second leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignThirdLeaderSkill(Skill.EnumSkills addedSkill)
    {
        if (SkillTrainings.Count > 4)
        {
            throw new Exception("Third leader skill already chosen.");
        }
        TrainSkill(addedSkill);
    }

    public void AssignFourthLeaderSkill(Skill.EnumSkills addedSkill)
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
        switch (Charter)
        {
            case EnumCharter.Conquest:
                if (charterChoice == EnumAbilityScore.Stability || charterChoice == EnumAbilityScore.Economy)
                {
                    BoostAbility(charterChoice);
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                BoostAbility(EnumAbilityScore.Loyalty);
                BoostAbility(EnumAbilityScore.Culture, true);
                break;
            case EnumCharter.Expansion:
                if (charterChoice == EnumAbilityScore.Loyalty || charterChoice == EnumAbilityScore.Economy)
                {
                    BoostAbility(charterChoice);
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                BoostAbility(EnumAbilityScore.Culture);
                BoostAbility(EnumAbilityScore.Stability, true);
                break;
            case EnumCharter.Exploration:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Loyalty)
                {
                    BoostAbility(charterChoice);
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                BoostAbility(EnumAbilityScore.Stability);
                BoostAbility(EnumAbilityScore.Economy, true);
                break;
            case EnumCharter.Grant:
                if (charterChoice == EnumAbilityScore.Culture || charterChoice == EnumAbilityScore.Stability)
                {                
                    BoostAbility(charterChoice);
                }
                else
                {
                    throw new Exception("You must choose a valid ability boost.");
                }
                BoostAbility(EnumAbilityScore.Economy);
                BoostAbility(EnumAbilityScore.Loyalty, true);
                break;
            case EnumCharter.Open:
                BoostAbility(charterChoice);
                break;
            default:
                throw new Exception("The Kingdom charter is not valid.");
        }

        switch (Heartland)
        {
            case EnumHeartland.Forest:
            case EnumHeartland.Swamp:
                BoostAbility(EnumAbilityScore.Culture);
                break;
            case EnumHeartland.Hill:
            case EnumHeartland.Plain:
                BoostAbility(EnumAbilityScore.Loyalty);
                break;
            case EnumHeartland.Lake:
            case EnumHeartland.River:
                BoostAbility(EnumAbilityScore.Economy);
                break;
            case EnumHeartland.Mountain:
            case EnumHeartland.Ruin:
                BoostAbility(EnumAbilityScore.Stability);
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
                BoostAbility(governmentChoice);
                BoostAbility(EnumAbilityScore.Stability);
                BoostAbility(EnumAbilityScore.Economy);
                break;
            case EnumGovernment.Feudalism:
                if (governmentChoice != EnumAbilityScore.Economy && governmentChoice != EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                BoostAbility(governmentChoice);
                BoostAbility(EnumAbilityScore.Stability);
                BoostAbility(EnumAbilityScore.Culture);
                break;
            case EnumGovernment.Oligarchy:
                if (governmentChoice != EnumAbilityScore.Culture && governmentChoice != EnumAbilityScore.Stability)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                BoostAbility(governmentChoice);
                BoostAbility(EnumAbilityScore.Loyalty);
                BoostAbility(EnumAbilityScore.Economy);
                break;
            case EnumGovernment.Republic:
                if (governmentChoice == EnumAbilityScore.Culture & governmentChoice == EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                BoostAbility(governmentChoice);
                BoostAbility(EnumAbilityScore.Stability);
                BoostAbility(EnumAbilityScore.Loyalty);
                break;
            case EnumGovernment.Thaumocracy:
                if (governmentChoice == EnumAbilityScore.Stability & governmentChoice == EnumAbilityScore.Loyalty)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                BoostAbility(EnumAbilityScore.Stability);
                BoostAbility(EnumAbilityScore.Culture);
                BoostAbility(EnumAbilityScore.Economy);
                break;
            case EnumGovernment.Yeomanry:
                if (governmentChoice == EnumAbilityScore.Stability && governmentChoice == EnumAbilityScore.Economy)
                {
                    throw new Exception("You must choose a valid government ability boost.");
                }
                BoostAbility(governmentChoice);
                BoostAbility(EnumAbilityScore.Culture);
                BoostAbility(EnumAbilityScore.Loyalty);
                break;
            default:
                throw new Exception("The Kingdom government is not valid.");
        }

        if (freeChoice1 == freeChoice2)
        {
            throw new Exception("You must choose two different additional ability boosts.");
        }
        BoostAbility(freeChoice1);
        BoostAbility(freeChoice2);
    }

    public void AddLeader(Leader addedLeader)
    {
        if (addedLeader == null) throw new Exception("You must choose a leader to add.");

        int totalInvested = 0;
        foreach (Leader forLeader in Leaders) 
        {
            if (forLeader.getRole() == addedLeader.getRole()) throw new Exception("There is already a leader in this Role.");

            if(forLeader.getInvested()) totalInvested++;
        }

        if (addedLeader.getInvested() && totalInvested >= 4) throw new Exception("There is already a leader in this Role.");

        Leaders.Add(addedLeader);
    }

    public void setPlayersLevel(int level)
    { this.PlayersLevel = level; }

    public void RemoveLeader(Leader removedLeader) { Leaders.Remove(removedLeader); } //TODO : Test the input. I guess I require an ID.

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

        return returnedDC;
    }

    public int RessourceDiceSize()
    {        
        if (1 <= KingdomSize() || KingdomSize() <= 9)
        { return 4; }
        else if (10 < KingdomSize() || KingdomSize() < 24)
        { return 6; }
        else if (25 < KingdomSize() || KingdomSize() < 49)
        { return 8; }
        else if (50 < KingdomSize() || KingdomSize() < 99)
        { return 10; }
        else if (KingdomSize() > 100)
        { return 12; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    public int KingdomSizeControlDCModifier()
    {
        if (1 <= KingdomSize() || KingdomSize() <= 9)
        { return 0; }
        else if (10 < KingdomSize() || KingdomSize() < 24)
        { return 1; }
        else if (25 < KingdomSize() || KingdomSize() < 49)
        { return 2; }
        else if (50 < KingdomSize() || KingdomSize() < 99)
        { return 3; }
        else if (KingdomSize() > 100)
        { return 4; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    public int KingdomSizeCommodityStorageModifier()
    {
        if (1 <= KingdomSize() || KingdomSize() <= 9)
        { return 4; }
        else if (10 < KingdomSize() || KingdomSize() < 24)
        { return 8; }
        else if (25 < KingdomSize() || KingdomSize() < 49)
        { return 12; }
        else if (50 < KingdomSize() || KingdomSize() < 99)
        { return 16; }
        else if (KingdomSize() > 100)
        { return 20; }
        else { throw new ArgumentOutOfRangeException(); }
    }

    public int FoodCommoditiesStorage()
    {
        return KingdomSizeCommodityStorageModifier();
    }

    public int LuxuriesCommoditiesStorage()
    {
        return KingdomSizeCommodityStorageModifier();
    }

    public int LumberCommoditiesStorage()
    {
        return KingdomSizeCommodityStorageModifier();
    }

    public int StoneCommoditiesStorage()
    {
        return KingdomSizeCommodityStorageModifier();
    }

    public int OreCommoditiesStorage()
    {
        return KingdomSizeCommodityStorageModifier();
    }

    public int RessourceDiceAmount()
    {
        int amount = KingdomLevel + 4;

        //TODO : Any reduction goes here.

        return Math.Max(amount, 0);
    }

    public int Consumption()
    {
        return 1; //TODO : Settlement - Farmlands + etc.
    }

    public bool EarnFame()
    {
        int FameThreshold = 3;

        if(KingdomLevel >= 20)
        {
            FameThreshold++;
        }

        if (FamePoints >= FameThreshold)
        {
            return false;
        }

        FamePoints += 1;
        return true;        
    }

    public EnumCheckResult KingdomCheck(int DC, int modifier = 0) 
    {
        EnumCheckResult returnedResult = Utility.MakeCheck(DC, modifier);

        if(returnedResult == EnumCheckResult.CritSuccess) { EarnFame(); }

        return returnedResult;
    }

    public EnumCheckResult UseSkill(Skill.EnumSkills usedSkill)
    {
        int totalModifier = 0;
        int proficiencyBonus = 0;
        int statusBonus = 0;
        
        if (SkillTrainings.ContainsKey(usedSkill))
        {
            proficiencyBonus += Skill.TrainingBonus(SkillTrainings[usedSkill]) + KingdomLevel;
        }
        
        if (SkillHasStatusBonusFromInvestedLeader(usedSkill))
        {
            int bonusFromInvested = 1;
            if (KingdomLevel >= 8) bonusFromInvested = 2;
            if (KingdomLevel >= 16) bonusFromInvested = 3;
            statusBonus = Math.Max(statusBonus, bonusFromInvested);
        }

        totalModifier += AbilityModifier(Skill.SkillList()[usedSkill].KeyAbility);
        totalModifier += proficiencyBonus;
        totalModifier += statusBonus;
        totalModifier -= RuinItemPenalty[RuinCategoryByAbility(Skill.SkillList()[usedSkill].KeyAbility)];

        return KingdomCheck(ControlDC(), totalModifier);
    }
    
    public void TrainSkill(Skill.EnumSkills enumSkill, Skill.EnumSkillTraining skillTraining = Skill.EnumSkillTraining.Trained)
    {
        
        if(SkillTrainings.ContainsKey(enumSkill) && SkillTrainings[enumSkill] == skillTraining)
        { 
            throw new Exception("This skill is already at ." + skillTraining.ToString());
        }

        SkillTrainings[enumSkill] = skillTraining;
    }

    public bool SkillHasStatusBonusFromInvestedLeader(Skill.EnumSkills enumSkill)
    {
        foreach (Leader forLeader in Leaders)
        {
            if(forLeader.getInvested() && Leader.KeyAbilityForRole(forLeader.getRole()) == Skill.SkillList()[enumSkill].KeyAbility)
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

    public void AddRuin(int ruinAmount, EnumRuinCategory ruinCategory)
    {
        RuinScore[ruinCategory] += ruinAmount;
        if(RuinScore[ruinCategory] > RuinThreshold[ruinCategory])
        {
            RuinScore[ruinCategory] -= RuinThreshold[ruinCategory];
            RuinItemPenalty[ruinCategory] += 1;
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
            default: throw new NotImplementedException("This ability score does'nt exist.");
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
                Feats[Feat.EnumFeats.ExpansionExpert] = "";
                Feats[Feat.EnumFeats.FineLiving] = "";
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                Feats[Feat.EnumFeats.ExpansionExpertPlus] = "";
                break;
            case 10:
                Feats[Feat.EnumFeats.LifeOfLuxury] = "";
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
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                throw new NotSupportedException("You're too OP, stop right there !");
        }
                
    }
}

