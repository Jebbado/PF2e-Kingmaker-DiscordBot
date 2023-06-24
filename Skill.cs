
public class Skill
{
    public EnumSkillList SkillName
    { get; set; }
    public Kingdom.EnumAbilityScore KeyAbility
    {
        get; set;
    }

    public static Dictionary<Skill.EnumSkillList, Skill> SkillList()
    {
        Dictionary<Skill.EnumSkillList, Skill> returnedMap = new Dictionary<Skill.EnumSkillList, Skill>();
       
        foreach (Skill.EnumSkillList forSkillEnum in (Skill.EnumSkillList[])Skill.EnumSkillList.GetValues(typeof(Skill.EnumSkillList)))
        {
            returnedMap[forSkillEnum] = new Skill(forSkillEnum);
        }

        return returnedMap;
    }

    public Skill(EnumSkillList enumSkill)
    {
        this.SkillName = enumSkill;                

        switch(enumSkill)
        {
            case EnumSkillList.Arts:
            case EnumSkillList.Folklore:
            case EnumSkillList.Magic:
            case EnumSkillList.Scholarship:
                this.KeyAbility = Kingdom.EnumAbilityScore.Culture;
                break;
            case EnumSkillList.Boating:
            case EnumSkillList.Exploration:
            case EnumSkillList.Industry:
            case EnumSkillList.Trade:
                this.KeyAbility = Kingdom.EnumAbilityScore.Economy;
                break;
            case EnumSkillList.Intrigue:
            case EnumSkillList.Politics:
            case EnumSkillList.Statecraft:
            case EnumSkillList.Warfare:
                this.KeyAbility = Kingdom.EnumAbilityScore.Loyalty;
                break;
            case EnumSkillList.Agriculture:
            case EnumSkillList.Defense:
            case EnumSkillList.Engineering:
            case EnumSkillList.Wilderness:
                this.KeyAbility = Kingdom.EnumAbilityScore.Stability;
                break;
            default: 
                throw new Exception();
        }
    }

    public enum EnumSkillList
    {
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

