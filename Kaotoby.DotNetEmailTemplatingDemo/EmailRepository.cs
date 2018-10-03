using Kaotoby.DotNetEmailTemplatingDemo.Models.Emails;
using PreMailer.Net;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Kaotoby.DotNetEmailTemplatingDemo.Config;
using System.Security;
using RazorEngine.Configuration;

namespace Kaotoby.DotNetEmailTemplatingDemo
{
    /// <summary>
    /// 寄送 Email 相關之商業邏輯
    /// </summary>
    public class EmailRepository
    {
        static EmailRepository()
        {
            #region Register Razor templates

            // Note: Use HostingEnvironment.MapPath(path) on ASP.Net project
            string emailViewDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\Emails\");

            // Loads files in-memory and disable razor engine waring on default AppDomain
            // See https://github.com/Antaris/RazorEngine/issues/244
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            templateConfig.DisableTempFileLocking = true;
            templateConfig.CachingProvider = new DefaultCachingProvider(t => { });
            Engine.Razor = RazorEngineService.Create(templateConfig);

            // Load .cshtml files
            foreach (string cshtmlFilePath in Directory.GetFiles(emailViewDir, "*.cshtml"))
            {
                string key = Path.GetFileNameWithoutExtension(cshtmlFilePath);
                Engine.Razor.AddTemplate(key, File.ReadAllText(cshtmlFilePath));
            }

            #endregion
        }

        /// <summary>
        /// 初始化 <see cref="EmailRepository"/>
        /// </summary>
        /// <param name="smtpClient">已經設定好的 <see cref="SmtpClient"/></param>
        /// <param name="senderEmailAddress">寄件人 Email</param>
        public EmailRepository(SmtpClient smtpClient, string senderEmailAddress)
        {
            this.SenderEmailAddress = new MailAddress(senderEmailAddress, GlobalConfig.WebsiteName);
            this.SmtpClient = smtpClient;
        }

        /// <summary>
        /// 產生 Email 的 Html 內容
        /// </summary>
        /// <param name="viewName">View 名稱</param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerateMailBody(string viewName, object model)
        {
            // Note: Use HostingEnvironment.MapPath(path) on ASP.Net project
            string emailViewDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Views\Emails\");

            // Prepare Viewbag
            dynamic viewBag = new DynamicViewBag();
            viewBag.WebsiteUrl = GlobalConfig.WebsiteUrl;
            viewBag.WebsiteName = GlobalConfig.WebsiteName;

            // Generate HTML
            string html = Engine.Razor.RunCompile(viewName, null, model, (DynamicViewBag)viewBag);

            // Inline CSS
            InlineResult result = PreMailer.Net.PreMailer.MoveCssInline(new Uri(emailViewDir), html);
            result.Warnings.ForEach(w => System.Diagnostics.Debug.WriteLine(w));

            return result.Html;
        }

        /// <summary>
        /// 寄送密碼重設信
        /// </summary>
        /// <param name="recipientEmailAddress">收件人 Email</param>
        /// <param name="passwordResetEmailModel">密碼重設信的資料</param>
        public void SendPasswordResetEmail(string recipientEmailAddress, PasswordResetEmailModel passwordResetEmailModel)
        {
            MailMessage mail = new MailMessage(this.SenderEmailAddress, new MailAddress(recipientEmailAddress));
            mail.Subject = $"重設您在 {GlobalConfig.WebsiteName} 的密碼";
            mail.Body = this.GenerateMailBody("PasswordReset", passwordResetEmailModel);
            mail.IsBodyHtml = true;

            this.SmtpClient.Send(mail);
        }

        /// <summary>
        /// 取得寄件人 Email
        /// </summary>
        public MailAddress SenderEmailAddress { get; private set; }

        /// <summary>
        /// 取得 SMTP 客戶端
        /// </summary>
        public SmtpClient SmtpClient { get; private set; }
    }
}
