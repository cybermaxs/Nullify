using Nullify.Utils;
using System;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    class MemberSetup<T> : IMemberSetup<T> where T : class
    {
        private readonly CreationPolicy policy;

        public MemberSetup(CreationPolicy policy)
        {
            this.policy = policy;
        }

        public IMemberSetup<T> PropertyOrMethod<TProperty>(Expression<Func<T, TProperty>> expression, TProperty returns)
        {
            var pinfo = TypeUtils.GetMemberInfo(expression);
            policy.ReturnValues[pinfo] = returns;
            return this;
        }
    }
}
