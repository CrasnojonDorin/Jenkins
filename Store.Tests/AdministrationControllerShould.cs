using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store.Controllers;
using Store.Models;
using Store.Models.DTO;
using Store.Tests.MockData;
using Store.Tests.MockData.MockIdentity;
using Store.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class AdministrationControllerShould : StoreMockContext
    {

        private Mock<FakeRoleManager> _mockRoleManager;
        private Mock<FakeUserManager> _mockUserManager;
        private Mock<FakeSignInManager> _mockSignInManager;
        private Mock<IWebHostEnvironment> _mockWebHostEnviroment;
        private IMapper _mapper;
        private DomainProfile domainProfile;
        private MapperConfiguration configuration;


        private AdministrationController _sut;

        public AdministrationControllerShould()
        {
            _mockUserManager = new FakeUserManagerBuilder().Build();
            _mockWebHostEnviroment = new Mock<IWebHostEnvironment>();
            _mockRoleManager = new FakeRoleManagerBuilder().Build();
            _mockSignInManager = new FakeSignInManagerBuilder().Build();

            //mapper configuration
            domainProfile = new DomainProfile();
            configuration = new MapperConfiguration(x => x.AddProfile(domainProfile));
            _mapper = new Mapper(configuration);
            _sut = new AdministrationController(_mockRoleManager.Object, _mockUserManager.Object, _context, _mockWebHostEnviroment.Object, _mapper, _mockSignInManager.Object);


        }

        //userform
        

        [Fact]
        public async Task SaveProductWhenValidModel()
        {
            //Arrange
            var productViewModel = new ProductFormViewModel() { Name = "SaveTest", Price = "2000", BrandId = 1, ColorId = 1, SexId = 1, CategoryId = 1, Description = "SaveTest", PhotoPath = "XD" };

            //Act
            await _sut.SaveProduct(productViewModel);
            var savedType = _context.Products.FirstOrDefault(x => x.Name.Equals("SaveTest"));


            //Assert
            Assert.NotNull(savedType);
            Assert.Equal(productViewModel.Name, savedType.Name);
        }

       

        [Fact]
        public async Task SaveCategoryWhenValidModel()
        {
            //Arrange
            var category = new Category
            {
                Name = "TestSave"
            };

            //Act
            await _sut.SaveCategory(category);
            var savedCategory = _context.Categories.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedCategory);
            Assert.Equal(category.Name, savedCategory.Name);
        }
  

        [Fact]
        public async Task SaveColorWhenValidModelAsync()
        {
            //Arrange
            var color = new Color
            {
                Name = "TestSave"
            };

            //Act
            await _sut.SaveColor(color);
            var savedColor = _context.Colors.FirstOrDefault(x => x.Name.Equals("TestSave"));


            //Assert
            Assert.NotNull(savedColor);
            Assert.Equal(color.Name, savedColor.Name);
        }


    }
}
