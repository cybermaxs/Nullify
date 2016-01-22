using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nullify.Configuration
{
    internal class CreationPolicy
    {
        public string Name { get; set; } = "default";
        public Type Target { get; private set; }

        public string FullName
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("NullOf");
                builder.Append(Target.Name);
                builder.Append("Named");
                builder.Append(Name);
                return builder.ToString();
            }
        }

        public Dictionary<MemberInfo, object> ReturnValues { get; private set; }

        public CreationPolicy(Type target)
        {
            Target = target;
            ReturnValues = new Dictionary<MemberInfo, object>();
            this.Name = DateTime.UtcNow.Ticks.ToString();
        }


    }
}
