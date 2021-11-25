using System.ComponentModel.DataAnnotations;

namespace BackendAnnonce.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
