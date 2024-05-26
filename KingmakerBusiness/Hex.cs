
using PF2e_Kingmaker_Bot.KingmakerBusiness;

public enum EnumTerrainFeature
{
    None,
    Bridge,
    Farmland,
    Freehold,
    Landmark,
    Refuge,
    Resource,
    Ruins,
    Settlement,
    Structure,
    WorkSite,
    RessourceWorksite
}

public class Hex
{
    public int IDHex { get; set; }
    public int IDKingdom { get; set; }
    private int CoordinateX;
    private int CoordinateY;
    private EnumHeartland TerrainType;
    public EnumTerrainFeature TerrainFeature { get; set; }
    public EnumCommodity Ressource { get; }
    //private bool IsReconnoitered;
    public bool InTerritory { get; set; }

    public int IDHeartland { get; set; }
    public int IDTerrainFeature { get; set; }
    public int? IDRessourceType { get; set; }

    public virtual Heartland IDHeartlandNavigation { get; set; } = null!;
    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;
    public virtual RessourceType? IDRessourceTypeNavigation { get; set; }
    public virtual TerrainFeature IDTerrainFeatureNavigation { get; set; } = null!;
    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();

    public Hex(int coordinateX, int coordinateY, EnumHeartland terrainType, EnumTerrainFeature feature = EnumTerrainFeature.None, EnumCommodity ressource = EnumCommodity.None)
    { 
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        TerrainType = terrainType;
        Ressource = EnumCommodity.None;
        TerrainFeature = feature;
        Ressource = ressource;
        InTerritory = true;
        //IsReconnoitered = false;
    }

    public void Reconnoiter()
    {
        //IsReconnoitered=true;
    }

    public string Key()
    {
        return CoordinateX + ":" + CoordinateY;
    }
}
