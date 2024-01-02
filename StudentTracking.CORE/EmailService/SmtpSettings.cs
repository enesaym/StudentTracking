using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.CORE.EmailService
{
    public class SmtpSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

/*
   !! Secrets.json a ekle

   dotnet user-secrets set "SmtpSettings:Username" "your_outlook_email@example.com"
   dotnet user-secrets set "SmtpSettings:Password" "your_outlook_password"

    VEYA

    {
      "SmtpSettings": {
        "Username": "your_outlook_email@example.com",
        "Password": "your_outlook_password"
      }
    }
 */