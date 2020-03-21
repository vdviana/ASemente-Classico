using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            
        PdfReader reader = new PdfReader(@"");
            try { reader.Close(); }
            catch { }

            List<string> listastring = new List<string>();
            
                for (int i=1; i < reader.NumberOfPages+1; i++)
                {

                    reader = new PdfReader(@"");
                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
                    try { reader.Close(); }
                    catch { }
                    listastring.Add(text);
                
                }

            listastring = listastring.ToList();
            string result = string.Join("", listastring);
            if (string.Empty.Equals(result))
            {
                return;
            }
            
            result = result.Replace("\n ", "\n").Replace(" \n", "\n").ToString();
            int cont = 1;
            while (cont<100)
            {
                result = result.Replace(("\n\n"+ cont.ToString() + "\n\n"), " ");
                result = result.Replace(("\n" + cont.ToString() + "\n")," ");
                cont++;
            }
       
            while (result.IndexOf("\n") < 2)
            {
             result=   result.Substring(1);
            }
            result = result.TrimStart('\n');
         
            int ooooooo = result.IndexOf("PROJETO DE");

            string epigrafo = result.IndexOf("PROJETO DE") > -1 ? result.Substring(result.IndexOf("PROJETO DE") , result.IndexOf("\n\n") + 1) : string.Empty;
            if (epigrafo.Length > 140) { epigrafo = string.Empty; }
            result = result.Substring(epigrafo.Length);
            while (result.IndexOf("\n") < 2)
            {
                result = result.Substring(1);
            }
            string ementa = result.Substring(0, result.IndexOf("\n\n")+1);
            if (ementa.Length > 250) { ementa = string.Empty; }
            result = result.Substring(ementa.Length);

            string Preambulo = result.Substring(0, result.IndexOf("decreta:") + 8);
            if (epigrafo.Length > 150) { epigrafo = string.Empty; }
            result = result.Substring(Preambulo.Length);

            IDictionary<string, string> Mapacapitulos = new Dictionary<string, string>();

            IDictionary<string, string> MapaArtigos = new Dictionary<string, string>();

            IDictionary<int, int> MapaSumario = new Dictionary<int, int>();

            int contcapitulo = 0;
            int contartigo = 0;
            result = result.IndexOf("decreta:")>-1? result.Substring(result.IndexOf("decreta:")):result;

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

                        string artigo = result.Substring(0, proximolimite);
                        MapaArtigos.Add(contartigo.ToString(), artigo);
                        result = result.Substring(artigo.Length);

                        MapaSumario.Add(contartigo, contcapitulo);
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
                        proximolimite = result.IndexOf("JUSTIFICA")>-1? result.IndexOf("JUSTIFICA"): result.LastIndexOf("Brasília");
                    }

                    string artigo = result.Substring(0, proximolimite);
                    MapaArtigos.Add(contartigo.ToString(), artigo);
                    result = result.Substring(artigo.Length);
                    contcapitulo = 0;
                    MapaSumario.Add(contartigo, contcapitulo);
                }


            }
            #endregion if Artigo direto
            if (result.IndexOf("JUSTIFICA") > -1)
            {
                string justificativa = result.Substring(0, result.IndexOf("\n\n"));
                if (justificativa.Length < 20)
                {
                    result = result.Substring(justificativa.Length);
                    while (result.IndexOf("\n") < 2)
                    {
                        result = result.Substring(1);
                    }
                    result  = justificativa + "\n" + result;
                    justificativa= result.Substring(0, result.IndexOf("\n\n"));
                }
                else if (justificativa.LastIndexOf("\n\n") > 30)
                {
                    justificativa = justificativa.Substring(0, justificativa.LastIndexOf("\n\n"));
                }
                result = result.Substring(justificativa.Length);
            }

            string fecho = result;


            Console.ReadKey();

        }


    }
}
