using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using System.Net;
using System.IO;
using System.Web.Hosting;

namespace TechnoDemokratia.Telas
{
    public partial class NovoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Pessoa"] != null)
            {
                Response.Redirect("..\\Telas\\Main.aspx");
            }

        }

        protected void btnCadastrarPessoa_Click(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {
                CadastrarNovoUsuario();
            }

        }

        private bool ValidarCampos()
        {
            string MsgAlerta = "";
            bool valido = true;
            if (!string.IsNullOrWhiteSpace(hfSocialID.Value))
            {
                return true;
            }
            else
            {

                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    txtNome.Focus();
                    MsgAlerta += "\n digite seu nome";
                    valido = false;
                    //alert digite seu nome;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtNome.Focus();
                    MsgAlerta += "\n digite seu Email";
                    valido = false;
                    //alert digite seu email;
                }

                if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
                {
                    txtDataNascimento.Focus();
                    MsgAlerta += "\n digite data em que nasceu";
                    valido = false;
                    //alert digite data em que nasceu;
                }

                if (string.IsNullOrWhiteSpace(txtCPF.Text))
                {
                    txtCPF.Focus();
                    MsgAlerta += "\n digite seu CPF";
                    valido = false;
                    //alert digite seu nome;
                }

                if (string.IsNullOrWhiteSpace(txtRG.Text))
                {
                    txtRG.Focus();
                    MsgAlerta += "\n digite seu RG";
                    valido = false;
                }

                if (string.IsNullOrWhiteSpace(txtTelefone.Text))
                {
                    txtTelefone.Focus();
                    MsgAlerta += "\n digite seu Telefone";
                    valido = false;
                    //alert digite seu nome;
                }
                //string s_passcheck = passcheck.Text.Trim().ToLower();
                ////   s_passcheck = hfPassconfirm.Value;
                //if ((s_passcheck != "moderado") && (s_passcheck != "forte") && (s_passcheck != "muito forte"))
                //{
                //    txtSenha.Focus();
                //    MsgAlerta += "\n digite uma senha válida";
                //    valido = false;
                //}
                //string s_confirmcheck = passconfirm.Text.Trim().ToLower();
                //if (s_confirmcheck != "OK")
                //{
                //    txtSenha.Focus();
                //    MsgAlerta += "\n confirme a senha";
                //    valido = false;
                //}
            }
            return valido;

        }

        private void CadastrarNovoUsuario()
        {

            ServicoAutenticacao.PessoaTO pessoa = new ServicoAutenticacao.PessoaTO();
            string socialId = string.Empty;
            string socialNet = string.Empty;
            #region Verifica e/ou autentica em rede social
            if (!string.IsNullOrWhiteSpace(hfSocialID.Value))
            {

                socialId = hfSocialID.Value.Split('|')[0].ToString();
                socialNet = hfSocialID.Value.Split('|')[1].ToString();
                using (ServicoAutenticacao.InterfaceAutenticacaoClient svcauth = new ServicoAutenticacao.InterfaceAutenticacaoClient())
                {
                    pessoa = svcauth.AutenticarRedeSocial(socialId, socialNet);
                }

                if (pessoa.Sucesso)
                {
                    Session["off"] = null;
                    Session["Pessoa"] = pessoa;
                    Response.Redirect("..\\Telas\\Main.aspx");
                }


            }

            #endregion Verifica e/ou autentica em rede social


            #region Inclui na base



            pessoa = new ServicoAutenticacao.PessoaTO();
            pessoa.Nome = txtNome.Text.Trim();


            Int64 iCPF;
            if (!string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                iCPF = Int64.Parse(txtCPF.Text.Trim());
            }
            else
            {
                iCPF = 0;
            }


            pessoa.CPF = iCPF;
            pessoa.DataCadastro = DateTime.Now;
            pessoa.Email = txtEmail.Text.Trim();
            pessoa.Situacao = "INATIVO";
            pessoa.Senha = txtSenha.Text;
            //pessoa.RG= txtRG.Text.Trim();
            //pessoa.DataNascimento = txtDataNascimento.Text.TratarData();
            if (string.IsNullOrWhiteSpace(hfImgRedeSocial.Value))
            {
                if (imgInput.HasFile)
                {
                    pessoa.ImagemPerfil = SalvarImagem(txtNome.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
            }
            else { pessoa.ImagemPerfil = hfImgRedeSocial.Value; }

            #region Get Social Network Informed Data
            if (!string.IsNullOrWhiteSpace(socialNet))
            {
                switch (socialNet)
                {
                    case "LinkedIn":
                        pessoa.LinkedInID = socialId;
                        break;
                    case "Facebook":
                        pessoa.FacebookID = socialId;
                        break;
                    case "Google+":
                        pessoa.Google_ID = socialId;
                        break;
                    case "Instagram":
                        pessoa.InstagramID = socialId;
                        break;

                }

            }
            #endregion Get Social Network informed Data

            using (ServicoAutenticacao.InterfaceAutenticacaoClient svcauth = new ServicoAutenticacao.InterfaceAutenticacaoClient())
            {
                var retornocadastro = svcauth.IncluirNovoPessoa(pessoa);
                if (retornocadastro.Sucesso)
                {

                    string sBodyAlerta = "";
                    string sBodySenha = "";
                    string sBodyUsuario = "";

                    if (string.IsNullOrWhiteSpace(pessoa.Senha))
                    {
                        string senhaacesso = svcauth.CadastrarSenhaCriptografadaRedeSocial(socialId, socialNet, pessoa.DataCadastro).InfoRetorno;
                        sBodyAlerta = "Caso não seja autenticado automaticamente pela validação de Rede Social:";
                        sBodyUsuario = "Digite seu E-mail no campo de usuario";
                        sBodySenha = string.Format(" <br/> e sua senha de acesso é '{0}' (sem aspas) .", senhaacesso);
                    }
                    else
                    {
                        sBodyAlerta = "";
                        sBodyUsuario = "< br /> Digite seu CPF no campo de usuario. ";
                        sBodySenha = string.Format(" <br/> sua senha cadastrada é '{0}' (sem aspas) .", txtSenha.Text);
                    }

                    string cumprimento = "Caro(a) ";
                    if (ddlSexo.SelectedValue == "F")
                    {
                        cumprimento = "Cara ";
                    }
                    else if (ddlSexo.SelectedValue == "M")
                    {
                        cumprimento = "Caro ";
                    }

                    svcauth.EnviarEmailAutenticacao(ServicoAutenticacao.MailTipoEmail.Confirmacao,
                        txtEmail.Text.Trim(),
                        string.Format(@"{0}{1},
                        {2}
                        {3}
                        {4}
                        #LINK#
                        <br/> Meus Agradecimentos , e seja bem vindo ao {5}
                        <br/> Atenciosamente , Equipe {5}", cumprimento, pessoa.Nome, sBodyAlerta, sBodyUsuario, sBodySenha, ConfigurationManager.AppSettings["NomeProduto"].ToString())
                        , "http://" + ConfigurationManager.AppSettings["DominioProduto"].ToString() + "/Telas/Ativacao.aspx?" + retornocadastro.InfoRetorno);
                }
                ClientScript.RegisterStartupScript(GetType(), "ativação", "O código de acesso e instruções de primeiro acesso foram enviados para o seu email, Obrigado.", true);
                //enviar dialog : 
                Response.Redirect("..\\Telas\\Main.aspx");
            }


            #endregion Inclui na base
        }

        private string SalvarImagem(string p_novoNomeArquivoImagem)
        {
            try
            {
                string rootpath = ConfigurationManager.AppSettings["RootProfilePictures"].ToString();
                if (string.IsNullOrWhiteSpace(rootpath)) { rootpath = HostingEnvironment.ApplicationVirtualPath.ToString(); }
                string imgname = UploadFile("imgInput", rootpath + "ImagemPerfil\\", p_novoNomeArquivoImagem);
                string imgpath = rootpath + "ImagemPerfil/" + imgname;
                FtpUploadFile(rootpath + "ImagemPerfil/", imgname);
                return imgpath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void FtpUploadFile(string filePath, string fileName)
        {
            if (ConfigurationManager.AppSettings["FTPUpload"].ToString() == "S")
            {
                using (Util.FTPTool tool = new Util.FTPTool())
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

        private string UploadFile(string FileUploadControlID, string pathUpload, string profileID)
        {

            HttpPostedFile file = Page.Request.Files[FileUploadControlID];

            string returnfilename = string.Empty;


            string newfilename = profileID + Path.GetExtension(file.FileName);


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
            Response.Redirect("..\\Telas\\Main.aspx");
        }

        //protected void hfSocialID_ValueChanged(object sender, EventArgs e)
        //{
        //    btnCadastrarPessoa_Click(null, null);
        //}
    }
}