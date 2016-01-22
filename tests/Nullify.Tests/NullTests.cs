using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void WhenNullOfSameInterface_ShouldCreate2InstancesOfDiffClass()
        {
            var o1 = Null.Of<INullTests>().Create();
            var o2 = Null.Of<INullTests>().Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenNullOfSameInterfaceAndOneisNamed_ShouldCreate2InstancesOftheDiffClass()
        {
            var o1 = Null.Of<INullTests>().Create();
            var o2 = Null.Of<INullTests>().Named("another").Create();

            Assert.NotEqual(o1.GetType(), o2.GetType());
        }

        [Fact]
        public void WhenSameName_ShouldCreate2InstancesOftheSameClass()
        {
            var o1 = Null.Of<INullTests>().Named("another").Create();
            var o2 = Null.Of<INullTests>().Named("another").Create();

            Assert.Equal(o1.GetType(), o2.GetType());
        }
    }
}
