using BackendAnnonce.Domain.Entities;
using BackendAnnonce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Queries
{
    public class GetAllAnnonceQuery : IRequest<IEnumerable<Annonce>>
    {

        public class GetAllAnnonceQueryHandler : IRequestHandler<GetAllAnnonceQuery, IEnumerable<Annonce>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllAnnonceQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Annonce>> Handle(GetAllAnnonceQuery request, CancellationToken cancellationToken)
            {
                var annonceList = await _context.Annonces.ToListAsync();
                if (annonceList == null)
                {
                    return null;
                }
                return annonceList.AsReadOnly();
            }
        }
    }
}
