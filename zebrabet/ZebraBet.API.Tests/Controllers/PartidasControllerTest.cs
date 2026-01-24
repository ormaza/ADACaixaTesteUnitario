using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ZebraBet.API.Controllers;
using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;
using Xunit;

namespace ZebraBet.API.Tests.Controllers
{
    public class PartidasControllerTests
    {
        private readonly IPartidaService _service;
        private readonly PartidasController _controller;

        public PartidasControllerTests()
        {
            _service = Substitute.For<IPartidaService>();
            _controller = new PartidasController(_service);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<Partida>
            {
                new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(2)),
                new Partida(2, 4, 3, 0, 0, DateTime.Today.AddDays(3))
            };
            _service.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _controller.ObterTodos();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(resultado.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Partida>>(ok.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkSeExiste()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(2));
            _service.ObterPorIdAsync(1).Returns(partida);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(resultado.Result);
            var retorno = Assert.IsType<Partida>(ok.Value);
            Assert.Equal(1, retorno.Id);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.ObterPorIdAsync(1).Returns((Partida?)null);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(resultado.Result);
        }

        [Fact]
        public async Task Criar_DeveRetornarOk_SeSucesso()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(3));

            // Act
            var resultado = await _controller.Criar(partida);

            // Assert
            Assert.IsType<OkResult>(resultado);
            await _service.Received(1)
                .AdicionarAsync(partida);
        }

        [Fact]
        public async Task Criar_DeveRetornarBadRequest_SeServiceLancarArgumentException()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(3));

            _service.When(s => s.AdicionarAsync(partida))
                    .Do(x => throw new ArgumentException("Erro"));

            // Act
            var resultado = await _controller.Criar(partida);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Erro", bad.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(3));
            _service.AtualizarAsync(partida)
                    .Returns(true);

            // Act
            var resultado = await _controller.Atualizar(1, partida);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(3));
            _service.AtualizarAsync(partida)
                    .Returns(false);

            // Act
            var resultado = await _controller.Atualizar(1, partida);

            // Assert
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequest_SeServiceLancarArgumentException()
        {
            // Arrange
            var partida = new Partida(1, 2, 1, 1, 2, DateTime.Today.AddDays(3));

            _service.When(s => s.AtualizarAsync(partida))
                    .Do(x => throw new ArgumentException("Erro"));

            // Act
            var resultado = await _controller.Atualizar(1, partida);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Erro", bad.Value);
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
            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
