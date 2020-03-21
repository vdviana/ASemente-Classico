using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using DateEncription;
using ServicoVotacao.TO;
using System.ServiceModel;
using System.Text;

namespace ServicoVotacao.TO
{
    [DataContract]
    public class ContagemVotosTO : RetornoServicoTO
    {
        [DataMember]
        public int ContagemAFavor { get; set; }
        [DataMember]
        public int ContagemContra { get; set; }
        [DataMember]
        public int ContagemTotal { get; set; }
        [DataMember]
        public int IdProjeto { get; set; }
        [DataMember]
        public int idArtigo { get; set; }
        [DataMember]
        public int IdPessoa { get; set; }

        public void ObterVotosDoProjeto(int p_IdProjeto)
        {
            using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
            {
                this.IdProjeto = p_IdProjeto;

                List<int> listaIdArtigos = contexto.Artigo.Where(x => x.IdProjeto == p_IdProjeto).OrderBy(x => x.IdArtigo).Select(x => x.IdArtigo).ToList();
                List<Voto> listavotos = contexto.Voto.Where(x => listaIdArtigos.Contains(x.IdArtigo)).ToList();

                foreach (var voto in listavotos)
                {
                    if (voto.OpcaoVoto)
                    {
                        this.ContagemAFavor++;
                    }
                    else
                    {
                        this.ContagemContra++;
                    }
                }



            }

        }
        public void ObterVotosDoArtigo(int p_IdArtigo)
        {
            using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
            {
                this.idArtigo = p_IdArtigo;

                List<Voto> listavotos = contexto.Voto.Where(x => p_IdArtigo.Equals(x.IdArtigo)).ToList();

                foreach (var voto in listavotos)
                {
                    if (voto.OpcaoVoto)
                    {
                        this.ContagemAFavor++;
                    }
                    else
                    {
                        this.ContagemContra++;
                    }
                }



            }


        }
        public void ObterVotosDoAutor(int IdAutor)
        {
            using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
            {
                this.IdPessoa = IdAutor;
                List<int> listaIdProjetos = contexto.Projeto.Where(x => x.IdPessoa == IdAutor).OrderBy(x => x.IdProjeto).Select(x => x.IdProjeto).ToList();
                List<int> listaIdArtigos = contexto.Artigo.Where(x => listaIdProjetos.Contains(x.IdProjeto)).OrderBy(x => x.IdArtigo).Select(x => x.IdArtigo).ToList();
                List<Voto> listavotos = contexto.Voto.Where(x => listaIdArtigos.Contains(x.IdArtigo)).ToList();

                foreach (Voto voto in listavotos)
                {
                    if (voto.OpcaoVoto)
                    {
                        this.ContagemAFavor++;
                    }
                    else
                    {
                        this.ContagemContra++;
                    }
                }



            }




        }
    }

    [DataContract]
    public class ListaVotoTO : RetornoServicoTO
    {
        public ListaVotoTO()
        {
            Lista = new List<VotoTO>();
        }

        [DataMember]
        public List<VotoTO> Lista { get; set; }
    }

    [DataContract]
    public class VotoTO : RetornoServicoTO
    {
        [DataMember]
        public int IdVoto { get; set; }
        [DataMember]
        public int IdArtigo { get; set; }
        [DataMember]
        public int IdPessoa { get; set; }
        [DataMember]
        public bool OpcaoVoto { get; set; }

        public dynamic PreencherVotoEntity(dynamic votoEntity)
        {
            votoEntity.IdVoto = this.IdVoto;
            votoEntity.IdArtigo = this.IdArtigo;
            votoEntity.IdPessoa = this.IdPessoa;
            votoEntity.OpcaoVoto = this.OpcaoVoto;

            return votoEntity;
        }

        public void PreencherVotoTO(dynamic votoEntity)
        {
            this.IdVoto = votoEntity.IdVoto;
            this.IdArtigo = votoEntity.IdArtigo;
            this.IdPessoa = votoEntity.IdPessoa;
            this.OpcaoVoto = votoEntity.OpcaoVoto;
        }
    }
}