using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TopMachine.Web.GameViewer.BLL;
using TopMachine.Topping.Dto;

namespace TopMachine.Web.GameViewer.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Topping
    {

        [OperationContract]
        public List<GameType> GetGameTypes()
        {
            ToppingAccessor ta = new ToppingAccessor();
            return ta.GetGameTypes(); 
        }

        [OperationContract]
        public List<GameCollector> GetGameCollectors(int typeId, string filter, string order, int page, int pageSize)
        {
            ToppingAccessor ta = new ToppingAccessor();
            return ta.GetGameCollectors(typeId, filter, order, page, pageSize);
        }

        [OperationContract]
        public List<GameSet> GetGameSets(int collectorId, string filter, string order, int page, int pageSize)
        {
            ToppingAccessor ta = new ToppingAccessor();
            return ta.GetGameSets(collectorId, filter, order, page, pageSize);
        }

        [OperationContract]
        public List<GameShort> GetGameShorts(int setId, string filter, string order, int page, int pageSize)
        {
            ToppingAccessor ta = new ToppingAccessor();
            return ta.GetGames(setId, filter, order, page, pageSize);
        }




    }
}
