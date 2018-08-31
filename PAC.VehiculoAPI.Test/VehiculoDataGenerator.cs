using PAC.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PAC.VehiculoAPI.Test
{
    public class VehiculoDataGenerator
    {
        public static IEnumerable<object[]> Data()
        {
            yield return new [] {
                new Vehiculo { IdVehiculo = 1, NumeroOrden = 100100, Placa = "CCG-123" },
                new Vehiculo { IdVehiculo = 2, NumeroOrden = 200200, Placa = "CCG-234" }
            };
        }

     
    }
}