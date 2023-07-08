
public enum EnumPhase
{
    Creation,
    Upkeep,
    Commerce,
    Activity,
    Event,
    Warfare
}

public enum EnumStep
{
    //Creation
    ChooseCharter,
    ChooseHeartland,
    ChooseGovernment,
    FinalizeAbilityScores,
    ChooseLeaders,
    FirstVillage,
    ChooseFameInfamy,

    //Upkeep
    AssignLeadership,
    AdjustUnrest,
    RessourceCollection,
    PayConsumption,

    //Commerce
    CollectTaxes,
    ApproveExpenses,
    TapCommodities,
    ManageTradeAgreements,

    //Activity
    Leadership,
    Region,
    Civic,

    //Event
    CheckEvent,
    EventResolution,
    ApplyXP,
    LevelUp
}

public class Game
{
    public int GameID;
    public Kingdom Kingdom;
    

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