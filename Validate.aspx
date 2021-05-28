<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validate.aspx.cs" Inherits="AssessmentMVC.Validate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:Label ID="Label1" runat="server" Text="Please select school and file to Validate the data:" Font-Bold="True" Font-Overline="False" Font-Size="X-Large" Font-Underline="True"/>
         <br />
         <br />
        <asp:Label ID="Label6" runat="server" Text="Select School:" Visible="true" Font-Bold="true"/>
        &nbsp;<asp:DropDownList ID="ddlSchools" runat="server" AppendDataBoundItems = "true" Visible="true">
        </asp:DropDownList>
        <br/>
        <br/>        
     <asp:Panel ID="Panel1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server"/>
         <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <br />
         
        <asp:Button ID="btnDownload" runat="server" Text="Dlownload Error File" Visible="false" OnClick="btnDownload_Click" />
         <br />
         
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>         
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible = "false" >
        <asp:Label ID="Label5" runat="server" Text="File Name:" Font-Bold="true"/>        
        <asp:Label ID="lblFileName" runat="server" Text="" />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Select Sheet:" Visible="true" Font-Bold="true"/>
        <asp:DropDownList ID="ddlSheets" runat="server" AppendDataBoundItems = "true" Visible="true">
        </asp:DropDownList>
        <br />
        <br />        
        <asp:Button ID="btnSave" runat="server" Text="Validate" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />        
     </asp:Panel>
    </div>
    </form>
</body>
</html>
