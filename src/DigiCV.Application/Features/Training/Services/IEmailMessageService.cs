using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Services
{
    public interface IEmailMessageService
    {
        void CreateConfirmationEmail(string receiverEmail, string receiverName, string confirmationLink);
        IList<Email> GetUnsentEmails();
        void UpdateEmail(Email email);

    }
}
