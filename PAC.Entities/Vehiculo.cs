using System;
using System.ComponentModel.DataAnnotations;

namespace PAC.Entities
{
    /// <summary>
    /// System.ComponentModel.DataAnnotations needed for attributes
    /// </summary>
    public class Vehiculo { 
        
        public int IdVehiculo { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int NumeroOrden{ get; set; }

        [Required, StringLength(60, MinimumLength = 5)]
        public string Placa { get; set; }
    }
}
