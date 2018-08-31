using System.Collections.Generic;
using System.Threading.Tasks;
using PAC.Business.Contracts;
using PAC.Entities;

namespace PAC.Business
{
    public interface IVehiculoBusiness
    {
        Task<int> AddAsync(VehiculoRequest vehiculoRequest);
        Task<int> UpdateAsync(VehiculoRequest vehiculoRequest);
        Task<List<Vehiculo>> GetAllAsync(int? pagedNumber);
        Task<Vehiculo> GetAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}