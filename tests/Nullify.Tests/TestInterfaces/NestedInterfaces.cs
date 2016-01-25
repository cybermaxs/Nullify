namespace Nullify.Tests.Interfaces
{
    #region Nested deps
    public interface INestFirstLevel
    {
        INestSecondLevel Sub { get; }
    }

    public interface INestSecondLevel
    {
        INestThirdLevel Sub { get; }
    }

    public interface INestThirdLevel
    {

    }
    #endregion

    #region Simple Circular
    public interface ISimpleCircularFirstLevel
    {
        ISimpleCircularSecondLevel Sub { get; }
    }

    public interface ISimpleCircularSecondLevel
    {
        ISimpleCircularFirstLevel Sub { get; }
    }
    #endregion

    #region Complex Circular
    public interface IComplexCircularFirstLevel
    {
        IComplexCircularSecondLevel Sub { get; }
    }

    public interface IComplexCircularSecondLevel
    {
        IComplexCircularThirdLevel Sub { get; }
    }

    public interface IComplexCircularThirdLevel
    {
        IComplexCircularFirstLevel Sub { get; }
    }
    #endregion

    #region Mix Circular
    public interface IMixedCircularFirstLevel
    {
        IMixedCircularSecondLevel Sub { get; }
    }

    public interface IMixedCircularSecondLevel
    {
        IMixedCircularThirdLevel Sub { get; }
    }

    public interface IMixedCircularThirdLevel
    {
        ISimpleCircularFirstLevel Sub { get; }
    }
    #endregion
}
