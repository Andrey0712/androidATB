using AutoMapper;
using System.Globalization;
using WebAtb.Data.Entities;
using WebAtb.Data.Entities.Identity;
using WebAtb.Model;

namespace WebAtb.Mapper
{
    public class AppMapProfile:Profile
    {
        public AppMapProfile()
        {
            var cultureInfo = new CultureInfo("uk-UA");

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
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.MapFrom(opt => opt.FirstName))
                .ForMember(x => x.SecondName, opt => opt.MapFrom(opt => opt.SecondName))
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.MapFrom(opt => opt.Phone));


            //мепери для категорії
            CreateMap<CreateCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, CategoryItemViewModel>();

        }

        
    }
}
