using Mojo.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.Persistance.Emails
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailMessage email);
    }
}
