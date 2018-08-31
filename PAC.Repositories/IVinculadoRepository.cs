using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PAC.Entities;

namespace PAC.Repositories
{
    public interface IVinculadoRepository
    {
        Task<Vinculado> GetAsync(long id);
        Task<IEnumerable<Vinculado>> GetAllAsync();
        Task<int> AddAsync(Vinculado vinculado);
    }
}