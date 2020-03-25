using AutoMapper;
using WebStore.ViewModels;

namespace WebStore.Models.DTO
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<CustomerSaveDTO, Customer>();
            CreateMap<Customer, CustomerSaveDTO>();
            CreateMap<User, User>();
            CreateMap<CustomerFormViewModel, User>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<Product, ProductDTO>();

        }
    }
}
