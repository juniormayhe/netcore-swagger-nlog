using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PAC.Business.Contracts;
using PAC.Common.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PAC.VinculadoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class VinculadoController : Controller
    {
        //stringlocalizer needs Localization.AspNetCore.TagHelpers
        private IStringLocalizer<VinculadoController> _stringLocalizer;
        private readonly IVinculadoBusiness _vinculadoBusiness;
        private readonly IEmailService _emailService;
        private readonly ILogger<VinculadoController> _logger;

        public VinculadoController(IVinculadoBusiness vinculadoBusiness, IEmailService emailService, IStringLocalizer<VinculadoController> stringLocalizer, ILogger<VinculadoController> logger)
        {
            _vinculadoBusiness = vinculadoBusiness;

            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
            _logger = logger;
        }

        // GET api/vinculado
        [HttpGet]
        public async Task<VinculadoResponse> Get()
        {
            return await _vinculadoBusiness.GetAllAsync();
        }

        // GET api/vinculado/5
        [HttpGet("{id}")]
        public async Task<VinculadoResponse> Get(long id)
        {
            return await _vinculadoBusiness.GetAsync(id);
        }


        // POST api/vinculado
        [HttpPost, ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task Post([FromBody]VinculadoRequest vinculadoRequest)
        {
            long i = await _vinculadoBusiness.AddAsync(vinculadoRequest);
            if (i > 0) {
                try
                {
                    _emailService.SendEmail(vinculadoRequest.Nombre, 
                        _stringLocalizer["EmailSubjectUserRegistered"], 
                        _stringLocalizer["EmailBodyUserRegistered", 
                        vinculadoRequest.Cedula]).Wait();
                }
                catch (Exception ex)
                {
                    string url = string.Concat(this.Request.Scheme, "://", this.Request.Host, this.Request.Path, this.Request.QueryString);
                    _logger.LogError(ex, $"Falla al intentar enviar correo de confirmación de registro para {vinculadoRequest.Cedula} en {url}");
                }
            }
        }

        // PUT api/vinculado/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]VinculadoRequest value)
        {
            //for update async
            throw new NotImplementedException();
        }

        // DELETE api/vinculado/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            //for delete async
            throw new NotImplementedException();
        }
    }
}
