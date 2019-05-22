using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesManger
{
    public interface INotifier
    {
        void OnLowSuplly(string msg);
        void OnEmptyStock(string msg);
        void OnError(string msg);
        void OnBoxRemoval(string msg);
        void OnSuccess(string msg);
    }
}
