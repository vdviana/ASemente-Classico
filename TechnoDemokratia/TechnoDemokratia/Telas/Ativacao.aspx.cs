using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoDemokratia.Telas
{
    public partial class Ativacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String stringcrypt = Request.QueryString["sfa"].ToString();
                string stringdate = Request.QueryString["dfa"].ToString();

                int ano = int.Parse(stringdate.Substring(0, 4));
                int mes = int.Parse(stringdate.Substring(4, 2));
                int dia = int.Parse(stringdate.Substring(6, 2));
                int hora = int.Parse(stringdate.Substring(8, 2));
                int minuto = int.Parse(stringdate.Substring(10, 2));
                int segundo = int.Parse(stringdate.Substring(12, 2));


                DateTime datecrypt = new DateTime(ano, mes, dia, hora, minuto, segundo);

                bool ativar = false;
                using (ServicoAutenticacao.InterfaceAutenticacaoClient svcauth = new ServicoAutenticacao.InterfaceAutenticacaoClient())
                {
                    ativar = svcauth.AtivarUsuario(stringcrypt, datecrypt);
                }

                if (ativar)
                {
                    lblativacao.Text = "Ativação efetuada com sucesso! \n Favor Digitar as informações passadas por email para acessar o sistema.";
                   // Response.Redirect("Main.aspx");
                }

            }
            catch (Exception ex)
            {
                lblativacao.Text = "Ocorreu um erro ao tentar ativar a conta , favor contatar pelo numero (11)95977-8611";
            }
        }
    }
}