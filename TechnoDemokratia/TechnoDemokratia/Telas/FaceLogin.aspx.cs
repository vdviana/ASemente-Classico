using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDemokratia.ServicoAutenticacao;

namespace TechnoDemokratia.Telas
{
    public partial class FaceLogin : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {



        }

        protected void btnLogarPessoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Pessoa"] == null)
                {

                    using (ServicoAutenticacao.InterfaceAutenticacaoClient scvauth = new ServicoAutenticacao.InterfaceAutenticacaoClient())
                    {
                        ServicoAutenticacao.PessoaTO pessoa = new PessoaTO();
                        pessoa = scvauth.AutenticarRedeSocial(txtID.Text.ToString(), "Facebook");
                        if (pessoa.Sucesso && pessoa.IdPessoa != 0)
                        {
                            pessoa.ImagemPerfil = txtImg.Text;
                            HttpContext.Current.Session["Pessoa"] = pessoa;
                            ClientScript.RegisterClientScriptBlock(GetType(), "", @"     
                        window.opener.document.location.reload();
                        window.close();", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "window.close();", true);
                        }
                    }
                }


                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "window.close();", true);
                }
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.close();", true);
            }








        }
    }
}