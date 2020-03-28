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
        private readonly Mock<FakeUserManager>_mockUserManager;
        private readonly Mock<FakeSignInManager> _mockSignInManager;
        private readonly AccountController _sut;


        public AccountControllerShould()
        {
            _mockUserManager = new FakeUserManagerBuilder().Build();
            _mockSignInManager = new FakeSignInManagerBuilder()
                .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                    .ReturnsAsync(SignInResult.Success))
                .Build();
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
        public async void RedirectUserAfterSuccesfoulLogin()
        {
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();
            _sut.Url = mockUrlHelper.Object;
            var result = await _sut.Login(new LoginViewModel(), "testPath");
            Assert.IsType<RedirectResult>(result);
        }

    }
}
