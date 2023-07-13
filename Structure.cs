public enum EnumStructureType
{
    Building,
    Yard,    
    Infrastructure
}

public enum EnumStructure
{
    None,
    Academy,
    AlchemyLaboratory,
    ArcanistTower,
    Arena,
    Bank,
    Barrack,
    Brewery,
    Bridge,
    Castle,
    Cathedral,
    Cemetery,
    ConstructionYard,
    Dump,
    Embassy,
    FestivalHall,
    Foundry,
    Garrison,
    GeneralStore,
    Granary,
    GuildHall,
    Herbalist,
    Houses,
    IllicitMarket,
    Inn,
    Jail,
    Library,
    Lumberyard,
    LuxuryStore,
    MagicShop,
    MagicalStreetlamp,
    Mansion,
    Marketplace,
    Menagerie,
    MilitaryAcademy,
    Mill,
    Mint,
    Monument,
    Museum,
    NobleVilla,
    OccultShop,
    OperaHouse,
    Orphanage,
    Palace,
    Park,
    PavedStreets,
    Pier,
    Rubble,
    SacredGrove,
    SecureWarehouse,
    SewerSystem,
    Shrine,
    Smithy,
    SpecializedArtisan,
    Stable,
    Stockyard,
    Stonemason,
    Tannery,
    TavernPopular,
    TavernLuxury,
    TavernWorldClass,
    Temple,
    Tenement,
    Theatre,
    ThievesGuild,
    TownHall,
    TradeShop,
    University,
    WallStone,
    WallWooden,
    Watchtower,
    Waterfront
}

public class Structure
{
    public EnumStructure Name { get; }
    public EnumStructureType StructureType { get; }
    private int RequiredLevel;
    public int LotsOccupied { get; }
    private int RPCost;
    List<Commodity> CommoditiesCost;
    Dictionary<EnumSkills, EnumSkillTraining> ConstructionSkills;
    private int ConstructionDC;
    public EnumStructure UpgradeFrom { get; }
    private bool IsEdifice;
    private bool IsFamous;
    private bool IsInfamous;
    public bool IsResidential { get; }

    public Structure(EnumStructure name, EnumStructureType type, int level, int lots, int rp, List<Commodity>  commodities, Dictionary<EnumSkills, EnumSkillTraining> skills,
                    int dc, EnumStructure upFrom, bool edifice, bool fame, bool infamy, bool residential)
    { 
        Name = name;
        StructureType = type;
        RequiredLevel = level;
        LotsOccupied = lots;
        RPCost = rp;
        CommoditiesCost = commodities;
        ConstructionSkills = skills;
        ConstructionDC = dc;
        UpgradeFrom = upFrom;        
        IsEdifice = edifice;
        IsFamous = fame;
        IsInfamous = infamy;
        IsResidential = residential;
    }

    public static Dictionary<EnumStructure, Structure> StructureList()
    {
        Dictionary < EnumStructure, Structure > returnedList = new Dictionary <EnumStructure, Structure>();

        returnedList[EnumStructure.Academy] = 
            new Structure(EnumStructure.Academy, EnumStructureType.Building, 10, 2, 52, 
                            new List<Commodity>() {new Commodity(EnumCommodity.Lumber, 12), new Commodity(EnumCommodity.Luxuries, 6), new Commodity(EnumCommodity.Stone, 12) }, 
                            new Dictionary<EnumSkills, EnumSkillTraining>() { {EnumSkills.Scholarship, EnumSkillTraining.Expert} }, 
                            27, EnumStructure.Library, true, false, false, false);

        returnedList[EnumStructure.AlchemyLaboratory] =
            new Structure(name:EnumStructure.AlchemyLaboratory, type:EnumStructureType.Building, level:3, lots:1, rp:18,
                            commodities:new List<Commodity>() { new Commodity(EnumCommodity.Ore, 2), new Commodity(EnumCommodity.Stone, 5) },
                            skills:new Dictionary<EnumSkills, EnumSkillTraining>() { { EnumSkills.Industry, EnumSkillTraining.Trained } },
                            dc:16, upFrom:EnumStructure.None, edifice:false, fame:false, infamy:false, residential:false);

        //returnedList[EnumStructure.ArcanistTower] = new Structure();
        //returnedList[EnumStructure.Arena] = new Structure();
        //returnedList[EnumStructure.Bank] = new Structure();
        //returnedList[EnumStructure.Barrack] = new Structure();
        //returnedList[EnumStructure.Brewery] = new Structure();
        //returnedList[EnumStructure.Bridge] = new Structure();
        //returnedList[EnumStructure.Castle] = new Structure();
        //returnedList[EnumStructure.Cathedral] = new Structure();
        //returnedList[EnumStructure.Cemetery] = new Structure();
        //returnedList[EnumStructure.ConstructionYard] = new Structure();
        //returnedList[EnumStructure.Dump] = new Structure();
        //returnedList[EnumStructure.Embassy] = new Structure();
        //returnedList[EnumStructure.FestivalHall] = new Structure();
        //returnedList[EnumStructure.Foundry] = new Structure();
        //returnedList[EnumStructure.Garrison] = new Structure();
        //returnedList[EnumStructure.GeneralStore] = new Structure();
        //returnedList[EnumStructure.Granary] = new Structure();
        //returnedList[EnumStructure.GuildHall] = new Structure();
        //returnedList[EnumStructure.Herbalist] = new Structure();
        //returnedList[EnumStructure.Houses] = new Structure();
        //returnedList[EnumStructure.IllicitMarket] = new Structure();
        //returnedList[EnumStructure.Inn] = new Structure();
        //returnedList[EnumStructure.Jail] = new Structure();
        //returnedList[EnumStructure.Library] = new Structure();
        //returnedList[EnumStructure.Lumberyard] = new Structure();
        //returnedList[EnumStructure.LuxuryStore] = new Structure();
        //returnedList[EnumStructure.MagicShop] = new Structure();
        //returnedList[EnumStructure.MagicalStreetlamp] = new Structure();
        //returnedList[EnumStructure.Mansion] = new Structure();
        //returnedList[EnumStructure.Marketplace] = new Structure();
        //returnedList[EnumStructure.Menagerie] = new Structure();
        //returnedList[EnumStructure.MilitaryAcademy] = new Structure();
        //returnedList[EnumStructure.Mill] = new Structure();
        //returnedList[EnumStructure.Mint] = new Structure();
        //returnedList[EnumStructure.Monument] = new Structure();
        //returnedList[EnumStructure.Museum] = new Structure();
        //returnedList[EnumStructure.NobleVilla] = new Structure();
        //returnedList[EnumStructure.OccultShop] = new Structure();
        //returnedList[EnumStructure.OperaHouse] = new Structure();
        //returnedList[EnumStructure.Orphanage] = new Structure();
        //returnedList[EnumStructure.Palace] = new Structure();
        //returnedList[EnumStructure.Park] = new Structure();
        //returnedList[EnumStructure.PavedStreets] = new Structure();
        //returnedList[EnumStructure.Pier] = new Structure();
        //returnedList[EnumStructure.Rubble] = new Structure();
        //returnedList[EnumStructure.SacredGrove] = new Structure();
        //returnedList[EnumStructure.SecureWarehouse] = new Structure();
        //returnedList[EnumStructure.SewerSystem] = new Structure();
        //returnedList[EnumStructure.Shrine] = new Structure();
        //returnedList[EnumStructure.Smithy] = new Structure();
        //returnedList[EnumStructure.SpecializedArtisan] = new Structure();
        //returnedList[EnumStructure.Stable] = new Structure();
        //returnedList[EnumStructure.Stockyard] = new Structure();
        //returnedList[EnumStructure.Stonemason] = new Structure();
        //returnedList[EnumStructure.Tannery] = new Structure();
        //returnedList[EnumStructure.TavernPopular] = new Structure();
        //returnedList[EnumStructure.TavernLuxury] = new Structure();
        //returnedList[EnumStructure.TavernWorldClass] = new Structure();
        //returnedList[EnumStructure.Temple] = new Structure();
        //returnedList[EnumStructure.Tenement] = new Structure();
        //returnedList[EnumStructure.Theatre] = new Structure();
        //returnedList[EnumStructure.ThievesGuild] = new Structure();
        //returnedList[EnumStructure.TownHall] = new Structure();
        //returnedList[EnumStructure.TradeShop] = new Structure();
        //returnedList[EnumStructure.University] = new Structure();
        //returnedList[EnumStructure.WallStone] = new Structure();
        //returnedList[EnumStructure.WallWooden] = new Structure();
        //returnedList[EnumStructure.Watchtower] = new Structure();
        //returnedList[EnumStructure.Waterfront] = new Structure();


        return returnedList;
    }
}

