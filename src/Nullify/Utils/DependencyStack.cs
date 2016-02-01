using System;
using System.Collections.Generic;
using System.Linq;

namespace Nullify.Utils
{
    /// <summary>
    /// Hold a dependency stack of a given type.
    /// Children is a stack of all nested types.
    /// Warning : may be circular.
    /// </summary>
    internal class DependencyStack
    {
        private readonly Type rootType;
        private readonly Stack<Type> stack = new Stack<Type>();

        public bool IsCircular { get; private set; }
        public IEnumerable<Type> Children { get { return stack.AsEnumerable(); } }
        public bool HasChildren { get { return stack.Count > 0; } }

        private DependencyStack(Type root)
        {
            rootType = root;
        }

        public static DependencyStack Enumerate(Type parentType)
        {
            var stack = new DependencyStack(parentType);
            stack.Walk();
            return stack;
        }

        public void Walk()
        {
            SubWalk(rootType);
        }

        private void SubWalk(Type type)
        {
            if (IsCircular)
                return; // early return

            var flattenChildrenTypes = new HashSet<Type>();

            //get all nested properties (get only)
            type
                .GetProperties()
                .Where(p => p.CanRead && p.PropertyType.IsInterface)
                .Select(p => p.PropertyType)
                .ToList()
                .ForEach(t => flattenChildrenTypes.Add(t));

            //get all nested methods (that returns somehting)
            type
                .GetMethods()
                .Where(m => m.ReturnType.IsInterface)
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
                if (child == rootType)
                    return true;
                if (stack.Contains(child))
                    return true;
            }

            return false;
        }
    }
}
