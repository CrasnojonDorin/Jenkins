using AutoMapper;
using WebStore.ViewModels;

namespace WebStore.Models.DTO
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            CreateMap<User, User>();
            CreateMap<UserFormViewModel, User>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<Product, ProductDTO>();

        }
    }
}
