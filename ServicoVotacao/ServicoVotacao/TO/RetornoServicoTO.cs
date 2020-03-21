using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServicoVotacao.TO
{
    [DataContract]
    public class RetornoServicoTO
    {
        [DataMember]
        public string Mensagem { get; set; }

        [DataMember]
        public string DescricaoFalha{get;set;}

        [DataMember]
        public bool Sucesso { get; set; }


    }
}