using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicoArticulacao.TO
{

    [DataContract]
    public class ListaReferenciaTO : RetornoServicoTO
    {

        public ListaReferenciaTO()
        {

            Lista = new List<ReferenciaTO>();
        }

        [DataMember]
        public List<ReferenciaTO> Lista { get; set; }

    }


    [DataContract]
    public class ReferenciaTO : RetornoServicoTO
    {

        [DataMember]
        public int IdReferencia { get; set; }

        [DataMember]
        public int IdComentario { get; set; }

        [DataMember]
        public String LinkReferencia { get; set; }


        public void preencherReferenciaTO(dynamic ReferenciaEntity)
        {
            this.IdReferencia = ReferenciaEntity.IdReferencia;
            this.IdComentario = ReferenciaEntity.IdComentario;
            this.LinkReferencia = ReferenciaEntity.LinkReferencia;
        }


        public dynamic PreencherReferenciaEntity(dynamic ReferenciaEntity)
        {
            ReferenciaEntity.IdReferencia = this.IdReferencia;
            ReferenciaEntity.IdComentario = this.IdComentario;
            ReferenciaEntity.LinkReferencia = this.LinkReferencia;

            return ReferenciaEntity;
        }

    }
}