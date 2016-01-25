using Nullify.Configuration;
using Nullify.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Nullify
{
    internal class TypeFactory
    {
        private readonly CreationPolicy policy;

        public TypeFactory(CreationPolicy policy)
        {
            this.policy = policy;
        }

        public Type Create()
        {
            var typeBuilder = TypeRegistry.CreateTypeBuilder(policy.Target, policy.FullName);

            var types = new List<Type>();
            types.Add(policy.Target);
            types.AddRange(policy.Target.GetInterfaces());

            foreach (var type in types)
            {
                FillMethods(typeBuilder, type);
                FillEvents(typeBuilder, type);
                FillProperties(typeBuilder, type);
            }

            var newtype = typeBuilder.CreateType();
            return newtype;
        }

        private void FillProperties(TypeBuilder typeBuilder, Type nullifiedType)
        {
            var properties = nullifiedType.GetProperties();

            foreach (var property in properties)
            {
                var indexParameters = property.GetIndexParameters().Select(p => p.ParameterType).ToArray();
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, property.Attributes, property.PropertyType, indexParameters);

                if (property.CanRead)
                {
                    object returns;
                    policy.ReturnValues.TryGetValue(property, out returns);
                    // Generate getter method
                    var getter = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot, property.PropertyType, indexParameters);
                    var il = getter.GetILGenerator();
                    il.Emit(OpCodes.Nop);
                    il.DeclareLocal(property.PropertyType);
                    if (returns != null)
                        il.EmitConstant(returns);
                    else
                        il.EmitDefault(property.PropertyType);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ret);
                    propertyBuilder.SetGetMethod(getter);
                }

                if (property.CanWrite)
                {
                    var types = new List<Type>();
                    if (indexParameters.Length > 0)
                        types.AddRange(indexParameters);
                    types.Add(property.PropertyType);
                    var setter = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot, null, types.ToArray());
                    var il = setter.GetILGenerator();

                    il.Emit(OpCodes.Nop);        // Push "this" on the stack
                    il.Emit(OpCodes.Ret);        // Push "value" on the stack

                    propertyBuilder.SetSetMethod(setter);
                }
            }
        }

        private void FillEvents(TypeBuilder typeBuilder, Type nullifiedType)
        {
            var events = nullifiedType.GetEvents();

            foreach (var evt in events)
            {
                // Event methods attributes
                var eventMethodAttr = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.SpecialName;
                var eventMethodImpAtr = MethodImplAttributes.Managed;

                var qualifiedEventName = evt.Name;
                var addMethodName = string.Format("add_{0}", evt.Name);
                var remMethodName = string.Format("remove_{0}", evt.Name);

                //FieldBuilder eFieldBuilder = typeBuilder.DefineField(qualifiedEventName, evt.EventHandlerType, FieldAttributes.Public);

                var eBuilder = typeBuilder.DefineEvent(qualifiedEventName, EventAttributes.None, evt.EventHandlerType);

                // ADD method
                var addMethodBuilder = typeBuilder.DefineMethod(addMethodName, eventMethodAttr, null, new Type[] { evt.EventHandlerType });

                addMethodBuilder.SetImplementationFlags(eventMethodImpAtr);

                // Code generation
                var ilgen = addMethodBuilder.GetILGenerator();
                ilgen.Emit(OpCodes.Ret);

                // REMOVE method
                var removeMethodBuilder = typeBuilder.DefineMethod(remMethodName, eventMethodAttr, null, new Type[] { evt.EventHandlerType });
                removeMethodBuilder.SetImplementationFlags(eventMethodImpAtr);

                var removeInfo = typeof(Delegate).GetMethod("Remove", new Type[] { typeof(Delegate), typeof(Delegate) });

                // Code generation
                ilgen = removeMethodBuilder.GetILGenerator();
                ilgen.Emit(OpCodes.Ret);

                // Finally, setting the AddOn and RemoveOn methods for our event
                eBuilder.SetAddOnMethod(addMethodBuilder);
                eBuilder.SetRemoveOnMethod(removeMethodBuilder);

                // Implement the method from the interface
                typeBuilder.DefineMethodOverride(addMethodBuilder, evt.GetAddMethod(false));

                // Implement the method from the interface
                typeBuilder.DefineMethodOverride(removeMethodBuilder, evt.GetRemoveMethod(false));
            }
        }

        private void FillMethods(TypeBuilder typeBuilder, Type nullifiedType)
        {
            var methods = nullifiedType.GetMethods();

            foreach (var method in methods)
            {
                if (method.IsSpecialName)
                    continue;//properties/events,indexers

                var methodBuilder = typeBuilder.DefineMethod(
                    method.Name,
                    MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot,
                             method.ReturnType ?? typeof(void),
                             method.GetParameters().Select(p => p.ParameterType).ToArray()
                             );

                typeBuilder.DefineMethodOverride(methodBuilder, method);

                var il = methodBuilder.GetILGenerator();
                il.Emit(OpCodes.Nop);

                if (method.ReturnType != typeof(void))
                {
                    il.DeclareLocal(method.ReturnType);

                    object returns;
                    policy.ReturnValues.TryGetValue(method, out returns);
                    if (returns != null)
                        il.EmitConstant(returns);
                    else
                        il.EmitDefault(method.ReturnType);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Ldloc_0);
                }

                il.Emit(OpCodes.Ret);
            }
        }

        public bool CanCreate()
        {
            return true;
        }
    }
}
