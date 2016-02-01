using Nullify.Tests.Interfaces;
using Xunit;

namespace Nullify.Tests
{
    public class NestedTests
    {
        [Fact]
        public void BasicCreate()
        {
            var l1 = Nullified.Of<INestFirstLevel>()
                .Create();

            Assert.NotNull(l1);
            Assert.NotNull(l1.Sub);
            Assert.NotNull(l1.Sub.Sub);

            var l2 = Nullified.Of<INestFirstLevel>()
                .Create();
            Assert.NotNull(l2);
            Assert.NotNull(l2.Sub);
            Assert.NotNull(l2.Sub.Sub);
        }
    }
}
