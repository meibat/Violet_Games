using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace VioletGames.Util.SendEmail
{
    public interface IEmail
    {
        bool Send(string email, string content, string message);
    }

    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Send(string email, string content, string message)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string name = _configuration.GetValue<string>("SMTP:Nome");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string passwd = _configuration.GetValue<string>("SMTP:Senha");
                int port = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage() 
                { 
                    From = new MailAddress(username, name)
                };

                //Montagem do email
                mail.To.Add(email); //Para quem?
                mail.Subject = content; //Assunto
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //Envio
                using(SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(username, passwd);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
            }
            catch (System.Exception e)
            {
                //gravar log de erro

                return false;
            }
        }
    }
}
