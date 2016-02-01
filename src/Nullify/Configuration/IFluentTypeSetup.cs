using System;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    /// <summary>
    /// Fluent API to configure the 'nullification' of T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFluentTypeSetup<T> where T : class
    {
        /// <summary>
        /// Indicate if it is possible to create a null of T.
        /// </summary>
        /// <returns></returns>
        bool CanCreate();
        /// <summary>
        /// Create a new nullified version of T.
        /// </summary>
        /// <returns>Nullified version of T.</returns>
        T Create();
        /// <summary>
        /// Name this setup. Could be reused later.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Instance of IFluentTypeSetup<T></returns>
        IFluentTypeSetup<T> Named(string name);
        /// <summary>
        /// Set this configuariton as the default config for all null of T. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Instance of IFluentTypeSetup<T></returns>
        IFluentTypeSetup<T> SetAsDefault();
        /// <summary>
        /// Configure a dependency of T.
        /// </summary>
        /// <typeparam name="TNested"></typeparam>
        /// <param name="setup"></param>
        /// <returns>Instance of IFluentTypeSetup<</returns>
        IFluentTypeSetup<T> Dependency<TNested>(Action<IMemberSetup<TNested>> setup) where TNested : class;
        /// <summary>
        /// Configure the return value of a Getter/Function for T.
        /// </summary>
        /// <typeparam name="TProperty">Type of return type.</typeparam>
        /// <param name="expression">Expression for Getter/function.</param>
        /// <param name="returns">Expected return value.</param>
        /// <returns>Instance of IFluentTypeSetup<</returns>
        IFluentTypeSetup<T> PropertyOrMethod<TProperty>(Expression<Func<T, TProperty>> expression, TProperty returns);
    }
}
