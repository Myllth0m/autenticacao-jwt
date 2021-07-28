using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace authenticationJWT.Services
{
    public static class EmailService
    {
        private const string emailDoRemetente = "miltondyama@outlook.com";
        private const string senha = "onedrive@131804";
        private const string nomeDoRemetente = "Milton D. Yama";
        private const string dominio = "smtp.office365.com";
        private const int porta = 587;
        private const bool ssl = true;

        public static async Task SendAsync(string mensagem)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(emailDoRemetente, nomeDoRemetente),
                Subject = $"Ocorreu um erro! Hr {DateTime.UtcNow}",
                Body = mensagem,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress("miltondyama@gmail.com"));

            using SmtpClient smtp = new SmtpClient(dominio, porta);
            smtp.Credentials = new NetworkCredential(emailDoRemetente, senha);
            smtp.EnableSsl = ssl;

            await smtp.SendMailAsync(mail);
        }
    }
}
