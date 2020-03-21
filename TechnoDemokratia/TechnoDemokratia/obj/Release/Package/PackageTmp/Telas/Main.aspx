<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TechnoDemokratia.Telas.Main" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>
<%@ Register Src="~/UserControls/progressbar.ascx" TagPrefix="uc1" TagName="progressbar" %>


<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />
    <!--[if lt IE 9]><script src="../../docs-assets/js/ie8-responsive-file-warning.js"></script><![endif]-->

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
<%--<script src="../Scripts/facebooklogin.js"></script>--%>
<%--<script src="../Scripts/facebooklogin.js"></script>--%>
<script type="text/javascript">


    function GetFaceLogin() {

        alert('j');
        if (document.getElementById('hfoff').value === "S") {
            alert('j');
            FaceLogout();
        }
        if (document.getElementById('hfLogged').value !== "S" && document.getElementById('hfoff').value === "") {
            var popup = window.open("FaceLogin.aspx", "", "width=200,height=100");
        }

    }



    function facebookConnectedResponse(responsevalue) {
        GetFaceLogin();

        //FB.api('http://graph.facebook.com/me/picture?type=large', function (responsepic) {

        //    document.getElementById('imgUsuFoto').removeAttribute('class');
        //    document.getElementById('imgUsuFoto').setAttribute('src', responsepic.data.url);
        //}

    }


</script>
<body>
    <form runat="server">
        <asp:ScriptManager EnablePageMethods="true" ID="scrptmgr" runat="server" />
        <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
        <div class="container" style="width: 98%; align-items: center; margin-top: unset">
            <br />
            <br />
            <br />
            <asp:LinkButton runat="server" ID="btnReload" OnClick="btnReload_Click" Visible="false">Recarregar projetos</asp:LinkButton>
            <asp:UpdatePanel ID="pnlprojetos" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlproj" runat="server" ScrollBars="Auto" Height="520px" Width="100%">
                        <asp:Repeater ID="rptprojetos" runat="server" OnItemCommand="rptprojetos_ItemCommand">
                            <ItemTemplate>
                                <div class="col-sm-3 resizeimage" style="background-image: url('<%# Eval("CaminhoImagem") %>'); margin: 0.2%; width: 24%; border-radius: 20px">
                                    <div class="row" style="font-size: medium; font-family: Arial; background-color: lightblue; border-top-left-radius: 20px; border-top-right-radius: 20px;">
                                        <asp:Label ID="lblPjName" runat="server" Text='<%# Eval("Titulo") %>' Style="height: 15%; font-size: medium; font-family: Arial; width: 100%;"></asp:Label>
                                    </div>
                                    <br />
                                    <div style="height: 150px; overflow-y: auto; overflow-x: hidden;">
                                        <asp:Label ID="lblPjDesc" runat="server" Text='<%# Eval("Descricao") %>' Style="font-family: Arial; color: darkblue; text-shadow: 1px 0px 0px 	#f0f8ff, -1px 0px 0px 	#f0f8ff, 0px 1px 0px 	#f0f8ff, 0px -1px 0px 	#f0f8ff; font-weight: bold"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:Button ID="btnAprov" runat="server" CommandName="Author" CommandArgument='<%# Eval("IdProjeto") %>' Text="Autor" Style="width: 41%; border-radius: 80px" CssClass="btn btn-default" />
                                    <asp:Button ID="btnCheck" runat="server" CommandName="Check" CommandArgument='<%# Eval("IdProjeto") %>' Text="Checar Projeto" Style="width: 57%; border-radius: 80px; background-color: orangered; color: white;" CssClass="btn btnorangered" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <asp:Button ID="hlMaisProjetos" runat="server" Text="Carregar mais projetos" OnClick="hlMaisProjetos_Click" CssClass="btn btn-link" />

                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfoff" runat="server" />
            <asp:HiddenField ID="hfLogged" runat="server" />
            <uc1:progressbar runat="server" ID="progressbar1" />
        </div>

        <script src="../Scripts/jquery-1.10.2.js"></script>
        <%--<script src="../Style/jquery1.11.3/jquery-1.11.3.min.js"></script>--%>
        <%--<script src="../Style/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>--%>
        <script src="../Scripts/facebooklogin.js"></script>




    </form>
</body>
</html>
