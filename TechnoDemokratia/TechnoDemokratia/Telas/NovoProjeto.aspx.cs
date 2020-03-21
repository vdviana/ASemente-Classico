using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.Util;

namespace TechnoDemokratia
{
    public partial class NovoProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Pessoa"] == null)
            {
                Response.Redirect("..\\Telas\\Main.aspx");
            }

            if (ViewState["httppostedImg"] != null && !imgInput.HasFile)
            {
                Page.Items.Add("HttpPostedFile", (HttpPostedFile)ViewState["httppostedImg"]);
            }
            else if (ViewState["httppostedImg"] == null && imgInput.HasFile)
            {
                ViewState["httppostedImg"] = imgInput.PostedFile;
            }

            if (ViewState["httppostedProj"] != null && !fileInput.HasFile)
            {
                Page.Items.Add("HttpPostedFile", (HttpPostedFile)ViewState["httppostedProj"]);
            }
            else if (ViewState["httppostedProj"] == null && fileInput.HasFile)
            {
                ViewState["httppostedProj"] = fileInput.PostedFile;
            }

            if (!IsPostBack)
            {
                Carregarddls();
            }
            else
            {

            }
        }

        public void Carregarddls()
        {
            try
            {
                ServicoPublicacao.ListaPosicaoPoliticaTO lista = new ServicoPublicacao.ListaPosicaoPoliticaTO();
                ServicoPublicacao.ListaTipoProjetoTO listatp = new ServicoPublicacao.ListaTipoProjetoTO();
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    lista = svcpub.ListarPosicoesPoliticas();
                    if (lista.Sucesso)
                    {
                        lista.Lista.Add(new ServicoPublicacao.PosicaoPoliticaTO { Id = -1, Descricao = "Selecione posição política" });
                        lista.Lista.Add(new ServicoPublicacao.PosicaoPoliticaTO { Id = 0, Descricao = "Não sei" });
                        lista.Lista = lista.Lista.OrderBy(x => x.Id).ToList();
                        ddlPosPolitica.DataSource = lista.Lista;
                    }
                    listatp = svcpub.ListarTiposProjeto();
                    if (lista.Sucesso)
                    {
                        listatp.Lista.Add(new ServicoPublicacao.TipoProjetoTO { Abreviacao = string.Empty, Descricao = "Selecione tipo de projeto" });
                        listatp.Lista.Add(new ServicoPublicacao.TipoProjetoTO { Abreviacao = "--", Descricao = "Não sei" });
                        listatp.Lista = listatp.Lista.OrderBy(x => x.Abreviacao).ToList();
                        ddlTipoProjeto.DataSource = listatp.Lista;
                    }
                }
                ddlPosPolitica.DataValueField = "Id";
                ddlPosPolitica.DataTextField = "Descricao";
                ddlTipoProjeto.DataValueField = "Abreviacao";
                ddlTipoProjeto.DataTextField = "Descricao";
                ddlPosPolitica.DataBind();
                ddlTipoProjeto.DataBind();
            }
            catch
            {
                ddlTipoProjeto.Items.Add(new ListItem { Value = "", Text = "Falha ao carregar , reinicie a pagina" });
                ddlPosPolitica.Items.Add(new ListItem { Value = "", Text = "Falha ao carregar , reinicie a pagina" });
            }
        }

        private string SalvarImagem()
        {
            try
            {
                string imgname = UploadFile("imgInput", HostingEnvironment.ApplicationVirtualPath + "ImagemProjetos\\", ddlTipoProjeto.SelectedValue.ToString(), txtNumRegistro.Text, txtAnoRegistro.Text);
                string imgpath = HostingEnvironment.ApplicationVirtualPath + "ImagemProjetos/" + imgname;
                FtpUploadFile(HostingEnvironment.ApplicationVirtualPath + "ImagemProjetos/", imgname);
                return imgpath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string SalvarProjeto()
        {
            try
            {
                string filename = UploadFile("fileInput", HostingEnvironment.ApplicationVirtualPath + "Projetos\\", ddlTipoProjeto.SelectedValue.ToString(), txtNumRegistro.Text, txtAnoRegistro.Text);
                string filepath = HostingEnvironment.ApplicationPhysicalPath + "Projetos\\" + filename;
                FtpUploadFile(HostingEnvironment.ApplicationVirtualPath + "Projetos/", filename);
                return filepath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ValidarCampos()
        {

            if (ddlTipoProjeto.SelectedIndex == 0)
            {
                return "Selecione o tipo de projeto";
            }
            if (ddlPosPolitica.SelectedIndex == -1)
            {
                return "Selecione a posição política com qual se encaixa este projeto";
            }
            if (string.IsNullOrWhiteSpace(txtAddTitulo.Text))
            {
                return "Digite um Titulo para o Projeto";
            }
            if (string.IsNullOrWhiteSpace(txtAnoRegistro.Text))
            {
                return "Digite o ano de registro do Projeto";
            }
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                return "Descreva o Projeto";
            }
            if (string.IsNullOrWhiteSpace(txtNumRegistro.Text))
            {
                return "Digite o Numero do registro do Projeto";
            }
            if (!imgInput.HasFile)
            {
                return "Anexe uma Imagem para o projeto";
            }
            if (!fileInput.HasFile)
            {
                return "Anexe o documento do projeto em seu inteiro teor";
            }

            return string.Empty;
        }

        private ServicoPublicacao.RetornoServicoTO CadastrarDadosProjetoLei(string imgpath, string filepath)
        {
            ServicoPublicacao.RetornoServicoTO retorno = new ServicoPublicacao.RetornoServicoTO();
            try
            {
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    ServicoPublicacao.ProjetoTO projeto = new ServicoPublicacao.ProjetoTO();

                    projeto.CaminhoImagem = imgpath;
                    projeto.NumeracaoRegistro = int.Parse(txtNumRegistro.Text);
                    projeto.AnoRegistro = int.Parse(txtAnoRegistro.Text);
                    projeto.CaminhoArquivo = filepath;
                    projeto.DataCadastro = DateTime.Now;
                    projeto.Descricao = txtDescricao.Text;
                    projeto.IdPessoa = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).IdPessoa;
                    projeto.PosicaoPolitica = ddlPosPolitica.SelectedItem.Text;
                    projeto.Titulo = txtAddTitulo.Text;
                    projeto.Situacao = "ANALISE";
                    projeto.TipoProjeto = ddlTipoProjeto.SelectedValue;

                    retorno = svcpub.IncluirProjeto(projeto);
                    return retorno;
                }
            }
            catch
            {
                retorno.DescricaoFalha = "Falha inesperada ao cadastrar o projeto";
                retorno.Sucesso = false;
                return retorno;
            }
        }

        public ServicoPublicacao.RetornoServicoTO EfetuarCadastroDinamicoArtigos()
        {
            ServicoPublicacao.RetornoServicoTO retorno = new ServicoPublicacao.RetornoServicoTO();
            try
            {
                using (ServicoPublicacao.InterfacePublicacaoClient svcpub = new ServicoPublicacao.InterfacePublicacaoClient())
                {
                    retorno = svcpub.AnexarArquivoProjeto(ddlTipoProjeto.SelectedValue, int.Parse(txtNumRegistro.Text), int.Parse(txtAnoRegistro.Text));
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                retorno.DescricaoFalha = ex.Message + ex.GetBaseException().Message;
                retorno.Mensagem = "falha ao tentar cadastrar dinamicamente os artigos pelo documento, por favor cadastre manualmente";
                retorno.Sucesso = false;
                return retorno;
            }



        }

        protected void btnIncluirProjeto_Click(object sender, EventArgs e)
        {
            string validacao = ValidarCampos();
            if (!string.IsNullOrWhiteSpace(validacao))
            {
                //mensagem = validacao;
                //todo mensagem campos invalidos 
                //, favor preencher todos os campos corretamente.
                return;
            }
            validacao = null;
            string imgpath = SalvarImagem();
            if (string.IsNullOrWhiteSpace(imgpath))
            {
                //todo n foi possivel anexar a imagem do projeto,
                // favor reinicie a pagine e tente novamente.
                return;
            }
            string filepath = SalvarProjeto();
            if (string.IsNullOrWhiteSpace(filepath))
            {
                //todo n foi possivel anexar o projeto em seu inteiro teor,
                //favor reinicie a pagine e tente novamente.
                return;
            }
            ServicoPublicacao.RetornoServicoTO retorno = CadastrarDadosProjetoLei(imgpath, filepath);
            if (!retorno.Sucesso)
            {
                //mensagem = retorno.DescricaoFalha;
                //todo n foi possivel cadastrar este projeto,
                //favor reinicie a pagine e tente novamente.
                return;
            }
            retorno = EfetuarCadastroDinamicoArtigos();
            if (!retorno.Sucesso)
            {
                //mensagem = retorno.DescricaoFalha;
                //todo n foi possivel cadastrar os artigos dinamicamente deste projeto,
                //favor Cadastre manuelmente os artigos do projeto.
                Session["IdProjeto"] = retorno.Mensagem.Split('|')[1].ToString();
                Response.Redirect("~/CadastrarDadosProjeto.aspx");
            }
            Response.Redirect("~/MeusProjetos.aspx");
        }

        private void FtpUploadFile(string filePath, string fileName)
        {
            if (ConfigurationManager.AppSettings["FTPUpload"].ToString() == "S")
            {
                using (FTPTool tool = new FTPTool())
                {
                    bool success = tool.CopiarArquivo(filePath + fileName);
                    if (!success)
                    {
                        //TODO mostrar mensagem erro ao subir foto
                        return;
                    }
                }
            }
        }

        private string UploadFile(string FileUploadControlID, string pathUpload, string tipoProjeto, string numeroRegistro, string anoRegistro)
        {

            HttpPostedFile file = Page.Request.Files[FileUploadControlID];

            string returnfilename = string.Empty;


            string newfilename = tipoProjeto + numeroRegistro + "-" + anoRegistro + Path.GetExtension(file.FileName);


            if (file != null && file.ContentLength > 0)
            {

                file.SaveAs(Server.MapPath(Path.Combine(pathUpload, newfilename)));
                string fname = Path.GetFileName(Path.Combine(pathUpload, newfilename));
                returnfilename = fname;
            }
            return returnfilename;
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}