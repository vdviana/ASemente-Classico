using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.ServicoAutenticacao;

namespace TechnoDemokratia
{
    public partial class MenuPrincipal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie myCookie = new HttpCookie("facebookID");
            //myCookie = Request.Cookies["facebookID"];
            //if (myCookie != null && !string.IsNullOrEmpty(myCookie.Value)&& Session["Pessoa"] == null)
            //{

            //}


            AtualizarPaineisLogin();

            if (!IsPostBack)
            {

            }
        }




        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if ((txtSenha.Text == string.Empty || txtUsuario.Text == string.Empty))
            {
                ltmsgAutent.Text = "Credenciais Inválidas";
                return;
            }

            PessoaTO pessoa = new PessoaTO();

            try
            {
                using (ServicoAutenticacao.InterfaceAutenticacaoClient svcautent = new ServicoAutenticacao.InterfaceAutenticacaoClient())
                {


                    pessoa = svcautent.Autenticar(txtSenha.Text, txtUsuario.Text);

                }
            }
            catch (EndpointNotFoundException ex)
            {


            }

            if (!pessoa.Sucesso)
            {

                ltmsgAutent.Text = pessoa.DescricaoFalha;
            }
            else
            {
                lkbUsuNome.Text = pessoa.Nome;
                Session["Pessoa"] = pessoa;
                Session["off"] = null;
                AtualizarPaineisLogin();

            }
        }

        private string TratarNomePerfil(string nomePerfil)
        {

            if (nomePerfil.Length > 27)
            {
                return nomePerfil.Substring(0, 27);
            }
            else
            {
                return nomePerfil;
            }

        }

        private void AtualizarPaineisLogin()
        {

            bool logado = false;
            if ((Session["Pessoa"] != null && ((PessoaTO)Session["Pessoa"]).IdPessoa > 0) && Session["off"] == null)
            {
                logado = true;
                lkbUsuNome.Text = TratarNomePerfil(((PessoaTO)Session["Pessoa"]).Nome ?? "");
                imgUsuFoto.ImageUrl = ((PessoaTO)Session["Pessoa"]).ImagemPerfil ?? "";


                var taualizada = Session["TelaAtualizada"]+"";
                if (Session["TelaAtualizada"].ToString() == "N" || Session["TelaAtualizada"] == null)
                {
                    Session["TelaAtualizada"] = "S";

                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                Session["TelaAtualizada"] = "N";

            }

            pnlLogado.Visible = logado;
            pnlLogin.Visible = !logado;
            pnlOptLogado.Visible = logado;
            pnlOptLogin.Visible = !logado;
        }

        protected void btnEsqueceuSenha_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lkbLogout_Click(object sender, EventArgs e)
        {

            Session["TelaAtualizada"] = "N";
            Session["off"] = true;
            Session["Pessoa"] = null;
            Response.Redirect(Request.RawUrl);
        }



        protected void btnInscrever_Click(object sender, EventArgs e)
        {

        }



        protected void btnHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Telas/Main.aspx");
        }

        protected void btnNewProj_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Telas/NovoProjeto.aspx");
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Telas/BuscarProjeto.aspx");
        }

        protected void btnProfile_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Telas/MinhaConta.aspx");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Telas/Pesquisar.aspx");
        }
    }
}