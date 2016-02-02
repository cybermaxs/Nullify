using Nullify.Tests.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace Nullify.Tests
{
    public interface IReturnValues
    {
        DateTime DateTime { get; }
        long Long { get; }
        string String { get; }
        object Object { get; }
        TimeSpan TimeSpan { get; }

    }
    public class ReturnsTests
    {
        [Fact]
        public void IReturnValuess_ShouldNotBeNullAndThrowException()
        {

            var n = Nullified
                .Of<IReturnValues>()
                .Named(DateTime.UtcNow.Ticks.ToString())
                .PropertyOrMethod(x => x.DateTime, DateTime.MaxValue)
                .PropertyOrMethod(x => x.Long, 5L)
                .PropertyOrMethod(x => x.String, "foo")
                //.PropertyOrMethod(x => x.Object, new ReturnsTests())
                .Create();
            Assert.NotNull(n);

           
            Assert.Equal(5, n.Long);
            Assert.Equal(DateTime.MaxValue, n.DateTime);
            Assert.Equal("foo", n.String);
            //Assert.NotNull(n.Object);
            //Assert.IsType<ReturnsTests>(n.Object);
        }

     

    }
}
