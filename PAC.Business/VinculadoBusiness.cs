using PAC.Business.Contracts;
using PAC.Entities;
using PAC.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAC.Business
{
    public class VinculadoBusiness : IVinculadoBusiness
    {
        private readonly IVinculadoRepository _vinculadoRepo;

        public VinculadoBusiness(IVinculadoRepository vinculadoRepo)
        {
            _vinculadoRepo = vinculadoRepo;
        }

        public async Task<VinculadoResponse> GetAsync(long id)
        {
            var vinculadoResponse = new VinculadoResponse();
            var vinculado = await _vinculadoRepo.GetAsync(id);

            if (vinculado == null)
            {
                vinculadoResponse.Message = "Vinculado not found.";
            }
            else
            {
                vinculadoResponse.Vinculados.Add(vinculado);
            }

            return vinculadoResponse;
        }

        public async Task<VinculadoResponse> GetAllAsync()
        {
            //TODO: Paging...

            var vinculadoResponse = new VinculadoResponse();
            IEnumerable<Vinculado> vinculados = await _vinculadoRepo.GetAllAsync();

            if (vinculados.ToList().Count == 0)
            {
                vinculadoResponse.Message = "Vinculados not found.";
            }
            else
            {
                vinculadoResponse.Vinculados.AddRange(vinculados);
            }

            return vinculadoResponse;
        }

        public async Task<long> AddAsync(VinculadoRequest VinculadoRequest)
        {
            var vinculado = new Vinculado
            {
                Cedula = VinculadoRequest.Cedula,
                Nombre = VinculadoRequest.Nombre
            };

            return await _vinculadoRepo.AddAsync(vinculado);
        }
    }
}
