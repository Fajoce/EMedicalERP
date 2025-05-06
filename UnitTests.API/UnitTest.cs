using Application.API.DTOs;
using Application.API.Repositories.Citas;
using EMedicalERP.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.API
{
   public  class UnitTest
    {
        //[Fact]
        //public async Task ObtenerDisponibles_DeberiaRetornarBadRequest_SiEspecialidadEsNula()
        //{
        //    // Arrange
        //    var mockService = new Mock<ICitaService>();
        //    var controller = new CitasController(mockService.Object);

        //    // Act
        //    var resultado = await controller.ObtenerDisponibles(null);

        //    // Assert
        //    var badRequest = Assert.IsType<BadRequestObjectResult>(resultado);
        //    Assert.Equal("Debe especificar una especialidad", badRequest.Value);
        //}

        //[Fact]
        //public async Task ObtenerDisponibles_DeberiaRetornarOkConCitas_SiEspecialidadEsValida()
        //{
        //    // Arrange
        //    var mockService = new Mock<ICitaService>();
        //    var especialidad = "Examen general";

        //    var citasFake = new List<CitaDisponibleDTO>
        //{
        //    new CitaDisponibleDTO { id = 1, especialidad = especialidad },
        //    new CitaDisponibleDTO { id = 2, especialidad = especialidad }
        //};

        //    mockService
        //        .Setup(service => service.ObtenerCitasDisponiblesAsync(especialidad))
        //        .ReturnsAsync(citasFake);

        //    var controller = new CitasController(mockService.Object);

        //    // Act
        //    var resultado = await controller.ObtenerDisponibles(especialidad);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(resultado);
        //    var citas = Assert.IsType<List<CitaDisponibleDTO>>(okResult.Value);
        //    Assert.Equal(2, citas.Count);
        //}
    }
}

