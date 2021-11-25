using AutoMapper;
using BackendAnnonce.Domain.Entities;
using BackendAnnonce.Infrastructure.ViewModel;

namespace BackendAnnonce.Infrastructure.Mapping
{
    public class AnnonceProfile : Profile
    {
        public AnnonceProfile()
        {
            CreateMap<AnnonceModel, Annonce>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.AnnonceId))
                .ReverseMap();
        }
    }
}
