<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalContatar.ascx.cs" Inherits="TechnoDemokratia.UserControls.ModalContatar" %>

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
<%--<asp:UpdatePanel ID="upnlMdlContatar" runat="server" ChildrenAsTriggers="true">--%>
    <%--<ContentTemplate>--%>
        <!-- The Modal -->
        <div id="ModalContatar" class="modal" style="height: 100%">
            <!-- Modal content -->
            <div class="modal-content">
                <span onclick="javascript:document.getElementById('ModalContatar').style.display = 'none';" style="color: white; opacity: 1" class="close">x</span>
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label>Insira seu nome:</label>
                        <asp:TextBox ID="txtNome" CssClass="text-center" Style="color: black" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Selecione motivo do contato:</label>
                        <asp:DropDownList ID="ddlMotivoContato" runat="server" Style="color: black">
                            <asp:ListItem Text="Sugestão" Value="Sugestao"></asp:ListItem>
                            <asp:ListItem Text="Reclamação" Value="Reclamacao"></asp:ListItem>
                            <asp:ListItem Text="Informativo" Value="Informativo"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label>Insira seu email:</label>
                        <asp:TextBox ID="txtEmail" CssClass="text-center" Style="color: black" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        <label>Insira um título (Opcional)</label><br />
                        <asp:TextBox ID="txtTitulo" CssClass="text-center" Style="color: black; width: 100%" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-7">
                        <label>Insira Descrição:</label><br />
                        <asp:TextBox ID="txtDescricao" CssClass="text-center" TextMode="MultiLine" Style="color: black; height: 100px; width: 100%" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12 ">
                    <div style="float: right ; margin-right:2%">
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" class=" btn btn" Style="border-radius: unset; background-color: red; color: white" OnClientClick="javascript:document.getElementById('ModalContatar').style.display = 'none';" />
                        &nbsp; 
                                <asp:Button runat="server" ID="btnEnviar" OnClick="btnEnviar_Click" Text="Enviar" class=" btn btn" Style="border-radius: unset; background-color: lightblue" OnClientClick="javascript:document.getElementById('ModalContatar').style.display = 'none';" />
                    </div>
                </div>
            </div>
        </div>
    <%--</ContentTemplate>--%>
<%--</asp:UpdatePanel>--%>

<%--<script type="text/javascript">--%>
  <%--  // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target != document.getElementById('ModalContatar')) {
            modalcontatar.style.display = "none";
        }
    }--%>
<%--</script>--%>

