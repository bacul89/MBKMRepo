using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MBKM.Common.Helpers
{
    public class MailHelper
    {
        #region Fields
        private string _sSMTPServer;
        private int _iSMTPPort;
        private string _sUsername;
        private string _sPassword;

        bool reSultError;
        #endregion

        #region Properties
        public string SMTPServer
        {
            get { return _sSMTPServer; }
            set { _sSMTPServer = value; }
        }

        public int SMTPPort
        {
            get { return _iSMTPPort; }
            set { _iSMTPPort = value; }
        }

        public string Username
        {
            get { return _sUsername; }
            set { _sUsername = value; }
        }

        public string Password
        {
            get { return _sPassword; }
            set { _sPassword = value; }
        }
        #endregion

        #region Constructors
        public MailHelper(string SMTPServer, int SMTPPort)
        {
            this._sSMTPServer = SMTPServer;
            this._iSMTPPort = SMTPPort;
        }

        public class RangeParameter
        {
            public string sSubject { get; set; }
            public string sBody { get; set; }
            public string sFrom { get; set; }
            public List<string> lToList { get; set; }
            public List<string> lCCList { get; set; }
            public List<string> lBccList { get; set; }
            public bool bIsBodyHTML { get; set; }
        }

        public MailHelper(string SMTPServer, int SMTPPort, string Username, string Password)
        {
            this._sSMTPServer = SMTPServer;
            this._iSMTPPort = SMTPPort;
            this._sUsername = Username;
            this._sPassword = Password;
        }
        #endregion

        #region Methods
        private bool ValidateSMTPSetting(ref string Message)
        {
            bool result = true;

            if (string.IsNullOrEmpty(this._sSMTPServer))
            {
                result = false;
                Message = "SMTP Server has not been set yet.";
            }

            return result;
        }

        private MailMessage PrepareMail(string Subject, string Body, string From, List<string> ToList, List<string> CCList, List<string> BccList, bool IsBodyHTML, ref string Message)
        {
            MailMessage mail = null;

            try
            {
                mail = new MailMessage();
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = IsBodyHTML;
                mail.From = new MailAddress(From);

                if (ToList != null && ToList.Count > 0)
                {
                    foreach (string sTo in ToList)
                    {
                        if (!string.IsNullOrEmpty(sTo))
                            mail.To.Add(sTo);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Message))
                    {
                        Message = "To is empty";
                    }
                    else
                    {
                        Message += " To is empty";
                    }
                }

                if (CCList != null && CCList.Count > 0)
                {
                    foreach (string sCC in CCList)
                    {
                        mail.CC.Add(sCC);
                    }
                }

                if (BccList != null && BccList.Count > 0)
                {
                    foreach (string sBCC in ToList)
                    {
                        mail.Bcc.Add(sBCC);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mail;
        }


        public void SendEmailInBackgroundThread(object mail)
        {
            string sMessage = string.Empty;

            try
            {
                if (ValidateSMTPSetting(ref sMessage))
                {
                    RangeParameter SendMailData = mail as RangeParameter;
                    MailMessage mailSend = PrepareMail(SendMailData.sSubject, SendMailData.sBody, SendMailData.sFrom, SendMailData.lToList, SendMailData.lCCList, SendMailData.lBccList, SendMailData.bIsBodyHTML, ref sMessage);
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = this._sSMTPServer;
                    smtpClient.Port = this._iSMTPPort;//dummyemailwal@gmail.com
                    //smtpClient.Credentials = new System.Net.NetworkCredential("sendercvonline@gmail.com", "dian081193");
                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPass"]);
                    if (mail != null)
                    {
                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        //ini yang di hidupkan
                        smtpClient.Send(mailSend);
                        mailSend.Dispose();
                    }

                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }

        public bool SendEmailInBackgroundThreadAfter(object mail)
        {
            bool iResult = false;

            string sMessage = string.Empty;
            try
            {
                if (ValidateSMTPSetting(ref sMessage))
                {
                    RangeParameter SendMailData = mail as RangeParameter;
                    MailMessage mailSend = PrepareMail(SendMailData.sSubject, SendMailData.sBody, SendMailData.sFrom, SendMailData.lToList, SendMailData.lCCList, SendMailData.lBccList, SendMailData.bIsBodyHTML, ref sMessage);
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = this._sSMTPServer;
                    smtpClient.Port = this._iSMTPPort;
                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPass"]);
                    //smtpClient.Credentials = new System.Net.NetworkCredential("sendercvonline@gmail.com", "dian081193");
                    if (mail != null)
                    {
                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        //ini yang di hidupkan
                        smtpClient.Send(mailSend);
                        mailSend.Dispose();
                    }

                }
                iResult = true;
            }
            catch (Exception ex)
            {
                //throw ex;
                iResult = false;
            }


            return iResult;

        }


        public bool SendMail(string Subject, string Body, string From, List<string> ToList, List<string> CCList, List<string> BccList, bool IsBodyHTML)
        {
            bool result = true;

            RangeParameter mail = new RangeParameter
            {
                sSubject = Subject,
                sBody = Body,
                sFrom = From,
                lToList = ToList,
                lCCList = CCList,
                lBccList = BccList,
                bIsBodyHTML = IsBodyHTML
            };

            //Thread bgThread = new Thread(new ParameterizedThreadStart(SendEmailInBackgroundThread));
            //bgThread.IsBackground = true;
            //bgThread.Start(mail);


            if (!SendEmailInBackgroundThreadAfter(mail)) { result = false; }
            return result;
        }

        //add hasto : Mail Asyncronous
        private void SendEmail(System.Net.Mail.MailMessage m, Boolean Async)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.Host = this._sSMTPServer;
            smtpClient.Port = this._iSMTPPort;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailPass"]);

            if (Async)
            {
                SendEmailDelegate sd = new SendEmailDelegate(smtpClient.Send);
                AsyncCallback cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(m, cb, sd);
            }
            else
            {
                smtpClient.Send(m);
            }
        }

        private delegate void SendEmailDelegate(System.Net.Mail.MailMessage m);

        private static void SendEmailResponse(IAsyncResult ar)
        {
            SendEmailDelegate sd = (SendEmailDelegate)(ar.AsyncState);

            sd.EndInvoke(ar);
        }
        //end hasto : Mail Asyncronous

        private static string LogFormat(string Message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Datetime \t : {0: dd MMM yyyy HH:mm:ss}", DateTime.Now);
            sb.AppendLine(string.Empty);
            sb.AppendFormat("Message \t : {0}", Message);
            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);

            return sb.ToString();
        }

        #endregion

    }
}
