public enum EnumCommodity
{
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

    public void AddCommodity(int addedAmount, ref Kingdom kingdom)
    {
        Amount = Math.Min(Amount + addedAmount, Storage(ref kingdom));
    }

    public int Storage(ref Kingdom kingdom)
    {
        int returnedStorage;

        returnedStorage = KingdomSizeCommodityStorageModifier(ref kingdom);

        //TODO : Amount of storage from buildings affecting the current commodity

        return returnedStorage;
    }  

    public int KingdomSizeCommodityStorageModifier(ref Kingdom kingdom)
    {
        if (1 <= kingdom.KingdomSize() || kingdom.KingdomSize() <= 9)
        { return 4; }
        else if (10 < kingdom.KingdomSize() || kingdom.KingdomSize() < 24)
        { return 8; }
        else if (25 < kingdom.KingdomSize() || kingdom.KingdomSize() < 49)
        { return 12; }
        else if (50 < kingdom.KingdomSize() || kingdom.KingdomSize() < 99)
        { return 16; }
        else if (kingdom.KingdomSize() > 100)
        { return 20; }
        else { throw new ArgumentOutOfRangeException(); }
    }
}

