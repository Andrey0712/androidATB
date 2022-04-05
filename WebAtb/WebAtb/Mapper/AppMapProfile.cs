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
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));
        }


    }
}
