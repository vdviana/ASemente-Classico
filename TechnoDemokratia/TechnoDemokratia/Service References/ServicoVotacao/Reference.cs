﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechnoDemokratia.ServicoVotacao {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RetornoServicoTO", Namespace="http://schemas.datacontract.org/2004/07/ServicoVotacao.TO")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TechnoDemokratia.ServicoVotacao.ContagemVotosTO))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TechnoDemokratia.ServicoVotacao.VotoTO))]
    public partial class RetornoServicoTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescricaoFalhaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MensagemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SucessoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescricaoFalha {
            get {
                return this.DescricaoFalhaField;
            }
            set {
                if ((object.ReferenceEquals(this.DescricaoFalhaField, value) != true)) {
                    this.DescricaoFalhaField = value;
                    this.RaisePropertyChanged("DescricaoFalha");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mensagem {
            get {
                return this.MensagemField;
            }
            set {
                if ((object.ReferenceEquals(this.MensagemField, value) != true)) {
                    this.MensagemField = value;
                    this.RaisePropertyChanged("Mensagem");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Sucesso {
            get {
                return this.SucessoField;
            }
            set {
                if ((this.SucessoField.Equals(value) != true)) {
                    this.SucessoField = value;
                    this.RaisePropertyChanged("Sucesso");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContagemVotosTO", Namespace="http://schemas.datacontract.org/2004/07/ServicoVotacao.TO")]
    [System.SerializableAttribute()]
    public partial class ContagemVotosTO : TechnoDemokratia.ServicoVotacao.RetornoServicoTO {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ContagemAFavorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ContagemContraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ContagemTotalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdPessoaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdProjetoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idArtigoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ContagemAFavor {
            get {
                return this.ContagemAFavorField;
            }
            set {
                if ((this.ContagemAFavorField.Equals(value) != true)) {
                    this.ContagemAFavorField = value;
                    this.RaisePropertyChanged("ContagemAFavor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ContagemContra {
            get {
                return this.ContagemContraField;
            }
            set {
                if ((this.ContagemContraField.Equals(value) != true)) {
                    this.ContagemContraField = value;
                    this.RaisePropertyChanged("ContagemContra");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ContagemTotal {
            get {
                return this.ContagemTotalField;
            }
            set {
                if ((this.ContagemTotalField.Equals(value) != true)) {
                    this.ContagemTotalField = value;
                    this.RaisePropertyChanged("ContagemTotal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdPessoa {
            get {
                return this.IdPessoaField;
            }
            set {
                if ((this.IdPessoaField.Equals(value) != true)) {
                    this.IdPessoaField = value;
                    this.RaisePropertyChanged("IdPessoa");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdProjeto {
            get {
                return this.IdProjetoField;
            }
            set {
                if ((this.IdProjetoField.Equals(value) != true)) {
                    this.IdProjetoField = value;
                    this.RaisePropertyChanged("IdProjeto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idArtigo {
            get {
                return this.idArtigoField;
            }
            set {
                if ((this.idArtigoField.Equals(value) != true)) {
                    this.idArtigoField = value;
                    this.RaisePropertyChanged("idArtigo");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VotoTO", Namespace="http://schemas.datacontract.org/2004/07/ServicoVotacao.TO")]
    [System.SerializableAttribute()]
    public partial class VotoTO : TechnoDemokratia.ServicoVotacao.RetornoServicoTO {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdArtigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdPessoaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdVotoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OpcaoVotoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdArtigo {
            get {
                return this.IdArtigoField;
            }
            set {
                if ((this.IdArtigoField.Equals(value) != true)) {
                    this.IdArtigoField = value;
                    this.RaisePropertyChanged("IdArtigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdPessoa {
            get {
                return this.IdPessoaField;
            }
            set {
                if ((this.IdPessoaField.Equals(value) != true)) {
                    this.IdPessoaField = value;
                    this.RaisePropertyChanged("IdPessoa");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdVoto {
            get {
                return this.IdVotoField;
            }
            set {
                if ((this.IdVotoField.Equals(value) != true)) {
                    this.IdVotoField = value;
                    this.RaisePropertyChanged("IdVoto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool OpcaoVoto {
            get {
                return this.OpcaoVotoField;
            }
            set {
                if ((this.OpcaoVotoField.Equals(value) != true)) {
                    this.OpcaoVotoField = value;
                    this.RaisePropertyChanged("OpcaoVoto");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicoVotacao.IInterfaceVotacao")]
    public interface IInterfaceVotacao {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ConsultarVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/ConsultarVotoResponse")]
        TechnoDemokratia.ServicoVotacao.VotoTO ConsultarVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ConsultarVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/ConsultarVotoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.VotoTO> ConsultarVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/EfetuarVotacao", ReplyAction="http://tempuri.org/IInterfaceVotacao/EfetuarVotacaoResponse")]
        TechnoDemokratia.ServicoVotacao.RetornoServicoTO EfetuarVotacao(TechnoDemokratia.ServicoVotacao.VotoTO p_voto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/EfetuarVotacao", ReplyAction="http://tempuri.org/IInterfaceVotacao/EfetuarVotacaoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> EfetuarVotacaoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_voto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/IncluirVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/IncluirVotoResponse")]
        TechnoDemokratia.ServicoVotacao.RetornoServicoTO IncluirVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/IncluirVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/IncluirVotoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> IncluirVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/AtualizarVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/AtualizarVotoResponse")]
        TechnoDemokratia.ServicoVotacao.RetornoServicoTO AtualizarVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/AtualizarVoto", ReplyAction="http://tempuri.org/IInterfaceVotacao/AtualizarVotoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> AtualizarVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorArtigo", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorArtigoResponse")]
        TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorArtigo(int p_idArtigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorArtigo", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorArtigoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorArtigoAsync(int p_idArtigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorProjeto", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorProjetoResponse")]
        TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorProjeto(int p_IdProjeto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorProjeto", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorProjetoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorProjetoAsync(int p_IdProjeto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorAutor", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorAutorResponse")]
        TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorAutor(int p_IdPessoa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ContarVotosPorAutor", ReplyAction="http://tempuri.org/IInterfaceVotacao/ContarVotosPorAutorResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorAutorAsync(int p_IdPessoa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ObterAprovacaoPopularAutorProjetos", ReplyAction="http://tempuri.org/IInterfaceVotacao/ObterAprovacaoPopularAutorProjetosResponse")]
        TechnoDemokratia.ServicoVotacao.ContagemVotosTO ObterAprovacaoPopularAutorProjetos(int IdPessoa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/ObterAprovacaoPopularAutorProjetos", ReplyAction="http://tempuri.org/IInterfaceVotacao/ObterAprovacaoPopularAutorProjetosResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ObterAprovacaoPopularAutorProjetosAsync(int IdPessoa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/AtualizarProjetoComAprovacao", ReplyAction="http://tempuri.org/IInterfaceVotacao/AtualizarProjetoComAprovacaoResponse")]
        TechnoDemokratia.ServicoVotacao.RetornoServicoTO AtualizarProjetoComAprovacao(int p_idprojeto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IInterfaceVotacao/AtualizarProjetoComAprovacao", ReplyAction="http://tempuri.org/IInterfaceVotacao/AtualizarProjetoComAprovacaoResponse")]
        System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> AtualizarProjetoComAprovacaoAsync(int p_idprojeto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IInterfaceVotacaoChannel : TechnoDemokratia.ServicoVotacao.IInterfaceVotacao, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InterfaceVotacaoClient : System.ServiceModel.ClientBase<TechnoDemokratia.ServicoVotacao.IInterfaceVotacao>, TechnoDemokratia.ServicoVotacao.IInterfaceVotacao {
        
        public InterfaceVotacaoClient() {
        }
        
        public InterfaceVotacaoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InterfaceVotacaoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InterfaceVotacaoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InterfaceVotacaoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TechnoDemokratia.ServicoVotacao.VotoTO ConsultarVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.ConsultarVoto(p_VotoTO);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.VotoTO> ConsultarVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.ConsultarVotoAsync(p_VotoTO);
        }
        
        public TechnoDemokratia.ServicoVotacao.RetornoServicoTO EfetuarVotacao(TechnoDemokratia.ServicoVotacao.VotoTO p_voto) {
            return base.Channel.EfetuarVotacao(p_voto);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> EfetuarVotacaoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_voto) {
            return base.Channel.EfetuarVotacaoAsync(p_voto);
        }
        
        public TechnoDemokratia.ServicoVotacao.RetornoServicoTO IncluirVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.IncluirVoto(p_VotoTO);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> IncluirVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.IncluirVotoAsync(p_VotoTO);
        }
        
        public TechnoDemokratia.ServicoVotacao.RetornoServicoTO AtualizarVoto(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.AtualizarVoto(p_VotoTO);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> AtualizarVotoAsync(TechnoDemokratia.ServicoVotacao.VotoTO p_VotoTO) {
            return base.Channel.AtualizarVotoAsync(p_VotoTO);
        }
        
        public TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorArtigo(int p_idArtigo) {
            return base.Channel.ContarVotosPorArtigo(p_idArtigo);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorArtigoAsync(int p_idArtigo) {
            return base.Channel.ContarVotosPorArtigoAsync(p_idArtigo);
        }
        
        public TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorProjeto(int p_IdProjeto) {
            return base.Channel.ContarVotosPorProjeto(p_IdProjeto);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorProjetoAsync(int p_IdProjeto) {
            return base.Channel.ContarVotosPorProjetoAsync(p_IdProjeto);
        }
        
        public TechnoDemokratia.ServicoVotacao.ContagemVotosTO ContarVotosPorAutor(int p_IdPessoa) {
            return base.Channel.ContarVotosPorAutor(p_IdPessoa);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ContarVotosPorAutorAsync(int p_IdPessoa) {
            return base.Channel.ContarVotosPorAutorAsync(p_IdPessoa);
        }
        
        public TechnoDemokratia.ServicoVotacao.ContagemVotosTO ObterAprovacaoPopularAutorProjetos(int IdPessoa) {
            return base.Channel.ObterAprovacaoPopularAutorProjetos(IdPessoa);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.ContagemVotosTO> ObterAprovacaoPopularAutorProjetosAsync(int IdPessoa) {
            return base.Channel.ObterAprovacaoPopularAutorProjetosAsync(IdPessoa);
        }
        
        public TechnoDemokratia.ServicoVotacao.RetornoServicoTO AtualizarProjetoComAprovacao(int p_idprojeto) {
            return base.Channel.AtualizarProjetoComAprovacao(p_idprojeto);
        }
        
        public System.Threading.Tasks.Task<TechnoDemokratia.ServicoVotacao.RetornoServicoTO> AtualizarProjetoComAprovacaoAsync(int p_idprojeto) {
            return base.Channel.AtualizarProjetoComAprovacaoAsync(p_idprojeto);
        }
    }
}
