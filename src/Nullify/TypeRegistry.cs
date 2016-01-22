using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Nullify
{
    internal class TypeRegistry
    {
        private static readonly AssemblyBuilder assembly;
        private static readonly ModuleBuilder module;
        private static readonly object syncLock = new object();

        static TypeRegistry()
        {
            assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("Nulls"), AssemblyBuilderAccess.RunAndSave);
            module = assembly.DefineDynamicModule("MyNullModule", Constants.DynamicAssemblyName);
        }

        public static bool TryGetType<T>(string typeName, out Type type)
        {
            lock (syncLock)
            {
                type = module.GetTypes().FirstOrDefault(t => t.Name == typeName && t.GetInterfaces().Contains(typeof(T)));

                return type != null;
            }
        }

        public static TypeBuilder CreateTypeBuilder(Type newType, string typeName)
        {
            lock (syncLock)
            {
                var typeBuilder = module.DefineType(typeName, TypeAttributes.Public, typeof(object), new Type[] { newType });
                return typeBuilder;
            }
        }

        public void Save()
        {
            assembly.Save(Constants.DynamicAssemblyName);
        }
    }
}
