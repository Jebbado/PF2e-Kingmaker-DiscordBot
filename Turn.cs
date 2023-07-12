
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
    LevelUp,

    //End
    Ingratiate
}

public enum EnumPausedReason
{
    None,
    RuinDown,
    RuinUp,
    ChoiceFromManageTrade,
    Ingratiate
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
        LeadersGivingUpActivity = new List<EnumLeaderRole>();
        LovedNewLeaders = new List<EnumLeaderRole>();
        RejectedNewLeaders = new List<EnumLeaderRole>();
        RejectedNewLeadersRetried = new List<EnumLeaderRole>();
        NewLeadersIngratiateTried = new List<EnumLeaderRole>();
    }

    public bool WentWithoutHex;
    public int NextTurnBonusDice;
    public int NextTurnBonusRP;
    public int NextTurnBonusCommodity;
    public int NextTurnBonusCommodityMaxLuxury;

    //Upkeep Phase
    public List<EnumLeaderRole> LeadersGivingUpActivity { get; set; } //To counteract Vacancy Penality.
    public List<EnumLeaderRole> LovedNewLeaders { get; set; }
    public List<EnumLeaderRole> RejectedNewLeaders { get; set; }
    public List<EnumLeaderRole> RejectedNewLeadersRetried { get; set; }
    public List<EnumLeaderRole> NewLeadersIngratiateTried { get; set; }
    public bool IngratiateEnded;
    public int UpkeepUnrestRuinPoints;
    public bool UpkeepUnrestLostHex;
    public bool CollectedTaxes;
    public int CollectedTaxesBonus;

    //Commerce Phase
    public int ImprovedLifestyleBonus;
    public int ImprovedLifestyleMalus;
    public bool TapTreasurySuccessMalus;
    public bool TapTreasuryFailureMalus;
    public bool TradedCommodity;
    public bool ManagedTradeAgreement;
    public int ManagedTradeAgreementAmount;
    public bool CritFailedManagedTradeAgreement;

    //Activity Phase
        //Leadership
    public Dictionary<EnumLeaderRole, List<EnumActivity>> LeadershipActivities = new Dictionary<EnumLeaderRole, List<EnumActivity>>();


    //Region
    public int RegionActivitiesUsed = 0;
    public bool UsedFavoredLand = false;

    public bool CapturedHex;
    public bool CapturedLandmark;
    public bool CapturedRefuge;

    //End
    public bool LeveledUp;

    public EnumPhase PausedPhase = EnumPhase.None;
    public EnumStep PausedStep = EnumStep.None;
    public EnumPausedReason PausedReason = EnumPausedReason.None;
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