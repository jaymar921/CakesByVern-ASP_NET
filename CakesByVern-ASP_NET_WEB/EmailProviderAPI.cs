using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace CakesByVern_ASP_NET_WEB
{
    public class EmailProviderAPI
    {

        private readonly string mailapi_email;
        private readonly string mailapi_password;

        public EmailProviderAPI(IConfiguration configuration)
        {
            mailapi_email = configuration["EmailProvider:Email"] ?? "";
            mailapi_password = configuration["EmailProvider:Password"] ?? "";

            Console.WriteLine("EmailProviderAPI: " + mailapi_email + " " + mailapi_password);
        }

        public bool SendEmail(MailData mailData, IEnumerable<string> CC)
        {
            try
            {
                MailMessage mm = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                mm.From = new MailAddress(mailapi_email, "CakesByVern@no-reply", System.Text.Encoding.UTF8);
                mm.To.Add(new MailAddress(mailData.To));
                mm.Subject = mailData.Subject;
                mm.Body = mailData.GetHTMLMailBody();

                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                smtp.Host = "smtp.gmail.com";

                CC.ToList().ForEach(cc => mm.CC.Add(cc));

                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(mailapi_email, mailapi_password);
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class MailData
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public string GetHTMLMailBody()
        {
            return @$"
                <html>
                <body style='background-color: #c986dd;padding: 25px;'>
                    <div style='position: absolute;width: 250px;left: 50%;top: 50%;transform: translate(-50%,-50%);text-align: center;border: 1px solid white;background-color: white;padding: 20px;border-radius: 15px;'>
                        <div style='width: 100%;'>
                            <img style='width: auto;height: 96px; border-radius: 150px; border: 5px solid grey;' src='https://github.com/jaymar921/CakesByVern-ASP_NET/blob/master/CakesByVern-ASP_NET_WEB/wwwroot/fav.png?raw=true'>
                        </div>
                        <h3>{Subject}</h3>
                        <p style='position: relative;left: 50%; transform: translateX(-50%);white-space: pre-line; width: 80%; text-align: justify; padding: 20px; background-color: rgba(0,0,0,0.2); font-size: 13px; border-radius: 8px;'>{Body}</p>
                        <p style='font-size: 10px; font-weight: 800; color: gray'>CakesByVern - All rights are reserved</p>
                    </div>
                </body>
                </html>
            ";
        }
    }
}
