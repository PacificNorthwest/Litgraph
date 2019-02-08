using System.Threading.Tasks;

namespace Litgraph.IdentityServer.Services.Contracts
{
    public interface ISeedDataInitializer
    {
        Task Initialize();
    }
}