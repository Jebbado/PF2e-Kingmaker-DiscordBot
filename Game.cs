
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
        Turns[TurnID].Step = EnumStep.FirstVillage; 
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