using EcoWatt.API.Controllers;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;
using EcoWatt.Model;
using EcoWatt.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace EcoWatt.Tests
{
    public class UsuariosControllerTests
    {
        private readonly Mock<IRepository<Usuario>> _mockUsuarioRepository;
        private readonly UsuariosController _usuariosController;

        public UsuariosControllerTests()
        {
            _mockUsuarioRepository = new Mock<IRepository<Usuario>>();
            _usuariosController = new UsuariosController(_mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfUsuarios()
        {
            // Arrange
            var mockUsuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, DsUsuario = "User1", DsNomeCompleto = "User One" },
                new Usuario { IdUsuario = 2, DsUsuario = "User2", DsNomeCompleto = "User Two" }
            };

            _mockUsuarioRepository.Setup(repo => repo.GetAll()).ReturnsAsync(mockUsuarios);

            // Act
            var result = await _usuariosController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var usuarios = Assert.IsAssignableFrom<IEnumerable<UsuarioResponse>>(okResult.Value);
            Assert.Equal(2, usuarios.Count());
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999; // ID que não existe no banco de dados
            _mockUsuarioRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Usuario)null);

            // Act
            var result = await _usuariosController.Get(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WhenUsuarioIsCreated()
        {
            // Arrange
            var usuarioRequest = new UsuarioRequest
            {
                DsUsuario = "User1",
                DsSenha = "Password123456789012345",
                DsNomeCompleto = "User One",
            };

            var usuario = new Usuario
            {
                IdUsuario = 1,
                DsUsuario = "User1",
                DsNomeCompleto = "User One"
            };

            _mockUsuarioRepository.Setup(repo => repo.Add(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            _mockUsuarioRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(usuario);

            // Act
            var result = await _usuariosController.Post(usuarioRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var usuarioResponse = Assert.IsType<UsuarioResponse>(okResult.Value);
            Assert.Equal("User1", usuarioResponse.DsUsuario);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;
            var usuarioRequest = new UsuarioRequest
            {
                DsUsuario = "User1",
                DsSenha = "Password123456789012345",
                DsNomeCompleto = "User One"
            };

            _mockUsuarioRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Usuario)null);

            // Act
            var result = await _usuariosController.Put(nonExistentId, usuarioRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenUsuarioIsDeleted()
        {
            // Arrange
            int usuarioId = 1; // ID do usuário que será deletado
            var usuario = new Usuario { IdUsuario = usuarioId };
            _mockUsuarioRepository.Setup(repo => repo.GetById(usuarioId)).ReturnsAsync(usuario);
            _mockUsuarioRepository.Setup(repo => repo.Delete(usuarioId)).Returns(Task.CompletedTask);

            // Act
            var result = await _usuariosController.Delete(usuarioId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
