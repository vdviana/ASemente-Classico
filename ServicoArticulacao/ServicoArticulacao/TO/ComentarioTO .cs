using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicoArticulacao.TO
{

    [DataContract]
    public class ListaComentarioTO : RetornoServicoTO
    {
        public ListaComentarioTO()
        {
            Lista = new List<ComentarioTO>();
        }

        [DataMember]
        public List<ComentarioTO> Lista { get; set; }
    }


    [DataContract]
    public class ComentarioTO : RetornoServicoTO
    {
        [DataMember]
        public int IdComentario { get; set; }

        [DataMember]
        public int IdArtigo { get; set; }

        [DataMember]
        public int IdPessoa { get; set; }

        [DataMember]
        public int IdComentarioPai { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public int Ordem { get; set; }

        public void preencherComentarioTO(dynamic ComentarioEntity)
        {
            this.IdComentario = ComentarioEntity.IdComentario;
            this.IdArtigo = ComentarioEntity.IdArtigo;
            this.IdPessoa = ComentarioEntity.IdPessoa;
            this.IdComentarioPai = ComentarioEntity.IdComentarioPai;
            this.Descricao = ComentarioEntity.Descricao;
            this.Ordem = ComentarioEntity.Ordem;
        }


        public dynamic PreencherComentarioEntity(dynamic ComentarioEntity)
        {
            ComentarioEntity.IdComentario = this.IdComentario;
            ComentarioEntity.IdArtigo = this.IdArtigo;
            ComentarioEntity.IdPessoa = this.IdPessoa;
            ComentarioEntity.IdComentarioPai = this.IdComentarioPai;
            ComentarioEntity.Descricao = this.Descricao;
            ComentarioEntity.Ordem = this.Ordem;

            return ComentarioEntity;
        }

    }
}