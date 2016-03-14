using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TopMachine.Topping.Dto;
using TopMachine.Web.GameViewer.BLL;
using System.ServiceModel.Activation;
using System.IO;
using System.Net;
using TopMachine.Web.GameViewer.DeleteGameService;

namespace TopMachine.Web.GameViewer.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TopMachineAdminSercvice" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TopMachineAdminService : ITopMachineAdminService
    {

        public void UploadFile(string xml)
        {
            try
            {
                string p = System.Configuration.ConfigurationManager.AppSettings["GamesPath"] + xml;
                WebRequest wr = WebRequest.Create(p);

                HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                FinalRoomDto dto = new TopMachine.Desktop.Overall.ObjectSerializer<FinalRoomDto>().Deserialize(responseFromServer);
                new ToppingAccessor().InsertFinalRoomDto(dto, 1);

                DeleteServicesClient c = new DeleteServicesClient();
                c.DeleteGameXML(xml);
            } catch
            {
                 ;
            }
        }
    }
}
