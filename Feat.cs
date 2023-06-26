
public class Feat
{
    private EnumFeats FeatName;
    private int RequiredLevel;
    private Skill.EnumSkills SkillPrerequisite;
    private Kingdom.EnumAbilityScore AbilityPrerequisite;

    public enum EnumFeats
    {
        None,
        //LevelUp abilities
        FineLiving,
        LifeOfLuxury,
        ExpansionExpert,
        ExpansionExpertPlus,
        //Feats
        CivilService,
        CooperativeLeadership,
        CrushDissent,
        FortifiedFiefs,
        InsiderTrading,
        KingdomAssurance,
        MuddleThrough,
        PracticalMagic,
        PullTogether,
        SkillTraining,
        EndureAnarchy,
        InspiringEntertainment,
        LiquidateResources,
        QuickRecovery,
        FreeAndFair,
        QualityOfLife,
        FameAndFortune
    }    

    public Feat(EnumFeats name, int level, Skill.EnumSkills skillPrerequisite, Kingdom.EnumAbilityScore abilityPrerequisite)
    { 
        FeatName = name;
        RequiredLevel = level;
        SkillPrerequisite = skillPrerequisite;
        AbilityPrerequisite = abilityPrerequisite;
    }

    public static Dictionary<EnumFeats, Feat> Feats() 
    {
        Dictionary<EnumFeats, Feat> returnedMap = new Dictionary<EnumFeats, Feat>();

        returnedMap[EnumFeats.CivilService]             = new Feat(EnumFeats.CivilService, 1, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.CooperativeLeadership]    = new Feat(EnumFeats.CooperativeLeadership, 1, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.CrushDissent]             = new Feat(EnumFeats.CrushDissent, 1, Skill.EnumSkills.Warfare, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.FortifiedFiefs]           = new Feat(EnumFeats.FortifiedFiefs, 1, Skill.EnumSkills.Defense, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.InsiderTrading]           = new Feat(EnumFeats.InsiderTrading, 1, Skill.EnumSkills.Industry, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.KingdomAssurance]         = new Feat(EnumFeats.KingdomAssurance, 1, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.MuddleThrough]            = new Feat(EnumFeats.MuddleThrough, 1, Skill.EnumSkills.Wilderness, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.PracticalMagic]           = new Feat(EnumFeats.PracticalMagic, 1, Skill.EnumSkills.Magic, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.PullTogether]             = new Feat(EnumFeats.PullTogether, 1, Skill.EnumSkills.Politics, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.SkillTraining]            = new Feat(EnumFeats.SkillTraining, 1, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.EndureAnarchy]            = new Feat(EnumFeats.EndureAnarchy, 3, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.Loyalty);
        returnedMap[EnumFeats.InspiringEntertainment]   = new Feat(EnumFeats.InspiringEntertainment, 3, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.Culture);
        returnedMap[EnumFeats.LiquidateResources]       = new Feat(EnumFeats.LiquidateResources, 3, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.Economy);
        returnedMap[EnumFeats.QuickRecovery]            = new Feat(EnumFeats.QuickRecovery, 3, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.Stability);
        returnedMap[EnumFeats.FreeAndFair]              = new Feat(EnumFeats.FreeAndFair, 7, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.QualityOfLife]            = new Feat(EnumFeats.QualityOfLife, 7, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);
        returnedMap[EnumFeats.FameAndFortune]           = new Feat(EnumFeats.FameAndFortune, 11, Skill.EnumSkills.None, Kingdom.EnumAbilityScore.None);

        return returnedMap;
    }
}

