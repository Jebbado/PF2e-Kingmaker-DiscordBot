
using System;

public enum EnumCheckResult
{ 
    Success,
    CritSuccess,
    Failure,
    CritFailure
}

public class Utility
{
            

    public static EnumCheckResult MakeCheck(int DC, int modifier = 0)
    {
        EnumCheckResult returnedResult;

        int diceResult = DiceRoller.RollDice(1, 20);
        int totalResult = diceResult + modifier;
        if (totalResult >= DC)
        {
            returnedResult = EnumCheckResult.Success;
            if (totalResult >= DC + 10) { returnedResult = EnumCheckResult.CritSuccess; }
        }
        else
        {
            returnedResult = EnumCheckResult.Failure;
            if (totalResult <= DC - 10) { returnedResult = EnumCheckResult.CritFailure; }
        }

        if (diceResult == 20)
        {
            switch (returnedResult)
            {
                case EnumCheckResult.CritFailure:
                    returnedResult = EnumCheckResult.Failure;
                    break;
                case EnumCheckResult.Failure:
                    returnedResult = EnumCheckResult.Success;
                    break;
                case EnumCheckResult.Success:
                    returnedResult = EnumCheckResult.CritSuccess;
                    break;
                case EnumCheckResult.CritSuccess:
                    returnedResult = EnumCheckResult.CritSuccess;
                    break;
            }
        }
            
        if (diceResult == 1)
        {
                switch (returnedResult)
                {
                    case EnumCheckResult.CritFailure:
                        returnedResult = EnumCheckResult.CritFailure;
                        break;
                    case EnumCheckResult.Failure:
                        returnedResult = EnumCheckResult.CritFailure;
                        break;
                    case EnumCheckResult.Success:
                        returnedResult = EnumCheckResult.Failure;
                        break;
                    case EnumCheckResult.CritSuccess:
                        returnedResult = EnumCheckResult.Success;
                        break;
                }
            }
         
        return returnedResult;
            
    }

    public static EnumCheckResult MakeFlatCheck(int DC)
    {
        return MakeCheck(DC, 0);
    }
}

public class DiceRoller
{
    private static Random random = new Random();

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

    public void AddBonus(EnumBonusType bonusType, int bonusValue)
    {
        if (bonusValue == 0) throw new Exception("A zero is not a Bonus.");
        
        BonusList.Add(new Bonus(bonusType, bonusValue));
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
