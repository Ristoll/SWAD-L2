namespace APSW_L_1
{
    //Singleton
    public class AccountsStorage
    {
        private static AccountsStorage? _instance;
        private static readonly object _lock = new object();

        public List<Account> Accounts { get; private set; }
        private AccountsStorage()
        {
            Accounts = DataSavier<Account>.LoadData();
        }
        public static AccountsStorage Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new AccountsStorage();
                }
            }
        }

        public void Add(Account account)
        { 
            Accounts.Add(account);
            DataSavier<Account>.SaveData(Accounts);
        }

        public bool Contains(Account account)
        {
            if (Accounts.Contains(account))
            {
                return true;
            }

            return false;
        }

        public void SaveAccounts()
        {
            DataSavier<Account>.SaveData(Accounts);
        }
    }
}
