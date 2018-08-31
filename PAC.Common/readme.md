# PAC.Common

## Configuración típica de email service en appsettings.json para proyecto WebAPI

	{
	  "ConnectionStrings": {
		"PACConnectionString": "Server=(local);Database=PAC;Trusted_Connection=True;MultipleActiveResultSets=true;"
	  },
	  "Logging": {
		"IncludeScopes": false,
		"Debug": {
		  "LogLevel": {
			"Default": "Warning"
		  }
		},
		"Console": {
		  "LogLevel": {
			"Default": "Warning"
		  }
		},

		"Email": {
		  "Enabled":  false,
		  "FromEmail": "desarrollo@tax-individual.com.co",
		  "MailType": "SMTP",
		  "MailServer": "192.168.1.3",
		  "MailPort": 25,
		  "UseSSL": false,
		  "Username": "",
		  "Password": "",
		  "RemoteServerAPI": "",
		  "RemoteServerKey": ""
		}
	  }
	}

