using BackendAnnonce.Domain.Entities;
using BackendAnnonce.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Queries
{
    public class GetAnnonceByIdQuery : IRequest<Annonce>
    {
        public int Id { get; set; }
        public class GetAnnonceByIdQueryHandler : IRequestHandler<GetAnnonceByIdQuery, Annonce>
        {
            private readonly IApplicationDbContext _context;
            public GetAnnonceByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Annonce> Handle(GetAnnonceByIdQuery request, CancellationToken cancellationToken)
            {
                var annonce = _context.Annonces.Where(a => a.Id == request.Id).FirstOrDefault();
                if (annonce == null) return null;
                return annonce;
            }
        }
    }
}
