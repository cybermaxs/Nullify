using Nullify.Tests.Interfaces;
using System.Linq;
using Xunit;

namespace Nullify.tests
{
    public class DependencyTests
    {
        [Fact]
        public void WhenHasNoCircular_ShouldBeOk()
        {
            var stack = new DependencyWalker(typeof(INestFirstLevel));
            stack.Walk();

            Assert.False(stack.IsCircular);
            Assert.True(stack.Children.Count() > 0);
        }

        [Fact]
        public void WhenHasCircular_ShouldBeKo()
        {
            var stack = new DependencyWalker(typeof(ISimpleCircularFirstLevel));
            stack.Walk();

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }

        [Fact]
        public void WhenHasComplexCircular_ShouldBeKo()
        {
            var stack = new DependencyWalker(typeof(IComplexCircularFirstLevel));
            stack.Walk();

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }

        [Fact]
        public void WhenHasMixedCircular_ShouldBeKo()
        {
            var stack = new DependencyWalker(typeof(IMixedCircularFirstLevel));
            stack.Walk();

            Assert.True(stack.IsCircular);
            Assert.Equal(0, stack.Children.Count());
        }
    }
}
