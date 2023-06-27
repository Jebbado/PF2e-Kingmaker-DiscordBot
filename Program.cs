internal class Program
{
    private static void Main(string[] args)
    {
        Tests();
    }

    private static void Tests()
    {
        Kingdom Valancie = new Kingdom("Valancie", "Port Sénécal");
        Valancie.AssignCharter(Kingdom.EnumCharter.Exploration);
        Valancie.AssignHeartland(Kingdom.EnumHeartland.Plain);
        Valancie.AssignGovernment(Kingdom.EnumGovernment.Feudalism);
        Valancie.AddLeader(new Leader("Sébaste Larivière", EnumRole.Ruler, true));
        Valancie.AddLeader(new Leader("Jacques Létourneau", EnumRole.None, false));

        Valancie.TrainSkill(EnumSkills.Agriculture);
        //Console.WriteLine(Valancie.SkillName.ToString());

        Settlement ofTest = new Settlement("Test", new Hex(1, 1, Kingdom.EnumHeartland.Lake));
        ofTest.Name = "Test2";
        ofTest.IsCapital = true;
        ofTest.SettlementType = EnumSettlementType.Village;
        Console.WriteLine(ofTest.Name + " " + ofTest.IsCapital);
    }
}