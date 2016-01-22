using System.Threading.Tasks;

namespace Nullify.Tests.Interfaces
{
    public interface IBasicMethods
    {
        void EmptyMethod();
        Task<int> ReturnTask();
        int WithParameters(int a, int b);
    }
}
