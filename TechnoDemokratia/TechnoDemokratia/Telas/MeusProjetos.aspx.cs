using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoDemokratia.Telas
{
    public partial class MeusProjetos : System.Web.UI.Page
    {


        protected void btnReload_Click(object sender, EventArgs e)
        {
            ListarProjetos();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Pessoa"] == null)
                {
                    Response.Redirect("..\\Telas\\Main.aspx");
                }

                ViewState["IndiceProjetos"] = 0;

                ListarProjetos();

            }
        }

        public void ListarProjetos(int index = 0)
        {
            try
            {
                ServicoPublicacao.ListaProjetoTO listaprojetos = new ServicoPublicacao.ListaProjetoTO();
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    int i = int.Parse(ViewState["IndiceProjetos"].ToString());

                    var pessoa = (ServicoAutenticacao.PessoaTO)Session["Pessoa"];

                    listaprojetos = svcpub.PesquisarProjetos(new ServicoPublicacao.ProjetoTO { IdPessoa = pessoa.IdPessoa }, 0, 5, null, null);
                    listaprojetos = svcpub.ListarProjetos(i, 6);
                    if (listaprojetos.Lista.Count < 6)
                    {
                        btnMaisProjetos.Visible = false;
                    }

                }

                if (ViewState["listaprojetos"] == null)
                {
                    ViewState["listaprojetos"] = listaprojetos.Lista;
                }
                else
                {
                    var j = new List<ServicoPublicacao.ProjetoTO>();

                    j = (List<ServicoPublicacao.ProjetoTO>)ViewState["listaprojetos"];
                    j.AddRange(listaprojetos.Lista);
                    ViewState["listaprojetos"] = j;
                }

                rptMeusProjetos.DataSource = ViewState["listaprojetos"];
                rptMeusProjetos.DataBind();

                pnlprojetos.Visible = true;
                btnReload.Visible = false;
            }
            catch
            {
                pnlprojetos.Visible = false;
                btnReload.Visible = true;
            }

        }

        protected void rptprojetos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Check")
            {

            }
            else if (e.CommandName == "Author")
            {

            }
        }

        protected void hlMaisProjetos_Click(object sender, EventArgs e)
        {
            ViewState["IndiceProjetos"] = int.Parse(ViewState["IndiceProjetos"].ToString()) + 1;
            ListarProjetos(int.Parse(ViewState["IndiceProjetos"].ToString()));
        }

        protected void rptMeusProjetos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Check")
            {
                Session["idProjeto"] = e.CommandArgument.ToString();
                Response.Redirect("\\Telas\\Projeto.aspx");
            }
        }
    }
}