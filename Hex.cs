
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
    private int CoordinateX;
    private int CoordinateY;
    private EnumHeartland TerrainType;
    public EnumTerrainFeature TerrainFeature { get; set; }
    public EnumCommodity Ressource { get; }
    //private bool IsReconnoitered;
    public bool InTerritory { get; set; }

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
