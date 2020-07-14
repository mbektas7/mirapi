using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Mirapi.Core.Domain;
using Mirapi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mirapi.Core.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;
        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Mail alanı zorunlu hocam orayı atlama bak.
        /// </summary>
        /// <param name="subject">mailin konusu. Buda baya zorunlu bunu da unutma.</param>
        /// <param name="message">Mailinizin bodysi (plaza ingilizcesi)</param>
        /// <param name="toEmail">Mail'in muhattabı olan ölümlünün mail adresi.</param>
        /// <returns></returns>
        public System.Threading.Tasks.Task Send(string subject, string message, string toEmail)
        {

            Execute(subject, message,toEmail).Wait();
            return System.Threading.Tasks.Task.FromResult(0);
        }

        /// <summary>
        /// Mail gönderme hizmetinin çalıştığı alandır. Normalde mail girilmemişse gdip configden alıyordu ancak maili zorunlu hale getirdik.
        /// </summary>
        /// <param name="subject">Mailin konusudur. Sistem tarafından değiştirilebilir halde bırakıldı</param>
        /// <param name="message">Mail içeriği dinamik olarak oluşturulabilir</param>
        /// <param name="toEmail">Mail'in muhattabı</param>
        /// <returns>NOTHING MUAHUAHAAA!</returns>
        public async System.Threading.Tasks.Task Execute( string subject, string message, string toEmail)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(_configuration["Mail:SecondaryPort"]);
                client.Host = _configuration["Mail:SecondayDomain"];
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_configuration["Mail:UsernameEmail"], _configuration["Mail:UsernamePassword"]);

                MailMessage mm = new MailMessage(_configuration["Mail:UsernameEmail"], toEmail, subject, message);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                await client.SendMailAsync(mm);
            }
            //ready to check-in
            catch (Exception e)
            {
                

            }
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}
