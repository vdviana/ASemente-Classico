using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ServicoPublicacao.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoPublicacao.Utils
{
    public class Arquivo
    {
        public DetalhesProjetoTO ObterDadosArquivoPDF(string NomecompletoArquivo, DetalhesProjetoTO p_projeto, out ListaArtigoTO p_listaArtigos)
        {

            p_listaArtigos = new ListaArtigoTO();

            PdfReader reader = new PdfReader(NomecompletoArquivo);
            //PdfReader reader = new PdfReader(@"C:\Users\Vinicius\Documents\Visual Studio 2015\Projects\demotechnokratia\ServicoPublicacao\ServicoPublicacao\Anexos\PL8672015.pdf");
            try { reader.Close(); }
            catch
            {
                p_projeto.Sucesso = false;
                p_projeto.DescricaoFalha = "erro ao tentar fechar o leitor de pdf";
                return p_projeto;
            }

            List<string> listastring = new List<string>();

            for (int i = 1; i < reader.NumberOfPages + 1; i++)
            {

                reader = new PdfReader(NomecompletoArquivo);
                string text = PdfTextExtractor.GetTextFromPage(reader, i);
                try { reader.Close(); }
                catch
                {
                    p_projeto.Sucesso = false;
                    p_projeto.DescricaoFalha = "erro ao tentar fechar o leitor de pdf";
                    return p_projeto;
                }
                listastring.Add(text);

            }

            listastring = listastring.ToList();
            string result = string.Join("", listastring);
            if (string.Empty.Equals(result))
            {
                p_projeto.DescricaoFalha = "Arquivo não legivel pelo servico";
                p_projeto.Sucesso = false;
                return p_projeto;
            }

            result = result.Replace("\n ", "\n").Replace(" \n", "\n").ToString();
            int cont = 1;
            while (cont < 100)
            {
                result = result.Replace(("\n\n" + cont.ToString() + "\n\n"), " ");
                result = result.Replace(("\n" + cont.ToString() + "\n"), " ");
                cont++;
            }

            while (result.IndexOf("\n") < 2)
            {
                result = result.Substring(1);
            }
            result = result.TrimStart('\n');

            p_projeto.Epigrafe = result.IndexOf("PROJETO DE") > -1 ? result.Substring(result.IndexOf("PROJETO DE"), result.IndexOf("\n\n") + 1) : string.Empty;
            if (p_projeto.Epigrafe.Length > 140) { p_projeto.Epigrafe = string.Empty; }
            result = result.Substring(p_projeto.Epigrafe.Length);


            while (result.IndexOf("\n") < 2)
            {
                result = result.Substring(1);
            }
            p_projeto.Ementa = result.Substring(0, result.IndexOf("\n\n") + 1);
            if (p_projeto.Ementa.Length > 250) { p_projeto.Ementa = string.Empty; }
            result = result.Substring(p_projeto.Ementa.Length);

            p_projeto.Preambulo = result.Substring(0, result.IndexOf("decreta:") + 8);
            if (p_projeto.Epigrafe.Length > 150) { p_projeto.Epigrafe = string.Empty; }
            result = result.Substring(p_projeto.Preambulo.Length);

            IDictionary<string, string> Mapacapitulos = new Dictionary<string, string>();

            IDictionary<string, string> MapaArtigos = new Dictionary<string, string>();

            IDictionary<int, int> MapaSumario = new Dictionary<int, int>();

            int contcapitulo = 0;
            int contartigo = 0;
            result = result.IndexOf("decreta:") > -1 ? result.Substring(result.IndexOf("decreta:")) : result;

            result = correcaoEspacamentoArtigo(result);

            #region if capitulo
            if (result.IndexOf("Capítulo") > -1)
            {

                while (result.IndexOf("Capítulo") > -1)
                {

                    contcapitulo++;

                    string capitulo = result.Substring(0, result.IndexOf("Art"));
                    Mapacapitulos.Add(contcapitulo.ToString(), capitulo);
                    result = result.Substring(capitulo.Length);

                    while (result.IndexOf("Art") > -1 && result.IndexOf("Art") < 8)
                    {
                        contartigo++;
                        int proximolimite = 0;

                        if (result.IndexOf("Capítulo") > -1 && result.IndexOf("Art. " + (contartigo + 1).ToString()) > -1)
                        {
                            proximolimite = (result.IndexOf("Art. " + (contartigo + 1).ToString()) < result.IndexOf("Capítulo"))
                                ? result.IndexOf("Art. " + (contartigo + 1).ToString())
                                : result.IndexOf("Capítulo");
                        }
                        else if (result.IndexOf("Capítulo") == -1 && result.IndexOf("Art. " + (contartigo + 1).ToString()) > -1)
                        {
                            proximolimite = result.IndexOf("Art. " + (contartigo + 1).ToString());
                        }
                        else
                        {
                            proximolimite = result.IndexOf("JUSTIFICA");
                        }
                        string s_contcapitulo = contcapitulo.ToString();
                        string artigo = result.Substring(0, proximolimite);
                        MapaArtigos.Add(contartigo.ToString(), artigo);
                        result = result.Substring(artigo.Length);
                        p_listaArtigos.Lista.Add(new ArtigoTO
                        {
                            Ordem = contartigo,
                            Descricao = artigo,
                            IdPessoa = p_projeto.IdPessoa,
                            IdProjeto = p_projeto.IdProjeto,
                            IsSugestao = false,
                            Capitulo = Mapacapitulos.Where(x => x.Key == s_contcapitulo).FirstOrDefault().Value
                        });

                    }
                }
            }
            #endregion if capitulo  
            #region if Artigo direto
            else if (result.IndexOf("Capítulo") == -1)
            {
                result = result.Substring(result.IndexOf("Art."));

                while (result.IndexOf("Art") > -1 && result.IndexOf("Art") < 8)
                {
                    contartigo++;
                    int proximolimite = 0;
                    if (result.IndexOf("Art. " + (contartigo + 1).ToString()) > -1)
                    {
                        proximolimite = result.IndexOf("Art. " + (contartigo + 1).ToString());
                    }
                    else
                    {
                        proximolimite = result.IndexOf("JUSTIFICA") > -1 ? result.IndexOf("JUSTIFICA") : result.LastIndexOf("Brasília");
                    }

                    string artigo = result.Substring(0, proximolimite);
                    MapaArtigos.Add(contartigo.ToString(), artigo);


                    result = result.Substring(artigo.Length);
                    contcapitulo = 0;
                    MapaSumario.Add(contartigo, contcapitulo);

                    string s_contcapitulo = contcapitulo.ToString();

                    p_listaArtigos.Lista.Add(new ArtigoTO
                    {
                        DataMovimento = DateTime.Now,
                        Ordem = contartigo,
                        Descricao = artigo,
                        IdPessoa = p_projeto.IdPessoa,
                        IdProjeto = p_projeto.IdProjeto,
                        IsSugestao = false,
                        Capitulo = Mapacapitulos.Where(x => x.Key == s_contcapitulo).FirstOrDefault().Value
                    });
                }
            }
            #endregion if Artigo direto
            if (result.IndexOf("JUSTIFICA") > -1)
            {
                p_projeto.Justificativa = result.Substring(0, result.IndexOf("\n\n") == -1 ? 0 : result.IndexOf("\n\n"));
                if (p_projeto.Justificativa.Length < 20)
                {
                    result = result.Substring(p_projeto.Justificativa.Length);
                    while (result.IndexOf("\n") < 2)
                    {
                        result = result.Substring(1);
                    }
                    result = p_projeto.Justificativa + "\n" + result;
                    p_projeto.Justificativa = result.Substring(0, result.IndexOf("\n\n") == -1 ? 0 : result.IndexOf("\n\n"));
                }
                else if (p_projeto.Justificativa.LastIndexOf("\n\n") > 30)
                {
                    p_projeto.Justificativa = p_projeto.Justificativa.Substring(0, p_projeto.Justificativa.LastIndexOf("\n\n"));
                }
                result = result.Substring(p_projeto.Justificativa.Length);

                if (result.IndexOf("JUSTIFICA") > -1)
                {
                    p_projeto.Justificativa = result.Substring(0, result.IndexOf("\n\n") == -1 ? 0 : result.IndexOf("\n\n"));
                }

            }

            p_projeto.Fecho = result;

            p_projeto.Sucesso = true;
            return p_projeto;
        }


        public string correcaoEspacamentoArtigo(string texto)
        {
            texto = texto.Replace("Art.  ", "Art.");
            texto = texto.Replace("Art. ", "Art.");
            texto = texto.Replace("Art. ", "Art.");

            texto = texto.Replace("Art.", "Art. ");
            return texto;
        }


    }
}