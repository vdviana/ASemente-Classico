//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicoVotacao
{
    using System;
    using System.Collections.Generic;
    
    public partial class Artigo
    {
        public int IdArtigo { get; set; }
        public int IdProjeto { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public bool IsSugestao { get; set; }
        public string Capitulo { get; set; }
        public Nullable<System.DateTime> DataMovimento { get; set; }
    }
}
