using BackendAnnonce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackendAnnonce.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Annonce> Annonces { get; set; }
        Task<int> SaveChangesAsync();
    }
}
