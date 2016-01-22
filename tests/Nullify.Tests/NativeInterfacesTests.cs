using System;
using System.Collections.Generic;
using Xunit;

namespace Nullify.Tests
{
    public class NativeInterfacesTests
    {

        [Fact]
        public void ICloneableTest()
        {
            var o = Null.Of<ICloneable>().Create();
            Assert.NotNull(o);
        }

        [Fact]
        public void ICollectionTest()
        {
            var o = Null.Of<ICollection<string>>().Create();
            Assert.NotNull(o);
        }

        [Fact]
        public void IListTest()
        {
            var o = Null.Of<IList<string>>().Create();
            Assert.NotNull(o);
        }
    }
}
