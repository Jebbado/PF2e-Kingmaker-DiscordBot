public enum EnumSkills
{
    None,
    Agriculture,
    Arts,
    Boating,
    Defense,
    Engineering,
    Exploration,
    Folklore,
    Industry,
    Intrigue,
    Magic,
    Politics,
    Scholarship,
    Statecraft,
    Trade,
    Warfare,
    Wilderness
}

public enum EnumSkillTraining
{
    None,
    Trained,
    Expert,
    Master,
    Legendary
}

public class Skill
{
    public EnumSkills SkillName
    { get; set; }
    public EnumAbilityScore KeyAbility
    {
        get; set;
    }

    public static Dictionary<EnumSkills, Skill> SkillList()
    {
        Dictionary<EnumSkills, Skill> returnedMap = new Dictionary<EnumSkills, Skill>();
       
        foreach (EnumSkills forSkillEnum in (EnumSkills[])EnumSkills.GetValues(typeof(EnumSkills)))
        {
            returnedMap[forSkillEnum] = new Skill(forSkillEnum);
        }

        return returnedMap;
    }

    public Skill(EnumSkills enumSkill)
    {
        this.SkillName = enumSkill;                

        switch(enumSkill)
        {
            case EnumSkills.Arts:
            case EnumSkills.Folklore:
            case EnumSkills.Magic:
            case EnumSkills.Scholarship:
                this.KeyAbility = EnumAbilityScore.Culture;
                break;
            case EnumSkills.Boating:
            case EnumSkills.Exploration:
            case EnumSkills.Industry:
            case EnumSkills.Trade:
                this.KeyAbility = EnumAbilityScore.Economy;
                break;
            case EnumSkills.Intrigue:
            case EnumSkills.Politics:
            case EnumSkills.Statecraft:
            case EnumSkills.Warfare:
                this.KeyAbility = EnumAbilityScore.Loyalty;
                break;
            case EnumSkills.Agriculture:
            case EnumSkills.Defense:
            case EnumSkills.Engineering:
            case EnumSkills.Wilderness:
                this.KeyAbility = EnumAbilityScore.Stability;
                break;
            default: 
                throw new Exception();
        }
    }   

    public static int TrainingBonus(EnumSkillTraining skillTraining)
    {
        switch(skillTraining)
        { 
            case EnumSkillTraining.None: return 0;
            case EnumSkillTraining.Trained: return 2;
            case EnumSkillTraining.Expert: return 4;
            case EnumSkillTraining.Master: return 6;
            case EnumSkillTraining.Legendary: return 8;
            default: throw new NotImplementedException();
        }
    }
}

