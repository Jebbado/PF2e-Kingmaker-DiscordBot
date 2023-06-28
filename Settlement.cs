
public enum EnumSettlementType
{
    None,
    Village,
    Town,
    City,
    Metropolis
}

public class Settlement
{
    public string Name { get; set; }
    public bool IsCapital { get; set; }
    public Hex SettlementHex { get; set; }
    public EnumSettlementType SettlementType { get; set; }

    private List<UrbanGrid> UrbanGrids = new List<UrbanGrid>();

    public Settlement(string name, Hex containerHex) 
    {
        Name = name;
        SettlementHex = containerHex;
    }
}

public class UrbanGrid
{
    private List<Block> Blocks = new List<Block>();

    public UrbanGrid()
    {
        
    }
}
public class Block
{
    private List<Lot> Lots = new List<Lot>();

    public Block()
    {
        
    }
}
public class Lot
{
    public Lot()
    {

    }
}