using Nullify.Tests.Interfaces;
using Nullify.Utils;
using System.Linq;
using Xunit;

namespace Nullify.tests
{
    public class DependencyStackTests
    {
        [Fact]
        public void WhenHasNoCircular_ShouldBeOk()
        {
            var stack = DependencyStack.Enumerate(typeof(INestFirstLevel));

            Assert.False(stack.IsCircular);
            Assert.True(stack.Children.Count() > 0);
        }

        [Fact]
        public void WhenHasCircular_ShouldBeKo()
        {
            var stack = DependencyStack.Enumerate(typeof(IDirectDepFirstLevel));

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }

        [Fact]
        public void WhenHasComplexCircular_ShouldBeKo()
        {
            var stack = DependencyStack.Enumerate(typeof(ITwoLevelsDepsFirstLevel));

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }

        [Fact]
        public void WhenHasMixedCircular_ShouldBeKo()
        {
            var stack = DependencyStack.Enumerate(typeof(IComplexFirstLevel));

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }
    }
}
