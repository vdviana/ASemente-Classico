using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoPublicacao.TO
{


    public class ListaPosicaoPoliticaTO : RetornoServicoTO
    {

        public ListaPosicaoPoliticaTO()
        {
            Lista = new List<PosicaoPoliticaTO>();
        }

        public List<PosicaoPoliticaTO> Lista { get; set; }


    }

    public class PosicaoPoliticaTO
    {

        public int Id { get; set; }
        public string Descricao { get; set; }


        public void PreencherPosicaoPoliticaTO(dynamic PosicaoPoliticaEntity)
        {


            this.Id = PosicaoPoliticaEntity.Id;
            this.Descricao = PosicaoPoliticaEntity.Descricao;



        }

        public dynamic PreencherPosicaoPoliticaEntity(dynamic posicaoPoliticaEntity)
        {


            posicaoPoliticaEntity.Id = this.Id;
            posicaoPoliticaEntity.Descricao = this.Descricao;

            return posicaoPoliticaEntity;

        }
    }
}