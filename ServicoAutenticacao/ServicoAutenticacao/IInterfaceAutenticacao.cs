using ServicoAutenticacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static Utils.Mail.Mail;

namespace ServicoAutenticacao
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInterfaceAutenticacao" in both code and config file together.
    [ServiceContract]
    public interface IInterfaceAutenticacao
    {


        [OperationContract]
        PessoaTO Autenticar(string Password, string userString = "");

        [OperationContract]
        PessoaTO AutenticarRedeSocial(string ID, string RedeSocial);

        [OperationContract]
        RetornoServicoTO CadastrarSenhaCriptografadaRedeSocial(string IdRedeSocial, string RedeSocial, DateTime datacadastro);

        [OperationContract]
        RetornoServicoTO IncluirNovoPessoa(PessoaTO PessoaTO);

        [OperationContract]
        RetornoServicoTO EnviarEmailAutenticacao(TipoEmail tpEmail, string Email, string Body = null, string linktramitacao = "");

        [OperationContract]
        RetornoServicoTO AlterarSenha(PessoaTO p_pessoa, string senhaAntiga, string novaSenha);

        [OperationContract]
        RetornoServicoTO AlterarSituacaoPessoa(PessoaTO p_pessoa, String p_situacao);

        [OperationContract]
        PessoaTO ConsultarPerfil(PessoaTO p_pessoa);

        [OperationContract]
        ListaPessoasTO BuscarPessoa(string p_infoParcial, int p_index, int p_range);

        [OperationContract]
        bool AtivarUsuario(string codigoAtivacao, DateTime dataCadastro);

        //[OperationContract]
        string descriptografarsenha(string criptado, DateTime datetimekey);
    }
}
