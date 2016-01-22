using System;
using System.Collections.Generic;

namespace Nullify.Configuration
{
    internal static class SetupRepository<T> where T : class
    {
        private static Dictionary<string, IFluentTypeSetup<T>> setups = new Dictionary<string, IFluentTypeSetup<T>>();

        public static void Register(string name, IFluentTypeSetup<T> setup)
        {
            if (!setups.ContainsKey(name))
                setups.Add(name, setup);
        }

        public static IFluentTypeSetup<T> Get(string name)
        {
            IFluentTypeSetup<T> setup;
            if (!setups.TryGetValue(name, out setup))
                setup = new FluentTypeSetup<T>();
            return setup;
        }
    }
}
