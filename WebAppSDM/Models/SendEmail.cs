using System.IO;
using System.Net.Mail;
using System.Net;

namespace WebAppSDM.Models
{
    public class SendEmail
    {
        public static void sendEmail(string to_email, string subjec, string text)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("automatic_ptkbi@outlook.com", "Jakarta2023");

                String from = "automatic_ptkbi@outlook.com";
                String to = to_email;
                String subject = subjec;
                String messageBody = text;
                MailMessage message = new MailMessage(from, to, subject, messageBody);
                message.IsBodyHtml = true;
                

                smtp.Send(message);
            }
            catch (Exception x)
            {

                throw;
            }

        }
    }
}
