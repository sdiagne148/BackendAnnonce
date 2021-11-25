using System.ComponentModel.DataAnnotations;

namespace BackendAnnonce.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
