using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PAC.Business;
using PAC.VehiculoAPI.Controllers;
using PAC.Common.Services;
using Xunit;
using FluentAssertions;
using Moq;
using PAC.Business.Contracts;
using System.Collections.Generic;
using PAC.Entities;
using PAC.Common;
using PAC.Common.Settings;

namespace PAC.VehiculoAPI.Test
{
    /// <summary>
    /// Fact: test case with no parameters
    /// Theory: test case that takes data from InlineData or another source (i.e. excel)
    /// http://fluentassertions.com/examples.html
    /// 
    /// Setup and teardwon https://xunit.github.io/docs/comparisons
    /// </summary>
    public class VehiculoControllerTest
    {
        Mock<IVehiculoBusiness> vehiculoBusiness;
        Mock<IEmailService> emailService;
        Mock<IStringLocalizer<VehiculoController>> stringLocalizer;
        Mock<ILogger<VehiculoController>> logger;
        List<Vehiculo> _vehiculos;

        public VehiculoControllerTest()
        {
            //setup
            vehiculoBusiness = new Mock<IVehiculoBusiness>();
            emailService = new Mock<IEmailService>();
            stringLocalizer = new Mock<IStringLocalizer<VehiculoController>>();
            logger = new Mock<ILogger<VehiculoController>>();
            _vehiculos = new List<Vehiculo>() {
                new Vehiculo { IdVehiculo=1, NumeroOrden=100100, Placa="TVH-123" },
                new Vehiculo { IdVehiculo=2, NumeroOrden=200100, Placa="TVH-234" },
                new Vehiculo { IdVehiculo=3, NumeroOrden=300300, Placa="TVH-345" },
                new Vehiculo { IdVehiculo=4, NumeroOrden=400400, Placa="TVH-456" },
                new Vehiculo { IdVehiculo=5, NumeroOrden=500500, Placa="TVH-567" },
                new Vehiculo { IdVehiculo=6, NumeroOrden=600600, Placa="TVH-678" },
            };

        }

        [Fact]
        public async void Get_All_Should_Show_List()
        {
            //vehiculoBusiness.Setup(m => m.GetAllAsync(null)).ReturnsAsync(new VehiculoResponse { Vehiculos = _vehiculos });
            //var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            //var vehiculoResponse = await controller.Get();
            //vehiculoResponse.Should().NotBeNull();
            //vehiculoResponse.Vehiculos.Should().NotBeNullOrEmpty();
            //vehiculoResponse.Vehiculos.Should().HaveCount(6);
            //vehiculoResponse.Vehiculos.Should().OnlyHaveUniqueItems();
            //vehiculoResponse.Vehiculos.Should().HaveElementAt(0, _vehiculos[0]);
            //vehiculoResponse.Vehiculos.Should().NotContain(new Vehiculo { IdVehiculo = 6, NumeroOrden = 600600, Placa = "TVH-678" });
        }

        [Theory]
        [InlineData(1)]
        public async void Get_Page_1_Should_Show_List(int page)
        {
            //const int TOTAL_RECORDS = 5;
            //vehiculoBusiness.Setup(m => m.GetAllAsync(It.IsAny<int?>())).ReturnsAsync(new VehiculoResponse { Vehiculos = _vehiculos.GetRange(0, TOTAL_RECORDS), PagingSettings = new PagingSettings { PageNumber = 1 } });
            //var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            //var vehiculoResponse = await controller.Page(page);
            //vehiculoResponse.Should().NotBeNull();
            //vehiculoResponse.Vehiculos.Should().NotBeNullOrEmpty();
            //vehiculoResponse.Vehiculos.Should().HaveCount(TOTAL_RECORDS);
            //vehiculoResponse.Vehiculos.Should().OnlyHaveUniqueItems();
            //vehiculoResponse.Vehiculos.Should().HaveElementAt(0, _vehiculos[0]);
            ////does not contain item from second page
            //vehiculoResponse.Vehiculos.Should().NotContain(new Vehiculo { IdVehiculo = 6, NumeroOrden = 600600, Placa = "TVH-678" });

            //vehiculoResponse.PagingSettings.PageNumber.Should().Be(1);
        }

        [Theory]
        [InlineData(2)]
        public async void Get_Page_2_Should_Show_List(int page)
        {
            //vehiculoBusiness.Setup(m => m.GetAllAsync(It.IsAny<int?>())).ReturnsAsync(new VehiculoResponse { Vehiculos = _vehiculos.GetRange(5, 1), PagingSettings = new PagingSettings { PageNumber = 2 } });
            //var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            //var vehiculoResponse = await controller.Page(page);
            //vehiculoResponse.Should().NotBeNull();
            //vehiculoResponse.Vehiculos.Should().NotBeNullOrEmpty();
            //vehiculoResponse.Vehiculos.Should().HaveCount(1);
            //vehiculoResponse.Vehiculos.Should().OnlyHaveUniqueItems();
            //vehiculoResponse.Vehiculos.Should().HaveElementAt(0, _vehiculos[5]);
            ////does not contain item from first page
            //vehiculoResponse.Vehiculos.Should().NotContain(new Vehiculo { IdVehiculo = 1, NumeroOrden = 100100, Placa = "TVH-123" });

            //vehiculoResponse.PagingSettings.PageNumber.Should().Be(2);
        }

        [Theory]
        [MemberData(nameof(VehiculoDataGenerator.Data), MemberType = typeof(VehiculoDataGenerator))]
        public async void Post_Should_Add_Item(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            stringLocalizer.SetupGet(m => m["Created"]).Returns(new LocalizedString("Created", "Record created"));

            vehiculo1.Should().NotBeNull();
            vehiculo2.Should().NotBeNull();

            var vehiculoRequest1 = new VehiculoRequest { Placa = vehiculo1.Placa, NumeroOrden = vehiculo1.NumeroOrden };
            var vehiculoRequest2 = new VehiculoRequest { Placa = vehiculo2.Placa, NumeroOrden = vehiculo2.NumeroOrden };

            vehiculoBusiness.Setup(m => m.AddAsync(vehiculoRequest1)).ReturnsAsync(1);
            vehiculoBusiness.Setup(m => m.AddAsync(vehiculoRequest2)).ReturnsAsync(1);

            var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            var vehiculoResponse1 = await controller.Post(vehiculoRequest1);
            var vehiculoResponse2 = await controller.Post(vehiculoRequest2);

            vehiculoResponse1.Should().NotBeNull();
            vehiculoResponse2.Should().NotBeNull();

            vehiculoResponse1.Message.Should().Be("Record created");
            vehiculoResponse2.Message.Should().Be("Record created");

        }

        [Theory]
        [MemberData(nameof(VehiculoDataGenerator.Data), MemberType = typeof(VehiculoDataGenerator))]
        public async void Put_Should_Update_Item(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            stringLocalizer.SetupGet(m => m["Updated"]).Returns(new LocalizedString("Updated", "Record updated"));

            vehiculo1.Should().NotBeNull();
            vehiculo2.Should().NotBeNull();

            var vehiculoRequest1 = new VehiculoRequest { IdVehiculo= 1, Placa = vehiculo1.Placa, NumeroOrden = vehiculo1.NumeroOrden };
            var vehiculoRequest2 = new VehiculoRequest { IdVehiculo= 2, Placa = vehiculo2.Placa, NumeroOrden = vehiculo2.NumeroOrden };

            vehiculoBusiness.Setup(m => m.UpdateAsync(vehiculoRequest1)).ReturnsAsync(1);
            vehiculoBusiness.Setup(m => m.UpdateAsync(vehiculoRequest2)).ReturnsAsync(1);

            var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            var vehiculoResponse1 = await controller.Put(vehiculoRequest1);
            var vehiculoResponse2 = await controller.Put(vehiculoRequest2);

            vehiculoResponse1.Should().NotBeNull();
            vehiculoResponse2.Should().NotBeNull();

            vehiculoResponse1.Message.Should().Be("Record updated");
            vehiculoResponse2.Message.Should().Be("Record updated");
        }

        [Theory]
        [MemberData(nameof(VehiculoDataGenerator.Data), MemberType = typeof(VehiculoDataGenerator))]
        public async void Delete_Should_Remove_Item(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            stringLocalizer.SetupGet(m => m["Deleted"]).Returns(new LocalizedString("Deleted", "Record deleted"));

            vehiculo1.Should().NotBeNull();
            vehiculo2.Should().NotBeNull();

            var vehiculoRequest1 = new VehiculoRequest { IdVehiculo = 1, Placa = vehiculo1.Placa, NumeroOrden = vehiculo1.NumeroOrden };
            var vehiculoRequest2 = new VehiculoRequest { IdVehiculo = 2, Placa = vehiculo2.Placa, NumeroOrden = vehiculo2.NumeroOrden };

            vehiculoBusiness.Setup(m => m.DeleteAsync(vehiculoRequest1.IdVehiculo)).ReturnsAsync(1);
            vehiculoBusiness.Setup(m => m.DeleteAsync(vehiculoRequest2.IdVehiculo)).ReturnsAsync(1);

            var controller = new VehiculoController(vehiculoBusiness.Object, emailService.Object, stringLocalizer.Object, logger.Object);
            var vehiculoResponse1 = await controller.Delete(vehiculoRequest1);
            var vehiculoResponse2 = await controller.Delete(vehiculoRequest2);

            vehiculoResponse1.Should().NotBeNull();
            vehiculoResponse2.Should().NotBeNull();

            vehiculoResponse1.Message.Should().Be("Record deleted");
            vehiculoResponse2.Message.Should().Be("Record deleted");
        }

    }
}
