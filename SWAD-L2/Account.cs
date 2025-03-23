using System.Text.RegularExpressions;

namespace APSW_L_1
{
    public class Account
    {
        private string email;
        public string UserName { get; private set; }
        public string Email
        {
            get
            {
                return email;
            }

            private set
            {                
                if (Regex.IsMatch(value, emailPattern))
                {
                    email = value;
                }
                else
                {
                    throw new ArgumentException(value);
                }
            }
        }
        public string Password { get; private set; }
        public Dictionary<string, GameData> GamesData { get; set; } = new Dictionary<string, GameData>();
        private string emailPattern { get; } = "^[^@]*@[^@]*$";
        public Account(string userName, string? email, string password)
        {          
            ArgumentException.ThrowIfNullOrEmpty(userName);
            ArgumentException.ThrowIfNullOrEmpty(email);
            ArgumentException.ThrowIfNullOrEmpty(password);
                
            UserName = userName;
            Email = email;
            Password = password;
        }
        public void SetRating(Game game, int rate)
        {
            if (!GamesData.ContainsKey(game.Name))
            {
                GamesData[game.Name] = new GameData();
            }
            GamesData[game.Name].Rating = rate;
            game.Rating += rate;
        }
        public void SetProgress(Game game, int progress)
        {
            if (!GamesData.ContainsKey(game.Name))
            {
                GamesData[game.Name] = new GameData();
            }
            GamesData[game.Name].Progress = progress;
            AccountsStorage.Instance.SaveAccounts();
        }
    }
}
