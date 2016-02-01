using Nullify.Tests.Interfaces;
using Xunit;

namespace Nullify.Tests
{
    public class ReturnsTests
    {
        [Fact]
        public void IBasicProperties_ShouldNotBeNullAndThrowException()
        {
            var n = Nullified
                .Of<IBasicProperties>()
                .PropertyOrMethod(x => x.Int, 5)
                .Create();
            Assert.NotNull(n);

           
            Assert.Equal(5, n.Int);
        }

     

    }
}
