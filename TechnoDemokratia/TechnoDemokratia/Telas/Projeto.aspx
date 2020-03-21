<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Projeto.aspx.cs" Inherits="TechnoDemokratia.Projeto" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>
<%@ Register Src="~/UserControls/progressbar.ascx" TagPrefix="uc1" TagName="progressbar" %>
<%@ Register Src="~/UserControls/Artigo.ascx" TagPrefix="uc1" TagName="Artigo" %>



<script src="../UserControls/jsuploadimage.js"></script>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />
    <style>
        hr.EstiloArtigo {
            padding: 0;
            border: none;
            border-top: medium double #333;
            color: #333;
            text-align: center;
        }

            hr.EstiloArtigo:after {
                content: "§";
                display: inline-block;
                position: relative;
                top: -0.7em;
                font-size: 1.5em;
                padding: 0 0.25em;
                background-color: #F1F1F1;
            }
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.6); /* Black w/ opacity */
        }

        .modal-content2 {
            margin-top: 15%;
            margin-right: 15%; /* 15% from the top and centered */
            margin-left: 15%;
            margin-bottom: 15%;
            width: 55%;
        }
        /* Modal Content/Box */
        .modal-content {
            background-color: none;
            margin-top: 15%;
            margin-right: 15%; /* 15% from the top and centered */
            margin-left: 15%;
            margin-bottom: 15%;
            border: 0px hidden;
            width: 55%; /* Could be more or less, depending on screen size */
        }

        /* The Close Button */
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }
    </style>



</head>

<body>

    <form id="form1" runat="server" enctype="multipart/form-data">


        <asp:ScriptManager ID="scrptmgr" runat="server" />
        <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
        <div>
            <br />
            <br />
            <br />
            <div style="align-content: center;">
                <asp:UpdatePanel ID="upPanel" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="col-md-4" style="height: 30%">
                                <asp:Image ID="imgProj" runat="server" Style="width: 70%; margin-left: 17%; float: left; height: 80%; border-radius: 20px" class="resizeimage" />
                                <br />
                                <br />

                            </div>
                            <br />
                            <br />
                            <div class="col-md-8" style="height: 10%">
                                <asp:Label runat="server" ID="lblTitulo" CssClass="text-center" Style="width: 100%; height: 100%; font-size: large" />
                            </div>
                            <br />
                            <br />
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblDescricao" CssClass="text-center" Style="width: 100%; height: 350px; font-size: medium" />
                            </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" ID="lblTipoProjeto" Style="width: 90%"></asp:Label>
                                &nbsp;
                                <asp:Label runat="server" ID="lblNumRegistro" CssClass="text-center" Style="width: 45%; height: 100%; font-size: small" />
                                &nbsp;
                                <asp:Label runat="server" ID="lblAnoRegistro" CssClass="text-center" Style="width: 45%; height: 100%; font-size: small" />
                                <br />
                                <br />
                                <asp:Label ID="lblPosPolitica" runat="server" Style="width: 90%"></asp:Label>
                                <br />
                                <br />
                                <asp:LinkButton runat="server" Text="inteiro Teor" ID="lkbProjeto" OnClick="lkbProjeto_Click" />
                                <asp:HiddenField ID="hfCaminhoArquivo" runat="server" />
                            </div>

                        </div>
                        <br>

                        <div>
                            <asp:Repeater ID="rptArtigos" OnItemDataBound="rptArtigos_ItemDataBound" runat="server">
                                <ItemTemplate>

                                    <uc1:Artigo ID="ucArtigo" runat="server" />

                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hfIndex" Value="0" />
                    </ContentTemplate>

                </asp:UpdatePanel>
                <br />
                <br />
                <br />
                <div class="col-md-12" style="align-items: center">
                    <asp:LinkButton ID="lkbMaisArtigos" runat="server" OnClick="lkbMaisArtigos_Click" Text="Carregar Mais Artigos" />
                </div>
            </div>
            <script type="text/javascript">

                function mostrarModalSugestao(artigo, descricao) {
                    $('#hfidartigopai').val(artigo.toString());
                    document.getElementById('ModalNovoSubstitutivo').style.display = 'block';
                    descricao = descricao.toString().replace("#", "\"").replace('#', "\"");
                    console.log(descricao);
                    descricao = descricao.toString().replace(";", ";\n");
                    //descricao = descricao.replace(';', ';\n');
                    //console.log(descricao);
                    //descricao = descricao.toString().replace('jjj', "\n");
                    console.log(descricao);
                    $('#txtDescricaoNArt').val(descricao);
                }

                function ocultarModalSugestao() {
                    $('#hfidartigopai').val('');
                    document.getElementById('ModalNovoSubstitutivo').style.display = 'none';
                }

            </script>


            <div runat="server" class="modal" id="ModalNovoSubstitutivo" style="display: none">
                <div class="modal-content2">
                    <span onclick="javascript: ocultarModalSugestao(); " style="color: white; opacity: 1" class="close">x</span>
                    <asp:Panel runat="server" ID="pnlAddSugestao" Visible="true">
                        <div class="col-md-12">
                            <label style="color: white">
                                Cadastrar nova sugestão</label><br />
                            <div class="col-md-12">
                                <asp:HiddenField ID="hfidartigopai" runat="server" />
                                <asp:TextBox ID="txtDescricaoNArt" Style="width: 100%; height: 200px" runat="server" TextMode="MultiLine" placeholder="Descreva..." />
                                <br />
                                <br />
                                <asp:TextBox ID="txtArgumento" Style="width: 100%; height: 200px" runat="server" TextMode="MultiLine" placeholder="Argumente (Opcional)..." />
                                <br />
                                <input class="btn" type="button" value="Adicionar Referência" id="btnAdReferencia" onclick="
    document.getElementById('ModalReferencia').style.display = 'block';"
                                    style="width: 25%; background-color: cadetblue; color: white;" />
                                <br />
                                <label>Referências</label>
                                <br />
                                <asp:GridView ID="grdReferencias" BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="false" runat="server" Style="width: 100%;" OnRowCommand="grdReferencias_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Mensagem" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href='<%# Eval("LinkReferencia")%>'><%# Eval("LinkReferencia")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandName="Excluir" CommandArgument='<%# Container.DataItemIndex %>' Style="color: red; opacity: 1; border: none" class="close" Text="x" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <input type="button" id="btnCadastrar" onclick="javascript:document.getElementById('ModalConfirmar').style.display='block';" class="btn " style="background-color: cadetblue; color: white" value="Concluir" />

                                <asp:Button ID="btnCancelarCadastro" CommandName="CancelCad" OnClick="btnCancelarCadastro_Click" CssClass="btn" Style="background-color: orangered; color: white; margin-left: 10px" Text="Cancelar" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>






            <div class="modal" id="ModalReferencia" style="display: none">
                <div class="modal-content2">
                    <span onclick="javascript:document.getElementById('ModalReferencia').style.display = 'none';" style="color: white; opacity: 1" class="close">x</span>
                    <div class="col-md-12">

                        <label style="color: white">
                            Aponte a referência</label><br />
                        <div class="col-md-11">
                            <asp:TextBox runat="server" ID="txtDescricaoReferencia" Text="" placeholder="Descreva a referência (Opcional)" Style="color: black; width: 100%" />
                        </div>
                        <div class="col-md-11">
                            <asp:TextBox runat="server" ID="txtReferencia" Text="" placeholder="cadastre um link" Style="color: black; width: 100%" />
                        </div>
                        <div class="col-md-11">
                            <label style="color: white">e/ou</label>
                        </div>
                        <div class="col-md-11">
                            <asp:FileUpload runat="server" onchange="javascript:readFileURL(this);" ID="fileInput" class="inputfile" />
                            <label for="fileInput" class="btnfupload btn btn-link " style="background-color: lightblue; border-radius: 5px" id="addfile">Envie um arquivo</label>
                        </div>
                        <div class="col-md-2" style="margin-top: 5px; float: right">
                            <asp:Button ID="btnCadReferencia" Style="background-color: lightblue" runat="server" OnClick="btnCadReferencia_Click" CssClass="btn" Text="Cadastrar" />
                        </div>
                        <div class="col-md-2" style="margin-top: 5px; float: right">
                            <input type="button" id="btnCancReferencia" onclick="javascript: document.getElementById('ModalReferencia').style.display = 'none';" style="background-color: red; color: white" runat="server" class="btn" value="Cancelar" />
                        </div>
                    </div>

                    <br />
                </div>
            </div>


            <div runat="server" class="modal" id="ModalConfirmar" style="display: none">
                <div class="modal-content2">
                    <div class="col-md-12">
                        <label style="color: white">
                            Após cadastrar a sugestão , e as referencias , não será possivel alterar as informações
                            Deseja realmente cadastrar esta sugestão?
                        </label>
                        <br />
                        <div class="col-md-2" style="margin-top: 5px; float: right">
                            <asp:Button ID="btnSim" Style="background-color: lightblue" runat="server" OnClick="btnCadastrar_Click" CssClass="btn" Text="Sim" />
                        </div>
                        <div class="col-md-2" style="margin-top: 5px; float: right">
                            <input type="button" id="btnNão" onclick="javascript: document.getElementById('ModalConfirmar').style.display = 'none';" style="background-color: red; color: white" runat="server" class="btn" value="Não" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <uc1:progressbar runat="server" ID="progressbar" />
    </form>
    <script src="../UserControls/jsuploadimage.js"></script>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
</body>
</html>
