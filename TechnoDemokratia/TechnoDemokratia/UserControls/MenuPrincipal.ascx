<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.ascx.cs" Inherits="TechnoDemokratia.MenuPrincipal" %>
<%@ Register Src="~/UserControls/ModalContatar.ascx" TagPrefix="uc1" TagName="ModalContatar" %>
<%@ Register Src="~/UserControls/progressbar.ascx" TagPrefix="uc1" TagName="progressbar" %>



<!DOCTYPE html>
<%--<link href="./Off Canvas Push Menu Template for Bootstrap_files/bootstrap.min.css" rel="stylesheet">
    <link href="./Off Canvas Push Menu Template for Bootstrap_files/jasny-bootstrap.min.css" rel="stylesheet">--%>
<link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
<link href="../Style/jasny-bootstrap/css/jasny-bootstrap.min.css" rel="stylesheet" />
<!-- Custom styles for this template -->
<%--  <link href="./Off Canvas Push Menu Template for Bootstrap_files/navmenu-push.css" rel="stylesheet">--%>
<link href="../Style/jasny-bootstrap/css/navmenu-push.css" rel="stylesheet" />
<link href="../UserControls/modalclass.css" rel="stylesheet" />
<!-- Just for debugging purposes. Don't actually copy this line! -->
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

    .lkNome {
        background-color: lightblue;
        border-color: lightblue;
        border: unset;
        width: auto;
        height: auto;
        margin: unset;
        border-radius: unset;
    }

        .lkNome:hover {
            background-color: lightblue;
            border-color: lightblue;
            border: unset;
            width: auto;
            height: auto;
            margin: unset;
            border-radius: unset;
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

    .hiddencontrol {
        width: 0.1px;
        height: 0.1px;
        opacity: 0;
        overflow: hidden;
        position: absolute;
        z-index: -1;
    }

    .linkclass {
        background: none !important;
        border: none;
        /*padding: 0 !important;*/
        /*optional*/
        font-family: arial,sans-serif; /*input has OS specific font-family*/
        color: darkgray;
        text-decoration: unset;
        cursor: pointer;
    }

        .linkclass:hover {
            background: none !important;
            border: none;
            /*padding:0!important;*/
            /*optional*/
            font-family: arial,sans-serif; /*input has OS specific font-family*/
            color: darkgray;
            text-decoration: unset;
            cursor: pointer;
        }
</style>

<div class="navmenu navmenu-default navmenu-fixed-left offcanvas linkbotao">

    <br />
    <asp:UpdatePanel ID="upnlLogin" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlLogin" runat="server">

                <asp:TextBox placeholder="Usuário" ID="txtUsuario" CssClass="text-left" runat="server" Style="background-color: lightgray; width: 60%; margin-bottom: 1%; margin-right: 28%; margin-left: 15%; border-top-left-radius: 10px; border-top-right-radius: 10px" />
                <asp:TextBox placeholder="Senha" ID="txtSenha" CssClass="text-left" runat="server" TextMode="Password" Style="background-color: lightgray; width: 60%; margin-bottom: 1%; margin-right: 28%; margin-left: 15%; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px" />
                <asp:Button ID="btnLogin" OnClick="btnLogin_Click" Text="Logar" runat="server" CssClass="btn" Style="width: 60%; margin-right: 9%; margin-left: 15%; margin-top: 1%; margin-bottom: 1%; border-radius: 10px" />
                <br />
                <asp:LinkButton ID="btnEsqueceuSenha" OnClick="btnEsqueceuSenha_Click" Text="Esqueci a senha" runat="server" Style="width: 34%; margin-left: 24%; margin-right: 16%" />
                <br />
                <a href="NovoUsuario.aspx" id="btnInscrever" style="width: 24%; margin-left: 7%">Cadastrar ou Logar em Rede Social</a>
                <br>
                <asp:Label ID="ltmsgAutent" runat="server" Style="font-size: small; color: red" />
            </asp:Panel>
            <asp:Panel ID="pnlLogado" runat="server">
                <ul class="nav navmenu-nav" style="color: lightblue; }: hover {background-color:lightblue}">
                    <li style="width: 100%">
                        <asp:ImageButton ID="imgUsuFoto" runat="server" PostBackUrl="../Telas/MinhaConta.aspx" Style="width: 20%; border-top-right-radius: 80px; border-bottom-right-radius: 80px" CssClass="resizeimage" />
                        <asp:LinkButton ID="lkbUsuNome" runat="server" Width="72%" PostBackUrl="../Telas/MinhaConta.aspx" />
                    </li>
                </ul>
            </asp:Panel>
            <hr />
            <asp:Panel ID="pnlOptLogado" runat="server">
                <ul class="nav navmenu-nav">
                    <li style="width: 100%"><a href="../Telas/MeusProjetos.aspx">Meus Projetos</a></li>
                    <li style="width: 100%"><a href="../Telas/Pesquisar.aspx">Buscar Projeto</a></li>
                    <li style="width: 100%"><a href="../Telas/NovoProjeto.aspx">Novo Projeto</a></li>
                </ul>
                <hr />
                <ul class="nav navmenu-nav">
                    <li style="width: 100%"><a href="../Telas/MinhaConta.aspx">Minha Conta</a></li>
                    <li style="width: 100%"><a href="../Telas/AlterarSenha.aspx">Alterar Senha</a></li>
                    <hr />
                    <li style="width: 100%"><a href="../Telas/SobreSistema.aspx">Sobre</a></li>
                    <li style="width: 100%"><a name="btnModalContatar" href="#" id="btnModalContatar" onclick="javascript:document.getElementById('ModalContatar').style.display = 'block';">Contatar</a></li>
                    <li style="width: 100%"><asp:LinkButton ID="lkbLogout" runat="server" OnClick="lkbLogout_Click">Logout</asp:LinkButton></li>
                </ul>

            </asp:Panel>

            <asp:Panel ID="pnlOptLogin" runat="server">
                <ul class="nav navmenu-nav">
                    <li style="width: 100%"><a href="../Telas/Pesquisar.aspx">Buscar Projeto</a></li>
                    <li style="width: 100%"><a href="../Telas/SobreSistema.aspx">Sobre</a></li>
                    <li style="width: 100%"><a name="btnModalContatar" href="#" id="btnoptModalContatar" onclick="javascript:document.getElementById('ModalContatar').style.display = 'block';">Contatar</a>
                </ul>
            </asp:Panel>
            <%--<uc1:ModalContatar runat="server" ID="uc1ModalContatar" />--%>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<ul class="nav navmenu-nav">--%>
    <%--<li class="dropdown ">
                    <a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#" class="dropdown-toggle" data-toggle="dropdown">Dropdown <b class="caret"></b></a>
                    <hr />
                    <ul class="dropdown-menu navmenu-nav">
                        <li style="width:100%"><a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#">Action</a></li>
                        <li style="width:100%"><a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#">Another action</a></li>
                        <li style="width:100%"><a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#">Something else here</a></li>
                        <li class="divider "></li>
                        <li class="dropdown-header ">Nav header</li>
                        <li style="width:100%"><a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#">Separated link</a></li>
                        <li style="width:100%"><a href="http://www.jasny.net/bootstrap/examples/navmenu-push/#">One more separated link</a></li>
                    </ul>
                    <hr />
                </li>--%>
    <%--</ul>--%>
    <uc1:progressbar runat="server" ID="progressbar" />
</div>

<div class=" row navbar-default " style="margin: unset; background-color: lightblue">

    <button type="button" class="navbar-toggle botaoApp" data-toggle="offcanvas" data-target=".navmenu" data-canvas="body">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
    </button>
    <asp:ImageButton ImageUrl="../ImgLayer/homeicon.png" Style="height: 32px; width: 36px" CssClass=" botaoApp" runat="server" ID="btnHome" OnClick="btnHome_Click" />
    <asp:ImageButton ImageUrl="../ImgLayer/newprojicon.png" Style="height: 32px; width: 36px" CssClass=" botaoApp" runat="server" ID="btnNewProj" OnClick="btnNewProj_Click" />
    <asp:ImageButton ImageUrl="../ImgLayer/profileicon.png" Style="height: 32px; width: 36px" CssClass=" botaoApp" runat="server" ID="btnProfile" OnClick="btnProfile_Click" />
    <asp:ImageButton ImageUrl="../ImgLayer/searchicon.png" Style="height: 32px; width: 36px" CssClass=" botaoApp" runat="server" ID="btnPesquisar" OnClick="btnPesquisar_Click" />
    <%--<input type="image" onclick="javascript:(document.getElementById('modalPesquisa')).style.display = 'block';" class=" botaoApp" src="../ImgLayer/searchicon.png" style="height: 32px; width: 36px">--%>

</div>

<!-- /.container -->

<!-- Bootstrap core JavaScript
    ================================================== -->
<!-- Placed at the end of the document so the pages load faster -->
<%--<script src="./Off Canvas Push Menu Template for Bootstrap_files/jquery-1.10.2.min.js"></script>--%>
<%--<script src="../Style/jquery1.11.3/jquery-1.11.3.min.js"></script>--%>

<%--<script>
    /// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" />
    document.ready(function () {


        console.log(document);
        function ConnectionResponseUserControl() {
            
            FB.api('/me?fields=id', function (response) {
               
               
                $('#facebookID').val(response.id);
                $('#btnLogin').click();

            });
        }

    });


</script>--%>

<script src="../Style/bootstrap-3.3.5-dist/js/bootstrap.min.js"></script>
<script src="../Style/jasny-bootstrap/js/jasny-bootstrap.min.js"></script>
<%--<script src="../Style/jasny-bootstrap/js/offcanvas.js"></script>--%>
<link href="../Style/jasny-bootstrap/js/offcanvas.css" rel="stylesheet" />
<%-- <script src="./Off Canvas Push Menu Template for Bootstrap_files/bootstrap.min.js"></script>
    <script src="./Off Canvas Push Menu Template for Bootstrap_files/jasny-bootstrap.min.js"></script>
    <script src="./Off Canvas Push Menu Template for Bootstrap_files/offcanvas.js"></script>--%>
    
