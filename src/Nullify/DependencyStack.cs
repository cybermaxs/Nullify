using System;
using System.Collections.Generic;
using System.Linq;

namespace Nullify
{
    /// <summary>
    /// Hold a dependency stack of Ref type for a given Type.
    /// </summary>
    internal class DependencyStack
    {
        private readonly Type parentType;
        private readonly Stack<Type> stack = new Stack<Type>();

        public bool IsCircular { get; private set; }
        public IEnumerable<Type> Children { get { return stack.AsEnumerable(); } }
        public bool HasChildren { get { return stack.Count > 0; } }

        public DependencyStack(Type parentType)
        {
            this.parentType = parentType;
        }

        public void Walk()
        {
            Walk(parentType);
        }

        private void Walk(Type type)
        {
            if (IsCircular)
                return; // realy abort

            var flattenChildrenTypes = new List<Type>();

            //get all nested properties
            var propertyTypes = type.GetProperties().Where(p => p.CanRead && !p.PropertyType.IsValueType).Select(p => p.PropertyType);
            flattenChildrenTypes.AddRange(propertyTypes);

            //get all nested methods
            var methodTypes = type.GetMethods().Where(m => !m.ReturnType.IsValueType).Select(m => m.ReturnType);
            flattenChildrenTypes.AddRange(methodTypes);

            if (CanAdd(flattenChildrenTypes))
            {
                //add to stack
                flattenChildrenTypes.ForEach(t => stack.Push(t));
                foreach (var t in flattenChildrenTypes)
                    Walk(t);
            }
            else
                this.IsCircular = true;
        }

        private bool CanAdd(IEnumerable<Type> types)
        {
            foreach (var t in types)
            {
                if (stack.Contains(t))
                    return false;
            }

            return true;
        }
    }
}
