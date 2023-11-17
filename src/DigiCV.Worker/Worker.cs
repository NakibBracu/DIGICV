using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Services;

namespace DigiCV.EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly IEmailMessageService _emailMessageService;

        public Worker(ILogger<Worker> logger, IEmailService emailService, IEmailMessageService emailMessageService)
        {
            _logger = logger;
            _emailService = emailService;
            _emailMessageService = emailMessageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               try
                {
                    foreach (var email in _emailMessageService.GetUnsentEmails())
                    {
                        _emailService.SendSingleEmail(email.ReceiverName, email.ReceiverEmail, email.Subject, email.Body);
                        email.IsSent = true;
                        _emailMessageService.UpdateEmail(email);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}