using System;
namespace Topping.Core.Logic.Threads
{
    public interface IBackgroundTask
    {
        void StartService();
        void StopService();
    }
}
