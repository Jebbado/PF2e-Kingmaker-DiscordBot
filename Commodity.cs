public enum EnumCommodity
{
    None,
    Food,
    Lumber,
    Stone,
    Ore,
    Luxuries
}

public class Commodity
{
    public EnumCommodity Name { get; set; }
    public int Amount;

    public Commodity(EnumCommodity name, int amount = 0) 
    {
        Name = name;
        Amount = amount;
    }    
}

