using APSW_L_1;

namespace SWAD_L2
{
    public interface IUserObservable
    {
        void Subscribe(IUserObserver observer);
        void Unsubscribe(IUserObserver observer);
        void Notify(string message);
    }
}
