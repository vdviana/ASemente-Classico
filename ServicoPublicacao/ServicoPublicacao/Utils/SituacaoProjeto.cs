using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public enum SituacaoProjeto
{
    [EnumMember]
    CADASTRADO,
    [EnumMember]
    VALIDADO,
    [EnumMember]
    ATIVO,
    [EnumMember]
    INATIVO,
    [EnumMember]
    DOCUMENTADO,
    [EnumMember]
    REGISTRADO


}