using Kaotoby.DotNetEmailTemplatingDemo.Config;
using Kaotoby.DotNetEmailTemplatingDemo.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Kaotoby.DotNetEmailTemplatingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfig.load();
            
            // SM
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = GlobalConfig.SmtpHost;
            smtpClient.Port = GlobalConfig.SmtpPort;
            smtpClient.EnableSsl = GlobalConfig.SmtpUseSsl;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(GlobalConfig.SmtpUsername, GlobalConfig.SmtpPassword);

            Console.Write("Please enter email address for recipient: ");
            string recipientEmailAddress = Console.ReadLine();

            try
            {
                EmailRepository emailRepository = new EmailRepository(smtpClient, GlobalConfig.SmtpSenderEmailAddress);
                // Send password reset email
                emailRepository.SendPasswordResetEmail(recipientEmailAddress, new PasswordResetEmailModel()
                {
                    PasswordResetLink = "https://www.example.com/reset-password/sdfdlfkjdsfjdslfjodfjdf"
                });
                Console.WriteLine($"Test Email sent to {recipientEmailAddress}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
