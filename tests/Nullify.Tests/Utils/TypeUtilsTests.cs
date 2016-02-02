using Nullify.Tests.Interfaces;
using Nullify.Utils;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Nullify.Tests.Utils
{
    public class TypeUtilsTests
    {
        [Fact]
        public void GetMemberInfo_WhenGetter()
        {
            var memberInfo = TypeUtils.GetMemberInfo<IList<string>, int>(l => l.Count);

            Assert.NotNull(memberInfo);
            Assert.Equal( MemberTypes.Property, memberInfo.MemberType);
            Assert.Equal("Count", memberInfo.Name);
        }

        [Fact]
        public void GetMemberInfo_WhenFunc()
        {
            var memberInfo = TypeUtils.GetMemberInfo<IList<string>, bool>(l => l.Contains(""));

            Assert.NotNull(memberInfo);
            Assert.Equal(MemberTypes.Method, memberInfo.MemberType);
            Assert.Equal("Contains", memberInfo.Name);
        }
    }
}
