using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using ZebraBet.API.Controllers;
using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        private readonly IUsuarioService _service;
        private readonly UsuariosController _controller;

        public UsuariosControllerTests()
        {
            _service = Substitute.For<IUsuarioService>();
            _controller = new UsuariosController(_service);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkSeExiste()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr.", "Ramon", "Valdez", DateTime.Today.AddYears(-25), "madruguinha@email.com");
            _service.ObterPorIdAsync(1).Returns(usuario);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var usuarioRetornado = Assert.IsType<Usuario>(okResult.Value);

            Assert.Equal("Ramon", usuarioRetornado.Nome);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.ObterPorIdAsync(1).Returns((Usuario?)null);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task Criar_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr.", "Ramon", "Valdez", DateTime.Today.AddYears(-25), "madruguinha@email.com");

            // Act
            var resultado = await _controller.Criar(usuario);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(resultado);
            Assert.Equal(nameof(_controller.ObterPorId), created.ActionName);
            Assert.Equal(usuario, created.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr.", "Ramon", "Valdez", DateTime.Today.AddYears(-25), "madruguinha@email.com");
            _service.AtualizarAsync(Arg.Any<Usuario>()).Returns(true);

            // Act
            var resultado = await _controller.Atualizar(1, usuario);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequestSeIdDiferente()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr.", "Ramon", "Valdez", DateTime.Today.AddYears(-25), "madruguinha@email.com");

            // Act
            var resultado = await _controller.Atualizar(2, usuario);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("ID da URL não corresponde ao do corpo da requisição.", badRequest.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr.", "Ramon", "Valdez", DateTime.Today.AddYears(-25), "madruguinha@email.com");
            _service.AtualizarAsync(Arg.Any<Usuario>()).Returns(false);

            // Act
            var resultado = await _controller.Atualizar(1, usuario);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            _service.RemoverAsync(1).Returns(true);

            // Act
            var resultado = await _controller.Deletar(1);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.RemoverAsync(1).Returns(false);

            // Act
            var resultado = await _controller.Deletar(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }
    }
}
