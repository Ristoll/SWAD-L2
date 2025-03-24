namespace APSW_L_1
{
    public class Adventure : Game, IAdventure
    {
        public Adventure(string name, EPlatform platform, Hardware hardware) : base(name, platform, hardware) { }
        public string GetGenre() => "Adventure";
    }

}
