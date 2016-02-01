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
                .Dependency<INestFirstLevel>(s=>
                {
                    s.PropertyOrMethod(a => a.Sub, null);
                })
                .Create();
               // .Type<IBasicMethods>(d=>d.PropertyOrMethod())

            //var o1 = Nullified
            //    .Of<ISetupTests>()
            //    .Type
            //    .PropertyOrMethod(i => i.MyProp).Returns(5)
            //    .Named("base1")
            //    .Create();

            //Assert.Equal(5, o1.MyProp);

            var o2 = Nullified.Of<ISetupTests>()
               .PropertyOrMethod(i => i.MyProp, 15)
                .Named("base2")
                .Create();
            Assert.Equal(15, o2.MyProp);

            //Assert.NotEqual(o1, o2);
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
