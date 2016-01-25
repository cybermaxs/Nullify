using Nullify.Utils;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Nullify.Configuration
{
    class FluentTypeSetup<T> : IFluentTypeSetup<T> where T : class
    {
        public CreationPolicy Policy { get; private set; }
        
        public FluentTypeSetup()
        {
            Policy = new CreationPolicy(typeof(T));
        }
        public bool CanCreate()
        {
            if (!typeof(T).IsInterface)
            {
                return false;
            }
            if (!typeof(T).IsPublic)
            {
                return false;
            }

            var walker = new DependencyWalker(typeof(T));
            walker.Walk();
            if(walker.IsCircular)
            {
                return false;
            }
            return true;
        }

        public T Create()
        {
            if (!CanCreate())
                return default(T);//null in this case :(

            Type targetType;

            if (!TypeRegistry.TryGetType<T>(Policy.FullName, out targetType))
            {
                //create a new type
                var factory = new TypeFactory(Policy);
                targetType = factory.Create();
            }

            //instanciate type
            var instance = Activator.CreateInstance(targetType) as T;

            return instance;
        }

        public IMemberSetup<T, TProperty> For<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return new MemberSetup<T, TProperty>(this, expression);
        }

        //public IFluentTypeSetup<T> Returns<TProperty>(Expression<Func<T, TProperty>> expression, TProperty initialValue)
        //{
        //    if (expression == null)
        //        throw new ArgumentNullException(nameof(expression));

        //    var pinfo = TypeUtils.GetMemberInfo(expression);

        //    Policy.ReturnValues[pinfo] = initialValue;
        //    Debug.WriteLine($"{pinfo.Name} of type {pinfo.Name} has value '{initialValue}'");
        //    return this;
        //}

        public IFluentTypeSetup<T> Named(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Policy.Name = name;
            return this;
        }
    }
}
