using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPublicacao.TO
{
    [DataContract]
    public class ListaArtigoTO : RetornoServicoTO
    {
        public ListaArtigoTO()
        {
            Lista = new List<ArtigoTO>();
        }
        [DataMember]
        public List<ArtigoTO> Lista { get; set; }
    }

    [DataContract]
    public class ArtigoTO : RetornoServicoTO
    {
        [DataMember]
        public int IdArtigo { get; set; }
        [DataMember]
        public int IdProjeto { get; set; }
        [DataMember]
        public int IdPessoa { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public int Ordem { get; set; }
        [DataMember]
        public bool IsSugestao { get; set; }
        [DataMember]
        public string Capitulo { get; set; }
        [DataMember]
        public DateTime DataMovimento { get; set; }
        [DataMember]
        public int IdArtigoSubstitutivo { get; set; }

        public void PreencherArtigoTO(dynamic ArtigoEntity)
        {
            this.IdArtigo = ArtigoEntity.IdArtigo;
            this.IdProjeto = ArtigoEntity.IdProjeto;
            this.IdPessoa = ArtigoEntity.IdPessoa;
            this.Descricao = ArtigoEntity.Descricao;
            this.Ordem = ArtigoEntity.Ordem;
            this.IsSugestao = ArtigoEntity.IsSugestao;
            this.Capitulo = ArtigoEntity.Capitulo;
            this.DataMovimento = ArtigoEntity.DataMovimento;
            this.IdArtigoSubstitutivo = ArtigoEntity.IdArtigoSubstitutivo;//artigo pai
        }


        public dynamic PreencherArtigoEntity(dynamic ArtigoEntity)
        {
            ArtigoEntity.IdArtigo = this.IdArtigo;
            ArtigoEntity.IdProjeto = this.IdProjeto;
            ArtigoEntity.IdPessoa = this.IdPessoa;
            ArtigoEntity.Descricao = this.Descricao;
            ArtigoEntity.Ordem = this.Ordem;
            ArtigoEntity.IsSugestao = this.IsSugestao;
            ArtigoEntity.Capitulo = this.Capitulo;
            ArtigoEntity.DataMovimento = this.DataMovimento;
            ArtigoEntity.IdArtigoSubstitutivo = this.IdArtigoSubstitutivo;
            return ArtigoEntity;
        }
    }
}
