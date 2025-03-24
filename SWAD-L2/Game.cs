namespace APSW_L_1
{
    public abstract class Game
    {
        private int usersRates;
        private int usersSummOfRates;
        private double rating;
        private int progress = 0;
        public string Name { get; }
        public EPlatform Platform { get; private set; }
        public Hardware Hardware { get; private set; }
        public double Rating {
            get => rating;
            set
            {
                usersSummOfRates += (int)value;
                usersRates++;
                rating = Math.Round((double)usersSummOfRates / usersRates, 2);
            }
        }
        public int ProgressPercentage
        {
            get => progress;

            set
            {
                if (value > 100)
                {
                    progress = 100;
                }
                else if (value < 0)
                {
                    throw new ArgumentException("Progress can`t be negative");
                }

                progress = value;
            }
        }
        public bool IsLaunched { get; set; }        
        public Game(string name, EPlatform platform, Hardware hardware)
        {
            Name = name;
            Platform = platform;
            Hardware = hardware;

            Random rand = new Random();
            usersRates = rand.Next(1, 10000);
            usersSummOfRates = rand.Next(usersRates, usersRates * 5);
            rating = Math.Round((double)usersSummOfRates / usersRates, 2);
        }
    }
}
