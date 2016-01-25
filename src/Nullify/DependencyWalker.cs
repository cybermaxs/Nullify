using System;
using System.Collections.Generic;
using System.Linq;

namespace Nullify
{
    /// <summary>
    /// Hold a dependency stack of Ref type for a given Type.
    /// </summary>
    internal class DependencyWalker
    {
        private readonly Type parentType;
        private readonly Stack<Type> stack = new Stack<Type>();

        public bool IsCircular { get; private set; }
        public IEnumerable<Type> Children { get { return stack.AsEnumerable(); } }
        public bool HasChildren { get { return stack.Count > 0; } }

        public DependencyWalker(Type parentType)
        {
            this.parentType = parentType;
        }

        public void Walk()
        {
            SubWalk(parentType);
        }

        private void SubWalk(Type type)
        {
            if (IsCircular)
                return; // early return

            var flattenChildrenTypes = new HashSet<Type>();

            //get all nested properties (get only)
            type
                .GetProperties()
                .Where(p => p.CanRead && !p.PropertyType.IsValueType)
                .Select(p => p.PropertyType)
                .ToList()
                .ForEach(t => flattenChildrenTypes.Add(t));

            //get all nested methods (that returns somehting)
            type
                .GetMethods()
                .Where(m => !m.ReturnType.IsValueType && m.ReturnType != typeof(void))
                .Select(m => m.ReturnType)
                .ToList()
                .ForEach(t => flattenChildrenTypes.Add(t));

            if (!HasCircularDependency(flattenChildrenTypes))
            {
                //add to stack
                flattenChildrenTypes.ToList().ForEach(t => stack.Push(t));
                foreach (var t in flattenChildrenTypes)
                    SubWalk(t);
            }
            else
            {
                stack.Clear();
                IsCircular = true;
            }
        }

        private bool HasCircularDependency(IEnumerable<Type> types)
        {
            //the dependency is not parent
            foreach (var child in types)
            {
                if (child == parentType)
                    return true;
                if (stack.Contains(child))
                    return true;
            }

            return false;
        }
    }
}
