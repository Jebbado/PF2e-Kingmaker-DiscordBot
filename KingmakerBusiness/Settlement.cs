
using PF2e_Kingmaker_Bot.KingmakerBusiness;
using System.Collections.Generic;

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
    public int IDSettlement { get; set; }
    public int IDKingdom { get; set; }
    public int IDSettlementType { get; set; }
    public int IDHex { get; set; }
    public virtual Hex IDHexNavigation { get; set; } = null!;
    public virtual Kingdom IDKingdomNavigation { get; set; } = null!;
    public virtual SettlementType IDSettlementTypeNavigation { get; set; } = null!;

    public string Name { get; set; }
    public bool IsCapital { get; set; }
    public Hex SettlementHex { get; set; }
    public EnumSettlementType SettlementType { get; set; }

    private Dictionary<int, UrbanGrid> UrbanGrids = new Dictionary<int, UrbanGrid>();
    private Dictionary<EnumSettlementSide, EnumSettlementSideFeature> Sides = new Dictionary<EnumSettlementSide, EnumSettlementSideFeature>(); //Might be a list of features for each side, still unclear.

    public virtual ICollection<Structure> Structures { get; set; } = new List<Structure>();

    private Kingdom kingdom;
    

    public Settlement(Kingdom kingdom, string name, Hex containerHex, bool isCapital = false) 
    {
        Name = name;
        SettlementHex = containerHex;
        IsCapital = isCapital;
        this.kingdom = kingdom;
    }

    public void PlaceStructure(EnumStructure structure, int blockNumber, bool isUpgrade = false, int gridNumber = 1) 
    {
        Structure placedStructure = Structure.StructureList()[structure];

        if (placedStructure.StructureType == EnumStructureType.Infrastructure)
        {
            if( ! UrbanGrids.ContainsKey(gridNumber))
            {
                throw new Exception("You can't build infrastructure in this Urban Grid yet.");
            }
            if(UrbanGrids[gridNumber].Infrastructures.Contains(structure))
            {
                throw new Exception("This Infrastructure is already present in this Urban Grid.");
            }
            UrbanGrids[gridNumber].Infrastructures.Add(structure);
            return;
        }
        else
        {
            int alreadyOccupiedLots = 0;
            if(UrbanGrids.ContainsKey(gridNumber))
            {
                alreadyOccupiedLots = UrbanGrids[gridNumber].Blocks[blockNumber].OccupiedLots();
            }
            if (ReadyForNextType() && alreadyOccupiedLots == 0)
            {
                if(SettlementType == EnumSettlementType.Metropolis)
                {
                    UrbanGrids[gridNumber] = new UrbanGrid();
                }
                SettlementType = NextType();
            }

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
        switch(SettlementType)
        {
            case EnumSettlementType.Village : return 1;
            case EnumSettlementType.Town : return 2;
            case EnumSettlementType.City: return 4;
            case EnumSettlementType.Metropolis: return 6;
            default: throw new Exception("Consumption can't be determined. Settlement type is invalid.");
        }
    }

    public int Influence()
    {
        switch (SettlementType)
        {
            case EnumSettlementType.Village: return 0;
            case EnumSettlementType.Town: return 1;
            case EnumSettlementType.City: return 2;
            case EnumSettlementType.Metropolis: return 3;
            default: throw new Exception("Influence can't be determined. Settlement type is invalid.");
        }
    }

    public int Size()
    {
        switch (SettlementType)
        {
            case EnumSettlementType.Village: return 1;
            case EnumSettlementType.Town: return 4;
            case EnumSettlementType.City: return 9;
            case EnumSettlementType.Metropolis: return 9001;
            default: throw new Exception("Size can't be determined. Settlement type is invalid.");
        }
    }
    
    public bool IsOvercrowded()
    {
        return OccupiedBlocks() > ResidentialAmount();
    }


    public bool ContainsStructure(EnumStructure structure)
    {
        foreach(UrbanGrid forGrid in UrbanGrids.Values)
        {
            foreach (Block forBlock in forGrid.Blocks.Values)
            {
                return forBlock.Structures.Contains(structure);
            }
        }
        return false;
    }

    public List<EnumStructure> AllStructures() 
    {
        List<EnumStructure> AllStructure = new List<EnumStructure>();
        foreach (UrbanGrid forGrid in UrbanGrids.Values)
        {
            foreach (Block forBlock in forGrid.Blocks.Values)
            {
                AllStructure.AddRange(forBlock.Structures);
            }
        }
        return AllStructure;
    }

    public int ResidentialAmount()
    {
        int amount = 0;
        foreach (EnumStructure structure in AllStructures())
        {
            if (Structure.StructureList()[structure].IsResidential) amount++;
        }
        return amount;
    }

    public int OccupiedBlocks()
    {
        int occupiedBlock = 0;
        foreach (UrbanGrid forGrid in UrbanGrids.Values)
        {
            foreach (Block forBlock in forGrid.Blocks.Values)
            {
                if (forBlock.OccupiedLots() > 0) occupiedBlock++;
            }
        }
        return occupiedBlock;
    }

    public bool ReadyForNextType()
    {
        if(IsOvercrowded()) return false;

        switch(SettlementType)
        {
            case EnumSettlementType.Village:
                if (kingdom.KingdomLevel < 3 || UrbanGrids[1].Blocks[1].OccupiedLots() <= UrbanGrids[1].Blocks[1].LotsAmount)
                {
                    return false;
                }
                break;
            case EnumSettlementType.Town:
                if (kingdom.KingdomLevel < 9)
                {
                    return false;
                }                
                break;
            case EnumSettlementType.City:
                if (kingdom.KingdomLevel < 15)
                {
                    return false;
                }                
                break;
            case EnumSettlementType.Metropolis:
                if (kingdom.KingdomLevel < 9)
                {
                    return false;
                }               
                break;
            default: throw new NotImplementedException("This settlement type is unknown.");

        }

        if(SettlementType != EnumSettlementType.Village)
        {
            foreach (UrbanGrid forGrid in UrbanGrids.Values)
            {
                foreach (Block forBlock in forGrid.Blocks.Values)
                {
                    if (forBlock.OccupiedLots() < 2)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public EnumSettlementType NextType()
    {
        switch (SettlementType)
        {
            case EnumSettlementType.Village:
                return EnumSettlementType.Town;
            case EnumSettlementType.Town:
                return EnumSettlementType.City;
            case EnumSettlementType.City:
                return EnumSettlementType.Metropolis;
            case EnumSettlementType.Metropolis:
                return EnumSettlementType.Metropolis;
            default: throw new NotImplementedException("Settlement type is invalid.");
        }
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