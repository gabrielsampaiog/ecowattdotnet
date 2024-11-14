using Moq;
using Xunit;
using EcoWatt.API.Controllers;
using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Repository;
using EcoWatt.Service.ConvertMapping;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EcoWatt.API.Tests
{
    public class BateriasControllerTests
    {
        private readonly Mock<IRepository<Bateria>> _mockBateriaRepository;
        private readonly Mock<IRepository<Usuario>> _mockUsuarioRepository;
        private readonly BateriasController _controller;

        public BateriasControllerTests()
        {
            _mockBateriaRepository = new Mock<IRepository<Bateria>>();
            _mockUsuarioRepository = new Mock<IRepository<Usuario>>();
            _controller = new BateriasController(_mockBateriaRepository.Object, _mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WhenBateriaExists()
        {
            // Arrange
            int bateriaId = 1;
            var bateria = new Bateria { IdBateria = bateriaId, IdUsuario = 1 };
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync(bateria);

            // Act
            var result = await _controller.Get(bateriaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BateriaResponse>(okResult.Value);
            Assert.Equal(bateriaId, returnValue.IdBateria);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenBateriaDoesNotExist()
        {
            // Arrange
            int bateriaId = 1;
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync((Bateria)null);

            // Act
            var result = await _controller.Get(bateriaId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Put_ShouldReturnOk_WhenBateriaIsUpdated()
        {
            // Arrange
            int bateriaId = 1;
            var bateriaRequest = new BateriaRequest { IdUsuario = 1, /* outros dados conforme necessário */ };
            var existingBateria = new Bateria { IdBateria = bateriaId, IdUsuario = 1 };
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync(existingBateria);
            _mockUsuarioRepository.Setup(repo => repo.GetById(existingBateria.IdUsuario)).ReturnsAsync(new Usuario());

            // Act
            var result = await _controller.Put(bateriaId, bateriaRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BateriaResponse>(okResult.Value);
            Assert.Equal(bateriaId, returnValue.IdBateria);
        }

        [Fact]
        public async Task Put_ShouldReturnNotFound_WhenBateriaDoesNotExist()
        {
            // Arrange
            int bateriaId = 1;
            var bateriaRequest = new BateriaRequest { IdUsuario = 1 };
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync((Bateria)null);

            // Act
            var result = await _controller.Put(bateriaId, bateriaRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenBateriaIsDeleted()
        {
            // Arrange
            int bateriaId = 1;
            var bateria = new Bateria { IdBateria = bateriaId };
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync(bateria);
            _mockBateriaRepository.Setup(repo => repo.Delete(bateriaId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(bateriaId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenBateriaDoesNotExist()
        {
            // Arrange
            int bateriaId = 1;
            _mockBateriaRepository.Setup(repo => repo.GetById(bateriaId)).ReturnsAsync((Bateria)null);

            // Act
            var result = await _controller.Delete(bateriaId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
