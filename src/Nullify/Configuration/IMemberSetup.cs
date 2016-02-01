using System;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    public interface IMemberSetup<T> where T : class
    {
        IMemberSetup<T> PropertyOrMethod<TProperty>(Expression<Func<T, TProperty>> expression, TProperty returns);
    }
}
