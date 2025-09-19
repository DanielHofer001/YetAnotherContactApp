using AutoMapper;
using YetAnotherContactApp.DTOs;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApi.DTOs
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ContactDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
        }
    }
}
