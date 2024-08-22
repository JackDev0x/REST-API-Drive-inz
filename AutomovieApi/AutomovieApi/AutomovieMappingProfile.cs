using AutoMapper;
using AutomovieApi.Entities;
using AutomovieApi.Models;
using AutomovieApi.Models.SuggestionsDto;

namespace AutomovieApi
{
    public class AutomovieMappingProfile : Profile
    {
        public AutomovieMappingProfile() {
            CreateMap<Announcement, AnnouncementDto>()
                       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AnId))
                       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                       .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                       .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                       .ForMember(dest => dest.Multimedia, opt => opt.MapFrom(src => src.Multimedia))
                       .ForMember(dest => dest.DriverAssistanceSystems, opt => opt.MapFrom(src => src.DriverAssistanceSystems))
                       .ForMember(dest => dest.Safety, opt => opt.MapFrom(src => src.Safety))
                       .ForMember(dest => dest.Performance, opt => opt.MapFrom(src => src.Performance))
                       .ForMember(dest => dest.Other, opt => opt.MapFrom(src => src.Other));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname));
            CreateMap<AnnouncementImages, AnnouncementImagesDto>();
            CreateMap<FavoriteAnnouncements, FavoriteAnnouncementsDto>()
                .ForMember(dest => dest.AnnouncementId, opt => opt.MapFrom(src => src.AnnouncementAnId));
            CreateMap<Brand, BrandDto>();
            CreateMap<Model, ModelDto>();
            CreateMap<User, UserDto>();

            CreateMap<Multimedia, MultimediaDto>()
           .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.MultimediaDataset.feature));

            CreateMap<DriverAssistanceSystems, DriverAssistanceSystemsDto>()
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.DriverAssistanceSystemsDataset.feature));

            CreateMap<Safety, SafetyDto>()
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.SafetyDataset.feature));

            CreateMap<Performance, PerformanceDto>()
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.PerformanceDataset.feature));

            CreateMap<Other, OtherDto>()
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.OtherDataset.feature));

            CreateMap<Brand, BrandOrModelOrTypeSuggestionsDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "-")));
            
            CreateMap<Model, BrandOrModelOrTypeSuggestionsDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "-")));

            CreateMap<BodyType, BrandOrModelOrTypeSuggestionsDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.BodyTypeID))
            .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Type.ToLower().Replace(" ", "-")));

            CreateMap<FuelType, BrandOrModelOrTypeSuggestionsDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.FuelTypeID))
            .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Type.ToLower().Replace(" ", "-")));
        }
    }
}
