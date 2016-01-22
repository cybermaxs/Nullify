using Nullify.Configuration;
using System;

namespace Nullify
{
    public static class Null
    {
        /// <summary>
        /// DefaultEntry point. All to create a new nullified type dynamically.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IFluentTypeSetup<T> Of<T>() where T : class
        {
            return new FluentTypeSetup<T>();
        }
        ///// <summary>
        ///// Register a new type setup. Will be used for every new nullify of T.
        ///// </summary>
        ///// <param name="setupCode">todo</param>
        ///// <typeparam name="T">Target type</typeparam>
        //public static void RegisterSetup<T>(Action<IFluentTypeSetup<T>> setupCode) where T : class
        //{
        //    var setup = new FluentTypeSetup<T>();
        //    setupCode?.Invoke(setup);
        //    if (string.IsNullOrEmpty(setup.Policy.Name))
        //        throw new InvalidOperationException("A name is required when");
        //    SetupRepository<T>.Register(setup);
        //}
    }
}
