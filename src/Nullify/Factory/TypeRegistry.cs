using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Nullify.Factory
{
    internal class TypeRegistry : ITypeRegistry
    {
        private readonly AssemblyBuilder assembly;
        private readonly ModuleBuilder module;
        private readonly object syncLock = new object();

        public static Lazy<TypeRegistry> lazyRegistry = new Lazy<TypeRegistry>(() => { return new TypeRegistry(); });
        public static ITypeRegistry Current { get { return lazyRegistry.Value; } }

        private TypeRegistry()
        {
            var ticks = DateTime.UtcNow.Ticks;
            assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("Nulls" + ticks), AssemblyBuilderAccess.RunAndSave);
            module = assembly.DefineDynamicModule("MyNullModule" + ticks, Constants.DynamicAssemblyName);
        }

        public TypeBuilder CreateTypeBuilder(Type newType, string className)
        {
            lock (syncLock)
            {
                var typeBuilder = module.DefineType(className, TypeAttributes.Public, typeof(object), new Type[] { newType });
                return typeBuilder;
            }
        }

        public bool TryGetType<T>(string className, out Type type)
        {
            lock (syncLock)
            {
                type = module.GetTypes().FirstOrDefault(t => t.Name == className && t.GetInterfaces().Contains(typeof(T)));

                return type != null;
            }
        }

        public bool TryGetType(Type baseInterfaceType, string className, out Type type)
        {
            lock (syncLock)
            {
                type = module.GetTypes().FirstOrDefault(t => t.Name == className && t.GetInterfaces().Contains(baseInterfaceType));
                return type != null;
            }
        }
    }
}
