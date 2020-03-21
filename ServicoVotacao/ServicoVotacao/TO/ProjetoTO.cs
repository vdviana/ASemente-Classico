using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicoVotacao.TO
{

    [DataContract]
    public class ListaProjetoTO : RetornoServicoTO
    {
        public ListaProjetoTO()
        {
            Lista = new List<ProjetoTO>();
        }

        [DataMember]
        public List<ProjetoTO> Lista { get; set; }
    }

    [DataContract]
    public class ProjetoTO : RetornoServicoTO
    {

        [DataMember]
        public int IdProjeto { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public int IdPessoa { get; set; }
        [DataMember]
        public string Epigrafe { get; set; }
        [DataMember]
        public string Ementa { get; set; }
        [DataMember]
        public string Preambulo { get; set; }
        [DataMember]
        public string Fecho { get; set; }
        [DataMember]
        public string Justificativa { get; set; }
        [DataMember]
        public int PorcentagemAprov { get; set; }
        [DataMember]
        public int PorcentagemReprov { get; set; }
        [DataMember]
        public string Situacao { get; set; }
        [DataMember]
        public int IndiceAprov { get; set; }
        [DataMember]
        public string PosicaoPolitica { get; set; }
        [DataMember]
        public DateTime DataCadastro { get; set; }

        public void PreencherProjetoTO(dynamic p_ProjetoEntity)
        {
            this.IdProjeto = p_ProjetoEntity.IdProjeto;
            this.Descricao = p_ProjetoEntity.Descricao;
            this.IdPessoa = p_ProjetoEntity.IdPessoa;
            this.PorcentagemAprov = p_ProjetoEntity.PorcentagemAprov;
            this.PorcentagemReprov = p_ProjetoEntity.PorcentagemReprov;
            this.Situacao = p_ProjetoEntity.Situacao;
            this.IndiceAprov = p_ProjetoEntity.IndiceAprov;
            this.PosicaoPolitica = p_ProjetoEntity.PosicaoPolitica;
            this.Ementa = p_ProjetoEntity.Ementa;
            this.Fecho = p_ProjetoEntity.Fecho;
            this.Epigrafe = p_ProjetoEntity.Epigrafe;
            this.Justificativa = p_ProjetoEntity.Justificativa;
            this.Preambulo = p_ProjetoEntity.Preambulo;
            this.DataCadastro = p_ProjetoEntity.DataCadastro;
        }


        public dynamic PreencherProjetoEntity(dynamic p_ProjetoEntity)
        {
            p_ProjetoEntity.IdProjeto = this.IdProjeto;
            p_ProjetoEntity.Descricao = this.Descricao;
            p_ProjetoEntity.IdPessoa = this.IdPessoa;
            p_ProjetoEntity.PorcentagemAprov = this.PorcentagemAprov;
            p_ProjetoEntity.PorcentagemReprov = this.PorcentagemReprov;
            p_ProjetoEntity.Situacao = this.Situacao;
            p_ProjetoEntity.IndiceAprov = this.IndiceAprov;
            p_ProjetoEntity.PosicaoPolitica = this.PosicaoPolitica;
            p_ProjetoEntity.Ementa = this.Ementa;
            p_ProjetoEntity.Fecho = this.Fecho;
            p_ProjetoEntity.Epigrafe = this.Epigrafe;
            p_ProjetoEntity.Justificativa = this.Justificativa;
            p_ProjetoEntity.Preambulo = this.Preambulo;
            p_ProjetoEntity.DataCadastro = this.DataCadastro;

            return p_ProjetoEntity;
        }
    }
}
