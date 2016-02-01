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
            var o = Nullified.Of<ICloneable>().Create();
            Assert.NotNull(o);
            Assert.NotNull(o.Clone());
        }

        [Fact]
        public void ICollectionTest()
        {
            var o = Nullified.Of<ICollection<string>>().Create();
            Assert.NotNull(o);
            Assert.Equal(0, o.Count);
            Assert.Equal(false, o.IsReadOnly);
            Assert.Equal(false, o.Remove("trre"));
            Assert.Equal(false, o.Contains("rzeer"));
        }

        [Fact]
        public void IListTest()
        {
            var o = Nullified.Of<IList<int>>().Create();
            Assert.NotNull(o);
            Assert.Equal(0, o[123456]);
            Assert.Equal(0, o.IndexOf(12));
        }

        [Fact]
        public void IComparerTest()
        {
            var o = Nullified.Of<IComparer<int>>().Create();
            Assert.NotNull(o);
            Assert.Equal(0, o.Compare(1, 2));
        }


        [Fact]
        public void IDictionaryTest()
        {
            var o = Nullified.Of<IDictionary<int, int>>().Create();
            Assert.NotNull(o);
            Assert.NotNull(o.Keys);
            Assert.Equal(0, o.Keys.Count);
            Assert.NotNull(o.Values);
            Assert.Equal(0, o.Values.Count);
        }
    }
}
