namespace APSW_L_1
{
    static class Program
    {
        static void Main()
        {
            List<EController> testControllers = new List<EController>() {EController.Gamepad, EController.Mouse };
            Hardware desktopHardware = new Hardware(4, 4, 4,4, Hardware.ControllersForPC());
            Hardware mobileHardware = new Hardware(3, 2, 4, 5, Hardware.ControllersForMobile());
            Hardware consoleHardware = new Hardware(4, 5, 4, 4, Hardware.ControllersForConsole());

            User desktopUser = new User(EPlatform.Desktop, desktopHardware);
            User mobileUser = new User(EPlatform.Mobile, mobileHardware);
            User consoleUser = new User(EPlatform.Console, consoleHardware);

            List<User> users = new List<User>() {desktopUser, mobileUser, consoleUser};

            Hardware minecraftHardware = new Hardware(3, 4, 2, 15, Hardware.ControllersForPC());
            Game gameM = new Game("Minecraft", EPlatform.Desktop, minecraftHardware);
            Adventure minecraftGame = new Adventure(gameM);

            Hardware fortniteHardware = new Hardware(4, 3, 4, 25, Hardware.ControllersForConsole());
            Game gameF = new Game("Fortnite", EPlatform.Console, fortniteHardware);
            Shooter fortniteS = new Shooter(gameF);
            Online fortniteGameO = new Online(fortniteS);

            Hardware TRexhardware = new Hardware(2, 1,2,0,Hardware.ControllersForMobile());
            Game gameT = new Game("T-Rex", EPlatform.Mobile, TRexhardware);
            Simulator gameS = new Simulator(gameT);
            Browser TRexGame = new Browser(gameS);

            Hardware HKHardware = new Hardware(3,2,3,15,Hardware.ControllersForPC());
            Game gameHK = new Game("Hollow Knight", EPlatform.Desktop, HKHardware);
            Adventure HollowKnightGame = new Adventure(gameHK);

            Hardware PUBGMhardware = new Hardware(4, 2, 4, 50, Hardware.ControllersForMobile());
            Game gamePM = new Game("PUBG: Mobile", EPlatform.Mobile, PUBGMhardware);
            Shooter PUBGMGame = new Shooter(gamePM);
            Online PUBGMGameO = new Online(PUBGMGame);

            Hardware DetroitHardware = new Hardware(4, 5, 4, 40, Hardware.ControllersForConsole());
            Game DetroitGame = new("Detroit: Become Human", EPlatform.Console, DetroitHardware);
            Adventure DetroitGameA = new Adventure(DetroitGame);

            Hardware CSHardware = new Hardware(5,4,3,40, Hardware.ControllersForPC());
            Game CSG = new Game("Counter-Strike", EPlatform.Desktop, CSHardware);
            Shooter CSGS = new Shooter(CSG);
            Online CSGO = new Online(CSGS);

            Hardware LDEHardware = new Hardware(2,3,1,25, Hardware.ControllersForMobile());
            Game LDEGame = new Game("Last Day On Earth", EPlatform.Mobile, LDEHardware);
            Adventure LDEGameA = new Adventure(LDEGame);
            Online LDEGameO = new Online(LDEGameA);

            Hardware TLOUHardware = new Hardware(3,4,4,20, Hardware.ControllersForConsole());
            Game TLOUGame = new Game("The Last Of Us", EPlatform.Console, TLOUHardware);
            Adventure TLOUGameA = new Adventure(TLOUGame);

            List<Game> gamesStore = new List<Game>() {minecraftGame, fortniteGameO, TRexGame, HollowKnightGame, 
                                                            PUBGMGameO, DetroitGameA, CSGO, LDEGameO, TLOUGameA};

            ConsoleMenu.StartConsole(users, AccountsStorage.Instance.Accounts, gamesStore);

        }
    }
}
