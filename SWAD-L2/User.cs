namespace APSW_L_1
{
    public class User //ГЕНЕРАТОР ПОДІЙ
    {
        private readonly List<IUserObserver> observers = new List<IUserObserver>();
        public EPlatform Platform { get; }
        public Hardware Hardware { get; }
        public bool NetworkConnection { get; set; } = true;
        public bool IsLoggedIn { get; set; } = false;
        public bool IsPlaying { get; set; } = false;
        public bool IsStreaming { get; set; } = false;
        private readonly List<Game> installedGamesLibrary = new List<Game>();
        public User(){}
    
        public User(EPlatform platform, Hardware hardware)
        {
            Platform = platform;
            Hardware = hardware;            
        }
        public List<Game> GetInstalledGamesLibrary() => installedGamesLibrary;
        public void Subscribe(IUserObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IUserObserver observer)
        {
            observers.Remove(observer);
        }
        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }
        public bool ToCheckNetConnection()
        {
            if (!NetworkConnection)
            {
                Notify("No internet connection.");
            }
            return NetworkConnection;
        }
        public bool ToCheckInstallation(Game game)
        {
            if (installedGamesLibrary.Contains(game))
            {
                Notify("Game has been already installed.");
            }
            return installedGamesLibrary.Contains(game);
        }
        public bool ToCheckHardware(Game game)
        {
            if (Hardware.CPU_quality >= game.Hardware.CPU_quality || 
                Hardware.RAM_capacity >= game.Hardware.RAM_capacity ||
                Hardware.GPU_quality >= game.Hardware.GPU_quality)
            {
                return true;
            }

            return false;
        }
        public bool ToCheckControllers(Game game)
        {
            switch (game.Platform)
            {
                case EPlatform.Desktop:
                    return Hardware.controllers.Contains(EController.Keyboard) 
                        && Hardware.controllers.Contains(EController.Mouse)
                        || (Hardware.controllers.Contains(EController.Gamepad) 
                        && game.Hardware.controllers.Contains(EController.Gamepad));

                case EPlatform.Console:
                    return Hardware.controllers.Contains(EController.Gamepad);

                case EPlatform.Mobile:
                    return true;

                default:
                    return false;
            }
        }
        public bool ToCheckPlatform(Game game)
        {
            if (Platform == game.Platform)
            {
                return true;
            }
            Notify("This game is not supported in your platform.");
            return false;
        }
        public bool ToCheckGenre(Game game)
        {
            if(installedGamesLibrary.Any(existingGame => (existingGame as IGenre)?.GetGenre() == (game as IGenre)?.GetGenre()))
            {
                Notify("You can`t install the game with genre you have already had.");
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Install(Game game)
        {
            if (!ToCheckGenre(game) && !ToCheckInstallation(game) && ToCheckPlatform(game))
            {
                if (ToCheckNetConnection())
                {
                    bool isBrowser = game is Browser;
                    if (!isBrowser)
                    {
                        try
                        {
                            Hardware.FillCapasity(game.Hardware.HDD_capacity);
                            installedGamesLibrary.Add(game);
                            Notify("Game has been installed.");
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Notify(ex.Message);
                        }

                    }
                }
            }
        }
        public void Launch(Game game, bool isLoggedIn)
        {
            bool isOnline = game is IOnline;
            bool isBrowser = game is IBrowser;
            bool needInternet = isOnline && !isBrowser;
            bool needInstallation = !isBrowser;
            bool isSuitableControllers = ToCheckControllers(game);

            bool canLaunch = isSuitableControllers && ToCheckHardware(game) && !IsPlaying && isLoggedIn &&
                 (!needInternet || ToCheckNetConnection()) &&
                 (!needInstallation || ToCheckInstallation(game));
            if (!isSuitableControllers)
            {
                Notify("Not all suitable controllers have been plugged.");
            }
            Notify(canLaunch ? "Game is launching." : "Game can't be launched.");
            game.IsLaunched = canLaunch;
        }       
        public Account? LogIn(string userName, string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Notify("Invalid input. Please provide all fields.");
                    return null;
                }

                Account account = new Account(userName, email, password);
                if (!AccountsStorage.Instance.Contains(account))
                {
                    AccountsStorage.Instance.Add(account);
                    Notify("You are logged in.");                   
                    IsLoggedIn = true;
                    return account;
                }
                else
                {
                    if (account.Password == password)
                    {
                        Notify("You are logged in.");
                        IsLoggedIn = true;
                        return account;
                    }
                }
            }
            catch(ArgumentException)
            {
                Notify("Invalid input. Please provide all fields.");
            }
            return null;
        }
        public void Play(Game game)
        {
            if (game.IsLaunched && IsLoggedIn)
            {
                IsPlaying = game.IsLaunched;
                if (game.ProgressPercentage == 100)
                {
                    Notify("You have finished the game.");
                    StopPlaying();
                }
            }
            else
            {
                Notify("Game isn`t launched to play.");
            }
        }
        public void StopPlaying()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                Notify("You have stopped playing game.");
            }
            else
            {
                Notify("You aren`t playing to stop playing.");
            }
        }
        public void Stream(Game game)
        {
            Notify((game.Platform == EPlatform.Mobile||game.Platform==EPlatform.Console) 
                && IsPlaying ? "Stream is running." : "It is unable to stream on this platform.");
            IsStreaming = (game.Platform == EPlatform.Mobile) && IsPlaying;
        }
        public void StopStream()
        {
            if (IsStreaming)
            {
                IsStreaming = false;
                Notify("Stream is stopped.");
            }
            else
            {
                Notify("You didn't have a stream to stop it.");
            }
        }        
        public void ConnectController(EController controller)
        {
            bool isPlugged = controller == EController.Gamepad || !Hardware.controllers.Contains(controller);
            if (isPlugged)
            {
                Hardware.controllers.Add(controller);
            }
            Notify(isPlugged ? "New controller has been plugged." : "You can`t plug this type of controller once more.");
        }
    }
}
