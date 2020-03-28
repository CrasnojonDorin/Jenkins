using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WebStore.Models;

namespace WebStore.Tests.FakeClasses.Identity
{
    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager()
            : base(userManager: new Mock<FakeUserManager>().Object,
                contextAccessor: new HttpContextAccessor(),
                claimsFactory: new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                optionsAccessor: new Mock<IOptions<IdentityOptions>>().Object,
                logger: new Mock<ILogger<SignInManager<User>>>().Object,
                schemes: new Mock<IAuthenticationSchemeProvider>().Object,
                new DefaultUserConfirmation<User>())
        { }
    }
}