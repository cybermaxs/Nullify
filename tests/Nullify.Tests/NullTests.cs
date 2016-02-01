using Nullify.Tests.Interfaces;
using Xunit;

namespace Nullify.Tests
{
    public interface INullTests
    {
        int MyProp { get; set; }
    }
    public class NullTests
    {
        [Fact]
        public void Simple_Create()
        {
            //basic
            var o = Nullified.Of<IBasicProperties>().Create();

            Assert.NotNull(o);
            Assert.Contains(typeof(IBasicProperties), o.GetType().GetInterfaces());
        }

        [Fact]
        public void WhenNullOfSameInterface_ShouldCreate2InstancesOfDiffClass()
        {
            var o1 = Nullified.Of<INullTests>()
                    .PropertyOrMethod(x => x.MyProp,1)
                    .PropertyOrMethod(x => x.MyProp,2)
                .Create();
            var o2 = Nullified.Of<INullTests>().Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenNullOfSameInterfaceAndOneisNamed_ShouldCreate2InstancesOftheDiffClass()
        {
            var o1 = Nullified.Of<INullTests>().Create();
            var o2 = Nullified.Of<INullTests>().Named("another").Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenSameName_ShouldCreate2InstancesOftheSameClass()
        {
            var o1 = Nullified.Of<INullTests>().Named("another").Create();
            var o2 = Nullified.Of<INullTests>().Named("another").Create();

            Assert.Equal(o1.GetType(), o2.GetType());
        }
    }
}
