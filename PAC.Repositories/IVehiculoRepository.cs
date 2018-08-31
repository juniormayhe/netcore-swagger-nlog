using System.Collections.Generic;
using System.Threading.Tasks;
using PAC.Entities;

namespace PAC.Repositories
{
    public interface IVehiculoRepository
    {
        Task<int> AddAsync(Vehiculo vehiculo);
        Task<int> UpdateAsync(Vehiculo vehiculo);
        Task<int> DeleteAsync(int id);
        Task<Vehiculo> GetAsync(int id);
        Task<KeyValuePair<int, IEnumerable<Vehiculo>>> GetAllPagedAsync(int? pageNumber = 1, int? rowsPerPage = 10);
    }
}