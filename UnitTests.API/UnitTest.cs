using Application.API.DTOs;
using Application.API.Features.Queries;
using Application.API.Repositories.Citas;
using EMedicalERP.API.Controllers;
using MediatR;
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
        private readonly Mock<ICitaService> _citaServiceMock;
        private readonly Mock<ISender> _senderMock;
        private readonly CitasController _controller;

        public UnitTest()
        {
            _citaServiceMock = new Mock<ICitaService>();
            _senderMock = new Mock<ISender>();
            _controller = new CitasController(_citaServiceMock.Object, _senderMock.Object);
        }

        [Fact]
        public async Task ObtenerDisponibles_SinEspecialidad_BadRequest()
        {
            // Act
            var result = await _controller.ObtenerDisponibles(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Debe especificar una especialidad", badRequest.Value);
        }

        [Fact]
        public async Task ObtenerDisponibles_ConEspecialidad_OkResult()
        {
            // Arrange
            var citas = new List<CitaDisponibleDTO> { new CitaDisponibleDTO() };
            _citaServiceMock.Setup(s => s.ObtenerCitasDisponiblesAsync("Medicina general")).ReturnsAsync(citas);

            // Act
            var result = await _controller.ObtenerDisponibles("Medicina general");

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(citas, ok.Value);
        }

        [Fact]
        public async Task ListaCitas_OkResult()
        {
            // Arrange
            var citas = new List<CitaDisponibleDTO> { new CitaDisponibleDTO() };
            _citaServiceMock.Setup(s => s.listCitasAsync()).ReturnsAsync(citas);

            // Act
            var result = await _controller.ListaCitas();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(citas, ok.Value);
        }

        [Fact]
        public async Task ObtenerDisponiblesById_SinId_BadRequest()
        {
            var result = await _controller.ObtenerDisponiblesById(0);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Debe especificar un id", badRequest.Value);
        }

        

        [Fact]
        public async Task Reservar_CitaYaReservada_BadRequest()
        {
            var dto = new ReservaCitaDTO { CitaId = 1, PacienteId = 2 };
            _citaServiceMock.Setup(s => s.ReservarCitaAsync(dto.CitaId, dto.PacienteId)).ReturnsAsync(false);

            var result = await _controller.Reservar(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("ya ha sido reservada", badRequest.Value!.ToString());
        }

        [Fact]
        public async Task Reservar_CitaExitosa_OkResult()
        {
            var dto = new ReservaCitaDTO { CitaId = 2, PacienteId = 2 };
            _citaServiceMock.Setup(s => s.ReservarCitaAsync(dto.CitaId, dto.PacienteId)).ReturnsAsync(true);

            var result = await _controller.Reservar(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("Cita reservada correctamente", ok.Value!.ToString());
        }          
    }
}

