using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Domain.Services;
using DigiCV.Infrastructure.EmailTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DigiCV.Infrastructure.Features.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public EmailMessageService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateEmail(string receiverEmail, string receiverName, string subject, string body)
        {
            Email email = new Email();
            email.ReceiverEmail = receiverEmail;
            email.ReceiverName = receiverName;
            email.Subject = subject;
            email.Body = body;
            email.IsSent = false;
             _unitOfWork.Emails.Add(email);
             _unitOfWork.Save();
        }
        public void UpdateEmail(Email email)
        {
            var updatedEmail = _unitOfWork.Emails.GetById(email.Id);
            updatedEmail.ReceiverEmail = email.ReceiverEmail;
            updatedEmail.ReceiverName = email.ReceiverName;
            updatedEmail.Subject = email.Subject;
            updatedEmail.Body = email.Body;
            updatedEmail.IsSent = email.IsSent;
            _unitOfWork.Save();

        }
        public IList<Email> GetUnsentEmails()
        {
            return _unitOfWork.Emails.GetAll().Where(x => x.IsSent == false).ToList();
        }
        public void CreateConfirmationEmail(string receiverEmail, string receiverName, string confirmationLink)
        {
            var template = new EmailConfirmationTemplate(receiverName, HtmlEncoder.Default.Encode(confirmationLink));
            string body = template.TransformText();
            string subject = "Please confirm your email";
            CreateEmail(receiverEmail, receiverName, subject, body);

        }

    }
}
