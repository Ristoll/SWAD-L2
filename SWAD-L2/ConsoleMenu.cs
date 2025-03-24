namespace APSW_L_1
{
    public static class ConsoleMenu
    {
        public static void StartConsole(List<User> users, List<Account> accounts, List<Game> games)
        {
            User currUser = ChooseUser(users);
            List<Game> neededGames = new List<Game>();
            foreach (Game game in games)
            {
                if (game.Platform == currUser.Platform)
                {
                    neededGames.Add(game);
                }
            }
            ChooseGameLibrary(neededGames, currUser);
        }

        private static User ChooseUser(List<User> users)
        {
            List<string> items = users.Select(user => user.Platform.ToString()).ToList();
            int index = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose user by platform:");
                DrawMenu(items, index);
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                    return users[index];

                index = NavigationMenu(index, items.Count - 1, key);

            }
        }

        private static void ChooseGameLibrary(List<Game> games, User user)
        {

            List<string> items = new List<string> { "My games list", "Games Store", "Connect Controller",
                                                    "Switch Internet Connection", "Exit"};
            int index = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose games library:");
                DrawMenu(items, index);
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    switch (items[index])
                    {
                        case "My games list":
                            Console.WriteLine("My games list: ");
                            List<Game> myGames = user.GetInstalledGamesLibrary();

                            if (myGames.Count == 0)
                            {
                                Console.WriteLine("There are no games.");
                                Console.ReadLine();
                                break;
                            }

                            ChooseGame(myGames, user);
                            break;
                        case "Games Store":
                            ChooseGame(games, user);
                            break;
                        case "Connect Controller":
                            ChooseController(user);
                            break;
                        case "Switch Internet Connection":
                            user.NetworkConnection = !user.NetworkConnection;
                            Console.WriteLine($"Network connection is: {user.NetworkConnection}");
                            Console.ReadLine();
                            break;
                        case "Exit":
                            AccountsStorage.Instance.SaveAccounts();
                            return;
                    }
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }
        private static void ChooseController(User user)
        {
            List<EController> controllersList = new List<EController>() { EController.Gamepad,
                                                        EController.Mouse, EController.Keyboard};
            List<string> items = controllersList.Select(controller => controller.ToString()).ToList();
            int index = 0;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Choose controller:");
                DrawMenu(items, index);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    EController selectedController = controllersList[index];
                    user.ConnectController(selectedController);
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }
        private static void ChooseGame(List<Game> gamesList, User user)
        {
            List<string> items = gamesList.Select(game => game.Name).ToList();
            int index = 0;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Choose game:");
                DrawMenu(items, index);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    Game selectedGame = gamesList[index];
                    ShowActions(selectedGame, user);
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }
        private static void ShowActions(Game game, User user)
        {
            List<string> items = new List<string> { "Install", "Play", "Stream", "Stop The Stream", "Exit" };
            int index = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose action:");
                DrawMenu(items, index);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    switch (items[index])
                    {
                        case "Install":
                            user.Install(game);
                            break;
                        case "Play":
                            if (user.ToCheckInstallation(game))
                            {
                                Console.WriteLine("Launching game: ");                                
                                Account account = ChooseAccountType(user);
                                user.Launch(game, user.IsLoggedIn);
                                user.Play(game);
                                bool continuePlay;
                                if (user.IsPlaying)
                                {
                                    do
                                    {
                                        continuePlay = PlayingMenu(game, user, account);
                                    } while (continuePlay);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Game has ben not installed.");
                                Console.ReadLine();
                            }
                            break;
                        case "Stream":
                            user.Stream(game);
                            break;
                        case "Stop The Stream":
                            user.StopStream();
                            break;
                        case "Exit":
                            return;
                    }
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }

        private static bool PlayingMenu(Game game, User user, Account account)
        {
            List<string> items = new List<string> { "Continue play", "Set Rating", "Exit" };
            int index = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("You are playing:");
                int progress = 0;
                if (account.GamesData.ContainsKey(game.Name))
                {
                    progress = account.GamesData[game.Name].Progress;
                }
                else
                {
                    account.GamesData[game.Name] = new GameData();
                    account.GamesData[game.Name].Progress = 0;
                    progress = 0;
                }
                Console.WriteLine($"Progress: {progress}%");
                DrawMenu(items, index);

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    switch (items[index])
                    {
                        case "Continue play":
                            user.Play(game);
                            game.ProgressPercentage = progress;
                            if (progress <= 100)
                            {
                                game.ProgressPercentage += 5;
                                account.SetProgress(game, game.ProgressPercentage);
                            }
                            return user.IsPlaying;
                        case "Set Rating":
                            Console.WriteLine($"Currect rating of the game: {game.Rating}");
                            Console.Write("Input your rate (1-5): ");
                            if (int.TryParse(Console.ReadLine(), out int rate) && rate >= 1 && rate <= 5)
                            {
                                account.SetRating(game, rate);
                                Console.WriteLine($"You rated {game.Name} with {rate} stars!");
                                Console.WriteLine($"Currect rating of the game: {game.Rating}");
                                AccountsStorage.Instance.SaveAccounts();
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                            }
                            Console.ReadLine();
                            break;
                        case "Exit":
                            user.StopPlaying();
                            return false;
                    }
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }
        public static Account ChooseAccountType(User user)
        {
            List<string> items = new List<string> { "Sign In", "Register" };
            int index = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome back in game!");
                DrawMenu(items, index);
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    switch (items[index])
                    {
                        case "Sign In":
                            return SignInMenu(user);
                        case "Register":
                            return RegisterMenu(user);
                    }
                }

                index = NavigationMenu(index, items.Count - 1, key);
            }
        }

        public static Account SignInMenu(User user)
        {
            List<string> items = AccountsStorage.Instance.Accounts.Select(account => account.UserName).ToList();
            int index = 0;
            if (items.Count == 0)
            {
                Console.WriteLine("There are no accounts. You must register first.");
                return RegisterMenu(user);
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose account:");

                DrawMenu(items, index);
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Input password:");
                    string password = Console.ReadLine()!;
                    if (password == AccountsStorage.Instance.Accounts[index].Password)
                    {
                        user.IsLoggedIn = true;
                        return AccountsStorage.Instance.Accounts[index];
                    }
                    else
                    {
                        Console.WriteLine("Invalid password.");
                    }
                }
                index = NavigationMenu(index, items.Count - 1, key);
            }
        }
        private static Account RegisterMenu(User user)
        {
            Console.WriteLine("Create your account:");
            Console.WriteLine("Input userName:");
            string name = Console.ReadLine()!;
            Console.WriteLine("Input email:");
            string email = Console.ReadLine()!;
            Console.WriteLine("Input password: ");
            string password = Console.ReadLine()!;
            user.IsLoggedIn = false;
            return user.LogIn(name, email, password)!;           
        }
        private static int NavigationMenu(int index, int maxIndex, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (index < maxIndex) index++;
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0) index--;
                    break;
            }
            return index;
        }
        private static void DrawMenu(List<string> items, int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i]); Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
