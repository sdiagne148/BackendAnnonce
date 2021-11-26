
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BackendAnnonce.Infrastructure.ViewModel
{
    public class AnnonceModel
    {

        public int AnnonceId { get; set; }
        [Required] 
        public IFormFile AnnonceFile { get; set; }

    }
}
