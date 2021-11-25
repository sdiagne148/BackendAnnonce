using BackendAnnonce.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Commands
{
    public class UpdateAnnonceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public class UpdateAnnonceCommandHandler : IRequestHandler<UpdateAnnonceCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateAnnonceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateAnnonceCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Annonces.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.Titre = request.Titre;
                    cust.Description = request.Description;
                    cust.Image = request.Image;
                    _context.Annonces.Update(cust);
                    await _context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
