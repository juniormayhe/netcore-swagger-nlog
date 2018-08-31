using PAC.Business.Contracts;
using PAC.Entities;
using PAC.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAC.Business
{
    public class VehiculoBusiness : IVehiculoBusiness
    {
        private readonly IVehiculoRepository _vehiculoRepo;

        public VehiculoBusiness(IVehiculoRepository vehiculoRepo)
        {
            _vehiculoRepo = vehiculoRepo;
        }

        public async Task<List<Vehiculo>> GetAllAsync(int? pageNumber)
        {
            var list = new List<Vehiculo>();
            int rowsPerPage = 5;
            KeyValuePair<int, IEnumerable<Vehiculo>> keypair;
            if (!pageNumber.HasValue)
                keypair = await _vehiculoRepo.GetAllPagedAsync(null, null);
            else { 
                keypair = await _vehiculoRepo.GetAllPagedAsync(pageNumber.Value, rowsPerPage);
                //vehiculoResponse.PagingSettings.TotalRecords = keypair.Key;
                //vehiculoResponse.PagingSettings.PageNumber = pageNumber.Value;
                //vehiculoResponse.PagingSettings.RowsPerPage = rowsPerPage;
                
            }

            if (keypair.Value.ToList().Count == 0)
            {
                //vehiculoResponse.Message = "Vehiculos not found.";
            }
            else
            {
                //vehiculoResponse.Vehiculos.AddRange(keypair.Value);
                list.AddRange(keypair.Value);
            }

            return list;
        }

        public async Task<int> AddAsync(VehiculoRequest vehiculoRequest)
        {
            var vehiculo = new Vehiculo
            {
                NumeroOrden = vehiculoRequest.NumeroOrden,
                Placa = vehiculoRequest.Placa
            };

            return await _vehiculoRepo.AddAsync(vehiculo);
        }


        public async Task<int> UpdateAsync(VehiculoRequest vehiculoRequest)
        {
            var vehiculo = new Vehiculo
            {
                IdVehiculo = vehiculoRequest.IdVehiculo,
                NumeroOrden = vehiculoRequest.NumeroOrden,
                Placa = vehiculoRequest.Placa
            };

            return await _vehiculoRepo.UpdateAsync(vehiculo);
        }


        public async Task<int> DeleteAsync(int idVehiculo)
        {
            return await _vehiculoRepo.DeleteAsync(idVehiculo);
        }

        public async Task<Vehiculo> GetAsync(int id)
        {
            return await _vehiculoRepo.GetAsync(id);
        }
    }
}
