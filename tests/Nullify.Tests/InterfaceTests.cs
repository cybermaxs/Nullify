using Nullify.Tests.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace Nullify.Tests
{
    public class InterfacesTests
    {
        [Fact]
        public void IBasicProperties_ShouldNotBeNullAndThrowException()
        {
            var p = Null.Of<IBasicProperties>().Create();
            Assert.NotNull(p);

            var ex = Record.Exception(() =>
            {
                p.GetType().GetProperties().ToList().ForEach(a =>
                {
                    if (a.CanRead)
                        a.GetValue(p);
                    if (a.CanWrite)
                        a.SetValue(p, 1);
                });
            });
            Assert.Null(ex);
        }

        [Fact]
        public void IBasicEvents_ShouldNotBeNullAndThrowException()
        {
            var e = Null.Of<IBasicEvents>().Create();
            Assert.NotNull(e);

            var ex = Record.Exception(() =>
            {
                e.GetType().GetEvents().ToList().ForEach(a =>
                {
                    //TODO : attach dynamic delegate
                    //a.AddEventHandler(e, this.E_MyEventOfString);
                });
            });
            Assert.Null(ex);
        }

        [Fact]
        public void IBasicMethods_ShouldNotBeNullAndThrowException()
        {
            var m = Null.Of<IBasicMethods>().Create();
            Assert.NotNull(m);

            var ex = Record.Exception(() =>
            {
                m.GetType().GetMethods().ToList().ForEach(a =>
                {
                    var parameters = a.GetParameters();
                    if (parameters.Length == 0)
                        a.Invoke(m, null);
                    else
                    {
                        a.Invoke(m, parameters.Select(p => p.DefaultValue).ToArray());
                    }
                });
            });
            Assert.Null(ex);
        }

    }
}
