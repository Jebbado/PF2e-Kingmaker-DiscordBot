
public class Hex
{
    private int CoordinateX;
    private int CoordinateY;
    private Kingdom.EnumHeartland TerrainType;
    //private bool IsReconnoitered;

    public Hex(int coordinateX, int coordinateY, Kingdom.EnumHeartland terrainType)
    { 
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        TerrainType = terrainType;
        //IsReconnoitered = false;
    }

    public enum EnumTerrainType
    {
        None,
        TEST
    }

    public void Reconnoiter()
    {
        //IsReconnoitered=true;
    }
}
