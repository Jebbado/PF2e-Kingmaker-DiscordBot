
public enum EnumPhase
{
    Creation,
    Upkeep,
    Commerce,
    Activity
}

public enum EnumStep
{
    ChooseCharter,
    ChooseHeartland,
    ChooseGovernment,
    FinalizeAbilityScores,
    ChooseLeaders,
    FirstVillage,
    ChooseFameInfamy,
    AssignLeadership,
    AdjustUnrest,
    RessourceCollection,
    PayConsumption,
    CollectTaxes,
    ApproveExpenses,
    TapCommodities,
    ManageTradeAgreements,
    Leadership,
    Region,
    Civic,
    CheckEvent,
    EventResolution,
    ApplyXP,
    LevelUp
}

public class Game
{
    public int GameID;
    public Kingdom Kingdom;
    public int ThisTurn = 0;
    public Dictionary<int, Turn> Turns = new Dictionary<int, Turn>();

    //This constructor loads an existing game
    public Game(int ID) 
    {
        //Loads the game
        Kingdom = new Kingdom("LoadedKingdom");
        GameID = ID;   
    }
    //This constructor starts a new game
    public Game(string kingdomName) 
    {
        Kingdom = new Kingdom(kingdomName);
        Turns[ThisTurn] = new Turn(ThisTurn, EnumPhase.Creation, EnumStep.ChooseCharter);
    }

    public void ChooseCharter(EnumCharter enumCharter)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You can't change your Charter after Kingdom creation."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseCharter) { throw new Exception("You're not at the 'Select a Charter' step right now."); }

        Kingdom.AssignCharter(enumCharter);
        Turns[ThisTurn].Step = EnumStep.ChooseHeartland;
    }

    public void ChooseHeartland(EnumHeartland enumHeartland)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You can't change your Heartland after Kingdom creation."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseHeartland) { throw new Exception("You're not at the 'Choose a Heartland' step right now."); }

        Kingdom.AssignHeartland(enumHeartland);
        Turns[ThisTurn].Step = EnumStep.ChooseGovernment;
    }

    public void ChooseGovernment(EnumGovernment enumGovernment)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You can't change Government after Kingdom creation."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseGovernment) { throw new Exception("You're not at the 'Choose a Government' step right now."); }

        Kingdom.AssignGovernment(enumGovernment);
        Turns[ThisTurn].Step = EnumStep.FinalizeAbilityScores;
    }

    public void FinalizeAbilityScore(EnumAbilityScore ab1, EnumAbilityScore ab2, EnumAbilityScore ab3, EnumAbilityScore ab4)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You can't change Ability Scores after Kingdom creation."); }
        if (Turns[ThisTurn].Step != EnumStep.FinalizeAbilityScores) { throw new Exception("You're not at the 'Finalize Ability Score' step right now."); }
        
        Kingdom.FinalizeAbilityScore(ab1, ab2, ab3, ab4);
        Turns[ThisTurn].Step = EnumStep.ChooseLeaders;
    }

    public void AddLeader(string name, EnumLeaderRole role, bool invested, bool isPC)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You can't add Leaders after Kingdom creation. You should use the 'New Leadership' Activity."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose a Government' step right now."); }

        Kingdom.AddLeader(new Leader(name, role, invested, isPC));  
    }

    public void EndLeaderStep() 
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose Leaders' step."); }

        if(Kingdom.InvestedLeadersCount() < 4) { throw new Exception("You must have 4 Invested Leaders before ending"); }

        Turns[ThisTurn].Step = EnumStep.FirstVillage; 
    }

    public void CreateCapital(string name, EnumStructure initialStructure = EnumStructure.None)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[ThisTurn].Step != EnumStep.FirstVillage) { throw new Exception("You're not at the 'Forst Village' step."); }

        Kingdom.AddSettlement(name, 0, 0, true);
        if (initialStructure != EnumStructure.None) 
        { 
            Kingdom.CapitalSettlement().PlaceStructure(initialStructure, 1);
        }

        Turns[ThisTurn].Step = EnumStep.ChooseFameInfamy;
    }

    public void ChooseFameInfamy(EnumFameAspiration fame)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[ThisTurn].Step != EnumStep.ChooseFameInfamy) { throw new Exception("You're not at the 'Fame or Infamy ?' step."); }

        Kingdom.ChooseFame(fame);

        EndTurn();
    }

    public void EndTurn()
    {
        ThisTurn++;
        Turns[ThisTurn].Phase = EnumPhase.Upkeep;
        Turns[ThisTurn].Step = EnumStep.AssignLeadership;
        Kingdom.ResetFame();
    }

    public void EndUpkeep()
    {

    }

    public EnumCheckResult UseActivity(EnumActivity usedActivity)
    {
        int totalModifier = 0;
        int proficiencyBonus = 0;

        EnumSkills usedSkill = Activity.ActivityList()[usedActivity].RequiredSkill;

        BonusManager BonusList = new BonusManager();

        //Skill Training
        if (Kingdom.SkillTrainings.ContainsKey(usedSkill))
        {
            proficiencyBonus += Skill.TrainingBonus(Kingdom.SkillTrainings[usedSkill]) + Kingdom.KingdomLevel;
        }

        //Invested leaders
        if (Kingdom.SkillHasStatusBonusFromInvestedLeader(usedSkill))
        {
            int bonusFromInvested = 1;
            if (Kingdom.Feats.Contains(EnumFeats.ExperiencedLeadership)) bonusFromInvested = 2;
            if (Kingdom.Feats.Contains(EnumFeats.ExperiencedLeadershipPlus)) bonusFromInvested = 3;

            BonusList.AddBonus(EnumBonusType.StatusBonus, bonusFromInvested);
        }

        //Vacancy Penalities
        if (Kingdom.Leaders[EnumLeaderRole.Ruler].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Ruler) == false)
        {
            BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
        }
        if (Kingdom.Leaders[EnumLeaderRole.Counselor].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Counselor) == false)
        {
            if (Skill.SkillList()[usedSkill].KeyAbility == EnumAbilityScore.Culture)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.Emissary].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Emissary) == false)
        {
            if (Skill.SkillList()[usedSkill].KeyAbility == EnumAbilityScore.Loyalty)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.Treasurer].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Treasurer) == false)
        {
            if (Skill.SkillList()[usedSkill].KeyAbility == EnumAbilityScore.Economy)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.Viceroy].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Viceroy) == false)
        {
            if (Skill.SkillList()[usedSkill].KeyAbility == EnumAbilityScore.Stability)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -1);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.Warden].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Warden) == false)
        {
            if (Activity.ActivityList()[usedActivity].Phase == EnumActivityPhase.Region)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.General].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.General) == false)
        {
            if (Activity.ActivityList()[usedActivity].Phase == EnumActivityPhase.Warfare)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }
        if (Kingdom.Leaders[EnumLeaderRole.Magister].IsVacant && Turns[ThisTurn].LeaderGaveUpActivity.Contains(EnumLeaderRole.Magister) == false)
        {
            if (Activity.ActivityList()[usedActivity].Phase == EnumActivityPhase.Warfare)
            {
                BonusList.AddBonus(EnumBonusType.UntypedPenalty, -4);
            }
        }



        totalModifier += Kingdom.Abilities[Skill.SkillList()[usedSkill].KeyAbility].Modifier();
        totalModifier += proficiencyBonus;
        totalModifier += BonusList.TotalBonus();
        totalModifier -= Kingdom.RuinItemPenalty[Kingdom.RuinCategoryByAbility(Skill.SkillList()[usedSkill].KeyAbility)];

        return Kingdom.KingdomCheck(Kingdom.ControlDC(), totalModifier);
    }

    //public void Activity(EnumActivity activity)
    //{
    //    if (activity == EnumActivity.NewLeadershipPolitics)
    //    {
            
    //    }
    //}



    public void DeclareVacantLeader(EnumLeaderRole role, bool sacrificeActivity)
    {
        if (Turns[ThisTurn].Phase != EnumPhase.Upkeep) throw new Exception("You're not in the 'Upkeep' phase.");
        if (Turns[ThisTurn].Step != EnumStep.AssignLeadership) { throw new Exception("You're not at the 'Assign Leadership Roles' step."); }

        //This means this step is finished
        if (role == EnumLeaderRole.None)
        {            
            Turns[ThisTurn].Step = EnumStep.AssignLeadership;
            return;
        }

        Kingdom.MakeLeaderVacant(role);
        if(sacrificeActivity)
        {
            Turns[ThisTurn].LeaderGaveUpActivity.Add(role);
        }
    }
}

public class Turn
{
    public int TurnID { get; set; }
    public EnumPhase Phase { get; set; }
    public EnumStep Step { get; set; }

    public Turn(int turnID, EnumPhase phase = EnumPhase.Upkeep, EnumStep step = EnumStep.AssignLeadership)
    {
        TurnID = turnID;
        Phase = phase;
        Step = step;
        LeaderGaveUpActivity = new List<EnumLeaderRole>();
    }

    public List<EnumLeaderRole> LeaderGaveUpActivity { get; set; } //To counteract Vacancy Penality.
}