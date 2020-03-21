<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaceLogin.aspx.cs" Inherits="TechnoDemokratia.Telas.FaceLogin" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />

    <style>
        .text-center {
            margin: 1.5%;
            width: 100%;
        }
    </style>

</head>
<script src="../Style/bootstrap-3.3.5-dist/js/facebooklogin.js"></script>

<script>


    function facebookConnectedResponse(responsevalue) {
        var id = 0;
        FB.api('/me?fields=id,name,birthday,email,gender', function (response) {
            id = response.id;

        });

        FB.api('http://graph.facebook.com/me/picture?type=large', function (responsepic) {
            $("#txtID").val(id);
            $("#txtImg").val(responsepic.data.url);
            document.getElementById("btnCadastrarPessoa").click();
        });


    }
</script>

<body>

    <form id="form1" runat="server" enctype="multipart/form-data">

        <asp:ScriptManager ID="scrptmgr" runat="server" />

        <div class="container">

            <div style="align-content: center;">
                <asp:UpdatePanel ID="upPanel" runat="server" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnCadastrarPessoa" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="active">
                            <div class="col-md-6 container container-fluid">
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtImg" CssClass="inputfile" />
                                    <asp:TextBox runat="server" ID="txtID" CssClass="inputfile" />
                                    <asp:Button runat="server" ID="btnCadastrarPessoa" CssClass="inputfile" OnClick="btnLogarPessoa_Click" Text="Cadastrar-se" class=" btn btn" Style="border-radius: unset; background-color: lightblue" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
</body>
</html>
