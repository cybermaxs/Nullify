using System;

namespace Nullify.Tests.Interfaces
{
    public interface IBasicProperties
    {
        DateTime DateTime { get; }
        int Int { get; }
        long Long { set; }
        decimal Decicmal { get; }
        double Double { get; }
        string String { get; }
        bool Bool { get; }
        int GetSet { get; set; }
    }
}
