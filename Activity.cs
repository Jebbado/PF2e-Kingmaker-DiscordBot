public enum EnumActivity
{
    None,
    AbandonHexExploration,
    AbandonHexWilderness,
    BuildRoads,
    CapitalInvestment,
    CelebrateHoliday,
    ClaimHexExploration,
    ClaimHexWilderness,
    ClaimHexIntrigue,   //This Skill might not be accepted to Claim Hex. It is not found in KPG20
    ClaimHexMagic,      //This Skill might not be accepted to Claim Hex. It is not found in KPG20
    ClandestineBusiness,
    ClearHexEngineering,
    ClearHexExploration,
    CollectTaxes,
    CraftLuxuries,
    CreateaMasterpiece,
    CreativeSolution,
    Demolish,
    EstablishFarmland,
    EstablishSettlementEngineering,
    EstablishSettlementIndustry,
    EstablishSettlementPolitics,
    EstablishSettlementScholarship,
    EstablishTradeAgreementTrade,
    EstablishTradeAgreementBoating,
    EstablishTradeAgreementMagicMaster,
    EstablishWorkSite,
    FocusedAttention,
    FortifyHex,
    GatherLivestock,
    GoFishing,
    HarvestCrops,
    HireAdventurers,
    ImproveLifestyle,
    Infiltration,
    Irrigation,
    ManageTradeAgreements,
    NewLeadershipIntrigue,
    NewLeadershipPolitics,
    NewLeadershipStatecraft,
    NewLeadershipWarfare,
    PledgeofFealtyIntrigue,
    PledgeofFealtyStatecraft,
    PledgeofFealtyWarfare,
    Prognostication,
    ProvideCare,
    PurchaseCommodities,
    QuellUnrestArts,
    QuellUnrestFolklore,
    QuellUnrestIntrigue,
    QuellUnrestMagic,
    QuellUnrestPolitics,
    QuellUnrestWarfare,
    RelocateCapital,
    RepairReputationCorruption,
    RepairReputationCrime,
    RepairReputationDecay,
    RepairReputationStrife,
    RequestForeignAid,
    RestAndRelaxArts,
    RestAndRelaxBoating,
    RestAndRelaxScholarship,
    RestAndRelaxTrade,
    RestAndRelaxWilderness,
    SendDiplomaticEnvoy,
    SupernaturalSolution,
    TapTreasury,
    TradeCommodities
}

public enum EnumActivityPhase
{
    Upkeep,
    Commerce,
    Leadership,
    Region,
    Civic
}

public class Activity
{
    EnumSkills RequiredSkill { get; }
    EnumActivityPhase Phase { get; }

    public Activity(EnumSkills skill, EnumActivityPhase phase)
    {
        RequiredSkill = skill;
        Phase = phase;
    }

    public static Dictionary<EnumActivity, Activity> ActivityList()
    {
        Dictionary<EnumActivity, Activity> returnedList = new Dictionary<EnumActivity, Activity>();

        returnedList[EnumActivity.AbandonHexExploration] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        returnedList[EnumActivity.AbandonHexWilderness] = new Activity(EnumSkills.Wilderness, EnumActivityPhase.Region);
        returnedList[EnumActivity.BuildRoads] = new Activity(EnumSkills.Engineering, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CapitalInvestment] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CelebrateHoliday] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClaimHexExploration] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClaimHexWilderness] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClaimHexIntrigue] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClaimHexMagic] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClandestineBusiness] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClearHexEngineering] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ClearHexExploration] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CollectTaxes] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CraftLuxuries] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CreateaMasterpiece] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.CreativeSolution] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.Demolish] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishFarmland] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishSettlementEngineering] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishSettlementIndustry] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishSettlementPolitics] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishSettlementScholarship] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishTradeAgreementTrade] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishTradeAgreementBoating] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishTradeAgreementMagicMaster] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.EstablishWorkSite] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.FocusedAttention] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.FortifyHex] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.GatherLivestock] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.GoFishing] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.HarvestCrops] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.HireAdventurers] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ImproveLifestyle] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.Infiltration] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.Irrigation] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ManageTradeAgreements] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.NewLeadershipIntrigue] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.NewLeadershipPolitics] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.NewLeadershipStatecraft] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.NewLeadershipWarfare] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.PledgeofFealtyIntrigue] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.PledgeofFealtyStatecraft] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.PledgeofFealtyWarfare] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.Prognostication] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.ProvideCare] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.PurchaseCommodities] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestArts] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestFolklore] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestIntrigue] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestMagic] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestPolitics] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.QuellUnrestWarfare] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RelocateCapital] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RepairReputationCorruption] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RepairReputationCrime] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RepairReputationDecay] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RepairReputationStrife] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RequestForeignAid] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RestAndRelaxArts] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RestAndRelaxBoating] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RestAndRelaxScholarship] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RestAndRelaxTrade] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.RestAndRelaxWilderness] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.SendDiplomaticEnvoy] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.SupernaturalSolution] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.TapTreasury] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);
        //returnedList[EnumActivity.TradeCommodities] = new Activity(EnumSkills.Exploration, EnumActivityPhase.Region);


        return returnedList;
    }
}