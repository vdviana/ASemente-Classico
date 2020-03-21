<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoProjeto.aspx.cs" Inherits="TechnoDemokratia.NovoProjeto" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />
    <script type="text/javascript"></script>

</head>
<script src="../Scripts/facebooklogin.js"></script>
<script type="text/javascript">
</script>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="scrptmgr" EnablePageMethods="true" runat="server" />
        <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <br />
            <br />
            <br />
            <div style="align-content: center;">
                <asp:UpdatePanel ID="upPanel" runat="server" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnIncluirProjeto" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="active">
                            <div class="col-md-4" style="height: 30%">
                                <asp:FileUpload ID="imgInput" class="inputfile " runat="server" onchange="javascript:readImageURL(this);" />
                                <label for="imgInput" class="btnfupload btn btn-link " style="float: left; width: 90%; background-color: lightblue; border-top-left-radius: 20px; border-top-right-radius: 20px" id="addimg">Anexar Imagem</label>
                                <img id="imgPreview" src="../Images/imgimg.png" style="float: left; width: 90%; height: 150px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px" class="resizeimage" />

                                <asp:DropDownList runat="server" ID="ddlTipoProjeto" Style="margin-top: 5%; width: 90%; border-radius: 5px">
                                    <asp:ListItem Text="Selecione tipo de projeto" Value="0" />
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:DropDownList ID="ddlPosPolitica" runat="server" Style="width: 90%; border-radius: 5px">
                                    <asp:ListItem Text="Selecione posição politica do projeto" Value="0" />
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:TextBox placeholder="Numero Registrado" runat="server" ID="txtNumRegistro" CssClass="text-center" Style="width: 45%; height: 100%; font-size: small; border-radius: 5px" />
                                <asp:TextBox placeholder="Ano de Registro" runat="server" ID="txtAnoRegistro" CssClass="text-center" Style="width: 45%; height: 100%; font-size: small; border-radius: 5px" />
                                <br />
                                <br />
                                <div>
                                    <asp:FileUpload runat="server" onchange="javascript:readFileURL(this);" ID="fileInput" class="inputfile" />
                                    <label for="fileInput" class="btnfupload btn btn-link " style="background-color: lightblue; border-radius: 5px" id="addfile">Anexar Arquivo</label>
                                </div>
                            </div>
                            <div class="col-md-8" style="height: 10%">
                                <asp:TextBox placeholder="Adicione Um titulo" runat="server" ID="txtAddTitulo" CssClass="text-center" Style="width: 100%; height: 100%; font-size: large; border-radius: 5px" />
                            </div>
                            <br />
                            <br />
                            <div class="col-md-8">
                                <asp:TextBox placeholder="Descreva o projeto" runat="server" ID="txtDescricao" CssClass="text-center" Style="width: 100%; height: 350px; font-size: medium; border-radius: 5px" TextMode="MultiLine" />
                            </div>
                            <br>
                            <div class="col-md-12 ">
                                <div style="float: right">
                                    <asp:Button runat="server" ID="btncancelar" Text="Cancelar" class=" btn btn" Style="border-radius: 5px; background-color: red; color: white" OnClick="btncancelar_Click" />
                                    &nbsp; 
                                <asp:Button runat="server" ID="btnIncluirProjeto" Text="Cadastrar ProjetoLei" class=" btn btn" Style="border-radius: 5px; background-color: lightblue" OnClick="btnIncluirProjeto_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <script src="../UserControls/jsuploadimage.js"></script>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
</body>
</html>
