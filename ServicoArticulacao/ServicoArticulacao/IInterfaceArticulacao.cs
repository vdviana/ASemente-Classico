using ServicoArticulacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoArticulacao
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInterfaceArticulacao" in both code and config file together.
    [ServiceContract]
    public interface IInterfaceArticulacao
    {
        [OperationContract]
        RetornoServicoTO Comentar(ComentarioTO p_Comentario, List<ReferenciaTO> p_referencias = null);

        [OperationContract]
        RetornoServicoTO EditarComentario(ComentarioTO p_comentarioTO);

        [OperationContract]
        RetornoServicoTO RemoverComentario(ComentarioTO p_comentarioTO);

        [OperationContract]
        RetornoServicoTO RemoverReferencia(ReferenciaTO p_ReferenciaTO);

        [OperationContract]
        RetornoServicoTO IncluirReferencia(ReferenciaTO p_ReferenciaTO);

        [OperationContract]
        ComentarioTO ConsultarComentario(int p_idArtigo);


        [OperationContract]
        ListaReferenciaTO ListarReferencias(int p_idComentario);

    }
}
