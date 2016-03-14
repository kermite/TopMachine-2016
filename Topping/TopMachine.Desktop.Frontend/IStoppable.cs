using System;
namespace TopMachine.Topping.Frontend
{
    public interface IStoppable
    {
        void Stop();
        void Reactivate();
    }
}
