using System;

namespace Nullify.Configuration
{
    internal interface IPolicyRepository
    {
        CreationPolicy Get(Type type, string name);
        void Register(CreationPolicy policy);
    }
}