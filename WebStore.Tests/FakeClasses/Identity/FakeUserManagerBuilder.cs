using System;
using Moq;

namespace WebStore.Tests.FakeClasses.Identity
{
    public class FakeUserManagerBuilder
    {
        private Mock<FakeUserManager> _mock = new Mock<FakeUserManager>();

        public FakeUserManagerBuilder With(Action<Mock<FakeUserManager>> mock)
        {
            mock(_mock);
            return this;
        }
        public Mock<FakeUserManager> Build()
        {
            return _mock;
        }
    }
}