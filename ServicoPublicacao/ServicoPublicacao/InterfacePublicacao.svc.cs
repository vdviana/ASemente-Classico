using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServicoPublicacao.TO;

using DateEncription;
using System.IO;
using System.Web.Hosting;
using ServicoPublicacao.Utils;
using System.IO.Compression;

namespace ServicoPublicacao
{
    public class InterfacePublicacao : IInterfacePublicacao
    {
        public ListaPosicaoPoliticaTO ListarPosicoesPoliticas()
        {
            ListaPosicaoPoliticaTO lista = new ListaPosicaoPoliticaTO();

            try
            {
                List<PosicaoPolitica> list = new List<PosicaoPolitica>();
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    list = contexto.PosicaoPolitica.ToList();
                }
                if (list == null && list.Count <= 0 && list.FirstOrDefault().Id <= 0)
                {
                    lista.Sucesso = false;
                    lista.DescricaoFalha = "Não foi possivel listar as Pos. politicas.";
                    return lista;
                }
                foreach (PosicaoPolitica item in list)
                {
                    PosicaoPoliticaTO posicaoPoliticaTO = new PosicaoPoliticaTO();
                    posicaoPoliticaTO.PreencherPosicaoPoliticaTO(item);
                    lista.Lista.Add(posicaoPoliticaTO);
                }

                lista.Sucesso = true;
                return lista;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                lista.Sucesso = false;
                lista.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return lista;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                lista.Sucesso = false;
                lista.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar listar Pos. politicas,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return lista;
            }
        }

        public ListaTipoProjetoTO ListarTiposProjeto()
        {
            ListaTipoProjetoTO lista = new ListaTipoProjetoTO();

            try
            {
                List<TipoProjeto> list = new List<TipoProjeto>();
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    list = contexto.TipoProjeto.ToList();
                }
                if (list == null && list.Count <= 0 && list.FirstOrDefault().Id <= 0)
                {
                    lista.Sucesso = false;
                    lista.DescricaoFalha = "Não foi possivel listar os Tipos de Projeto.";
                    return lista;
                }
                foreach (TipoProjeto item in list)
                {
                    TipoProjetoTO TipoProjetoTO = new TipoProjetoTO();
                    TipoProjetoTO.PreencherTipoProjetoTO(item);
                    lista.Lista.Add(TipoProjetoTO);
                }

                lista.Sucesso = true;
                return lista;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                lista.Sucesso = false;
                lista.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return lista;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                lista.Sucesso = false;
                lista.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar listar os Tipos de Projeto,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return lista;
            }
        }

        public ListaProjetoTO PesquisarProjetos(ProjetoTO parametros, int indice = 0
            , int alcance = 0, DateTime? rangeInicialDataCadastro = null, DateTime? rangeFinalDataCadastro = null)
        {
            ListaProjetoTO listaprojetos = new ListaProjetoTO();
            try
            {
                indice = indice * alcance;
                List<ProjetoLei> listaprojeto = new List<ProjetoLei>();
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    bool b_descricao = string.IsNullOrWhiteSpace(parametros.Descricao);
                    bool b_tipo = string.IsNullOrWhiteSpace(parametros.TipoProjeto);
                    bool b_titulo = string.IsNullOrWhiteSpace(parametros.Titulo);
                    bool b_Situacao = string.IsNullOrWhiteSpace(parametros.Situacao);

                    listaprojeto = contexto.ProjetoLei.Where(
                        x => (x.AnoRegistro == parametros.AnoRegistro || (parametros.AnoRegistro == 0)) &&
                        (x.Descricao.Contains(parametros.Descricao) || b_descricao) &&
                        (x.IdPessoa == parametros.IdPessoa || parametros.IdPessoa == 0) &&
                        (x.NumeracaoRegistro == parametros.NumeracaoRegistro || parametros.NumeracaoRegistro == 0) &&
                        (x.AnoRegistro == parametros.AnoRegistro || parametros.AnoRegistro == 0) &&
                        (x.TipoProjeto == parametros.TipoProjeto || b_tipo) &&
                        (x.Titulo.Contains(parametros.Titulo) || b_titulo) &&
                        (x.Situacao == parametros.Situacao || b_Situacao) &&
                        ((x.DataCadastro >= rangeInicialDataCadastro && x.DataCadastro <= rangeFinalDataCadastro) || (!rangeInicialDataCadastro.HasValue || !rangeFinalDataCadastro.HasValue))
                        ).ToList();

                    listaprojeto = listaprojeto.OrderByDescending(x => x.DataCadastro).Skip(indice).Take(alcance).ToList();
                }
                if (listaprojeto == null && listaprojeto.Count <= 0 && listaprojeto.FirstOrDefault().IdProjeto <= 0)
                {
                    listaprojetos.Sucesso = false;
                    listaprojetos.DescricaoFalha = "Não foi possivel listar os projetos.";
                    return listaprojetos;
                }
                foreach (ProjetoLei projetoLei in listaprojeto)
                {
                    ProjetoTO projetoTO = new ProjetoTO();
                    projetoTO.PreencherProjetoTO(projetoLei);
                    listaprojetos.Lista.Add(projetoTO);
                }

                listaprojetos.Sucesso = true;
                return listaprojetos;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                listaprojetos.Sucesso = false;
                listaprojetos.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return listaprojetos;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                listaprojetos.Sucesso = false;
                listaprojetos.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar listar os projetos,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return listaprojetos;
            }
        }

        public RetornoServicoTO IncluirProjeto(ProjetoTO p_ProjetoTO)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    ProjetoLei projeto = new ProjetoLei();

                    p_ProjetoTO.DataCadastro = DateTime.Now;
                    projeto = p_ProjetoTO.PreencherProjetoEntity(projeto);

                    contexto.ProjetoLei.Add(projeto);
                    contexto.SaveChanges();

                }
                retorno.Mensagem = "Cadastrado";
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar incluir o Projeto,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public RetornoServicoTO AnexarArquivoProjeto(string TipoProjeto, int NumeroRegistro, int AnoRegistro)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            DetalhesProjetoTO projetoTO = new DetalhesProjetoTO();
            try
            {
                string caminhoProjeto = string.Empty;
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    ProjetoLei projeto = contexto.ProjetoLei.Where(x => x.TipoProjeto == TipoProjeto && x.NumeracaoRegistro == NumeroRegistro && x.AnoRegistro == AnoRegistro).FirstOrDefault();

                    if (projeto == null || projeto.IdProjeto < 0)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "Projeto não encontrado, acesse novamente o projeto criado e tente anexar o arquivo novamente";
                        return retorno;
                    }
                    caminhoProjeto = projeto.CaminhoArquivo;
                    projetoTO.PreencherProjetoTO(projeto);

                }
                ListaArtigoTO listaArtigoTO = new ListaArtigoTO();

                Arquivo arquivo = new Arquivo();

                projetoTO = arquivo.ObterDadosArquivoPDF(caminhoProjeto, projetoTO, out listaArtigoTO);

                if (projetoTO.Sucesso == false)
                {
                    retorno.Sucesso = projetoTO.Sucesso;
                    retorno.Mensagem = projetoTO.Mensagem;
                    retorno.DescricaoFalha = projetoTO.DescricaoFalha;
                    return retorno;
                }

                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {

                    ProjetoLei projeto = contexto.ProjetoLei.Where(x => x.TipoProjeto == TipoProjeto && x.NumeracaoRegistro == NumeroRegistro && x.AnoRegistro == AnoRegistro).FirstOrDefault();
                    projeto = projetoTO.PreencherProjetoEntity(projeto);

                    foreach (ArtigoTO r_artigo in listaArtigoTO.Lista)
                    {
                        Artigo artigo = new Artigo();
                        r_artigo.DataMovimento = DateTime.Now;
                        artigo = r_artigo.PreencherArtigoEntity(artigo);
                        contexto.Artigo.Add(artigo);
                    }
                    contexto.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.Mensagem = "Falha|" + projetoTO.IdProjeto.ToString();
                retorno.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                retorno.Sucesso = false;
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar anexar o arquivo e cadastrar as informações na base,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                retorno.Mensagem = "Falha|" + projetoTO.IdProjeto.ToString();
                return retorno;
            }
            retorno.Sucesso = true;
            retorno.Mensagem = "Artigos Cadastrados com Sucesso;";
            retorno.Mensagem = "Sucesso|" + projetoTO.IdProjeto.ToString();
            return retorno;
        }

        public ListaProjetoTO ListarProjetos(int indice = 0, int alcance = 0)
        {
            ListaProjetoTO listaprojetos = new ListaProjetoTO();
            try
            {
                indice = indice * alcance;
                List<ProjetoLei> listaprojeto = new List<ProjetoLei>();
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    listaprojeto = contexto.ProjetoLei.Where(x => x.Visibilidade == true).OrderByDescending(x => x.DataCadastro).Skip(indice).Take(alcance).ToList();
                }
                if (listaprojeto == null && listaprojeto.Count <= 0 && listaprojeto.FirstOrDefault().IdProjeto <= 0)
                {
                    listaprojetos.Sucesso = false;
                    listaprojetos.DescricaoFalha = "Não foi possivel listar os projetos.";
                    return listaprojetos;
                }
                foreach (ProjetoLei projetoLei in listaprojeto)
                {
                    ProjetoTO projetoTO = new ProjetoTO();
                    projetoTO.PreencherProjetoTO(projetoLei);
                    listaprojetos.Lista.Add(projetoTO);
                }

                listaprojetos.Sucesso = true;
                return listaprojetos;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                string rs = TratarExcecaoValidacaoEntidade(ex);
                DateTime dataErro = DateTime.Now;
                listaprojetos.Sucesso = false;
                listaprojetos.DescricaoFalha = string.Format("{0}, {1}", TratarMensagemErro(rs, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return listaprojetos;
            }
            catch (Exception ex)
            {
                DateTime dataErro = DateTime.Now;
                listaprojetos.Sucesso = false;
                listaprojetos.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar listar os projetos,{0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return listaprojetos;
            }
        }

        public DetalhesProjetoTO ConsultarProjeto(int p_idProjeto)
        {
            DetalhesProjetoTO retorno = new DetalhesProjetoTO();
            try
            {
                ProjetoLei projetoLei;
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    projetoLei = contexto.ProjetoLei.Where(x => x.IdProjeto == p_idProjeto).FirstOrDefault();
                }

                if (projetoLei == null && projetoLei.IdProjeto <= 0)
                {
                    retorno.Sucesso = false;
                    retorno.DescricaoFalha = "Erro ao obter Projeto.";
                    return retorno;
                }

                retorno.PreencherProjetoTO(projetoLei);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar obter o projeto {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public ListaArtigoTO ListarArtigosdoProjeto(int p_idProjeto, int indice, int alcance)
        {
            ListaArtigoTO retorno = new ListaArtigoTO();
            try
            {
                indice = indice * alcance;

                List<Artigo> ListaArtigos;
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    ListaArtigos = contexto.Artigo.Where(x => x.IdProjeto == p_idProjeto
                    && x.IsSugestao == false).OrderBy(x => x.IdArtigo).Skip(indice).Take(alcance).ToList();
                }

                if (ListaArtigos == null && ListaArtigos.Count <= 0 && ListaArtigos.FirstOrDefault().IdArtigo <= 0)
                {
                    retorno.Sucesso = false;
                    retorno.DescricaoFalha = "Erro ao obter Artigos.";
                    return retorno;
                }

                foreach (var artigo in ListaArtigos)
                {
                    ArtigoTO artigoTO = new ArtigoTO();
                    artigoTO.PreencherArtigoTO(artigo);
                    retorno.Lista.Add(artigoTO);

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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar obter os artigos {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO AlterarSituacaoProjeto(SituacaoProjeto p_SituacaoProjeto, int p_idProjeto)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    ProjetoLei projeto = contexto.ProjetoLei.Where(x => x.IdProjeto == p_idProjeto).FirstOrDefault();

                    if (projeto == null && projeto.IdProjeto <= 0)
                    {
                        retorno.DescricaoFalha = "ocorreu um erro ao obter e atualizar o projeto";
                        retorno.Sucesso = false;
                        return retorno;
                    }

                    projeto.Situacao = p_SituacaoProjeto.ToString();
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar obter os artigos {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public ListaArtigoTO ListarArtigosSubstitutivosDoArtigo(int p_IdArtigo, int indice = 0, int alcance = 0)
        {
            ListaArtigoTO retorno = new ListaArtigoTO();
            try
            {
                indice = indice * alcance;

                List<Artigo> ListaArtigos;
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    ListaArtigos = contexto.Artigo.Where(x => x.IdArtigoSubstitutivo == p_IdArtigo
                    && x.IsSugestao == true).OrderByDescending(x => x.DataMovimento).Skip(indice).Take(alcance).ToList();
                }

                if (ListaArtigos == null && ListaArtigos.Count <= 0 && ListaArtigos.FirstOrDefault().IdArtigo <= 0)
                {
                    retorno.Sucesso = false;
                    retorno.DescricaoFalha = "Erro ao obter Artigos.";
                    return retorno;
                }

                foreach (var artigo in ListaArtigos)
                {
                    ArtigoTO artigoTO = new ArtigoTO();
                    artigoTO.PreencherArtigoTO(artigo);
                    retorno.Lista.Add(artigoTO);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar obter os artigos {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }

        public RetornoServicoTO IncluirSugestaoArtigo(ArtigoTO p_artigoTO)
        {
            p_artigoTO.IsSugestao = true;
            RetornoServicoTO retorno = new RetornoServicoTO();
            Artigo artigo = new Artigo();
            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    artigo = p_artigoTO.PreencherArtigoEntity(artigo);
                    contexto.Artigo.Add(artigo);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar incluir uma sugestão {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO ConsultarHaSugestaoPessoa(int p_IdPessoa, int p_IdArtigo)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            Artigo artigo = new Artigo();
            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    artigo = contexto.Artigo.Where(x => x.IdPessoa == p_IdPessoa &&x.IdArtigoSubstitutivo==p_IdArtigo).FirstOrDefault();
                }

                if (artigo != null && artigo.IdArtigo > -0)
                {
                    retorno.Sucesso = true;
                    retorno.InfoRetorno = "S";
                }
                else
                {
                    retorno.InfoRetorno = "N";
                    retorno.Sucesso = false;
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar consultar se ha sugestão para pessoa e projeto: {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }
        }


        public RetornoServicoTO IncluirSugestaoArtigo(int p_IdartigoPai, string p_Descricao)
        {
            ArtigoTO artigoto = new ArtigoTO();
            int idartigoretorno = 0;
            RetornoServicoTO retorno = new RetornoServicoTO();

            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    Artigo artigo = contexto.Artigo.Where(x => x.IdArtigo == p_IdartigoPai).FirstOrDefault();
                    if (artigo == null)
                    {
                        retorno.Sucesso = false;
                        retorno.DescricaoFalha = "Não foi possivel encontrar o artigo pai";
                        return retorno;
                    }

                    artigoto.PreencherArtigoTO(artigo);
                    artigoto.IsSugestao = true;
                    artigoto.Descricao = p_Descricao;
                    artigoto.IdArtigoSubstitutivo = p_IdartigoPai;
                    artigoto.IdArtigo = 0;

                    artigo = new Artigo();
                    artigo = artigoto.PreencherArtigoEntity(artigo);

                    contexto.Artigo.Add(artigo);
                    contexto.SaveChanges();
                    idartigoretorno = artigo.IdArtigo;
                }
                retorno.Sucesso = true;
                retorno.InfoRetorno = idartigoretorno.ToString();
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar incluir uma sugestão {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }

        }

        public RetornoServicoTO DeletarArtigosObsoletos()
        {
            RetornoServicoTO retorno = new RetornoServicoTO();
            try
            {
                using (ConexaoDemoTecnokratia contexto = new ConexaoDemoTecnokratia())
                {
                    //mais de 3 meses sem movimento ou articulação
                    DateTime dataLimite = DateTime.Now.AddMonths(-3);
                    List<Artigo> listaartigos = contexto.Artigo.Where(x => x.DataMovimento <= dataLimite).ToList();

                    if (listaartigos != null && listaartigos.Count > 0 && listaartigos.First().IdArtigo > 0)
                    {
                        foreach (Artigo artigo in listaartigos)
                        {
                            contexto.Artigo.Remove(artigo);
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
                retorno.DescricaoFalha = string.Format("Ocorreu um problema não esperado ao tentar deletar um artigo {0}, {1}", TratarErro(ex, dataErro), "---" + dataErro.ToString("yyyy/MM/dd hh:mm:ss:ff"));
                return retorno;
            }



        }

        #region Privados

        public static byte[] Decompress(byte[] bSource)
        {
            // Fonte: http://stackoverflow.com/questions/6350776/help-with-programmatic-compression-decompression-to-memorystream-with-gzipstream
            using (var inStream = new MemoryStream(bSource))
            using (var gzip = new GZipStream(inStream, CompressionMode.Decompress))
            using (var outStream = new MemoryStream())
            {
                gzip.CopyTo(outStream);
                return outStream.ToArray();
            }
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
