using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopMachine.Topping.Dto;
using TopMachine.Web.GameViewer.BLL;

namespace TopMachine.Web.GameViewer.userControls.Topping
{
    public partial class Upload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string xml = System.Text.Encoding.UTF8.GetString(FileUpload1.FileBytes);
            FinalRoomDto dto = new TopMachine.Desktop.Overall.ObjectSerializer<FinalRoomDto>().Deserialize(xml);
            new ToppingAccessor().InsertFinalRoomDto(dto, 1); 
        }
    }
}