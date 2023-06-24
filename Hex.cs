
public class Hex
{
    private int CoordinateX;
    private int CoordinateY;
    private EnumTerrainType TerrainType;
    private bool IsReconnoitered;

    public Hex(int coordinateX, int coordinateY, EnumTerrainType terrainType)
    { 
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        TerrainType = terrainType;
        IsReconnoitered = false;
    }

    public enum EnumTerrainType
    {
        None,
        TEST
    }

    public void Reconnoiter()
    {
        IsReconnoitered=true;
    }
}
