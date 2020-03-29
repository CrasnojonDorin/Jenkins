using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebStore.Tests.Controller
{
    public class AccountControllerShould : StoreTestBase
    {
        private Mock<FakeSignInManager> _mockSignInManager;
        private Mock<FakeUserManager> _mockUserManager;
        private Mock<IWebHostEnvironment> _mockWebHostEnviroment;
        private IMapper _mapper;
        private DomainProfile domainProfile;
        private MapperConfiguration configuration;


        private AccountController _sut;


        public AccountControllerShould()
        {
            _mockUserManager = new FakeUserManagerBuilder().Build();
            _mockSignInManager = new FakeSignInManagerBuilder().Build();
            _mockWebHostEnviroment = new Mock<IWebHostEnvironment>();
            //mapper configuration
            domainProfile = new DomainProfile();
            configuration = new MapperConfiguration(x=>x.AddProfile(domainProfile));
            _mapper = new Mapper(configuration);

            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object, _mockWebHostEnviroment.Object, _mapper);
        }


        [Fact]
        public void ReturnViewForRegister()
        {
            //Act
            IActionResult result = _sut.Register();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForLogin()
        {
            //Act
            IActionResult result = _sut.Login();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void RedirectUserToLocalPageAfterSuccessfulLoginIfHeWasLoggingFromLocalPage()
        {
            //Arrange
            _mockSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success))
                .Build();

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();

            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object, _mockWebHostEnviroment.Object, _mapper);

            //Act
            _sut.Url = mockUrlHelper.Object;
            var result = await _sut.Login(new LoginViewModel(), "testPath");
            
            //Assert
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async void RedirectUserToHomePageAfterSuccessfulLoginIfHeWasLoggingNotFromLocalPage()
        {
            //Arrange
            _mockSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                    .ReturnsAsync(SignInResult.Success))
                .Build();

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(false)
                .Verifiable();
            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object, _mockWebHostEnviroment.Object, _mapper);


            //Act
            _sut.Url = mockUrlHelper.Object;
            var result = await _sut.Login(new LoginViewModel(), "testPath");

            //Assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionResult.ActionName, $"Index");
            Assert.Equal(actionResult.ControllerName, $"Home");

        }

        [Fact]
        public async void RedirectUserToLoginViewIfModelIsInvalidWithData()
        {

            //Act
            _sut.ModelState.AddModelError("x", "test error");
            var result = await _sut.Login(new LoginViewModel(), "testPath");

            //Assert
            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LoginViewModel>(actionResult.Model);
            Assert.Equal(actionResult.ViewName, $"Login");
        }


        //register
        [Fact]
        public async void RedirectUserToHomePageAfterSuccessfulRegistration()
        {
            //Arrange
            _mockSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                    .ReturnsAsync(SignInResult.Success))
                .Build();

            _mockUserManager = new FakeUserManagerBuilder()
                .With(x => x.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .ReturnsAsync(IdentityResult.Success)).Build();

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object, _mockWebHostEnviroment.Object, _mapper);


            //Act
            _sut.Url = mockUrlHelper.Object;
            var result = await _sut.Register(new RegisterViewModel());

            //Assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionResult.ActionName, $"Index");
            Assert.Equal(actionResult.ControllerName, $"Home");

        }

        [Fact]
        public async void RedirectUserToRegisterViewIfModelIsInvalidWithData()
        {

            //Act
            _sut.ModelState.AddModelError("x", "test error");
            var testViewModel = new RegisterViewModel
                {FirstName = "Test", LastName = "Test2", UserName = "Test3", GenderId = 1};
            var result = await _sut.Register(testViewModel);

            //Assert
            var actionResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<RegisterViewModel>(actionResult.Model);

            Assert.Equal(actionResult.ViewName, $"Register");
            Assert.Equal(model.FirstName, testViewModel.FirstName);
            Assert.Equal(model.LastName, testViewModel.LastName);
            Assert.Equal(model.UserName, testViewModel.UserName);
            Assert.Equal(model.GenderId, testViewModel.GenderId);

        }


        //logout
        [Fact]
        public async void RedirectUserToIndexViewWhenLogout()
        {

            //Act
            var result = await _sut.Logout();

            //Assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionResult.ActionName, $"Index");
        }


        [Fact]
        public async void ReturnViewForIndex()
        {
            var result = await _sut.Index();

            var view = Assert.IsType<ViewResult>(result);
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
            var result = await _sut.UserForm(1);
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
            var result = await _sut.UserForm(2);
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
            var result = await _sut.UserForm(viewModel);


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
            var viewModel = new UserFormViewModel{FirstName = "TestName", LastName = "TestLastName", Town = "testTown", GenderId = 1,PhoneNumber = "223322"};
            var result = await _sut.UserForm(viewModel);


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
            var result = await _sut.UserForm(viewModel);


            var view = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(view.ActionName, $"Index");
            Assert.Equal(view.ControllerName, $"Account");
        }


    }
}
