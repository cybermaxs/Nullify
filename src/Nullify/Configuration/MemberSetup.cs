using Nullify.Utils;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    class MemberSetup<T, TProperty> : IMemberSetup<T,TProperty> where T : class
    {
        private readonly FluentTypeSetup<T> typeSetup;
        private readonly Expression<Func<T, TProperty>> expression;

        public MemberSetup(FluentTypeSetup<T> typeSetup, Expression<Func<T, TProperty>> expression)
        {
            this.expression = expression;
            this.typeSetup = typeSetup;
        }

        public IFluentTypeSetup<T> Returns(TProperty propertyValue)
        {
            var pinfo = TypeUtils.GetMemberInfo(expression);

            typeSetup.Policy.ReturnValues[pinfo] = propertyValue;
            Debug.WriteLine($"{pinfo.Name} of type {pinfo.Name} has value '{propertyValue}'");

            return typeSetup;
        }
    }
}
