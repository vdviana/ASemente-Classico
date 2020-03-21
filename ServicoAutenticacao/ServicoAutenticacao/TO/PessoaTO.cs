using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServicoAutenticacao.TO
{
    [DataContract]
    public class ListaPessoasTO : RetornoServicoTO
    {
        public ListaPessoasTO()
        {
            Lista = new List<PessoaTO>();
        }

        [DataMember]
        public List<PessoaTO> Lista { get; set; }
    }
    [DataContract]
    public class PessoaTO : RetornoServicoTO
    {
        public PessoaTO()
        {
            Situacao = "VIVO";
            ImagemPerfil = "";
        }

        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public int IdPessoa { get; set; }
        [DataMember]
        public string Senha { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime DataCadastro { get; set; }
        [DataMember]
        public Int64 CPF { get; set; }
        [DataMember]
        public string Situacao { get; set; }
        [DataMember]
        public string ImagemPerfil { get; set; }
        [DataMember]
        public string FacebookID { get; set; }
        [DataMember]
        public string InstagramID { get; set; }
        [DataMember]
        public string TwitterID { get; set; }
        [DataMember]
        public string Google_ID { get; set; }
        [DataMember]
        public string LinkedInID { get; set; }
        [DataMember]
        public string IdAtivacao { get; set; }

        public void PreencherPessoaTo(dynamic PessoaEntity)
        {
            this.Nome = PessoaEntity.Nome;
            this.IdPessoa = PessoaEntity.IdPessoa;
            this.Email = PessoaEntity.Email;
            this.Senha = PessoaEntity.Senha;
            this.DataCadastro = PessoaEntity.DataCadastro;
            this.CPF = PessoaEntity.CPF;
            this.Situacao = PessoaEntity.Situacao;
            this.ImagemPerfil = PessoaEntity.ImagemPerfil;
            this.InstagramID = PessoaEntity.InstagramID;
            this.TwitterID = PessoaEntity.TwitterID;
            this.Google_ID = PessoaEntity.Google_ID;
            this.LinkedInID = PessoaEntity.LinkedInID;
            this.FacebookID = PessoaEntity.FacebookID;
            this.IdAtivacao = PessoaEntity.Ativacao;
        }

        public dynamic PreencherPessoaEntity(dynamic PessoaEntity)
        {
            PessoaEntity.Nome = this.Nome;
            PessoaEntity.IdPessoa = this.IdPessoa;
            PessoaEntity.Email = this.Email;
            PessoaEntity.Senha = this.Senha;
            PessoaEntity.DataCadastro = this.DataCadastro;
            PessoaEntity.CPF = this.CPF;
            PessoaEntity.Situacao = this.Situacao;
            PessoaEntity.ImagemPerfil = this.ImagemPerfil;
            PessoaEntity.InstagramID = this.InstagramID;
            PessoaEntity.TwitterID = this.TwitterID;
            PessoaEntity.Google_ID = this.Google_ID;
            PessoaEntity.LinkedInID = this.LinkedInID;
            PessoaEntity.FacebookID = this.FacebookID;
            PessoaEntity.Ativacao = this.IdAtivacao;

            return PessoaEntity;
        }

    }

}