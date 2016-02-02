using System;
using System.Collections.Generic;

namespace Nullify.Factory
{
    interface ICreationScope
    {
        IList<Type> PinnedTypes { get; set; }
        void Attach(Type type);
        bool TryGet(Type interfaceType, string className, out Type returnType);
    }
}