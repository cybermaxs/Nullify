using Nullify.Tests.Interfaces;
using Xunit;

namespace Nullify.Tests
{
    public interface INullTests
    {
        int MyProp { get; set; }
    }

    public interface INullTests2
    {
        int MyProp { get; set; }
    }
    public interface INullTests3
    {
        int MyProp { get; set; }
    }
    public interface INullTests4
    {
        int MyProp { get; set; }
    }
    public class NullifiedTests
    {
        [Fact]
        public void Simple_Create()
        {
            //basic
            var o = Nullified.Of<INullTests>().Create();

            Assert.NotNull(o);
            Assert.Contains(typeof(INullTests), o.GetType().GetInterfaces());
        }

        [Fact]
        public void WhenNullOfSameInterface_ShouldCreate2InstancesOfDiffClass()
        {
            var o1 = Nullified.Of<INullTests2>()
                    .PropertyOrMethod(x => x.MyProp,1)
                    .PropertyOrMethod(x => x.MyProp,2)
                .Create();
            var o2 = Nullified.Of<INullTests2>().Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenNullOfSameInterfaceAndOneisNamed_ShouldCreate2InstancesOftheDiffClass()
        {
            var o1 = Nullified.Of<INullTests3>().Create();
            var o2 = Nullified.Of<INullTests3>().Named("another").Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenSameName_ShouldCreate2InstancesOftheSameClass()
        {
            var o1 = Nullified.Of<INullTests4>().Named("another").Create();
            var o2 = Nullified.Of<INullTests4>().Named("another").Create();

            Assert.Equal(o1.GetType(), o2.GetType());
        }
    }
}
