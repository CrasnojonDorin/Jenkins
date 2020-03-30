using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using WebStore.Models;

namespace WebStore.Tests.FakeClasses.Identity
{
    public class FakeRoleManager : RoleManager<Role>
    {
        public FakeRoleManager()
            : base(
                new Mock<IRoleStore<Role>>().Object,
                new IRoleValidator<Role>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<Role>>>().Object)
        { }

    }
}
