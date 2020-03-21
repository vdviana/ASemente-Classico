using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;



namespace ServicoPublicacao.TO
{
    [DataContract]
    public class ListaTipoProjetoTO : RetornoServicoTO
    {
        
        public ListaTipoProjetoTO()
        {
            Lista = new List<TipoProjetoTO>();
        }
        [DataMember]
        public List<TipoProjetoTO> Lista { get; set; }

    }
    
    [DataContract]
    public class TipoProjetoTO
    {
        [DataMember]
        public String Abreviacao { get; set; }
        [DataMember]
        public String Descricao { get; set; }


        public void PreencherTipoProjetoTO(dynamic TipoProjetoEntity)
        {

            this.Abreviacao = TipoProjetoEntity.Abreviacao;
            this.Descricao= TipoProjetoEntity.Descricao;



        }


    }
}
