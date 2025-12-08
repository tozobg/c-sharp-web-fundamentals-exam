using System.Threading.Tasks;

namespace TransactionSystem.Core.Interfaces
{
    public interface IEngine
    {
        Task Run();
    }
}