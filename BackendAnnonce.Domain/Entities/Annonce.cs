using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAnnonce.Domain.Entities
{
    public class Annonce : BaseEntity
    {
        public string Titre { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
