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
            Nullified
                .Of<IBasicProperties>()
                .Dependency<INestFirstLevel>(s =>
                {
                    s.PropertyOrMethod(a => a.Sub, null);
                })
                .Create();

            var o2 = Nullified.Of<ISetupTests>()
               .PropertyOrMethod(i => i.MyProp, 15)
                .Named("base2")
                .Create();
            Assert.Equal(15, o2.MyProp);
        }

        [Fact]
        public void WhenNotInteface_ShouldCreateNull()
        {
            var setup = Nullified.Of<Version>();

            Assert.False(setup.CanCreate());

            Assert.Null(setup.Create());
        }
    }
}
