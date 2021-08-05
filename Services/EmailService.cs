using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace authenticationJWT.Services
{
    public static class EmailService
    {
        private const string emailDoRemetente = "exemple@outlook.com";
        private const string senha = "Senha@123";
        private const string nomeDoRemetente = "Myllth0m";
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

            mail.To.Add(new MailAddress("exemple@gmail.com"));

            using SmtpClient smtp = new SmtpClient(dominio, porta);
            smtp.Credentials = new NetworkCredential(emailDoRemetente, senha);
            smtp.EnableSsl = ssl;

            await smtp.SendMailAsync(mail);
        }
    }
}
