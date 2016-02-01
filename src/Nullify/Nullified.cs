using Nullify.Configuration;
using System;

namespace Nullify
{
    /// <summary>
    /// Nullify entry point.
    /// </summary>
    public static class Nullified
    {
        private static IPolicyRepository policyRepository;

        static Nullified()
        {
            policyRepository = new PolicyRepository();
        }

        /// <summary>
        /// DefaultEntry point. Allow to create a new nullified type dynamically.
        /// </summary>
        /// <typeparam name="TInterface">Inteface to implement</typeparam>
        /// <returns>Nullified Type of T.</returns>
        public static IFluentTypeSetup<TInterface> Of<TInterface>() where TInterface : class
        {
            return new FluentTypeSetup<TInterface>(policyRepository);
        }

        public static IFluentTypeSetup<TInterface> Of<TInterface>(string name) where TInterface : class
        {
            var policy = policyRepository.Get(typeof(TInterface), name);

            if (policy == null)
                throw new InvalidOperationException($"Nothing was registered as '{name}' for Type '{typeof(TInterface)}'");

            var setup = new FluentTypeSetup<TInterface>(policyRepository)
            {
                Policy = policy
            };
            return setup;
        }
    }
}
