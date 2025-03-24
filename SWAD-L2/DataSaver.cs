using System.Text.Json;

namespace APSW_L_1
{
    public static class DataSaver
    {
        private static string _fileName = @"C:\Users\Крістіна\OneDrive\Рабочий стол\AccountsData.txt";
        public static void SaveData(List<Account> data)
        {
            

            using FileStream fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write);
            
            JsonSerializer.Serialize(fs, data, new JsonSerializerOptions { WriteIndented = true });
        }

        public static List<Account> LoadData()
        {
            FileInfo fileInfo = new FileInfo(_fileName);
            if (!File.Exists(_fileName) || fileInfo.Length == 0)
            {
                Console.WriteLine("File with data does not exist or is empty.");
                Console.ReadKey();
                return new List<Account>();
            }

            using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                var data = JsonSerializer.Deserialize<List<Account>>(fs) ?? new List<Account>();
                return data;
            }
        }

    }

}

