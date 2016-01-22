namespace Nullify.Configuration
{
    public interface IMemberSetup<T, TProperty> where T : class
    {
        IFluentTypeSetup<T> Returns(TProperty property); 
    }
}
