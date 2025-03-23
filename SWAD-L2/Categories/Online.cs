namespace APSW_L_1
{
    public class Online : Game, IOnline
    {
        public Online(Game game) 
            :base (game.Name, game.Platform, game.Hardware)
        { }
    }
}
