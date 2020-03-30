using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using WebStore.Controllers;
using WebStore.Models;
using WebStore.Models.DTO;
using WebStore.Tests.FakeClasses;
using WebStore.Tests.FakeClasses.Identity;
using WebStore.ViewModels;
using Xunit;

namespace WebStore.Tests.Controller
{
    public class AdministrationControllerShould : StoreTestBase
    {

        private Mock<FakeRoleManager> _mockRoleManager;
        private Mock<FakeUserManager> _mockUserManager;
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

            //mapper configuration
            domainProfile = new DomainProfile();
            configuration = new MapperConfiguration(x => x.AddProfile(domainProfile));
            _mapper = new Mapper(configuration);


            

            _sut = new AdministrationController(_mockRoleManager.Object, _mockUserManager.Object, _context, _mockWebHostEnviroment.Object, _mapper);


        }

        //details
        [Fact]
        public async void ReturnViewForDetailsWithData()
        {
            //in fake database added element with id = 1
            var result = await _sut.Details(1);
            var user = _context.Users.Single(x => x.Id.Equals(1));

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(view.Model);
            Assert.Equal(model.Id, user.Id);
            Assert.Equal(model.FirstName, user.FirstName);
            Assert.Equal(model.LastName, user.LastName);
            Assert.Equal(model.GenderId, user.GenderId);
            Assert.Equal(model.Email, user.Email);
        }


        //userform
        [Fact]
        public async void ReturnViewForUserFormWithData()
        {
            //in fake database created user with id = 1
            var result = await _sut.EditUser(1);
            var user = await _context.Users.SingleAsync(x => x.Id.Equals(1));


            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserFormViewModel>(view.Model);
            Assert.Equal(model.FirstName, user.FirstName);
            Assert.Equal(model.LastName, user.LastName);
            Assert.Equal(model.GenderId, user.GenderId);
            Assert.Equal(model.Town, user.Town);

        }

        [Fact]
        public async void ReturnNotFoundViewForIfUserWasNotFounded()
        {
            //in fake database created user with id = 1
            var result = await _sut.EditUser(2);
            var user = await _context.Users.SingleAsync(x => x.Id.Equals(1));


            var view = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void ReturnViewWithDataWhenInvalidModelStateInUserForm()
        {
            //Arrange

            var viewModel = new UserFormViewModel
            {
                FirstName = "Test",
                LastName = "Test",
                GenderId = 1,
                PhoneNumber = "999999999",
                Town = "Test"
            };
            _sut.ModelState.AddModelError("x", "Test Error");


            //Act
            var result = await _sut.EditUser(viewModel);


            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserFormViewModel>(viewResult.Model);
            Assert.Equal(viewModel.FirstName, model.FirstName);
            Assert.Equal(viewModel.GenderId, model.GenderId);
            Assert.Equal(viewModel.LastName, model.LastName);
            Assert.Equal(viewModel.PhoneNumber, model.PhoneNumber);
            Assert.Equal(viewModel.Town, model.Town);

        }

        [Fact]
        public async void ReturnUserFormViewWithDataWhenModelInvalid()
        {
            _sut.ModelState.AddModelError("x", "test error");

            //in fake database created user with id = 1
            var viewModel = new UserFormViewModel { FirstName = "TestName", LastName = "TestLastName", Town = "testTown", GenderId = 1, PhoneNumber = "223322" };
            var result = await _sut.EditUser(viewModel);


            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserFormViewModel>(view.Model);
            Assert.Equal(model.FirstName, viewModel.FirstName);
            Assert.Equal(model.LastName, viewModel.LastName);
            Assert.Equal(model.GenderId, viewModel.GenderId);
            Assert.Equal(model.Town, viewModel.Town);
            Assert.Equal(model.PhoneNumber, viewModel.PhoneNumber);


        }

        [Fact]
        public async void RedirectToIndexViewWhenModelStateIsValid()
        {

            //in fake database created user with id = 1
            var viewModel = new UserFormViewModel { FirstName = "TestName", LastName = "TestLastName", Town = "testTown", GenderId = 1, PhoneNumber = "223322" };
            var result = await _sut.EditUser(viewModel);


            var view = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(view.ActionName, $"Index");
            Assert.Equal(view.ControllerName, $"Administration");
        }
    }
}
