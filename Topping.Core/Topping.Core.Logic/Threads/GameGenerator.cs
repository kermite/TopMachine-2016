using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Engine.GameController;
using TopMachine.Topping.Dto; 

namespace Topping.Core.Logic.Threads
{
    public class GameGenerator
    {
        public Guid GameConfigId;
        public string ServerPath;
        public VRoom Room;
        public VGameState ServerState; 

        public GameGenerator(VGameState state, Guid id, string sp, VRoom r)
        {
            ServerState = state; 
            GameConfigId = id;
            ServerPath = sp;
            Room = r; 
        }

        public void Start()
        {  // call local and call server thus not necessary

            //GeneratedGameDto g = new GeneratedGameDto();
            //g.Config = Room.Configuration; 

            //GameOnlineLoader go = new GameOnlineLoader(g);
            //go.OnGameCreated += new GameOnlineLoader.OnGameCreatedEvent(go_OnGameCreated);
            //go.GenerateLocalGame();
        }

        void go_OnGameCreated(GeneratedGameDto game)
        {

           Room.GameId = Guid.NewGuid(); 

        }

       private void SendMessage(MessageDto m)
        {
            MessageProcessor.Instance.AddMessage(m);
        }

        private void SendRoomMessage(VRoom r, MessageDto m)
        {
            RoomMessageProcessor.Instance.AddMessage(m);
        }


    }
}
