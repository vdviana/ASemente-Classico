using ServicoVotacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoVotacao
{

    [ServiceContract]
    public interface IInterfaceVotacao
    {
        [OperationContract]
        VotoTO ConsultarVoto(VotoTO p_VotoTO);
        [OperationContract]
        RetornoServicoTO EfetuarVotacao(VotoTO p_voto);
        [OperationContract]
        RetornoServicoTO IncluirVoto(VotoTO p_VotoTO);
        [OperationContract]
        RetornoServicoTO AtualizarVoto(VotoTO p_VotoTO);
        [OperationContract]
        ContagemVotosTO ContarVotosPorArtigo(int p_idArtigo);
        [OperationContract]
        ContagemVotosTO ContarVotosPorProjeto(int p_IdProjeto);
        [OperationContract]
        ContagemVotosTO ContarVotosPorAutor(int p_IdPessoa);
        [OperationContract]
        ContagemVotosTO ObterAprovacaoPopularAutorProjetos(int IdPessoa);
        [OperationContract]
        RetornoServicoTO AtualizarProjetoComAprovacao(int p_idprojeto);
    }
}