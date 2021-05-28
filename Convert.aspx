<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Convert.aspx.cs" Inherits="AssessmentMVC.Convert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

  <script type="text/javascript">
   function setfilename()
   {
       document.getElementById("lblMessage").innerText = '';
  }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Please select a word file to convert in Excel:" Font-Bold="True" Font-Overline="False" Font-Size="X-Large" Font-Underline="True"/>
        <br/>
        <br/>        
       <asp:Panel ID="Panel1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server" onchange="setfilename();"/>
         <asp:Button ID="btnUpload" runat="server" Text="Convert & Download" OnClick="btnUpload_Click" />
        <br />
        <br />
         
        <%--<asp:Button ID="btnDownload" runat="server" Text="Dlownload Error File" Visible="false" OnClick="btnDownload_Click" />
         <br />
        --%> 
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>         
    </asp:Panel>
    </div>
    </form>
</body>
</html>
