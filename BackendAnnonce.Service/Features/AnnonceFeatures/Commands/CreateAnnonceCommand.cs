using BackendAnnonce.Domain.Entities;
using BackendAnnonce.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Commands
{
    public class CreateAnnonceCommand : IRequest<int>
    {
        public string Titre { get; set; }
        public string Description { get; set; }
       
        public class CreateAnnonceCommandHandler : IRequestHandler<CreateAnnonceCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateAnnonceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateAnnonceCommand request, CancellationToken cancellationToken)
            {
                var annonce = new Annonce();
                annonce.Titre = request.Titre;
                annonce.Description = request.Description;

                _context.Annonces.Add(annonce);
                await _context.SaveChangesAsync();
                return annonce.Id;
            }
        }
    }
}
