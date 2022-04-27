using AutoMapper;
using WebAtb.Data.Entities.Identity;
using WebAtb.Model;

namespace WebAtb.Mapper
{
    public class AppMapProfile:Profile
    {
        public AppMapProfile()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(opt => opt.SecondName))
                .ForMember(x => x.Email, opt => opt.MapFrom(opt => opt.Email))
                .ForMember(x => x.Phone, opt => opt.MapFrom(opt => opt.Phone));

            //мепер для вівода юзера
            CreateMap<AppUser, UserItemViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => $"/images/{x.Photo}"));

            CreateMap<UserEditViewModel, AppUser>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(opt => opt.SecondName))
                .ForMember(x => x.Email, opt => opt.MapFrom(opt => opt.Email))
                .ForMember(x => x.Phone, opt => opt.MapFrom(opt => opt.Phone));
        }

        
    }
}
