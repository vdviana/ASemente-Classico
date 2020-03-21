using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.ServicoVotacao;

namespace TechnoDemokratia.UserControls
{
    public partial class Artigo : System.Web.UI.UserControl
    {
        public string Descricao { get; set; }
        public string IdArtigo { get; set; }
        public int IdPessoa { get; set; }
        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(hfIdArtigo.Value))
            {
                return;
            }
            if (IsPostBack)
            {
                //pnlAddSugestao.Visible = false;//Session["opensug"] != null && Session["opensug"].ToString() == hfIdArtigo.Value.ToString();
            }

            if (Session["Pessoa"] == null)
            {
                litbtnsug.Text = "<input type=\"button\"  disabled=\"disabled\"  Value=\"Sugerir\" Style=\"width: 24%; border-radius: unset; background-color: cadetblue; color: white;\" Class=\"btn\" />";
                btnAprov.Enabled = false;
                btnReprov.Enabled = false;
                //btnSug.Enabled = false;
                return;
            }

            IdPessoa = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa;
            IdArtigo = hfIdArtigo.Value;
            Descricao = lblDescricao.Text;
            //txtDescricaoNArt.Text = lblDescricao.Text;
            //if (Session["lstReferencias"] != null && Session["opensug"]!= null && Session["opensug"].ToString()== IdArtigo)
            //{
            //    grdReferencias.DataSource = Session["lstReferencias"];
            //    grdReferencias.DataBind();
            //}


            using (ServicoVotacao.InterfaceVotacaoClient svcvotacao = new ServicoVotacao.InterfaceVotacaoClient())
            {

                ServicoVotacao.VotoTO votoTO = new ServicoVotacao.VotoTO();


                votoTO.IdPessoa = IdPessoa;
                votoTO.IdArtigo = int.Parse(hfIdArtigo.Value);

                votoTO = svcvotacao.ConsultarVoto(votoTO);
                //tratar exceções do tipo endpoint not found exception para "sem comunicação com o serviço do contexto, por favor acesse mais tarde"

                if (votoTO.IdVoto > 0)
                {
                    btnAprov.Enabled = !votoTO.OpcaoVoto;
                    btnReprov.Enabled = votoTO.OpcaoVoto;
                }
            }


            using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
            {
                ServicoPublicacao.RetornoServicoTO retorno = new ServicoPublicacao.RetornoServicoTO();
                retorno = svcpub.ConsultarHaSugestaoPessoa(((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa, Convert.ToInt32(hfIdArtigo.Value.ToString()));



                if (retorno.InfoRetorno == "S")
                {
                    litbtnsug.Text = "<input type=\"button\"  disabled=\"disabled\"  ID=\"btnSug\"  value=\"Sugerir\" Style=\"width: 24%; border-radius: unset; background-color: cadetblue; color: white;\" Class=\"btn\" />";
                }
                else
                {
                    litbtnsug.Text = "<input type=\"button\"  ID=\"btnSug\" onclick=\"javascript:mostrarModalSugestao(" + IdArtigo + ", '" + Descricao.ToString().Replace("\"", "#").Replace("”", "#").Replace("\n", "").Replace("\r", "").Replace("\t", "") + "');\" Value=\"Sugerir\" Style=\"width: 24%; border-radius: unset; background-color: cadetblue; color: white;\" Class=\"btn\" />";
                }
            }



        }

        private void Votar(bool p_OpcaoVoto, VotoTO p_Voto = null)
        {
            using (ServicoVotacao.InterfaceVotacaoClient svcvotacao = new ServicoVotacao.InterfaceVotacaoClient())
            {
                ServicoVotacao.VotoTO votoTO = new ServicoVotacao.VotoTO { IdPessoa = IdPessoa, IdArtigo = int.Parse(hfIdArtigo.Value), OpcaoVoto = p_OpcaoVoto };
                if (p_Voto != null)
                {
                    votoTO = p_Voto;
                }

                RetornoServicoTO retorno = svcvotacao.EfetuarVotacao(votoTO);

                if (retorno.Sucesso)
                {
                    Response.Redirect("Projeto.aspx");
                }
            }
        }

        protected void btnAprov_Click(object sender, EventArgs e)
        {
            Votar(true);
        }

        protected void btnReprov_Click(object sender, EventArgs e)
        {
            Votar(false);
        }

        //protected void btnSug_Click(object sender, EventArgs e)
        //{

        //    Session["opensug"] = hfIdArtigo.Value;
        //    Session["lstReferencias"] = null;
        //    grdReferencias.DataBind();
        //    pnlAddSugestao.Visible = !pnlAddSugestao.Visible;
        //}

        public void ListarSugestoes(int id_artigo)
        {
            try
            {
                //Repeater rptsubs = (Repeater)rptArtigos.Items[id_registro].FindControl("rptSubstitutivos");

                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    var ArtigosSubst = svcpub.ListarArtigosSubstitutivosDoArtigo(id_artigo, 0, 20);

                    if (ArtigosSubst != null && ArtigosSubst.Lista != null && ArtigosSubst.Lista.Count > 0)
                    {
                        rptSubstitutivos.DataSource = ArtigosSubst.Lista;
                        rptSubstitutivos.DataBind();
                        // rptArtigos.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //tenho que pensar no que fazer aqui: talvez popar uma mensagem criptografada ou enviar um email automatico , aviso_erro , com o textpo criptografado...
            }
        }

        protected void btnLstSug_Click(object sender, EventArgs e)
        {
            pnlsugestoes.Visible = true;
            ListarSugestoes(int.Parse(hfIdArtigo.Value));

        }


        protected void rptSubstitutivos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idartigo = Convert.ToInt32(e.CommandArgument);

            VotoTO votoTO = new VotoTO();

            votoTO.IdPessoa = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa;
            votoTO.IdArtigo = idartigo;


            if (e.CommandName == "Aprov")
            {
                votoTO.OpcaoVoto = true;
                Votar(true, votoTO);

            }
            else if (e.CommandName == "Reprov")
            {
                votoTO.OpcaoVoto = false;
                Votar(false, votoTO);
            }
            else if (e.CommandName == "Arg")
            {
                //modalargumentos.Carregar(idartigo)
                //modal argumentos.open()
                //do nothing
            }



        }

        protected void rptSubstitutivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button btnAprovSub = (Button)e.Item.FindControl("btnsubstaprov");
            Button btnReprovSub = (Button)e.Item.FindControl("btnsubstreprov");


            ServicoVotacao.VotoTO votoTO = new ServicoVotacao.VotoTO();

            using (ServicoArticulacao.InterfaceArticulacaoClient svcart = new ServicoArticulacao.InterfaceArticulacaoClient())
            {
                ServicoArticulacao.ComentarioTO comentario = svcart.ConsultarComentario(int.Parse(((HiddenField)e.Item.FindControl("hfartigosub")).Value));

                if (comentario != null && comentario.IdComentario > 0)
                {

                    ((Label)e.Item.FindControl("lblArgumento")).Text = comentario.Descricao;

                    ServicoArticulacao.ListaReferenciaTO listareferencias = svcart.ListarReferencias(comentario.IdComentario);

                    if (listareferencias != null && listareferencias.Lista.Count() > 0)
                    {

                        ((Literal)e.Item.FindControl("litReferencias")).Text += string.Join(", <br/> ", listareferencias.Lista.Select(x => x.LinkReferencia).ToList());
                    }
                }
            }




            if (Session["Pessoa"] == null)
            {

                litbtnsug.Text = "<input type=\"button\"  enabled=\"false\"  ID=\"btnSug\" onclick=\"javascript:mostrarModalSugestao(" + Eval("IdArtigo") + ", '" + Eval("Descricao").ToString().Replace("\"", "#").Replace("”", "#").Replace("\n", "").Replace("\r", "").Replace("\t", "") + "');\" Value=\"Sugerir\" Style=\"width: 24%; border-radius: unset; background-color: cadetblue; color: white;\" Class=\"btn\" />";
                btnAprovSub.Enabled = false;
                btnReprovSub.Enabled = false;
                return;
            }





            votoTO.IdPessoa = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa;
            votoTO.IdArtigo = int.Parse(((HiddenField)e.Item.FindControl("hfartigosub")).Value);

            using (ServicoVotacao.InterfaceVotacaoClient svcvotacao = new ServicoVotacao.InterfaceVotacaoClient())
            {
                votoTO = svcvotacao.ConsultarVoto(votoTO);
            }

            if (votoTO.IdVoto > 0)
            {
                btnAprovSub.Enabled = !votoTO.OpcaoVoto;
                btnReprovSub.Enabled = votoTO.OpcaoVoto;
            }




        }

        protected void btnArg_Click(object sender, EventArgs e)
        {

        }

        protected void btnSug_Click(object sender, EventArgs e)
        {

        }
    }
}