using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topping.Core.Data.Db4o;
using Topping.Core.Logic.Threads;


namespace Topping.Core.Logic.DB4o
{
   public class GameLocalState : VGameState
    {
     
        public static GameLocalState _instance = null;

        public static GameLocalState Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new GameLocalState();
                        _instance.ServerPath = AppDomain.CurrentDomain.BaseDirectory;
                        _instance.InitRooms();

                        GameTimer.InitializeInstance(_instance);
                        RoomMessageProcessor.InitializeInstance(_instance);
                        MessageProcessor.InitializeInstance(_instance);
                        SendGames.InitializeInstance(_instance);
                    }
                }
                return _instance;
            }
        }


        public override void InitRooms()
        {
            MaxAmountRooms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxRooms"]);
            Rooms = new List<VRoom>();

            for (int x = 0; x < MaxAmountRooms; x++)
            {
                Room r = new Room();
                r.RoomId = x;
                r.OnRoomStatusChange += new Room.OnRoomStatusChangeDelegate(r_OnRoomStatusChange);
                Rooms.Add(r);
            }

            ToppingAccessor ta = new ToppingAccessor();
            Configurations = ta.GetConfigurations();
            ta.Dispose();
            Players = new Dictionary<string, Player>();
            Pseudos = new List<string>();
            PlayersByGuid = new Dictionary<Guid, string>();

        }
    }
}
