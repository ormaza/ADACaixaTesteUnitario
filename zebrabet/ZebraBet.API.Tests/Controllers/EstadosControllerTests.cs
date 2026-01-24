using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ZebraBet.API.Controllers;
using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;
using Xunit;

namespace ZebraBet.API.Tests.Controllers
{
    public class EstadosControllerTests
    {
        private readonly IEstadoService _service;
        private readonly EstadosController _controller;

        public EstadosControllerTests()
        {
            _service = Substitute.For<IEstadoService>();
            _controller = new EstadosController(_service);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<Estado>
            {
                new Estado(1, "São Paulo", "SP"),
                new Estado(2, "Minas Gerais", "MG")
            };

            _service.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var estados = Assert.IsAssignableFrom<IEnumerable<Estado>>(okResult.Value);
            Assert.Equal(2, estados.Count());
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkSeExiste()
        {
            // Arrange
            var estado = new Estado(1, "São Paulo", "SP");
            _service.ObterPorIdAsync(1).Returns(estado);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var estadoRetornado = Assert.IsType<Estado>(okResult.Value);
            Assert.Equal(1, estadoRetornado.Id);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.ObterPorIdAsync(1).Returns((Estado?)null);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task Criar_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var estado = new Estado(3, "Rio de Janeiro", "RJ");

            // Act
            var resultado = await _controller.Criar(estado);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(resultado);
            Assert.Equal(nameof(_controller.ObterPorId), created.ActionName);
            Assert.Equal(estado, created.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            var estado = new Estado(1, "São Paulo", "SP");
            _service.AtualizarAsync(Arg.Any<Estado>()).Returns(true);

            // Act
            var resultado = await _controller.Atualizar(1, estado);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequestSeIdDiferente()
        {
            // Arrange
            var estado = new Estado(1, "São Paulo", "SP");

            // Act
            var resultado = await _controller.Atualizar(2, estado);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("ID da URL não corresponde ao do corpo da requisição.", badRequest.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            var estado = new Estado(1, "São Paulo", "SP");
            _service.AtualizarAsync(Arg.Any<Estado>()).Returns(false);

            // Act
            var resultado = await _controller.Atualizar(1, estado);

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
    }
}
