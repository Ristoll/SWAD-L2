namespace APSW_L_1
{
    public class Shooter : Game, IShooter
    {
        public Shooter(string name, EPlatform platform, Hardware hardware) : base(name, platform, hardware) { }
        public string GetGenre() => "Shooter";
    }
}
