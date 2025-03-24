namespace APSW_L_1
{
    public class Simulator : Game, ISimulator
    {
       public Simulator(string name, EPlatform platform, Hardware hardware) : base(name, platform, hardware) { }
        public string GetGenre() => "Simulator";
    }
}
