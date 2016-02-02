using System;
using System.Linq;
using System.Collections.Generic;

namespace Nullify.Factory
{
    class CreationScope : IDisposable, ICreationScope
    {
        private readonly ITypeRegistry typeRegistry;
        public IList<Type> PinnedTypes { get; set; }

        public CreationScope(ITypeRegistry typeRegistry)
        {
            PinnedTypes = new List<Type>();
            this.typeRegistry = typeRegistry;
        }

        public void Dispose()
        {
            PinnedTypes.Clear();
        }

        public void Attach(Type type)
        {
            PinnedTypes.Add(type);
        }

        public bool TryGet(Type interfaceType, string className, out Type returnType)
        {
            returnType = null; ;

            //look in created types
            returnType = PinnedTypes.FirstOrDefault(t => t.GetInterfaces().Contains(interfaceType));
            if (returnType != null)
                return true;

            //else look up in registry
           return  typeRegistry.TryGetType(interfaceType, className, out returnType);
        }
    }
}
