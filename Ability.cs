public enum EnumAbilityScore
{
    None,
    Culture,
    Economy,
    Loyalty,
    Stability
}

public class Ability
{
    private EnumAbilityScore Name;
    private int Score { get; set; }

    public Ability(EnumAbilityScore name, int value = 10)
    {
        Name = name;
        Score = value;
    }

    

    public void BoostAbility(bool isFlaw = false)
    {
        if (Score >= 18)
        {
            if (isFlaw) { throw new Exception("You can't have a flaw after Kingdom creation."); }
            Score += 1;
        }
        else
        {
            Score += 2 * (isFlaw ? -1 : 1);
        }        
    }

    public int Modifier()
    {
        return (Score - 10) / 2;
    }
}

