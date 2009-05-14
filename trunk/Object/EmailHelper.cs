using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace hwj.CommonLibrary.Object
{
    public class EmailHelper
    {
        public static bool Send(string SmtpServer, string EmailFrom, string EmailFromPassword, string EmailTo, string Subject, string Body)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient(SmtpServer);
            client.UseDefaultCredentials = false;
            client.Credentials =
            new System.Net.NetworkCredential(EmailFrom, EmailFromPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            System.Net.Mail.MailMessage message = new MailMessage(EmailFrom, EmailTo, Subject, Body);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            client.Send(message);
            return true;
        }
    }
}
