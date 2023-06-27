public enum EnumRole
{
    None,
    Ruler,
    Warden,
    Viceroy,
    Treasurer,
    Magister,
    Emissary,
    General,
    Counselor
}

public class Leader
{
    private string Name;
    private string SomeKey; //Link with Discord ID to know which member is the Leader. Hypothetical futur usage.
    private EnumRole Role;
    private bool Invested;        

    public Leader(string name, EnumRole role, bool invested)
    {
        if (name == null || name == "") throw new Exception("Name must not be empty.");

        if (role == EnumRole.None && invested) throw new Exception("A leader without a Role can't be Invested.");

        this.Name = name;
        this.Role = role;
        this.Invested = invested;
        this.SomeKey = "";
    }

    public string getName()
    { return Name; }
    public string getSomeKey()
    { return SomeKey; }
    public EnumRole getRole()
    { return Role; }
    public bool getInvested()
    { return Invested; }
    
    public static EnumAbilityScore KeyAbilityForRole(EnumRole role)
    {
        switch(role) 
        {
            case EnumRole.Counselor:
            case EnumRole.Magister:
                return EnumAbilityScore.Culture;
            case EnumRole.Treasurer:
            case EnumRole.Viceroy:
                return EnumAbilityScore.Economy;
            case EnumRole.Ruler:
            case EnumRole.Emissary:
                return EnumAbilityScore.Loyalty;
            case EnumRole.General:
            case EnumRole.Warden:
                return EnumAbilityScore.Stability;
            default: 
                throw new Exception("This leader's Role has no Key Ability.");
        }
    }

}