using System;
using System.Collections.Generic;
namespace Topping.Core.Logic.Client
{
    public interface ISession
    {
        void AddMessageHandler(string key, IMessageHandler h, bool updateCollection);
        bool Authenticated { get; set; }
        System.Collections.Generic.List<TopMachine.Topping.Dto.ConfigGameDto> Configurations { get; set; }
        Topping.Core.Logic.VRoom CurrentRoom { get; set; }
        void HandleMessage(TopMachine.Topping.Dto.MessageDto message);
        string Password { get; set; }
        string Pseudo { get; set; }
        List<string> Pseudos { get; set; }
        void RemoveMessageHandler(string key);
        void SetCompleteGame(int roomid);
        System.Collections.Generic.List<Topping.Core.Logic.VRoom> Rooms { get; set; }
    }
}
