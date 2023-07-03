public enum EnumTerrainType
{
    None,
    TEST
}

public enum EnumTerrainFeature
{
    None,
    Bridge,
    Farmland,
    Freeholf,
    Landmark,
    Refuge,
    ResourceLumber,
    ResourceOre,
    ResourceStone,
    Ruins,
    Settlement,
    Structure,
    WorkSite
}

public class Hex
{
    private int CoordinateX;
    private int CoordinateY;
    private EnumHeartland TerrainType;
    public EnumTerrainFeature TerrainFeature { get; set; }
    //private bool IsReconnoitered;

    public Hex(int coordinateX, int coordinateY, EnumHeartland terrainType)
    { 
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        TerrainType = terrainType;
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
