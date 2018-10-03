using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaotoby.DotNetEmailTemplatingDemo.Config
{
    /// <summary>
    /// 全域設定檔
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 載入設定檔
        /// </summary>
        public static void load()
        {
            GlobalConfig.WebsiteName = ConfigurationManager.AppSettings.Get("WebsiteName");
            GlobalConfig.WebsiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            GlobalConfig.SmtpHost = ConfigurationManager.AppSettings.Get("SmtpHost");
            GlobalConfig.SmtpPort = Int32.Parse(ConfigurationManager.AppSettings.Get("SmtpPort"));
            GlobalConfig.SmtpUseSsl = Boolean.Parse(ConfigurationManager.AppSettings.Get("SmtpUseSsl"));
            GlobalConfig.SmtpUsername = ConfigurationManager.AppSettings.Get("SmtpUsername");
            GlobalConfig.SmtpPassword = ConfigurationManager.AppSettings.Get("SmtpPassword");
            GlobalConfig.SmtpSenderEmailAddress = ConfigurationManager.AppSettings.Get("SmtpSenderEmailAddress");
        }

        /// <summary>
        /// 取得網站名稱
        /// </summary>
        public static string WebsiteName { get; private set; }
        /// <summary>
        /// 取得網站網址
        /// </summary>
        public static string WebsiteUrl { get; private set; }

        /// <summary>
        /// 取得 SMTP 主機
        /// </summary>
        public static string SmtpHost { get; private set; }
        /// <summary>
        /// 取得 SMTP 連接埠
        /// </summary>
        public static int SmtpPort { get; private set; }
        /// <summary>
        /// 取得 SMTP 是否使用 SSL 加密
        /// </summary>
        public static bool SmtpUseSsl { get; private set; }
        /// <summary>
        /// 取得 SMTP 帳號
        /// </summary>
        public static string SmtpUsername { get; private set; }
        /// <summary>
        /// 取得 SMTP 密碼
        /// </summary>
        public static string SmtpPassword { get; private set; }
        /// <summary>
        /// 取得 SMTP 寄件人 Email
        /// </summary>
        public static string SmtpSenderEmailAddress { get; private set; }
    }
}
