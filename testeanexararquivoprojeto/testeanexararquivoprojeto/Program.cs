using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeanexararquivoprojeto
{
    class Program
    {
        static void Main(string[] args)
        {

            var fileStream = new FileStream(@"C:\Users\Vinicius\Documents\Visual Studio 2015\Projects\demotechnokratia\ServicoPublicacao\ServicoPublicacao\Anexos\PL8672015.pdf", FileMode.Open);


            var i = fileStream;

            dynamic u = "";
        
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = i.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                u = ms.ToArray();
            }

            using (ServiceReference1.InterfacePublicacaoClient svc = new ServiceReference1.InterfacePublicacaoClient())
            {

                u = Compress(u);

                var j = svc.AnexarArquivoProjeto(u, "PL8672015.pdf", 257, 4);


            }




    }

        public static byte[] Compress(byte[] data)
        {
            // Fonte: http://stackoverflow.com/a/271264/194717
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }


    }
}
