using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.ServicoPublicacao;
using TechnoDemokratia.Util;

namespace TechnoDemokratia
{
    public partial class Projeto : System.Web.UI.Page
    {


        

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
            

                MaintainScrollPositionOnPostBack = true;

                if (Session["Pessoa"] == null)
                {
            
                    //TODO Carrega as opções pra cadastrar sugestõese para votar
                    //    Response.Redirect("..\\Telas\\Main.aspx");
                }

                if (Session["IdProjeto"] == null)
                {
                    Response.Redirect("..\\Telas\\Main.aspx");
                }
                Session["lstReferencias"] = null;
                Session["listaArtigos"] = null;
                ViewState["IndiceArtigos"] = 0;
                ObterDadosProjetoLei();
                ObterDetalhesEArtigosProjetoLei();
            }


            if (!string.IsNullOrWhiteSpace(hfidartigopai.Value))
            {
                ModalNovoSubstitutivo.Attributes.Remove("style");
                ModalNovoSubstitutivo.Attributes.Add("style", "display:block");
            }
        }

        private void ObterDetalhesEArtigosProjetoLei()
        {
            try
            {
                ServicoPublicacao.ListaArtigoTO artigos = new ServicoPublicacao.ListaArtigoTO();
                artigos.Lista = new List<ServicoPublicacao.ArtigoTO>();
                ServicoPublicacao.DetalhesProjetoTO detalhesprojeto = new ServicoPublicacao.DetalhesProjetoTO();
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {


                    int i = int.Parse(ViewState["IndiceArtigos"].ToString());

                    artigos = svcpub.ListarArtigosdoProjeto(Convert.ToInt32(Session["IdProjeto"].ToString()), i, 8);
                    if (artigos.Lista.Count < 8)
                    {
                        lkbMaisArtigos.Visible = false;
                    }

                }

                if (Session["listaArtigos"] == null)
                {
                    Session["listaArtigos"] = artigos.Lista;
                }
                else
                {
                    var j = new List<ArtigoTO>();

                    j = (List<ArtigoTO>)Session["listaArtigos"];
                    j.AddRange(artigos.Lista);
                    Session["listaArtigos"] = j;
                }

                rptArtigos.DataSource = Session["listaArtigos"];
                rptArtigos.DataBind();

                //pnlprojetos.Visible = true;
                //btnReload.Visible = false;

                //pnlprojetos.Visible = false;
                //btnReload.Visible = true;

                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", string.Format("window.Alert('{0}');", ex.Message, ex.GetBaseException().Message), true);
            }
        }

        private void ObterDadosProjetoLei()
        {
            ServicoPublicacao.ProjetoTO projeto = new ServicoPublicacao.ProjetoTO();
            try
            {
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    projeto.IdProjeto = Convert.ToInt32(Session["IdProjeto"]);
                    projeto = svcpub.PesquisarProjetos(projeto, 0, 1, null, null).Lista.FirstOrDefault();

                    if (projeto == null || projeto.IdProjeto == 0)
                    {
                        projeto.DescricaoFalha = string.Format("Não foi possivel encontrar o projeto id {0}", Session["IdProjeto"].ToString());
                        projeto.Sucesso = false;
                        return;
                    }
                    else
                    {
                        lblAnoRegistro.Text = projeto.AnoRegistro.ToString();
                        lblNumRegistro.Text = projeto.NumeracaoRegistro.ToString();
                        lblTipoProjeto.Text = projeto.TipoProjeto;
                        lblPosPolitica.Text = projeto.PosicaoPolitica.ToString();
                        lblTitulo.Text = projeto.Titulo;
                        lblDescricao.Text = projeto.Descricao;
                        imgProj.ImageUrl = projeto.CaminhoImagem;
                        hfCaminhoArquivo.Value = projeto.CaminhoArquivo;
                    }
                }
            }
            catch
            {
                projeto.DescricaoFalha = "Falha inesperada ao obter o projeto";
                projeto.Sucesso = false;
                return;
            }
        }

        protected void lkbProjeto_Click(object sender, EventArgs e)
        {
            //todo , popar inteiro teor          
        }

        protected void lkbMaisArtigos_Click(object sender, EventArgs e)
        {

            ViewState["IndiceArtigos"] = (1 + int.Parse(hfIndex.Value.Split(',')[0].ToString())).ToString();
            ObterDadosProjetoLei();
            ObterDetalhesEArtigosProjetoLei();
        }

        protected void rptArtigos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {




            var Artigo = (UserControl)e.Item.FindControl("ucArtigo");
            var item = (ArtigoTO)e.Item.DataItem;
            ((HiddenField)Artigo.FindControl("hfIdArtigo")).Value = item.IdArtigo.ToString();
            ((Label)Artigo.FindControl("lblDescricao")).Text = item.Descricao.ToString();




            if (Session["opensug"] != null && item.IdArtigo.ToString() == Session["opensug"].ToString())
            {

                ((Panel)Artigo.FindControl("pnlAddSugestao")).Visible = true;

                if (Session["lstReferencias"] != null)
                {
                    ((GridView)Artigo.FindControl("grdReferencias")).DataSource = Session["lstReferencias"];
                    ((GridView)Artigo.FindControl("grdReferencias")).DataBind();
                }
            }
            else
            {
                //((Panel)Artigo.FindControl("pnlAddSugestao")).Visible = false;
            }

        }

        protected void btnCadReferencia_Click(object sender, EventArgs e)
        {
            List<ServicoArticulacao.ReferenciaTO> listareferencia = new List<ServicoArticulacao.ReferenciaTO>();
            if (Session["lstReferencias"] != null)
            {
                listareferencia.AddRange((List<ServicoArticulacao.ReferenciaTO>)Session["lstReferencias"]);
            }
            ServicoArticulacao.ReferenciaTO referenciaTO = new ServicoArticulacao.ReferenciaTO();
            string Descricao = string.IsNullOrWhiteSpace(txtDescricaoReferencia.Text) ? "" : txtDescricaoReferencia.Text;
            if (fileInput.HasFile)
            {
                string referenciaPath = ConfigurationManager.AppSettings["UploadReferenciaPath"].ToString();
                referenciaPath = Path.Combine(referenciaPath, "referencia_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + Path.GetExtension(fileInput.FileName).Replace(".", ""));
                fileInput.SaveAs(referenciaPath);
                referenciaTO.IdComentario = int.Parse(hfidartigopai.Value);
                referenciaTO.Mensagem = Descricao;
                referenciaTO.LinkReferencia = referenciaPath;
                listareferencia.Add(referenciaTO);
            }

            if (!string.IsNullOrWhiteSpace(txtReferencia.Text))
            {
                referenciaTO = new ServicoArticulacao.ReferenciaTO();
                referenciaTO.IdComentario = int.Parse(hfidartigopai.Value);
                referenciaTO.Mensagem = Descricao;
                referenciaTO.LinkReferencia = txtReferencia.Text;
                listareferencia.Add(referenciaTO);
            }

            if (listareferencia.Count > 0)
            {

                Session["lstReferencias"] = listareferencia;

                //ObterDetalhesEArtigosProjetoLei();

                grdReferencias.DataSource = Session["lstReferencias"];
                grdReferencias.DataBind();



            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cadastrar(txtDescricaoNArt.Text + " ", hfidartigopai.Value, txtArgumento.Text);

        }

        protected void Cadastrar(string Descricao, string id_artigoPai, string Argumentacao)
        {
            try
            {

                ServicoPublicacao.RetornoServicoTO sucesso = new ServicoPublicacao.RetornoServicoTO();
                ServicoPublicacao.ArtigoTO artigo = new ServicoPublicacao.ArtigoTO();
                int sugRetorno = 0;
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    sucesso = svcpub.IncluirSugestaoArtigo(int.Parse(id_artigoPai), Descricao);
                    //TODO retornar  id do artigo , sugest]ao cadastrado para cadastrar nas ref3erencias apontadas
                    sugRetorno = int.Parse(sucesso.InfoRetorno);
                }

                if (!string.IsNullOrWhiteSpace(hfidartigopai.Value) && Session["lstReferencias"] != null)
                {
                    using (ServicoArticulacao.InterfaceArticulacaoClient svcart = new ServicoArticulacao.InterfaceArticulacaoClient())
                    {
                        List<ServicoArticulacao.ReferenciaTO> listaReferencias = new List<ServicoArticulacao.ReferenciaTO>();
                        listaReferencias = ((List<ServicoArticulacao.ReferenciaTO>)Session["lstReferencias"]);

                        ServicoArticulacao.ComentarioTO comm = new ServicoArticulacao.ComentarioTO();
                        comm.Descricao = Argumentacao;
                        comm.IdArtigo = sugRetorno;
                        comm.IdPessoa = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa;
                        var retorno = svcart.Comentar(comm, listaReferencias.ToArray());

                        sucesso.Sucesso = retorno.Sucesso;

                        //foreach (var refer in listaReferencias)
                        //{
                        //    refer.IdComentario = argRetorno;
                        //    svcart.IncluirReferencia(refer);
                        //}
                    }
                }

                if (sucesso.Sucesso)
                {

                    hfidartigopai.Value = "";
                    ModalNovoSubstitutivo.Attributes.Remove("style");
                    ModalNovoSubstitutivo.Attributes.Add("style", "display:none");
                    //pnlAddSugestao.Visible = false;

                    //ListarSugestoes(int.Parse(hfIdArtigo.Value));
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelarCadastro_Click(object sender, EventArgs e)
        {
            //pnlAddSugestao.Visible = false;
            hfidartigopai.Value = "";
            ModalNovoSubstitutivo.Attributes.Remove("style");
            ModalNovoSubstitutivo.Attributes.Add("style", "display:none");
        }

        protected void grdReferencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            var linha = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName.ToString() == "Excluir")
            {
                var listaReferencias = ((List<ServicoArticulacao.ReferenciaTO>)Session["lstReferencias"]);
                listaReferencias.RemoveAt(linha);

                Session["lstReferencias"] = listaReferencias;
                grdReferencias.DataSource = listaReferencias;
                grdReferencias.DataBind();
                pnlAddSugestao.Visible = true;
            }
        }

    }
}