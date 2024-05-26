
using System;

public enum EnumCheckResult
{ 
    Success,
    CritSuccess,
    Failure,
    CritFailure
}

public class Check
{
            

    public static EnumCheckResult MakeCheck(int DC, int modifier = 0)
    {
        EnumCheckResult returnedResult;

        int diceResult = DiceRoller.RollDice(1, 20);
        int totalResult = diceResult + modifier;
        if (totalResult >= DC)
        {
            returnedResult = EnumCheckResult.Success;
            if (totalResult >= DC + 10) 
            {
                returnedResult = ImproveCheckResult(returnedResult);
            }
        }
        else
        {
            returnedResult = EnumCheckResult.Failure;
            if (totalResult <= DC - 10) 
            {
                returnedResult = WorsenCheckResult(returnedResult);
            }
        }

        if (diceResult == 20)
        {
            returnedResult = ImproveCheckResult(returnedResult);
        }
            
        if (diceResult == 1)
        {
            returnedResult = WorsenCheckResult(returnedResult);
        }
         
        return returnedResult;
            
    }

    public static bool MakeFlatCheck(int DC)
    {
        EnumCheckResult result = MakeCheck(DC, 0);
        return (result == EnumCheckResult.Success || result == EnumCheckResult.CritSuccess);
        
    }

    public static EnumCheckResult WorsenCheckResult(EnumCheckResult checkResult)
    {
        switch(checkResult)
        {
            case EnumCheckResult.CritSuccess:
                return EnumCheckResult.Success;
            case EnumCheckResult.Success:
                return EnumCheckResult.Failure;
            case EnumCheckResult.Failure:
                return EnumCheckResult.CritFailure;
            case EnumCheckResult.CritFailure:
                return EnumCheckResult.CritFailure;
            default: throw new NotImplementedException("Unknown check result. Can't worsen it.");

        }
    }

    public static EnumCheckResult ImproveCheckResult(EnumCheckResult checkResult)
    {
        switch (checkResult)
        {
            case EnumCheckResult.CritSuccess:
                return EnumCheckResult.CritSuccess;
            case EnumCheckResult.Success:
                return EnumCheckResult.CritSuccess;
            case EnumCheckResult.Failure:
                return EnumCheckResult.Success;
            case EnumCheckResult.CritFailure:
                return EnumCheckResult.Failure;
            default: throw new NotImplementedException("Unknown check result. Can't improve it.");
        }
    }
}

public class DiceRoller
{
    private static Random random = new Random(); //TODO : Verify it's the good way to create a good random.

    public static int RollDice(int numberOfDice, int numberOfSides)
    {
        int total = 0;

        for (int i = 0; i < numberOfDice; i++)
        {
            total += random.Next(1, numberOfSides + 1);
        }

        return total;
    }
}

public enum EnumBonusType
{
    UntypedPenalty,
    StatusBonus,
    StatusPenalty,
    CircumstanceBonus,
    CircumstancePenalty,
    ItemBonus,
    ItemPenalty
}

public class Bonus
{
    public EnumBonusType BonusType { get; }
    public int BonusAmount { get; }
    public bool IsPenality { get; }

    public Bonus(EnumBonusType bonusType, int bonusAmount) 
    {
        if (bonusAmount == 0) throw new Exception("A zero is not a Bonus.");
        BonusType = bonusType; 
        BonusAmount = bonusAmount;
    }
}

public class BonusManager
{
    private List<Bonus> BonusList = new List<Bonus>();

    public BonusManager()
    {

    }

    //If an 
    public BonusManager(BonusManager paramBonusList)
    {

    }    

    public void AddBonus(EnumBonusType bonusType, int bonusValue)
    {                
        BonusList.Add(new Bonus(bonusType, bonusValue));
    }

    public void AddBonus(Bonus bonus)
    {
        BonusList.Add(bonus);
    }

    public int TotalBonus()
    {
        Dictionary<EnumBonusType, int> Bonuses = new Dictionary<EnumBonusType, int>();
        foreach (EnumBonusType enumValue in Enum.GetValues(typeof(EnumBonusType)))
        {
            Bonuses.Add(enumValue, 0);
        }


        foreach (Bonus bonus in BonusList)
        {
            if (bonus.BonusType == EnumBonusType.UntypedPenalty) 
            {
                Bonuses[bonus.BonusType] += bonus.BonusAmount;
                continue;
            }

            if (bonus.BonusAmount > 0)
                Bonuses[bonus.BonusType] = Math.Max(bonus.BonusAmount, Bonuses[bonus.BonusType]);
            else if (bonus.BonusAmount < 0)
                Bonuses[bonus.BonusType] = Math.Min(bonus.BonusAmount, Bonuses[bonus.BonusType]);
        }

        int returnedBonus = 0;
        foreach (int value in Bonuses.Values)
        {
            returnedBonus += value;
        }

        return returnedBonus;
    }
}
