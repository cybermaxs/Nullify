using Nullify.Tests.Interfaces;
using System;
using Xunit;

namespace Nullify.Tests
{
    public interface ISetupTests
    {
        int MyProp { get; set; }

    }
    public class SetupTests
    {
        [Fact]
        public void BasicSetup()
        {
            var o1 = Null.Of<ISetupTests>()
                .For(i => i.MyProp).Returns(5)
                .Named("base1")
                .Create();

            Assert.Equal(5, o1.MyProp);

            var o2 = Null.Of<ISetupTests>()
               .For(i => i.MyProp).Returns(15)
                .Named("base2")
                .Create();
            Assert.Equal(15, o2.MyProp);

            Assert.NotEqual(o1, o2);
        }

        [Fact]
        public void WhenNotInteface_ShouldCreateNull()
        {
            var setup = Null.Of<Version>();
               
            Assert.False(setup.CanCreate());

            Assert.Null(setup.Create());
        }
    }
}
