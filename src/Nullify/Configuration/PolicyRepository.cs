using System;
using System.Collections.Generic;
using System.Linq;

namespace Nullify.Configuration
{
    internal class PolicyRepository : IPolicyRepository
    {
        private Dictionary<string, CreationPolicy> allPolicies = new Dictionary<string, CreationPolicy>();

        public CreationPolicy Get(Type type, string name = "default")
        {
            var key = ComputeKeyName(type, name);

            //try to get named policy first
            CreationPolicy policy;
            if (!allPolicies.TryGetValue(key, out policy))
            {
                //try to get default one
                policy = allPolicies.Values.FirstOrDefault(p => p.Target == type);
                if (policy == null)
                    policy = new CreationPolicy(type);
            }

            //try to get 
            return policy;
        }

        public void Register(CreationPolicy policy)
        {
            var key = ComputeKeyName(policy.Target, policy.Name);
            if (!allPolicies.ContainsKey(key))
                allPolicies.Add(key, policy);
        }

        private static string ComputeKeyName(Type targetType, string policyName) => targetType.Name + "_" + policyName;
    }
}
