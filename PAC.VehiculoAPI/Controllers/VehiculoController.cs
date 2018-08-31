using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PAC.Business;
using PAC.Business.Contracts;
using PAC.Common.Services;
using PAC.Entities;

namespace PAC.VehiculoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class VehiculoController : Controller
    {
        //stringlocalizer needs Localization.AspNetCore.TagHelpers
        private IStringLocalizer<VehiculoController> _stringLocalizer;
        readonly ILogger<VehiculoController> _logger;
        readonly IEmailService _emailService;
        private readonly IVehiculoBusiness _vehiculoBusiness;

        public VehiculoController(IVehiculoBusiness vehiculoBusiness, IEmailService emailService, IStringLocalizer<VehiculoController> stringLocalizer, ILogger<VehiculoController> logger)
        {
            _vehiculoBusiness = vehiculoBusiness;

            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
            _logger = logger;
        }

        // GET api/vehiculo
        [HttpGet]
        public async Task<List<Vehiculo>> Get()
        {
            //send email sample
            _emailService.SendEmail("desarrollo@tax-individual.com.co",
                _stringLocalizer["EmailSubjectVehicleRegistered"],
                _stringLocalizer["EmailBodyVehicleDateRegistered", DateTime.Now.ToString("g")]).Wait();

            //log service sample
            _logger.LogError(_stringLocalizer["InvalidData"]);

            return await _vehiculoBusiness.GetAllAsync(null);
        }

        // GET api/vehiculo/5
        [HttpGet("{id}")]
        public async Task<Vehiculo> Get(int id)
        {

            return await _vehiculoBusiness.GetAsync(id);
        }

        // GET api/vehiculo/page/1
        [HttpGet]
        [Route("[action]/{pageNumber}")]
        public async Task<List<Vehiculo>> Page([FromRoute]int? pageNumber)
        {
            return await _vehiculoBusiness.GetAllAsync(pageNumber);
        }

        // POST api/vehiculo
        [HttpPost, ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<VehiculoResponse> Post([FromBody]VehiculoRequest vehiculoRequest)
        {
            var response = new VehiculoResponse();
            bool modelValid = vehiculoRequest.IdVehiculo == 0 && ModelState.IsValid;
            if (!modelValid) {
                response.Message = _stringLocalizer["InvalidRequest"];
                return response;
            }

            int i = await _vehiculoBusiness.AddAsync(vehiculoRequest);
            
            if (i > 0)
            {
                response.Message = _stringLocalizer["Created"];

                try
                {
                    _emailService.SendEmail("desarrollo@tax-individual.com.co",
                        _stringLocalizer["EmailSubjectVehicleRegistered"],
                        _stringLocalizer["EmailBodyVehicleDateRegistered", DateTime.Now.ToString("g")]).Wait();
                }
                catch (Exception ex)
                {
                    string url = string.Concat(this.Request.Scheme, "://", this.Request.Host, this.Request.Path, this.Request.QueryString);
                    _logger.LogError(ex, $"Falla al intentar enviar correo de confirmación de registro para {vehiculoRequest.Placa} en {url}");
                }
            }
            return response;
        }

        // PUT api/vehiculo
        [HttpPut]
        public async Task<VehiculoResponse> Put([FromBody]VehiculoRequest vehiculoRequest)
        {
            var response = new VehiculoResponse();
            bool modelValid = vehiculoRequest?.IdVehiculo > 0 && ModelState.IsValid;
            if (!modelValid)
            {
                response.Message = _stringLocalizer["InvalidRequest"];
                return response;
            }

            int i = await _vehiculoBusiness.UpdateAsync(vehiculoRequest);
            if (i <= 0)
            {
                response.Message = _stringLocalizer["NotFound"];
            }
            else
                response.Message = _stringLocalizer["Updated"];

            return response;
        }

        // DELETE api/vehiculo
        [HttpDelete]
        public async Task<VehiculoResponse> Delete([FromBody]VehiculoRequest vehiculoRequest)
        {
            var response = new VehiculoResponse();
            bool modelValid = vehiculoRequest?.IdVehiculo > 0;
            if (!modelValid)
            {
                response.Message = _stringLocalizer["InvalidRequest"];
                return response;
            }

            int i = await _vehiculoBusiness.DeleteAsync(vehiculoRequest.IdVehiculo);
            if (i <= 0) {
                response.Message = _stringLocalizer["NotFound"];
            }
            else
                response.Message = _stringLocalizer["Deleted"];

            return response;
        }
    }
}
