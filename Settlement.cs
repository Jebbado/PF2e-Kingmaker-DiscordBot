
public enum EnumSettlementType
{
    None,
    Village,
    Town,
    City,
    Metropolis
}

public enum EnumSettlementSide
{
    None,
    North,
    South,
    East,
    West
}

public enum EnumSettlementSideFeature
{
    None,
    Water,
    Bridge,
    WallWood,
    WallStone
}

public class Settlement
{
    public string Name { get; set; }
    public bool IsCapital { get; set; }
    public Hex SettlementHex { get; set; }
    public EnumSettlementType SettlementType { get; set; }

    private Dictionary<int, UrbanGrid> UrbanGrids = new Dictionary<int, UrbanGrid>();
    private Dictionary<EnumSettlementSide, EnumSettlementSideFeature> Sides = new Dictionary<EnumSettlementSide, EnumSettlementSideFeature>(); //Might be a list of features for each side, still unclear.
    

    public Settlement(string name, Hex containerHex) 
    {
        Name = name;
        SettlementHex = containerHex;
    }

    public void PlaceStructure(EnumStructure structure, int blockNumber, bool isUpgrade = false, int gridNumber = 1) 
    {
        Structure placedStructure = Structure.StructureList()[structure];

        if (placedStructure.StructureType == EnumStructureType.Infrastructure)
        {
            if(UrbanGrids[gridNumber].Infrastructures.Contains(structure))
            {
                throw new Exception("This Infrastructure is already present in this Urban Grid.");
            }
            UrbanGrids[gridNumber].Infrastructures.Add(structure);
            return;
        }
        else
        {
            int alreadyOccupiedLots = UrbanGrids[gridNumber].Blocks[blockNumber].OccupiedLots();
            int placedStructureOccupiedLots = placedStructure.LotsOccupied;
            if (isUpgrade)
            {
                placedStructureOccupiedLots -= Structure.StructureList()[placedStructure.UpgradeFrom].LotsOccupied;
                
            }
            if (alreadyOccupiedLots + placedStructureOccupiedLots > UrbanGrids[gridNumber].Blocks[blockNumber].LotsAmount)
            {
                throw new Exception("There are not enough lots left to build this Structure in this Block.");
            }
            UrbanGrids[gridNumber].Blocks[blockNumber].Structures.Add(structure);
        }
    }

    public int Consumption()
    {
        int consumption = 0;



        return consumption;
    }

    public int Influence()
    {
        int influence = 0;



        return influence;
    }
}

public class UrbanGrid
{
    public Dictionary<int, Block> Blocks = new Dictionary<int, Block>();
    public List<EnumStructure> Infrastructures = new List<EnumStructure>();

    public UrbanGrid()
    {
        
    }
}
public class Block
{
    //private List<Lot> Lots = new List<Lot>();
    public int LotsAmount { get; }
    public List<EnumStructure> Structures = new List<EnumStructure>();

    public Block(int lotsAmount = 4)
    {
        LotsAmount = lotsAmount;
    }

    public int OccupiedLots()
    { 
        int occupiedLots = 0;

        foreach (EnumStructure structure in Structures)
        {
            occupiedLots += Structure.StructureList()[structure].LotsOccupied;
        }

        return occupiedLots; 
    }
}
//public class Lot
//{
//    public Lot()
//    {
        
//    }
//}