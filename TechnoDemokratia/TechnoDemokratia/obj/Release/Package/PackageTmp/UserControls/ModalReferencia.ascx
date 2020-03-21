<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalReferencia.ascx.cs" Inherits="TechnoDemokratia.UserControls.ModalReferencia" %>


<style>
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

    /* Modal Content/Box */
    .modal-content {
        background-color: #fefefe;
        margin-top: 15%;
        margin-right: 15%; /* 15% from the top and centered */
        margin-left: 15%;
        margin-bottom: 15%;
        border: 1px hidden #888;
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

<!-- Trigger/Open The Modal -->
<asp:UpdatePanel ID="upnlMdlReferencia" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <!-- The Modal -->
        <div id="modalReferencia" class="modal" style="height: 100%">
            <!-- Modal content -->
            <div class="modal-content">
                <span onclick="javascript:document.getElementById('ModalContatar').style.display = 'none';" style="color: white; opacity: 1" class="close">x</span>

                <div class="col-md-12">

                    <asp:HiddenField runat="server" ID="hfArtigoReferencia" />
                    <label>
                        Aponte a referência</label><br />
                    <div class="col-md-11">
                        <asp:TextBox runat="server" Text="" placeholder="cadastre um link" Style="color: black; width: 100%" />

                    </div>
                    <div class="col-md-11">ou</div>
                    <div class="col-md-11">
                        <asp:FileUpload runat="server" onchange="javascript:readFileURL(this);" ID="fileInput" class="inputfile" />
                        <label for="fileInput" class="btnfupload btn btn-link " style="background-color: lightblue; border-radius: 5px" id="addfile">Envie um arquivo</label>

                    </div>
                </div>
                <input class="btn" type="button" value="Adicionar Referência" id="btnAdReferencia" onclick="javascript: document.getElementById('ModalReferencia').style.display = 'block'" style="width: 25%; background-color: cadetblue; color: white;" />
                <br />
                <label>Referências</label>
                <br />
                <asp:GridView ID="grdReferencias" runat="server" Style="width: 100%;" OnRowCommand="grdReferencias_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--<asp:HyperLink runat="server" ID="lkbReferencia" NavigateUrl='<%# Eval("Descricao") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
