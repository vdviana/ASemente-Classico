using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServicoAutenticacao.TO;
using DateEncription;
using Utils.Mail;
using static Utils.Mail.Mail;

namespace ServicoAutenticacao
{
    public class InterfaceAutenticacao : IInterfaceAutenticacao
    {
        public RetornoServicoTO CadastrarSenhaCriptografadaRedeSocial(string IdRedeSocial, string RedeSocial, DateTime datacadastro)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                NewDateKey crypt = new NewDateKey();

                crypt.AttachData(IdRedeSocial.Substring(IdRedeSocial.Length - 8, 8), datacadastro, "DE2353C3472BEF1");

                string senha = crypt.Encrypt();

                crypt = null;

                Pessoa pessoa = null;
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    switch (RedeSocial)
                    {
                        case "LinkedIn":
                            pessoa = contexto.Pessoa.Where(x => x.LinkedInID == IdRedeSocial).FirstOrDefault();
                            break;
                        case "Facebook":
                            pessoa = contexto.Pessoa.Where(x => x.FacebookID == IdRedeSocial).FirstOrDefault();
                            break;
                        case "Google+":
                            pessoa = contexto.Pessoa.Where(x => x.Google_ID == IdRedeSocial).FirstOrDefault();
                            break;
                        case "Instagram":
                            pessoa = contexto.Pessoa.Where(x => x.InstagramID == IdRedeSocial).FirstOrDefault();
                            break;
                    }

                    if (pessoa != null && pessoa.IdPessoa > 0)
                    {
                        retorno.InfoRetorno = senha;
                        pessoa.Senha = CriptografarSenha(senha, datacadastro);
                        contexto.SaveChanges();
                    }
                }

                retorno.Sucesso = true;

                return retorno;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar cadastrar uma senha por cadastro em rede social,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public PessoaTO Autenticar(string Password, string userString = "")
        {
            PessoaTO retorno = new PessoaTO();
            try
            {
                List<ServicoAutenticacao.Pessoa> ListaPessoas = new List<ServicoAutenticacao.Pessoa>();
                using (ServicoAutenticacao.ConexaoDemoTechnokratia contexto = new ServicoAutenticacao.ConexaoDemoTechnokratia())
                {
                    Int64 i_userString;
                    if (!Int64.TryParse(userString, out i_userString))
                    {
                        i_userString = 0;
                    }
                    ListaPessoas = contexto.Pessoa.Where(x => x.CPF == i_userString && x.Situacao == "ATIVO").ToList();
                }
                if (ListaPessoas != null && ListaPessoas.Count > 0 && ListaPessoas.FirstOrDefault().IdPessoa > 0)
                {
                    foreach (var reg in ListaPessoas)
                    {
                        string u = CriptografarSenha(Password.Trim(), TratarDataCrypt(reg.DataCadastro));
                        if (u == reg.Senha)
                        {
                            retorno.PreencherPessoaTo(reg);
                            retorno.Sucesso = true;

                            return retorno;
                        }
                    }
                }
                else
                    retorno.Sucesso = false;
                retorno.Mensagem = "Login ou Senha invalidos , por favor tente novamente";
                return retorno;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar cadastrar uma Pessoa,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public PessoaTO AutenticarRedeSocial(string ID, string RedeSocial)
        {
            PessoaTO retorno = new PessoaTO();
            try
            {
                List<ServicoAutenticacao.Pessoa> ListaPessoas = new List<ServicoAutenticacao.Pessoa>();
                using (ServicoAutenticacao.ConexaoDemoTechnokratia contexto = new ServicoAutenticacao.ConexaoDemoTechnokratia())
                {
                    if (RedeSocial == "Facebook")
                    {
                        ListaPessoas = contexto.Pessoa.Where(x => ID == x.FacebookID).ToList();
                    }
                    else if (RedeSocial == "LinkedIn")
                    {
                        ListaPessoas = contexto.Pessoa.Where(x => ID == x.LinkedInID).ToList();
                    }
                    else if (RedeSocial == "Google+")
                    {
                        ListaPessoas = contexto.Pessoa.Where(x => ID == x.Google_ID).ToList();
                    }
                    else if (RedeSocial == "Twitter")
                    {
                        ListaPessoas = contexto.Pessoa.Where(x => ID == x.TwitterID).ToList();
                    }
                    else if (RedeSocial == "Instegram")
                    {
                        ListaPessoas = contexto.Pessoa.Where(x => ID == x.InstagramID).ToList();
                    }

                    ListaPessoas = ListaPessoas.Where(x => x.Situacao == "ATIVO").ToList();

                }
                if (ListaPessoas != null && ListaPessoas.Count > 0 && ListaPessoas.FirstOrDefault().IdPessoa > 0)
                {
                    foreach (var reg in ListaPessoas)
                    {
                        retorno.PreencherPessoaTo(reg);
                    }
                    retorno.Sucesso = true;

                    return retorno;
                }
                else
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = "Falha ao Autenticar";
                    return retorno;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar cadastrar uma Pessoa,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public RetornoServicoTO IncluirNovoPessoa(PessoaTO p_PessoaTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            string sAtivacao;
            try
            {
                if (string.IsNullOrWhiteSpace(GetIdRedeSocial(p_PessoaTO)))
                {
                    p_PessoaTO.Senha = CriptografarSenha(p_PessoaTO.Senha.Trim(), TratarDataCrypt(p_PessoaTO.DataCadastro));
                    p_PessoaTO.DataCadastro = TratarDataCrypt(p_PessoaTO.DataCadastro);
                    sAtivacao = CriptografarSenha(p_PessoaTO.Senha, p_PessoaTO.DataCadastro);
                    p_PessoaTO.IdAtivacao = CriptografarSenha(sAtivacao, TratarDataCrypt(p_PessoaTO.DataCadastro));
                }
                else
                {
                    p_PessoaTO.Senha = string.Empty;
                    sAtivacao = CriptografarSenha(GetIdRedeSocial(p_PessoaTO), p_PessoaTO.DataCadastro);
                    p_PessoaTO.IdAtivacao = CriptografarSenha(sAtivacao, TratarDataCrypt(p_PessoaTO.DataCadastro));
                }
                using (ServicoAutenticacao.ConexaoDemoTechnokratia contexto = new ServicoAutenticacao.ConexaoDemoTechnokratia())
                {
                    ServicoAutenticacao.Pessoa pessoaentity = new ServicoAutenticacao.Pessoa();
                    pessoaentity = p_PessoaTO.PreencherPessoaEntity(pessoaentity);
                    contexto.Pessoa.Add(pessoaentity);
                    contexto.SaveChanges();
                }
                retorno.InfoRetorno = string.Format("sfa={0}&dfa={1}", sAtivacao, p_PessoaTO.DataCadastro.ToString("yyyyMMddhhmmss"));
                retorno.Sucesso = true;
                return retorno;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar cadastrar uma Pessoa,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO AlterarSenha(PessoaTO p_pessoa, string senhaAntiga, string novaSenha)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    senhaAntiga = CriptografarSenha(senhaAntiga, p_pessoa.DataCadastro);
                    Pessoa pessoa = contexto.Pessoa.Where(x => x.IdPessoa == p_pessoa.IdPessoa && senhaAntiga == x.Senha).FirstOrDefault();

                    if (pessoa != null && pessoa.IdPessoa > 0)
                    {
                        pessoa.Senha = CriptografarSenha(novaSenha, pessoa.DataCadastro);
                        contexto.SaveChanges();
                    }
                    else
                    {
                        retorno.Sucesso = false;
                        retorno.Mensagem = "Senha incorreta";
                    }

                }
                return retorno;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar alterar senha,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO AlterarSituacaoPessoa(PessoaTO p_pessoa, String p_situacao)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {

                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {

                    Pessoa pessoa = contexto.Pessoa.Where(x => x.IdPessoa == p_pessoa.IdPessoa).FirstOrDefault();

                    pessoa.Situacao = p_situacao;

                    contexto.SaveChanges();
                }

                retorno.Sucesso = true;
                return retorno;

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar alterar status de uma Pessoa,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }



        }

        public bool AtivarUsuario(string codigoAtivacao, DateTime dataCadastro)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                string sAtivacao = CriptografarSenha(codigoAtivacao, dataCadastro);

                Pessoa pessoa;
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    pessoa = contexto.Pessoa.Where(x => x.Ativacao == sAtivacao).FirstOrDefault();

                    if (pessoa != null && pessoa.IdPessoa > 0)
                    {
                        pessoa.Situacao = "ATIVO";

                        contexto.SaveChanges();
                    }
                }

                retorno.Sucesso = true;
                return true;

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return false;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar alterar status de uma Pessoa,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return false;
            }





        }

        public RetornoServicoTO EnviarEmailAutenticacao(TipoEmail tpEmail, string Email, string Body = null, string linktramitacao = "")
        {
            string typesend = string.Empty;
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Pessoa pessoa = contexto.Pessoa.Where(x => x.Email == Email).FirstOrDefault();

                    if (pessoa != null && pessoa.IdPessoa > 0)
                    {
                        Mail mail = new Mail();

                        //TODO: criar codigo de acesso unico à tela de reset de senha e obtenção de conta
                        //TODO: criar a tela de acesso pela querystring para reset de senha e obtenção de conta. q redireciona para a tela de login

                        switch (tpEmail)
                        {
                            case TipoEmail.Confirmacao:
                                typesend = "confirmação de conta";
                                Body = Body.Replace("#LINK#", "<br/>segue o link para ativação de sua conta:</br> " + linktramitacao);
                                break;
                            case TipoEmail.RecuperacaoConta:
                                typesend = "recuperação de conta";
                                Body = Body.Replace("#LINK#", "<br/>segue o link para recuperação de conta:</br> " + linktramitacao);
                                break;
                            case TipoEmail.RecuperacaoSenha:
                                typesend = "recuperação de senha";
                                Body = Body.Replace("#LINK#", "<br/>segue o link para recuperação de senha:</br> " + linktramitacao);
                                break;
                            default: return new RetornoServicoTO { Sucesso = false, DescricaoFalha = "Tipo de email ñ especificado" };
                        }

                        mail.EnviarEmail(Email, Body, tpEmail);
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar enviar link de recuperação,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            retorno.Sucesso = true;
            retorno.Mensagem = "aguarde alguns minutos que enviaremos para você o link para " + typesend;
            return retorno;
        }

        public PessoaTO ConsultarPerfil(PessoaTO p_pessoa)
        {
            PessoaTO retorno = new PessoaTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {

                    var pessoa = contexto.Pessoa.Where(x => x.IdPessoa == p_pessoa.IdPessoa).FirstOrDefault();

                    retorno.PreencherPessoaTo(pessoa);

                    retorno.Sucesso = true;

                    return retorno;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar consultar o perfil spolicitado,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public ListaPessoasTO BuscarPessoa(string p_infoParcial, int p_index, int p_range)
        {
            ListaPessoasTO retorno = new ListaPessoasTO();
            try
            {
                List<Pessoa> pessoas = new List<Pessoa>();
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    int i_infoParcial;
                    int.TryParse(p_infoParcial, out i_infoParcial);

                    int i_skip = p_index * p_range;

                    pessoas = contexto.Pessoa.Where(x => x.Email.Contains(p_infoParcial)
                    || x.Nome.Contains(p_infoParcial) || i_infoParcial == x.CPF).OrderBy(x => x.IdPessoa).Skip(i_skip).Take(p_range).ToList();
                }
                if (pessoas.Count() > 0 && pessoas.FirstOrDefault().IdPessoa > 0)
                {
                    foreach (Pessoa reg in pessoas)
                    {
                        PessoaTO pessoaTO = new PessoaTO();
                        pessoaTO.PreencherPessoaTo(reg);
                        retorno.Lista.Add(pessoaTO);
                    }
                }
                retorno.Lista = retorno.Lista.OrderBy(x => x.IdPessoa).ToList();
                retorno.Sucesso = true;
                return retorno;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);

                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:fffffff tt"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar pesquisar perfis com este parametro,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:fffffff tt"));
                return retorno;
            }
        }

        #region Privados

        private DateTime TratarDataCrypt(DateTime data)
        {

            string stringdate = data.ToString("yyyyMMddhhmmss");

            int ano = int.Parse(stringdate.Substring(0, 4));
            int mes = int.Parse(stringdate.Substring(4, 2));
            int dia = int.Parse(stringdate.Substring(6, 2));
            int hora = int.Parse(stringdate.Substring(8, 2));
            int minuto = int.Parse(stringdate.Substring(10, 2));
            int segundo = int.Parse(stringdate.Substring(12, 2));

            return new DateTime(ano, mes, dia, hora, minuto, segundo);



        }

        private string GetIdRedeSocial(PessoaTO pessoa)
        {
            string retorno;
            if (!string.IsNullOrWhiteSpace(pessoa.Google_ID))
            {
                retorno = pessoa.Google_ID;
            }
            else if (!string.IsNullOrWhiteSpace(pessoa.FacebookID))
            {
                retorno = pessoa.FacebookID;
            }
            else if (!string.IsNullOrWhiteSpace(pessoa.TwitterID))
            {
                retorno = pessoa.TwitterID;
            }
            else if (!string.IsNullOrWhiteSpace(pessoa.InstagramID))
            {
                retorno = pessoa.InstagramID;
            }
            else if (!string.IsNullOrWhiteSpace(pessoa.LinkedInID))
            {
                retorno = pessoa.LinkedInID;
            }
            else
            {
                retorno = string.Empty;
            }

            return retorno;

        }

        private string TratarMensagemErro(string mensagemErro, DateTime DataErro)
        {

            string HexaCriptacao = "E33231EFDC3462B";
            NewDateKey cripto = new NewDateKey();

            cripto.AttachData(mensagemErro, DataErro, HexaCriptacao);
            mensagemErro = cripto.Encrypt();

            return mensagemErro;
        }

        private string TratarErro(Exception ex, DateTime DataErro)
        {

            string HexaCriptacao = "E33231EFDC3462B";
            NewDateKey cripto = new NewDateKey();

            cripto.AttachData(ex.Message, DataErro, HexaCriptacao);
            string mensagemErro = cripto.Encrypt();

            if (ex.GetBaseException().Message != null && ex.GetBaseException().Message != ex.Message)
            {
                cripto = new NewDateKey();
                cripto.AttachData(ex.GetBaseException().Message, DataErro, HexaCriptacao);
                mensagemErro = string.Format(mensagemErro + " {0}", cripto.Encrypt());
            }
            return mensagemErro;
        }

        public string descriptografarsenha(string criptado, DateTime datetimekey)
        {

            criptado = @"v^" +
    @"\\vpo§û(Ub= Wéù=äpá\'Sì.FL^îiWiù$ó\\( àUIQEs$dU Tîb\\\\ifë&^âbUeW Vg|_átoU9QE³Ei*sRUü" +
    @"áïét=ºpU}vr&eô?UVádeYIáZé§ ¨hK;b*p añû(Z^âoamgE_*p \'è1Hõ}]";
            string HexaCriptacao = "E33231EFDC3462B";
            NewDateKey cripto = new NewDateKey();
            datetimekey = new DateTime(2016, 06, 03, 08, 50, 25, 2730701);
            cripto.AttachData(criptado, datetimekey, HexaCriptacao);
            string dadodescriptografado = cripto.Decrypt();
            return dadodescriptografado;

        }

        private string CriptografarSenha(string senha, DateTime DataCadastro)
        {
            string HexaCriptacao = "E23531EFDC3472B";
            NewDateKey cripto = new NewDateKey();

            cripto.AttachData(senha, DataCadastro, HexaCriptacao);
            string SenhaCriptografada = cripto.Encrypt();
            return SenhaCriptografada;
        }

        private string TratarExcecaoValidacaoEntidade(System.Data.Entity.Validation.DbEntityValidationException ex)
        {
            string rs = "";
            foreach (var eve in ex.EntityValidationErrors)
            {
                rs = string.Format("Entidadedo tipo \"{0}\" no estado \"{1}\" contém os seguintes erros de validação", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                Console.WriteLine(rs);

                foreach (var ve in eve.ValidationErrors)
                {
                    rs += "<br />" + string.Format("- Propriedade: \"{0}\", Erro: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                }

            }

            return rs;


        }

        #endregion Privados

    }
}
