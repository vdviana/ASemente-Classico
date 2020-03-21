using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoDemokratia.UserControls
{
    public partial class ModalContatar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Pessoa"] != null)
            {
                txtNome.Text = ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).Nome;
                txtEmail.Text= ((ServicoAutenticacao.PessoaTO)Session["Pessoa"]).Email;
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            ddlMotivoContato.SelectedIndex = 0;
         
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            
            
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
}