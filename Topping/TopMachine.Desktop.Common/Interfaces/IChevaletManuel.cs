using System;
namespace TopMachine.Topping.Common.Interfaces
{
    public delegate bool OnRackSubmitEvent();
    public delegate void OnRackUpdatedEvent();

    public interface IChevaletManuel
    {
        event OnRackSubmitEvent OnRackSubmit;
        event OnRackUpdatedEvent OnRackUpdated;

        System.Windows.Forms.Control ResultControl { get; }
        void SetTirage(string Tirage);
        String ToString();
    }
}

