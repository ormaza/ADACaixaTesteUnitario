using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ZebraBet.API.Controllers;
using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;
using Xunit;

namespace ZebraBet.API.Tests.Controllers
{
    public class ApostasControllerTests
    {
        private readonly IApostaService _service;
        private readonly ApostasController _controller;

        public ApostasControllerTests()
        {
            _service = Substitute.For<IApostaService>();
            _controller = new ApostasController(_service);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<Aposta>
            {
                new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1)),
                new Aposta(2, 2, 2, 0, 3, DateTime.Today.AddDays(2))
            };
            _service.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _controller.ObterTodos();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(resultado.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Aposta>>(ok.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkSeExiste()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1));
            _service.ObterPorIdAsync(1).Returns(aposta);

            // Act
            var resultado = await _controller.ObterPorId(2);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(resultado.Result);

            Assert.Equal(aposta, ok.Value);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.ObterPorIdAsync(1).Returns((Aposta?)null);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(resultado.Result);
        }

        [Fact]
        public async Task Criar_DeveRetornarOk_SeSucesso()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1));

            // Act
            var resultado = await _controller.Criar(aposta);

            // Assert
            Assert.IsType<OkResult>(resultado);
            await _service.Received(1).AdicionarAsync(aposta);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1));

            _service.AtualizarAsync(aposta)
                    .Returns(true);

            // Act
            var resultado = await _controller.Atualizar(1, aposta);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1));
            _service.AtualizarAsync(aposta)
                    .Returns(false);

            // Act
            var resultado = await _controller.Atualizar(1, aposta);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequest_SeServiceLancarArgumentException()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.Today.AddDays(1));

            _service.When(s => s.AtualizarAsync(aposta))
                    .Do(x => throw new ArgumentException("Erro ao atualizar aposta"));

            // Act
            var resultado = await _controller.Atualizar(1, aposta);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Erro ao atualizar apostas", bad.Value);
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
