using System;

namespace PAC.Common
{
    /// <summary>
    /// Configure email
    /// 
    /// <code>
    /// <![CDATA[
    /// {
    ///     "Email": {
    ///         "Enabled":  true,
    ///         "FromEmail": "sistemas@tax-individual.com.co",
    ///         "MailType": "SMTP",
    ///         "SenderEmail": "desarrollo@tax-individual.com.co",
    ///         "MailServer": "192.168.1.3",
    ///         "MailPort": 25,
    ///         "UseSSL": false,
    ///         "Username": "",
    ///         "Password": "",
    ///         "RemoteServerAPI": "",
    ///         "RemoteServerKey": ""
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </summary>
    public class EmailSettings
    {

        /// <summary>
        /// sender email address
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// SMTP
        /// </summary>
        public string MailType { get; set; }

        /// <summary>
        /// IP or hostname
        /// </summary>
        public string MailServer { get; set; }
        
        /// <summary>
        /// mailserver port
        /// </summary>
        public int MailPort { get; set; }
        
        /// <summary>
        /// use ssl
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// remote server API URL (i.e. sendgrid)
        /// </summary>
        public string RemoteServerAPI { get; set; }

        /// <summary>
        /// remote server API key (i.e. sendgrid)
        /// </summary>
        public string RemoteServerKey { get; set; }

        /// <summary>
        /// enables mail
        /// </summary>
        public bool Enabled { get; set; }

        public EmailSettings()
        {

        }

        public EmailSettings(string mailType,
                                string mailServer, int mailPort, bool useSSL,
                                string fromEmail, string username, string password, 
                                string remoteServerAPI, string remoteServerKey, bool enabled)
        {
            MailType = mailType;
            MailServer = mailServer;
            MailPort = mailPort;
            UseSSL = useSSL;
            FromEmail = fromEmail;
            Username = username;
            Password = password;
            RemoteServerAPI = remoteServerAPI;
            RemoteServerKey = remoteServerKey;
            Enabled = enabled;
        }
    }
}
