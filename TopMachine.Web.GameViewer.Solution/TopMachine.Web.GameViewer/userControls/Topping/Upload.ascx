<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload.ascx.cs" Inherits="TopMachine.Web.GameViewer.userControls.Topping.Upload" %>
<p>
    Upload a new Game :
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
        Text="Upload" />
</p>

