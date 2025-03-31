namespace APSW_L_1
{
    public class UserObserver : IUserObserver //СПОСТЕРІГАЧ
    {
        public void Update(object sender, UserEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
    }
}
