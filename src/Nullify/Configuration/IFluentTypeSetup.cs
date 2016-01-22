using System;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    /// <summary>
    /// Define basic methods to create a new nullified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFluentTypeSetup<T> where T : class
    {
        IMemberSetup<T, TProperty> For<TProperty>(Expression<Func<T, TProperty>> expression);
        IFluentTypeSetup<T> Named(string name);
        bool CanCreate();
        T Create();
    }
}
