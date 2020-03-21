using DateEncription;
using ServicoVotacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoVotacao
{
    public class InterfaceVotacao : IInterfaceVotacao
    {
        public VotoTO ConsultarVoto(VotoTO p_VotoTO)
        {
            VotoTO retorno = new VotoTO();

            try
            {

                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    var voto = contexto.Voto.Where(x => x.IdArtigo == p_VotoTO.IdArtigo && x.IdPessoa == p_VotoTO.IdPessoa).FirstOrDefault();
                    if (voto == null || voto.IdVoto <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "erro ao obter voto";
                        return retorno;
                    }
                    retorno.PreencherVotoTO(voto);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar consultar o voto {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }


        public RetornoServicoTO EfetuarVotacao(VotoTO p_voto)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            Voto voto = new Voto();
            using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
            {
                voto = contexto.Voto.Where(x => x.IdPessoa == p_voto.IdPessoa && x.IdArtigo == p_voto.IdArtigo).FirstOrDefault();
            }
            if (voto != null && voto.IdVoto > 0)
            {
                VotoTO votoTO = new VotoTO();
                votoTO.PreencherVotoTO(voto);
                votoTO.OpcaoVoto = p_voto.OpcaoVoto;
                retorno = AtualizarVoto(votoTO);
            }
            else
            {
                retorno = IncluirVoto(p_voto);
            }

            return retorno;
        }

        public RetornoServicoTO IncluirVoto(VotoTO p_VotoTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Voto voto = new Voto();

                    voto = p_VotoTO.PreencherVotoEntity(voto);

                    contexto.Voto.Add(voto);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar incluir o voto {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO AtualizarVoto(VotoTO p_VotoTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {

                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    var voto = contexto.Voto.Where(x => x.IdVoto == p_VotoTO.IdVoto).FirstOrDefault();
                    if (voto == null || voto.IdVoto <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "erro ao obter voto";
                        return retorno;
                    }
                    voto.OpcaoVoto = p_VotoTO.OpcaoVoto;
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar atualizar o voto {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public ContagemVotosTO ContarVotosPorArtigo(int p_idArtigo)
        {
            ContagemVotosTO retorno = new ContagemVotosTO();
            try
            {
                retorno.ObterVotosDoArtigo(p_idArtigo);

                if (retorno.idArtigo <= 0)
                {
                    retorno.DescricaoFalha = "Ocorreu erro ao obter contagem de votos";
                    retorno.Sucesso = false;
                    return retorno;
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public ContagemVotosTO ContarVotosPorProjeto(int p_IdProjeto)
        {
            ContagemVotosTO retorno = new ContagemVotosTO();
            try
            {
                retorno.ObterVotosDoProjeto(p_IdProjeto);

                if (retorno.IdProjeto <= 0)
                {
                    retorno.DescricaoFalha = "Ocorreu erro ao obter contagem de votos";
                    retorno.Sucesso = false;
                    return retorno;
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public ContagemVotosTO ContarVotosPorAutor(int p_IdPessoa)
        {
            ContagemVotosTO retorno = new ContagemVotosTO();
            try
            {
                retorno.ObterVotosDoAutor(p_IdPessoa);

                if (retorno.idArtigo <= 0)
                {
                    retorno.DescricaoFalha = "Ocorreu erro ao obter contagem de votos";
                    retorno.Sucesso = false;
                    return retorno;
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public ContagemVotosTO ObterAprovacaoPopularAutorProjetos(int IdPessoa)
        {
            ContagemVotosTO retorno = new ContagemVotosTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    int contagemTotal = contexto.Pessoa.ToList().Count();
                    retorno.ObterVotosDoAutor(IdPessoa);
                    retorno.ContagemTotal = contagemTotal;
                    if (retorno.IdPessoa <= 0 || contagemTotal <= 0)
                    {
                        retorno.DescricaoFalha = "erro ao obter a contagem de aprovacao";
                        retorno.Sucesso = false;
                        return retorno;
                    }
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO AtualizarProjetoComAprovacao(int p_idprojeto)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                ContagemVotosTO contagem = ContarVotosPorProjeto(p_idprojeto);
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Projeto proj = contexto.Projeto.Where(x => x.IdProjeto == p_idprojeto).FirstOrDefault();

                    if (proj == null || proj.IdProjeto <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "ocorreu um erro ao obter o projeto para atualização";
                        return retorno;
                    }
                    int total = contagem.ContagemAFavor + contagem.ContagemContra;
                    decimal aprov = 0;
                    decimal reprov = 0;
                    if (contagem.ContagemAFavor > 0)
                    {
                        aprov = ((total / contagem.ContagemAFavor) - 1) * 100; ;
                    }
                    if (contagem.ContagemContra > 0)
                    {
                        reprov = ((total / contagem.ContagemContra) - 1) * 100;
                    }

                    proj.PorcentagemAprov = int.Parse(aprov.ToString());
                    proj.PorcentagemReprov = int.Parse(reprov.ToString());



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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        #region Privados

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
            string HexaCriptacao = "E33231EFDC3462B";
            NewDateKey cripto = new NewDateKey();
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
