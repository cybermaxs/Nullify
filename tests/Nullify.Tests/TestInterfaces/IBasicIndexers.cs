using System;

namespace Nullify.Tests.Interfaces
{
    public interface IBasicIndexers
    {

        int this[int index] { get; }
        int GetSet { get; set; }
    }
}
