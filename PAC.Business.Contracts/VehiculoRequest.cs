using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PAC.Business.Contracts
{
    public class VehiculoRequest
    {
        public int IdVehiculo { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int NumeroOrden { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Placa { get; set; }
    }
}