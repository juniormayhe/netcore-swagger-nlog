using System.Collections.Generic;
using Newtonsoft.Json;
using PAC.Common.Settings;
using PAC.Entities;

namespace PAC.Business.Contracts
{
    public class VehiculoResponse
    {
        public VehiculoResponse()
        {
            Vehiculos = new List<Vehiculo>();
            PagingSettings = new PagingSettings();
        }

        //do not render Message if it is empty
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        
        public List<Vehiculo> Vehiculos { get; set; }
        public PagingSettings PagingSettings { get; set; }
    }
}