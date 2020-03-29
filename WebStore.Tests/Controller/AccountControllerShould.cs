using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
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
        private AccountController _sut;


        public AccountControllerShould()
        {
            _mockUserManager = new FakeUserManagerBuilder().Build();
            _mockSignInManager = new FakeSignInManagerBuilder().Build();
            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object);
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

            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object);

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
            _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object);


            //Act
            _sut.Url = mockUrlHelper.Object;
            var result = await _sut.Login(new LoginViewModel(), "testPath");

            //Assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionResult.ActionName, $"Index");
            Assert.Equal(actionResult.ControllerName, $"Home");

        }

        [Fact]
        public async void RedirectUserToLoginViewIfModelIsInvalid()
        {

            //Act
            //_sut.Url = mockUrlHelper.Object;
            _sut.ModelState.AddModelError("x", "test error");
            var result = await _sut.Login(new LoginViewModel(), "testPath");

            //Assert
            var actionResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(actionResult.ViewName, $"Login");
        }


        //todo register tests

        //[Fact]
        //public async void RedirectUserToHomePageAfterSuccessfulRegistration()
        //{
        //    //Arrange
        //    _mockSignInManager = new FakeSignInManagerBuilder()
        //        .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
        //                It.IsAny<string>(),
        //                It.IsAny<bool>(),
        //                It.IsAny<bool>()))
        //            .ReturnsAsync(SignInResult.Success))
        //        .Build();

        //    _mockUserManager = new FakeUserManagerBuilder()
        //        .With(x => x.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
        //            .ReturnsAsync(IdentityResult.Success)).Build();

        //    var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
        //    mockUrlHelper
        //        .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
        //        .Returns(false)
        //        .Verifiable();

        //    _sut = new AccountController(_context, _mockUserManager.Object, _mockSignInManager.Object);


        //    //Act
        //    _sut.Url = mockUrlHelper.Object;
        //    var result = await _sut.Register(new RegisterViewModel());

        //    //Assert
        //    var actionResult = Assert.IsType<RedirectToActionResult>(result);
        //    Assert.Equal(actionResult.ActionName, $"Index");
        //    Assert.Equal(actionResult.ControllerName, $"Home");

        //}


        [Fact]
        public async void RedirectUserToIndexViewWhenLogout()
        {

            //Act
            var result = await _sut.Logout();

            //Assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(actionResult.ActionName, $"Index");
        }



    }
}
