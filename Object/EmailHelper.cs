using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace hwj.CommonLibrary.Object
{
    public class EmailHelper
    {
        public static bool Send(string SmtpServer, string EmailFrom, string EmailFromPassword,
            string EmailTo, string cc, string Subject, string Body, bool isBodyHtml)
        {
            MailMessage message = new MailMessage(EmailFrom, EmailTo, Subject, Body);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            if (!string.IsNullOrEmpty(cc))
                message.CC.Add(cc);
            message.IsBodyHtml = isBodyHtml;
            return Send(SmtpServer, EmailFrom, EmailFromPassword, message, isBodyHtml);
        }
        public static bool Send(string SmtpServer, string EmailFrom, string EmailFromPassword, MailMessage message, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient(SmtpServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(EmailFrom, EmailFromPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = isBodyHtml;
            client.Send(message);
            return true;
        }
        public static bool isValidEmail(string xEmailAddress)
        {
            bool myIsEmail = false;
            string myRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(myRegex);
            if (reg.IsMatch(xEmailAddress))
            {
                myIsEmail = true;
            }
            return myIsEmail;
        }
    }
}
