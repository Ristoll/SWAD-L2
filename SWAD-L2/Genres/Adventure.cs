namespace APSW_L_1
{
    public class Adventure : Game, IAdventure
    {
        public Adventure(Game game)
            : base(game.Name, game.Platform, game.Hardware)
        { }
        public string GetGenre() => "Adventure";
    }

}
