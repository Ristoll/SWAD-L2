namespace APSW_L_1
{
    public class Browser : Game, IBrowser
    {
        public Browser(Game game)
            :base(game.Name, game.Platform, game.Hardware)
        { }
    }
}
