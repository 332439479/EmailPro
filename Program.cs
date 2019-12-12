using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace EmailPro
{
    class Program
    {
        static void Main(string[] args)
        {
            string mailserver = ConfigurationManager.AppSettings["mailserver"];
            string teststatus = ConfigurationManager.AppSettings["test"];
            string testmail = ConfigurationManager.AppSettings["testmail"].Trim();
            string strSubject = "Emial Function";
            string strFrom = "82006987@ath.asmpt.com";
            string strTo = "82006987@ath.asmpt.com"; //ConfigurationManager.AppSettings["MailTo"]; ;
            string strCc = "82006987@ath.asmpt.com";// ConfigurationManager.AppSettings["MailCC"]; ;// "riva.wong@asmpt.com; paco.cheung@asmpt.com";
            string strBcc = string.Empty;
            string strBody = "Email with table and picture template.  <br >" + "Demo:  <br >" +
                "<table border=1>" +
                "<tr >" +
                "<td><img src=\"cid:Email001\"></td>" +
                "<td>Column 2</td>" +
                "</tr>" +
                "</table>";
            MailPriority MailPriority = 0;



            MailMessage clsMailSvc;
            clsMailSvc = new MailMessage();


            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(strBody, null, "text/html");
            LinkedResource lrImage = new LinkedResource(@"C:\Users\82006987\Desktop\Program\Practice\EmailPro\EmailPro\img\1.jpg", "image/gif");
            lrImage.ContentId = "Email001";
            htmlBody.LinkedResources.Add(lrImage);
            clsMailSvc.AlternateViews.Add(htmlBody);



            clsMailSvc.From = new MailAddress(strFrom);
            if (strTo != "")
            {
                string[] str = strTo.Split(';');
                for (int i = 0; i < str.Length; i++)
                {
                    if (str.GetValue(i).ToString() != null && str.GetValue(i).ToString().Trim() != "")
                        clsMailSvc.To.Add(new MailAddress(str.GetValue(i).ToString()));
                }
            }
            clsMailSvc.Subject = strSubject;
            if (strCc != "")
            {
                string[] str = strCc.Split(';');
                for (int i = 0; i < str.Length; i++)
                {//"The hash code for \"{0}\" is: 0x{1:X8}, {1}"
                    if (str.GetValue(i).ToString() != null && str.GetValue(i).ToString().Trim() != "")

                        clsMailSvc.CC.Add(new MailAddress(str.GetValue(i).ToString()));
                }
            }
            if (strBcc != "")
            {
                string[] str = strBcc.Split(';');
                for (int i = 0; i < str.Length; i++)
                {
                    if (str.GetValue(i).ToString() != null && str.GetValue(i).ToString().Trim() != "")
                        clsMailSvc.CC.Add(new MailAddress(str.GetValue(i).ToString()));
                }
            }
            clsMailSvc.Body = strBody;
            clsMailSvc.BodyEncoding = Encoding.GetEncoding("utf-8");
            clsMailSvc.IsBodyHtml = true;
            clsMailSvc.Priority = MailPriority;


            SmtpClient clsSmtpClient = new SmtpClient(mailserver);
            try
            {
                clsSmtpClient.Send(clsMailSvc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
