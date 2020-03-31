using AutoMapper;
using WebStore.ViewModels;
using WebStore.ViewModels.ProductViewModels;

namespace WebStore.Models.DTO
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            CreateMap<User, User>();
            CreateMap<UserFormViewModel, User>();
            CreateMap<User,UserFormViewModel>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserSaveDTO, User>();
            CreateMap<User, UserSaveDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductFormViewModel>();
            CreateMap<ProductFormViewModel, Product>();
        }
    }
}
