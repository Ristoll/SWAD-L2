using System.Text.Json;

namespace APSW_L_1
{
    public static class DataSaver<T>
    {
        public static void SaveData(List<T> data)
        {
            string fileName = $"{typeof(T).Name}sData.txt";

            using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            
            JsonSerializer.Serialize(fs, data, new JsonSerializerOptions { WriteIndented = true });
        }

        public static List<T> LoadData()
        {
            string fileName = $"{typeof(T).Name}sData.txt";
            FileInfo fileInfo = new FileInfo(fileName);
            if (!File.Exists(fileName) || fileInfo.Length == 0)
                return new List<T>();

            using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<T>>(fs) ?? new List<T>();
        }
    }

}

