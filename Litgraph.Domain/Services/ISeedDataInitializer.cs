using System.Threading.Tasks;

namespace Litgraph.Domain.Services
{
    public interface ISeedDataInitializer
    {
        Task Initialize();
    }
}