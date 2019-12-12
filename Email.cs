using System;
using System.Configuration;
using System.Net.Mail;

/// <summary>
/// MailSvc 的摘要描述
/// </summary>
public class Email
{
    public Email()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
/// <summary>
/// 發郵件函數
/// </summary>
/// <param name="strSubject">主題</param>
/// <param name="strFrom">發件人</param>
/// <param name="strTo">收件人</param>
/// <param name="strCc">CC</param>
/// <param name="strBcc">BCC</param>
/// <param name="strBody">主題</param>
/// <param name="MailPriority">郵件重要性</param>
    public static void SendMail(string strSubject, string strFrom, string strTo, string strCc, string strBcc, string strBody, System.Net.Mail.MailPriority MailPriority)
    {
        string mailserver = ConfigurationManager.AppSettings["mailserver"];
        string teststatus = ConfigurationManager.AppSettings["test"];
        string testmail = ConfigurationManager.AppSettings["testmail"].Trim();
        if (teststatus == "1")
        {
            strBody += "\r\n*" + strTo + "&" + strCc;
            strTo = testmail;
            strCc = testmail;
            strBcc = "";
        }

        MailMessage  clsMailSvc;
        clsMailSvc = new MailMessage ( );

        clsMailSvc.From  =  new MailAddress ( strFrom );
        if ( strTo != "" )
        {
            string [] str = strTo.Split ( ';' );
            for ( int i = 0 ; i < str.Length ; i++ )
            {
                if ( str.GetValue ( i ).ToString ( ) != null && str.GetValue ( i ).ToString ( ).Trim ( ) != "" )
                    clsMailSvc.To.Add ( new MailAddress ( str.GetValue ( i ).ToString ( ) ) );
            }
        }
        clsMailSvc.Subject = strSubject;
        if ( strCc != "" )
        {
            string [] str = strCc.Split ( ';' );
            for ( int i = 0 ; i < str.Length ; i++ )
            {//"The hash code for \"{0}\" is: 0x{1:X8}, {1}"
                if ( str.GetValue ( i ).ToString ( ) != null && str.GetValue ( i ).ToString ( ).Trim ( ) != "" )
                    
                    clsMailSvc.CC.Add ( new MailAddress ( str.GetValue ( i ).ToString ( ) ) );
            }
        }
        if ( strBcc != "" )
        {
            string [] str = strBcc.Split ( ';' );
            for ( int i = 0 ; i < str.Length ; i++ )
            {
                if ( str.GetValue ( i ).ToString ( ) != null &&  str.GetValue ( i ).ToString ( ).Trim ( ) != "" )
                clsMailSvc.CC.Add ( new MailAddress ( str.GetValue ( i ).ToString ( ) ) );
            }
        }
        clsMailSvc.Body = strBody;
        clsMailSvc.BodyEncoding = System.Text.Encoding.GetEncoding ( "utf-8" );
        clsMailSvc.IsBodyHtml = true;
        clsMailSvc.Priority = MailPriority;


        System.Net.Mail.SmtpClient clsSmtpClient = new SmtpClient(mailserver);
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
