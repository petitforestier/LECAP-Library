using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mail
{
    public class MailTools
    {
        #region Public CONSTRUCTORS

        public MailTools(string iSMTPHost, int iSMTPPort, string iFrom)
        {
            SMTPHost = iSMTPHost;
            SMTPPort = iSMTPPort;
            SMTPFrom = iFrom;
        }

        #endregion

        #region Public METHODS

        public void SendMail(List<string> iToAddressList, List<string> iCopyAddressList, string iSubject, string iMessage, List<string> iAttachmentList)
        {
            if (iToAddressList.IsNullOrEmpty())
                throw new Exception("La liste des destinataires est requise");

            if (iSubject.IsNullOrEmpty())
                throw new Exception("Le sujet du mail est requis");

            using (var client = new SmtpClient())
            {
                client.Port = SMTPPort;
                client.Host = SMTPHost;

                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                client.Timeout = 20000;

                using (var mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new MailAddress(SMTPFrom);
                    foreach (var toItem in iToAddressList.Enum())
                        mail.To.Add(new MailAddress(toItem));

                    foreach (var ccItem in iCopyAddressList.Enum())
                        mail.CC.Add(new MailAddress(ccItem));

                    mail.Body = iMessage;
                    mail.Subject = iSubject;

                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    foreach (var attachmentItem in iAttachmentList.Enum())
                    {
                        var attachment = new Attachment(attachmentItem, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attachmentItem);
                        disposition.ModificationDate = File.GetLastWriteTime(attachmentItem);
                        disposition.ReadDate = File.GetLastAccessTime(attachmentItem);
                        disposition.FileName = Path.GetFileName(attachmentItem);
                        disposition.Size = new FileInfo(attachmentItem).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        mail.Attachments.Add(attachment);
                    }

                    client.Send(mail);
                }
            }
        }

        #endregion

        #region Private FIELDS

        private string SMTPHost;
        private int SMTPPort;
        private string SMTPFrom;

        #endregion
    }
}