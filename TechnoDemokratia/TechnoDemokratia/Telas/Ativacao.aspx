<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ativacao.aspx.cs" Inherits="TechnoDemokratia.Telas.Ativacao" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>

    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style>
        .linkbotao {
            background-color: lightblue;
            border: unset;
        }

        li:hover {
            border-color: darkgray;
            color: white;
            background-color: lightgray;
        }

        .botaoApp {
            background-color: lightblue;
            border-color: lightblue;
            border: unset;
            width: auto;
            height: auto;
            margin: unset;
            border-radius: unset;
        }

        .btnorangered:hover {
            color: white;
            border-color: darkred;
            background-color: darkred;
        }

        .botaoApp:hover {
            color: white;
            border-color: darkgray;
            background-color: lightgray;
        }

        hr {
            border-color: azure;
            color: azure;
            background-color: azure;
        }
    </style>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="scrptmgr" runat="server" />
        <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
        <div class="container" style="width: 98%; align-items: center">
            <br />
            <br />
            <br />            
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="520px" Width="100%">
<asp:Label ID="lblativacao" runat="server" />                        
                    </asp:Panel>            
        </div>
        <script src="../Style/jquery1.11.3/jquery-1.11.3.min.js"></script>
        <script src="../Style/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
