namespace Nullify.Tests.Interfaces
{
    public interface IFirstLevel
    {
        ISecondLevel Sub { get; }
    }

    public interface ISecondLevel
    {
        IThirdLevel Sub { get; }
    }

    public interface IThirdLevel
    {

    }
}
