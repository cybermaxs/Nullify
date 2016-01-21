Warning, this is a work in progress.

# Nullify
Lightweight NullObject/SpecialCase implementation

# Motivations
> Nulls are awkward things in object-oriented programs because they defeat polymorphism, Martin Fowler

*add more motivations*

Suppose you have an interface defined like this
```
public interface IDataService
{
    bool IsConnected { get; }
    Task SendDataAsync();
    Task<int> PingAsync();
    //... methods
}
```
You may need to create a disconnected implementation used when the client is disconnected, and when the server is unresponsive.
```
 var disconnected = Null.Of<IDataService>()
                        .Returns(x => x.IsConnected, false)
                        .Create();
// disconnected.IsConnected == false
```
# Benefits
- Less code to write for Null Objects
- Easier to maintain (when the parent interface is altered)
- Less null-checks (if myobject!=null)
- No dynamic type-checking (the L of SOLID aka Liskov substitution principle)

# Quick start
*TODO*

# Nullify is *not*
- a new mocking framework
- a new library to build proxy classes 
- somehting that will avoid NullReferenceException
- ...

But it has some advantages, and may help you _sometimes_

