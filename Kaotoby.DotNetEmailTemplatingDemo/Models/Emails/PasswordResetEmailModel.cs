using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaotoby.DotNetEmailTemplatingDemo.Models.Emails
{
    /// <summary>
    /// 密碼重設信的資料
    /// </summary>
    public class PasswordResetEmailModel
    {
        /// <summary>
        /// 取得或設定重設密碼連結
        /// </summary>
        public string PasswordResetLink { get; set; }
    }
}
