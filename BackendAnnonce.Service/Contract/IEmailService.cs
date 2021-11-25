using BackendAnnonce.Domain.Settings;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
