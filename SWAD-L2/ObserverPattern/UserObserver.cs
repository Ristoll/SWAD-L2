namespace APSW_L_1
{
    public class UserObserver : IUserObserver //СПОСТЕРІГАЧ
    {
        public void Update(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
