using Nullify.Factory;
using Nullify.Utils;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace Nullify.Configuration
{
    class FluentTypeSetup<T> : IFluentTypeSetup<T> where T : class
    {
        private readonly IPolicyRepository policyRepository;
        private NullifiedBuilder Builder;

        public CreationPolicy Policy { get; set; }

        public FluentTypeSetup(IPolicyRepository policyRepository)
        {
            Policy = new CreationPolicy(typeof(T));
            this.policyRepository = policyRepository;
            Builder = new NullifiedBuilder(policyRepository, TypeRegistry.Current);
        }
        public bool CanCreate()
        {
            //should be interface
            if (!typeof(T).IsInterface)
            {
                return false;
            }

            //should be publi
            if (!typeof(T).IsPublic)
            {
                return false;
            }

            //should not have circular dependencies
            var deps = DependencyStack.Enumerate(typeof(T));
            if (deps.IsCircular)
            {
                return false;
            }
            return true;
        }

        public T Create()
        {
            if (!CanCreate())
                return default(T);  //null in this case :(

            //register if name was set
            if (!string.IsNullOrEmpty(Policy.Name))
                policyRepository.Register(Policy);

            var nullified = Builder.GetOrBuild(Policy);

            //instanciate type
            var instance = Activator.CreateInstance(nullified) as T;

            return instance;
        }

        public IFluentTypeSetup<T> PropertyOrMethod<TProperty>(Expression<Func<T, TProperty>> expression, TProperty returns)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var pinfo = TypeUtils.GetMemberInfo(expression);

            Policy.ReturnValues[pinfo] = returns;

            return this;
        }

        public IFluentTypeSetup<T> SetAsDefault()
        {
            Policy.Name = Constants.DefaultPolicyName;
            return this;
        }

        public IFluentTypeSetup<T> Named(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Policy.Name = name;
            return this;
        }

        public IFluentTypeSetup<T> Dependency<TNested>(Action<IMemberSetup<TNested>> setup) where TNested : class
        {
            if (Policy.NestedPolicies.FirstOrDefault(t => t.Target == typeof(TNested)) != null)
                return this;

            var childPolicy = new CreationPolicy(typeof(TNested));
            Policy.NestedPolicies.Add(childPolicy);

            var msetup = new MemberSetup<TNested>(childPolicy);
            setup?.Invoke(msetup);
            return this;
        }
    }
}
