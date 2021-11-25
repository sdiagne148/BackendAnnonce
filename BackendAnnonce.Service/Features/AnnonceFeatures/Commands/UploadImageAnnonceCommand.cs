using BackendAnnonce.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAnnonce.Service.Features.AnnonceFeatures.Commands
{
    public class UploadImageAnnonceCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public class UploadImageAnnonceCommandHandler : IRequestHandler<UploadImageAnnonceCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UploadImageAnnonceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UploadImageAnnonceCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Annonces.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.Image = request.Image;
                    _context.Annonces.Update(cust);
                    await _context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
