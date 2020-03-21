using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServicoAutenticacao.TO
{
    [DataContract]
    public class RetornoServicoTO
    {
        public RetornoServicoTO()
        {
            this.Sucesso = false;
        }

        [DataMember]
        public string Mensagem { get; set; }
        [DataMember]
        public bool Sucesso { get; set; }
        [DataMember]
        public string DescricaoFalha { get; set; }
        [DataMember]
        public string InfoRetorno { get; set; }
    }
}

