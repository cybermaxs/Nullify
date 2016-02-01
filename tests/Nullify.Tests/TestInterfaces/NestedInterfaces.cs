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

    #region Direct Dep
    public interface IDirectDepFirstLevel
    {
        IDirectDepSecondLevel Sub { get; }
    }

    public interface IDirectDepSecondLevel
    {
        IDirectDepFirstLevel Sub { get; }
    }
    #endregion

    #region TwoLevelsDeps
    public interface ITwoLevelsDepsFirstLevel
    {
        ITwoLevelsDepsSecondLevel Sub { get; }
    }

    public interface  ITwoLevelsDepsSecondLevel
    {
        ITwoLevelsDepsThirdLevel Sub { get; }
    }

    public interface ITwoLevelsDepsThirdLevel
    {
        ITwoLevelsDepsFirstLevel Sub { get; }
    }
    #endregion

    #region Complex Circular
    public interface IComplexFirstLevel
    {
        IComplexSecondLevel Sub { get; }
    }

    public interface IComplexSecondLevel
    {
        IComplexThirdLevel Sub { get; }
        IDirectDepFirstLevel SubDirect { get; }
    }

    public interface IComplexThirdLevel
    {
        IDirectDepFirstLevel SubDirect { get; }
    }
    #endregion
}
