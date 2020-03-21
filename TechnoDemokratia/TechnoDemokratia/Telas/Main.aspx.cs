using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.ServicoAutenticacao;
using TechnoDemokratia.ServicoPublicacao;

namespace TechnoDemokratia.Telas
{
    public partial class Main : System.Web.UI.Page
    {
        ListaProjetoTO listaprojetos = new ListaProjetoTO();





        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["off"] != null)
            {

                hfoff.Value = "S";

            }



            if (Session["Pessoa"] != null)
            {
                hfLogged.Value = "S";
            }

            if (!IsPostBack)
            {
                ViewState["IndiceProjetos"] = 0;

                ListarProjetos();

            }
        }

        public void ListarProjetos(int index = 0)
        {
            try
            {

                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new InterfacePublicacaoClient())
                {
                    int i = int.Parse(ViewState["IndiceProjetos"].ToString());


                    listaprojetos = svcpub.ListarProjetos(i, 6);
                    if (listaprojetos.Lista.Count < 6)
                    {
                        hlMaisProjetos.Visible = false;
                    }

                }

                if (ViewState["listaprojetos"] == null)
                {
                    ViewState["listaprojetos"] = listaprojetos.Lista;
                }
                else
                {
                    var j = new List<ProjetoTO>();

                    j = (List<ProjetoTO>)ViewState["listaprojetos"];
                    j.AddRange(listaprojetos.Lista);
                    ViewState["listaprojetos"] = j;
                }

                rptprojetos.DataSource = ViewState["listaprojetos"];
                rptprojetos.DataBind();

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
                Session["IdProjeto"] = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("\\Telas\\Projeto.aspx");
            }
            else if (e.CommandName == "Author")
            {
                Session["IdAutor"] = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("\\Telas\\Autor.aspx");
            }
        }

        protected void hlMaisProjetos_Click(object sender, EventArgs e)
        {
            ViewState["IndiceProjetos"] = int.Parse(ViewState["IndiceProjetos"].ToString()) + 1;
            ListarProjetos(int.Parse(ViewState["IndiceProjetos"].ToString()));
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            ListarProjetos();
        }
    }
}