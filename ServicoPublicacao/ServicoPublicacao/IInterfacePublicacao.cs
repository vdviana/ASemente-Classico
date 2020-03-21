using ServicoPublicacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoPublicacao
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInterfacePublicacao" in both code and config file together.
    [ServiceContract]
    public interface IInterfacePublicacao
    {
        [OperationContract]
        RetornoServicoTO ConsultarHaSugestaoPessoa(int p_IdPessoa, int p_IdArtigo);
        
        [OperationContract]
        ListaArtigoTO ListarArtigosSubstitutivosDoArtigo(int p_IdArtigo, int indice = 0, int alcance = 0);

        [OperationContract]
        ListaProjetoTO PesquisarProjetos(ProjetoTO parametros, int indice = 0, int alcance = 0, DateTime? rangeInicialDataCadastro = null, DateTime? rangeFinalDataCadastro = null);

        [OperationContract]
        RetornoServicoTO IncluirProjeto(ProjetoTO p_ProjetoTO);

        [OperationContract]
        RetornoServicoTO AnexarArquivoProjeto(string TipoProjeto, int NumeroRegistro, int AnoRegistro);

        [OperationContract]
        ListaProjetoTO ListarProjetos(int indice = 0, int alcance = 0);

        [OperationContract]
        DetalhesProjetoTO ConsultarProjeto(int p_idProjeto);

        [OperationContract]
        ListaTipoProjetoTO ListarTiposProjeto();


        [OperationContract]
        ListaArtigoTO ListarArtigosdoProjeto(int p_idProjeto, int indice, int alcance);

        [OperationContract]
        RetornoServicoTO AlterarSituacaoProjeto(SituacaoProjeto p_SituacaoProjeto, int p_idProjeto);

        //[OperationContract]
        //RetornoServicoTO IncluirSugestaoArtigo(ArtigoTO p_artigoTO);

        [OperationContract]
        RetornoServicoTO IncluirSugestaoArtigo(int p_IdartigoPai, string p_Descricao);

        [OperationContract]
        RetornoServicoTO DeletarArtigosObsoletos();

        [OperationContract]
        ListaPosicaoPoliticaTO ListarPosicoesPoliticas();

    }
}
