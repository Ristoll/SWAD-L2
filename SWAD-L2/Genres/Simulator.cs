namespace APSW_L_1
{
    public class Simulator : Game, ISimulator
    {
        public Simulator(Game game) 
            :base(game.Name, game.Platform, game.Hardware)
        { }
        public string GetGenre() => "Simulator";
    }
}
