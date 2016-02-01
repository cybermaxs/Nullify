using System;
using System.Reflection.Emit;

namespace Nullify.Factory
{
    internal interface ITypeRegistry
    {
        TypeBuilder CreateTypeBuilder(Type newType, string typeName);
        bool TryGetType<T>(string typeName, out Type type);
        bool TryGetType(Type baseInterfaceType, string typeName, out Type type);
    }
}