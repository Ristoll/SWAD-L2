namespace APSW_L_1
{
    public class UserEventArgs : EventArgs //АРГУМЕЕНТТИ ПОДІЙ
    {
        public string Message { get; }
        public UserEventArgs(string message)
        {
            Message = message;
        }
    }
}
