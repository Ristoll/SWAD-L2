namespace APSW_L_1
{
    public class Shooter : Game, IShooter
    {
        public Shooter(Game game) 
            :base(game.Name, game.Platform, game.Hardware)
        { }
        public string GetGenre() => "Shooter";
    }
}
