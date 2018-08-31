using PAC.Business.Contracts;
using System.Threading.Tasks;

namespace PAC.Business.Contracts
{
    public interface IVinculadoBusiness
    {
        Task<long> AddAsync(VinculadoRequest VinculadoRequest);
        Task<VinculadoResponse> GetAllAsync();
        Task<VinculadoResponse> GetAsync(long id);
    }
}