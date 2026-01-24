using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;
using NSubstitute;
using Xunit;

namespace ZebraBet.API.Tests.Services
{
    public class EquipeServiceTests
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly EquipeService _service;

        public EquipeServiceTests()
        {
            _equipeRepository = Substitute.For<IEquipeRepository>();
            _service = new EquipeService(_equipeRepository);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeEquipes()
        {
            // Arrange
            var lista = new List<Equipe>
            {
                new Equipe(1, "Flamengo","RJ"),
                new Equipe(2, "São Paulo", "SP")
            };
            _equipeRepository.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _service.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarEquipeSeExistir()
        {
            // Arrange
            var equipe = new Equipe(1, "Flamengo", "RJ");
            _equipeRepository.ObterPorIdAsync(1).Returns(equipe);

            // Act
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(equipe, resultado);
        }

        [Fact]
        public async Task Adicionar_DeveChamarRepositorio()
        {
            // Arrange
            var equipe = new Equipe(1, "Flamengo", "RJ");

            // Act
            await _service.AdicionarAsync(equipe);

            // Assert
            await _equipeRepository.Received(1).AdicionarAsync(equipe);
        }

        [Fact]
        public async Task Atualizar_DeveChamarRepositorio()
        {
            // Arrange
            var equipe = new Equipe(1, "Flamengo", "RJ");
            _equipeRepository.AtualizarAsync(equipe).Returns(true);

            // Act
            var resultado = await _service.AtualizarAsync(equipe);

            // Assert
            Assert.True(resultado);
            await _equipeRepository.Received(1).AtualizarAsync(equipe);
        }
    }
}
