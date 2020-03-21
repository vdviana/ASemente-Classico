<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pesquisar.aspx.cs" Inherits="TechnoDemokratia.Telas.BuscarProjeto" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <script src="../Scripts/facebooklogin.js"></script>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="facebookID" runat="server" />
         <asp:ScriptManager ID="scrptmgr" EnablePageMethods="true" runat="server" />
                <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
    </div>
    </form>
</body>
</html>
