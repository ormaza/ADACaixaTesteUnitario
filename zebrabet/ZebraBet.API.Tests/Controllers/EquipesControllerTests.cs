using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using ZebraBet.API.Controllers;
using ZebraBet.API.Models;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API.Tests.Controllers
{
    public class EquipeControllerTests
    {
        private readonly IEquipeService _service;
        private readonly EquipesController _controller;

        public EquipeControllerTests()
        {
            _service = Substitute.For<IEquipeService>();
            _controller = new EquipesController(_service);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkComLista()
        {
            // Arrange
            var lista = new List<Equipe>
            {
                new Equipe(1, "São Paulo", "SP"),
                new Equipe(2, "Palmeiras", "SP")
            };
            _service.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var equipes = Assert.IsAssignableFrom<IEnumerable<Equipe>>(okResult.Value);
            Assert.Equal(2, equipes.Count());
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOk_SeExiste()
        {
            // Arrange
            var equipe = new Equipe(1, "São Paulo", "SP");
            _service.ObterPorIdAsync(1).Returns(equipe);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            var equipeRetornada = Assert.IsType<Equipe>(okResult.Value);

            Assert.Equal("São Paulo", equipeRetornada.Nome);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            _service.ObterPorIdAsync(1).Returns((Equipe?)null);

            // Act
            var resultado = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task Criar_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var equipe = new Equipe(1, "São Paulo", "SP");

            // Act
            var resultado = await _controller.Criar(equipe);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(resultado);
            Assert.Equal(nameof(_controller.ObterPorId), created.ActionName);
            Assert.Equal(equipe, created.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNoContent_SeSucesso()
        {
            // Arrange
            var equipe = new Equipe(1, "São Paulo", "SP");
            _service.AtualizarAsync(Arg.Any<Equipe>()).Returns(true);

            // Act
            var resultado = await _controller.Atualizar(1, equipe);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequest_SeIdDiferente()
        {
            // Arrange
            var equipe = new Equipe(1, "São Paulo", "SP");

            // Act
            var resultado = await _controller.Atualizar(2, equipe);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("ID da URL não corresponde ao do corpo da requisição.", badRequest.Value);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFound_SeNaoExiste()
        {
            // Arrange
            var equipe = new Equipe(1, "São Paulo", "SP");
            _service.AtualizarAsync(Arg.Any<Equipe>()).Returns(false);

            // Act
            var resultado = await _controller.Atualizar(1, equipe);

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
