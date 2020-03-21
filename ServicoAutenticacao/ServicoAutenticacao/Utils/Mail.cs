using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using ServicoAutenticacao.TO;

namespace Utils.Mail
{
    public class Mail
    {
        public RetornoServicoTO EnviarEmail(string email, string body, TipoEmail tpEmail)
        {
            RetornoServicoTO retorno = new RetornoServicoTO();

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential =
                new NetworkCredential("vinicius200691@hotmail.com", "thisisthenewwar@666");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(email);

            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            smtpClient.EnableSsl = true;

            message.From = fromAddress;
            message.Subject = tpEmail.ToString();
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(email);

            try
            {
                dynamic mailreturn = null;
                smtpClient.SendCompleted += SmtpClient_SendCompleted;
                smtpClient.SendAsync(message, mailreturn);

                var j = mailreturn;

            }
            catch (Exception ex)
            {

                retorno.Mensagem = "erro ao enviar mensagem , tente novamente mais tarde";
                retorno.DescricaoFalha = ex.Message + " " + ex.GetBaseException().Message;
                retorno.Sucesso = true;
                return retorno;
            }

            retorno.Mensagem = "mensagem enviada";
            retorno.Sucesso = true;

            return retorno;
        }

        private void SmtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var u = e.Cancelled;
            var i = e.Error;
            var a = e.UserState;
        }

        public enum TipoEmail
        {
            RecuperacaoConta,
            RecuperacaoSenha,
            Confirmacao
        }


    }
}