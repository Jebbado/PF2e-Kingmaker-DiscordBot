
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
            if (totalResult >= DC + 10) { returnedResult = EnumCheckResult.CritSuccess; }
            returnedResult = EnumCheckResult.Success;
        }
        else
        {
            if (totalResult <= DC - 10) { returnedResult = EnumCheckResult.CritFailure; }
            returnedResult = EnumCheckResult.Failure;
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
