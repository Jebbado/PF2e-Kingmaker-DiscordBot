
public class Skill
{
    public EnumSkills SkillName
    { get; set; }
    public Ability.EnumAbilityScore KeyAbility
    {
        get; set;
    }

    public static Dictionary<Skill.EnumSkills, Skill> SkillList()
    {
        Dictionary<Skill.EnumSkills, Skill> returnedMap = new Dictionary<Skill.EnumSkills, Skill>();
       
        foreach (Skill.EnumSkills forSkillEnum in (Skill.EnumSkills[])Skill.EnumSkills.GetValues(typeof(Skill.EnumSkills)))
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
                this.KeyAbility = Ability.EnumAbilityScore.Culture;
                break;
            case EnumSkills.Boating:
            case EnumSkills.Exploration:
            case EnumSkills.Industry:
            case EnumSkills.Trade:
                this.KeyAbility = Ability.EnumAbilityScore.Economy;
                break;
            case EnumSkills.Intrigue:
            case EnumSkills.Politics:
            case EnumSkills.Statecraft:
            case EnumSkills.Warfare:
                this.KeyAbility = Ability.EnumAbilityScore.Loyalty;
                break;
            case EnumSkills.Agriculture:
            case EnumSkills.Defense:
            case EnumSkills.Engineering:
            case EnumSkills.Wilderness:
                this.KeyAbility = Ability.EnumAbilityScore.Stability;
                break;
            default: 
                throw new Exception();
        }
    }

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

