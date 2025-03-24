namespace APSW_L_1
{
    static class Program
    {
        static void Main()
        {
            Hardware desktopHardware = new Hardware(4, 4, 4,15, Hardware.ControllersForPC());
            Hardware mobileHardware = new Hardware(3, 2, 4, 40, Hardware.ControllersForMobile());
            Hardware consoleHardware = new Hardware(4, 5, 4, 100, Hardware.ControllersForConsole());

            User desktopUser = new User(EPlatform.Desktop, desktopHardware);
            User mobileUser = new User(EPlatform.Mobile, mobileHardware);
            User consoleUser = new User(EPlatform.Console, consoleHardware);

            List<User> users = new List<User>() {desktopUser, mobileUser, consoleUser};
            UserObserver observer = new UserObserver();
            desktopUser.Subscribe(observer);
            mobileUser.Subscribe(observer);
            consoleUser.Subscribe(observer);

            Hardware minecraftHardware = new Hardware(3, 4, 2, 15, Hardware.ControllersForPC());
            Adventure minecraftGameA = new Adventure("Minecraft", EPlatform.Desktop, minecraftHardware);

            Hardware fortniteHardware = new Hardware(4, 3, 4, 25, Hardware.ControllersForConsole());
            Shooter gameF = new Shooter("Fortnite", EPlatform.Console, fortniteHardware);
            Online fortniteGameO = new Online(gameF);

            Hardware TRexhardware = new Hardware(2, 1,2,0,Hardware.ControllersForMobile());
            Simulator gameT = new Simulator("T-Rex", EPlatform.Mobile, TRexhardware);
            Browser TRexGame = new Browser(gameT);

            Hardware HKHardware = new Hardware(3,2,3,15,Hardware.ControllersForPC());
            Adventure HollowKnightGame = new Adventure("Hollow Knight", EPlatform.Desktop, HKHardware);

            Hardware PUBGMhardware = new Hardware(4, 2, 4, 50, Hardware.ControllersForMobile());
            Shooter PUBGMGame = new Shooter("PUBG: Mobile", EPlatform.Mobile, PUBGMhardware);
            Online PUBGMGameO = new Online(PUBGMGame);

            Hardware DetroitHardware = new Hardware(4, 5, 4, 40, Hardware.ControllersForConsole());
            Adventure DetroitGameA = new Adventure("Detroit: Become Human", EPlatform.Console, DetroitHardware);

            Hardware CSHardware = new Hardware(5,4,3,40, Hardware.ControllersForPC());
            Shooter CSGS = new Shooter("Counter-Strike", EPlatform.Desktop, CSHardware);
            Online CSGO = new Online(CSGS);

            Hardware LDEHardware = new Hardware(2,3,1,25, Hardware.ControllersForMobile());
            Adventure LDEGameA = new Adventure("Last Day On Earth", EPlatform.Mobile, LDEHardware);
            Online LDEGameO = new Online(LDEGameA);

            Hardware TLOUHardware = new Hardware(3,4,4,20, Hardware.ControllersForConsole());
            Adventure TLOUGameA = new Adventure("The Last Of Us", EPlatform.Console, TLOUHardware);

            List<Game> gamesStore = new List<Game>() {minecraftGameA, fortniteGameO, TRexGame, HollowKnightGame, 
                                                            PUBGMGameO, DetroitGameA, CSGO, LDEGameO, TLOUGameA};

            ConsoleMenu.StartConsole(users, AccountsStorage.Instance.Accounts, gamesStore);

        }
    }
}
