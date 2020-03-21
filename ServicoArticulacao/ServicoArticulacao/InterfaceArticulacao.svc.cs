using DateEncription;
using ServicoArticulacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoArticulacao
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "InterfaceArticulacao" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select InterfaceArticulacao.svc or InterfaceArticulacao.svc.cs at the Solution Explorer and start debugging.
    public class InterfaceArticulacao : IInterfaceArticulacao
    {

        public ComentarioTO ConsultarComentario(int p_idArtigo)
        {
            ComentarioTO retorno = new ComentarioTO();
            try
            {
                Comentario comm;
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    comm = contexto.Comentario.Where(x => x.IdArtigo == p_idArtigo).FirstOrDefault();

                    if (comm != null && comm.IdComentario > 0)
                    {
                        retorno.preencherComentarioTO(comm);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar consultar o comentario {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public ListaReferenciaTO ListarReferencias(int p_idComentario)
        {

            ListaReferenciaTO retorno = new ListaReferenciaTO();
            try
            {

                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    var referencias = contexto.Referencia.Where(x => x.IdComentario == p_idComentario).ToList();

                    if (referencias != null && referencias.Count > 0)
                    {

                        foreach (var reg in referencias)
                        {
                            ReferenciaTO refer = new ReferenciaTO();

                            refer.preencherReferenciaTO(reg);

                            retorno.Lista.Add(refer);
                        }
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar listar as referencias {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }


        public RetornoServicoTO Comentar(ComentarioTO p_Comentario, List<ReferenciaTO> p_referencias = null)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                Comentario comentario = new Comentario();
                p_Comentario.PreencherComentarioEntity(comentario);

                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    contexto.Comentario.Add(comentario);
                    contexto.SaveChanges();
                    int idComentario = comentario.IdComentario;
                    if (idComentario > 0 && p_referencias != null
                        && p_referencias.Count > 0
                        && !string.IsNullOrWhiteSpace(p_referencias.FirstOrDefault().LinkReferencia))
                    {
                        foreach (ReferenciaTO referencia in p_referencias)
                        {
                            Referencia refr = new Referencia();
                            referencia.IdComentario = idComentario;
                            refr = referencia.PreencherReferenciaEntity(refr);
                            contexto.Referencia.Add(refr);
                        }
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar cadastrar o comentario {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public RetornoServicoTO EditarComentario(ComentarioTO p_comentarioTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Comentario comentario = contexto.Comentario.Where(x => x.IdComentario == p_comentarioTO.IdComentario).FirstOrDefault();
                    if (comentario == null || comentario.IdComentario <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "erro ao obter comentario  para alteração";
                        return retorno;
                    }

                    comentario = p_comentarioTO.PreencherComentarioEntity(comentario);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar editar o comentario {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public RetornoServicoTO RemoverComentario(ComentarioTO p_comentarioTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Comentario comentario = contexto.Comentario.Where(x => x.IdComentario == p_comentarioTO.IdComentario).FirstOrDefault();
                    if (comentario == null || comentario.IdComentario <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "erro ao obter comentario para remoção";
                        return retorno;
                    }


                    contexto.Comentario.Remove(comentario);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar remover o comentario {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }


        }

        public RetornoServicoTO RemoverReferencia(ReferenciaTO p_ReferenciaTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Referencia referencia = contexto.Referencia.Where(x => x.IdReferencia == p_ReferenciaTO.IdReferencia).FirstOrDefault();
                    if (referencia == null || referencia.IdComentario <= 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "erro ao obter comentario para remoção";
                        return retorno;
                    }


                    contexto.Referencia.Remove(referencia);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar remover a referencia {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }


        }

        public RetornoServicoTO IncluirReferencia(ReferenciaTO p_ReferenciaTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTechnokratia contexto = new ConexaoDemoTechnokratia())
                {
                    Referencia referencia = new Referencia();

                    referencia = p_ReferenciaTO.PreencherReferenciaEntity(referencia);

                    contexto.Referencia.Add(referencia);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar remover a referencia {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
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
