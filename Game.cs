
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
    public int TurnID = 0;
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
        Turns[TurnID] = new Turn(TurnID, EnumPhase.Creation, EnumStep.ChooseCharter);
    }

    public void ChooseCharter(EnumCharter enumCharter)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You can't change your Charter after Kingdom creation."); }
        if (Turns[TurnID].Step != EnumStep.ChooseCharter) { throw new Exception("You're not at the 'Select a Charter' step right now."); }

        Kingdom.AssignCharter(enumCharter);
        Turns[TurnID].Step = EnumStep.ChooseHeartland;
    }

    public void ChooseHeartland(EnumHeartland enumHeartland)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You can't change your Heartland after Kingdom creation."); }
        if (Turns[TurnID].Step != EnumStep.ChooseHeartland) { throw new Exception("You're not at the 'Choose a Heartland' step right now."); }

        Kingdom.AssignHeartland(enumHeartland);
        Turns[TurnID].Step = EnumStep.ChooseGovernment;
    }

    public void ChooseGovernment(EnumGovernment enumGovernment)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You can't change Government after Kingdom creation."); }
        if (Turns[TurnID].Step != EnumStep.ChooseGovernment) { throw new Exception("You're not at the 'Choose a Government' step right now."); }

        Kingdom.AssignGovernment(enumGovernment);
        Turns[TurnID].Step = EnumStep.FinalizeAbilityScores;
    }

    public void FinalizeAbilityScore(EnumAbilityScore ab1, EnumAbilityScore ab2, EnumAbilityScore ab3, EnumAbilityScore ab4)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You can't change Ability Scores after Kingdom creation."); }
        if (Turns[TurnID].Step != EnumStep.FinalizeAbilityScores) { throw new Exception("You're not at the 'Finalize Ability Score' step right now."); }
        
        Kingdom.FinalizeAbilityScore(ab1, ab2, ab3, ab4);
        Turns[TurnID].Step = EnumStep.ChooseLeaders;
    }

    public void AddLeader(string name, EnumRole role, bool invested, bool isPC)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You can't add Leaders after Kingdom creation. You should use the 'New Leadership' Activity."); }
        if (Turns[TurnID].Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose a Government' step right now."); }

        Kingdom.AddLeader(new Leader(name, role, invested, isPC));  
    }

    public void EndLeaderStep(string name) 
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[TurnID].Step != EnumStep.ChooseLeaders) { throw new Exception("You're not at the 'Choose Leaders' step."); }

        if(Kingdom.InvestedLeadersCount() < 4) { throw new Exception("You must have 4 Invested Leaders before ending"); }

        Turns[TurnID].Step = EnumStep.FirstVillage; 
    }

    public void CreateCapital(string name, EnumStructure initialStructure = EnumStructure.None)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[TurnID].Step != EnumStep.FirstVillage) { throw new Exception("You're not at the 'Forst Village' step."); }

        Kingdom.AddSettlement(name, 0, 0, true);
        if (initialStructure != EnumStructure.None) 
        { 
            Kingdom.CapitalSettlement().PlaceStructure(initialStructure, 1);
        }

        Turns[TurnID].Step = EnumStep.ChooseFameInfamy;
    }

    public void ChooseFameInfamy(EnumFameAspiration fame)
    {
        if (Turns[TurnID].Phase != EnumPhase.Creation) { throw new Exception("You're not in the 'Kingdom Creation' phase."); }
        if (Turns[TurnID].Step != EnumStep.ChooseFameInfamy) { throw new Exception("You're not at the 'Fame or Infamy ?' step."); }

        Kingdom.ChooseFame(fame);
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
    }
}