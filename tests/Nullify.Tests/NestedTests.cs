using Nullify.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nullify.tests
{
    public class NestedTests
    {
        [Fact]
        public void Test()
        {
            var l1 = Null.Of<INestFirstLevel>().Create();

            Assert.NotNull(l1);
            Assert.NotNull(l1.Sub);
            Assert.NotNull(l1.Sub.Sub);
        }
    }
}
