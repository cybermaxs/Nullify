using System;

namespace Nullify.Tests.Interfaces
{
    public interface IBasicEvents
    {
        event EventHandler MyEvent;
        event EventHandler<string> MyEventOfString;
    }
}
