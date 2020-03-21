using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServicoArticulacao.TO
{
    [DataContract]
    public class RetornoServicoTO
    {
        public RetornoServicoTO()
        {
            this.Sucesso = false;
            this.Mensagem = string.Empty;
            this.DescricaoFalha = string.Empty;
        }

        [DataMember]
        public string Mensagem { get; set; }
        [DataMember]
        public bool Sucesso { get; set; }
        [DataMember]
        public string DescricaoFalha { get; set; }
    }
}

