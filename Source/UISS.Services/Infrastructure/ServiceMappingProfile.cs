namespace UISS.Services.Infrastructure
{
    using AutoMapper;

    using Models.User;
    using Data.Models.Identity;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<User, UserServiceModel>();
        }
    }
}
