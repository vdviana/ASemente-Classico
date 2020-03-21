<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Artigo.ascx.cs" Inherits="TechnoDemokratia.UserControls.Artigo" %>
<%--<%@ Register Src="~/UserControls/ModalReferencia.ascx" TagPrefix="uc1" TagName="ModalReferencia" %>--%>
<%--<script src="jsuploadimage.js"></script>--%>
<%--<uc1:ModalReferencia runat="server" ID="mdlreferencia" />--%>

<div class="col-md-12">

    <hr class="EstiloArtigo">
    <asp:Label runat="server" ID="lblDescricao" />
    <br />
    <br />
    <br />
    <asp:HiddenField ID="hfIdArtigo" runat="server" />
    <asp:Button ID="btnAprov" runat="server" OnClick="btnAprov_Click" Text="Aprovar" Style="width: 24%; border-radius: unset; background-color: lawngreen" CssClass="btn " />
    <asp:Button ID="btnReprov" runat="server" OnClick="btnReprov_Click" Text="Reprovar" Style="width: 24%; border-radius: unset; background-color: orangered; color: white;" CssClass="btn " />
    <asp:Literal id="litbtnsug" runat="server" />
    <asp:Button ID="btnLstSug" runat="server" OnClick="btnLstSug_Click" Text="Sugestões" Style="width: 24%; border-radius: unset; background-color: yellow; color: black;" CssClass="btn " />
</div>
<asp:Panel ID="pnlsugestoes" Visible="false" runat="server">
    <div style="margin-left: 20px">
        <div class="col-md-12" style="width: 97%; background-color: lightblue">
            <br />
            <br />
            <asp:Repeater ID="rptSubstitutivos" OnItemCommand="rptSubstitutivos_ItemCommand" OnItemDataBound="rptSubstitutivos_ItemDataBound" runat="server">
                <ItemTemplate>
                    <br />
                    <br />
                    <asp:Label runat="server" Text='<%# Eval("Descricao") %>' />
                    <br />
                    <asp:Label runat="server" ID="lblArgumento" />
                    <br />
                    <asp:Literal runat="server" ID="litReferencias" />
                    <br />
                    <asp:HiddenField ID="hfartigosub" Value='<%# Eval("IdArtigo") %>' runat="server" />
                    <asp:Button ID="btnsubstaprov" runat="server" CommandName="Aprov" CommandArgument='<%# Eval("IdArtigo") %>' Text="Aprovar" Style="width: 24%; border-radius: unset; background-color: lawngreen" CssClass="btn " />
                    <asp:Button ID="btnsubstreprov" runat="server" CommandName="Reprov" CommandArgument='<%# Eval("IdArtigo") %>' Text="Reprovar" Style="width: 24%; border-radius: unset; background-color: orangered; color: white;" CssClass="btn " />
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <br />
    </div>
</asp:Panel>





