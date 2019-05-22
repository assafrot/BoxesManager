using BoxesManger;

namespace BoxesUnitTest
{
    class DebugNotifier : INotifier
    {
        public void OnBoxRemoval(string msg)
        {
        }

        public void OnEmptyStock(string msg)
        {
        }

        public void OnError(string msg)
        {
        }

        public void OnLowSuplly(string msg)
        {
        }

        public void OnSuccess(string msg)
        {
        }
    }
}
