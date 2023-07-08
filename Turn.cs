
public enum EnumPhase
{
    None,
    Creation,
    Upkeep,
    Commerce,
    Activity,
    Event,
    Warfare,
    GameOver // :(
}

public enum EnumStep
{
    None,

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
    CollectTaxesRuin,
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

public enum EnumPausedReason
{
    None,
    RuinFromNoRP,
    RuinFromRefuge
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
    public int UpkeepUnrestRuinPoints;
    public bool UpkeepUnrestLostHex;
    public bool CollectedTaxes;
    public int CollectedTaxesBonus;
    public EnumPhase PausedPhase = EnumPhase.None;
    public EnumStep PausedStep = EnumStep.None;
    public EnumPausedReason PausedReason = EnumPausedReason.None;
    public bool WentWithoutHex;
    public bool CapturedHex;
    public bool CapturedLandmark;
    public bool CapturedRefuge;

    public void Pause(EnumPausedReason pausedReason)
    {
        PausedPhase = Phase;
        Phase = EnumPhase.None;
        PausedStep = Step;
        Step = EnumStep.None;
        PausedReason = pausedReason;
    }

    public void Unpause()
    {
        Phase = PausedPhase;
        PausedPhase = EnumPhase.None;
        Step = PausedStep;
        PausedStep = EnumStep.None;
        PausedReason = EnumPausedReason.None;
    }
}