using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TechnoDemokratia.Util
{
    public class FTPTool : IDisposable
    {
        public String Usuario = ConfigurationManager.AppSettings["FTPUsername"].ToString();
        public String Senha = ConfigurationManager.AppSettings["FTPPassword"].ToString();
        public String ftpDirectoryUrl = ConfigurationManager.AppSettings["FtpDirectory"].ToString();
        public String localDirectoryUrl = ConfigurationManager.AppSettings["LocalDirectory"].ToString();

        public bool CopiarArquivo(string NomeArquivo)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(localDirectoryUrl + NomeArquivo);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(Usuario, Senha);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                Upload(ftpDirectoryUrl + NomeArquivo, ToByteArray(responseStream), Usuario, Senha);
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public bool Upload(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                System.IO.Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(Image, 0, Image.Length);

                clsStream.Close();
                clsStream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Usuario = null;
            Senha = null;
            ftpDirectoryUrl = null;
        }
    }
}