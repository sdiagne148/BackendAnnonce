using BackendAnnonce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Commands
{
    public class DeleteAnnonceByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteAnnonceByIdCommandHandler : IRequestHandler<DeleteAnnonceByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteAnnonceByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteAnnonceByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Annonces.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null) return default;
                _context.Annonces.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
