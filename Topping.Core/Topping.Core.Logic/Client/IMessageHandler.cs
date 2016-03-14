using System;
using System.Net;
using TopMachine.Topping.Dto;


namespace Topping.Core.Logic.Client
{
    public interface IMessageHandler
    {
        bool HandleMessage(MessageDto message);
    }
}