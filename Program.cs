internal class Program
{
    private static void Main(string[] args)
    {
        Tests();
    }

    private static void Tests()
    {
        Kingdom Valancie = new Kingdom("Valancie");
        Valancie.AssignCharter(EnumCharter.Exploration);
        Valancie.AssignHeartland(EnumHeartland.Plain);
        Valancie.AssignGovernment(EnumGovernment.Feudalism);
        Valancie.AddLeader("Sébaste Larivière", EnumLeaderRole.Ruler, true, true);
        Valancie.AddLeader("Jacques Létourneau", EnumLeaderRole.None, false, false);

        Valancie.TrainSkill(EnumSkills.Agriculture);
        //Console.WriteLine(Valancie.SkillName.ToString());

        Settlement ofTest = new Settlement("Test", new Hex(1, 1, EnumHeartland.Lake));
        ofTest.Name = "Test2";
        ofTest.IsCapital = true;
        ofTest.SettlementType = EnumSettlementType.Village;
        Console.WriteLine(ofTest.Name + " " + ofTest.IsCapital);
    }
}