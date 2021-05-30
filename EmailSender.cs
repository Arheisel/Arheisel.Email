using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Arheisel.Email
{
    public class EmailSender
    {
        private readonly MailMessage mail;
        private readonly SmtpClient smtp;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server">the url of the SMTP server</param>
        /// <param name="port">the port of the SMTP server</param>
        public EmailSender(string server, int port)
        {
            mail = new MailMessage();
            smtp = new SmtpClient(server, port);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server">the url of the SMTP server</param>
        public EmailSender(string server)
        {
            mail = new MailMessage();
            smtp = new SmtpClient(server);
        }

        /// <summary>
        /// Sets the From Address
        /// </summary>
        public string From
        {
            set
            {
                mail.From = new MailAddress(value);
            }
        }

        /// <summary>
        /// Adds a new recipient
        /// </summary>
        public string To
        {
            set
            {
                foreach (var address in value.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    mail.To.Add(address);
            }
        }


        /// <summary>
        /// Sets the Subject of the email
        /// </summary>
        public string Subject
        {
            set
            {
                mail.Subject = value;
            }
        }

        public bool IsHTML
        {
            get
            {
                return mail.IsBodyHtml;
            }
            set
            {
                mail.IsBodyHtml = value;
            }
        }


        /// <summary>
        /// Sets the body of the email
        /// </summary>
        public string Body
        {
            set
            {
                mail.Body = value;
            }
        }


        /// <summary>
        /// Sets the credentials for the SMTP Client
        /// </summary>
        public System.Net.NetworkCredential Credentials
        {
            set
            {
                smtp.Credentials = value;
            }
        }

        /// <summary>
        /// Specifies whether the SMTPClient should use SSL
        /// </summary>
        public bool EnableSSL
        {
            get
            {
                return smtp.EnableSsl;
            }
            set
            {
                smtp.EnableSsl = value;
            }
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        public void Send()
        {
            smtp.Send(mail);
        }
    }

    public static class EmailHTMLBody
    {
        /// <summary>
        /// Loads an HTML file from Disk and replaces the keywords marked as {{keyword}} with the corresponding
        /// value in the key-value pair
        /// </summary>
        /// <param name="args">a ("keyword", "replacement") Dictionary</param>
        /// <returns></returns>
        public static string Build(string file, Dictionary<string, string> args)
        {
            if (file.IndexOf("..\\") != -1 || file.IndexOf("../") != -1) return string.Empty; //Very poor attempt to prevent directory escalation
            /* BEWARE, HIGH SECURITY RISK:
             * NEVER FEED USER INPUT INTO PATH.COMBINE
             * As per Microsoft Docs: if an argument other than the first contains a rooted path, any previous path components are ignored, 
             * and the returned string begins with that rooted path component.
            */
            var body = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file));

            foreach (KeyValuePair<string, string> arg in args)
            {
                body = body.Replace("{{" + arg.Key + "}}", arg.Value);
            }

            return body;
        }

        public static string ImageToBase64(string path)
        {
            var result = string.Empty;
            if (!File.Exists(path)) throw new Exception("File doesn't exist");
            var ext = Path.GetExtension(path);
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    result = "data:image/jpeg;base64,";
                    break;
                case ".png":
                    result = "data:image/png;base64,";
                    break;
                case ".gif":
                    result = "data:image/gif;base64,";
                    break;
                default:
                    throw new Exception("Unsopported extension");
            }
            result += Convert.ToBase64String(File.ReadAllBytes(path));
            return result;
        }
    }
}
