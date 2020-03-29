using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Tests.FakeClasses.Identity;
using WebStore.ViewModels;
using Xunit;

namespace WebStore.Tests.Controller
{
    public class CustomerControllerShould : StoreTestBase
    {
        private readonly Mock<IMapper> _mapper;
        private Mock<FakeUserManager> _mockUserManager;
        private readonly Mock<IWebHostEnvironment> _mockHostEnvironment;
        private readonly CustomerController _sut;


        public CustomerControllerShould()
        {
            _mapper = new Mock<IMapper>();
            _mockHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockUserManager = new FakeUserManagerBuilder().Build();
            _sut = new CustomerController(_context, _mapper.Object, _mockUserManager.Object, _mockHostEnvironment.Object);
        }


        [Fact]
        public void ReturnViewForIndex()
        {
            var result = _sut.Index();

            var view = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void ReturnViewForDetails()
        {
            var result = _sut.Details("TestUserName");

            var view = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForCustomerForm()
        {
            var result = _sut.CustomerForm();

            var view = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async Task ReturnViewWithDataWhenInvalidModelStateInCustomerFormAsync()
        {
            //Arrange

            var customerViewModel = new CustomerFormViewModel
            {
                FirstName = "Test", LastName = "Test", GenderId = 1, PhoneNumber = 999999999, Town = "Test"
            };
            _sut.ModelState.AddModelError("x", "Test Error");


            //Act
            var result = await _sut.CustomerForm(customerViewModel);


            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CustomerFormViewModel>(viewResult.Model);
        }



    }
}
