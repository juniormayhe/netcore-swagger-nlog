using System.Collections.Generic;
using Newtonsoft.Json;
using PAC.Entities;

namespace PAC.Business.Contracts
{
    public class VinculadoResponse
    {
        public VinculadoResponse() => Vinculados = new List<Vinculado>();

        //do not render Message if it is empty
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public List<Vinculado> Vinculados { get; set; }
    }
}