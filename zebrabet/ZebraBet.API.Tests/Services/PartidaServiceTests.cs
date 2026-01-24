using NSubstitute;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;
using Xunit;

namespace ZebraBet.API.Tests.Services
{
    public class PartidaServiceTests
    {
        private readonly IPartidaRepository _partidaRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly PartidaService _sut;

        public PartidaServiceTests()
        {
            _partidaRepository = Substitute.For<IPartidaRepository>();
            _equipeRepository = Substitute.For<IEquipeRepository>();
            _sut = new PartidaService(_partidaRepository, _equipeRepository);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            // Arrange
            _partidaRepository.ObterTodosAsync().Returns(new List<Partida>
            {
                new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2))
            });

            // Act
            var partidas = await _sut.ObterTodosAsync();

            // Assert
            Assert.Single(partidas);
            Assert.Equal(1, partidas[0].Id);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarPartida()
        {
            // Arrange
            var partida = new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2));
            _partidaRepository.ObterPorIdAsync(1).Returns(partida);

            // Act
            var resultado = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado!.Id);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_SeNaoExistir()
        {
            // Arrange
            _partidaRepository.ObterPorIdAsync(1).Returns((Partida?)null);

            // Act
            var resultado = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task AdicionarAsync_DeveCriarPartida_QuandoObjetosValidos()
        {
            // Arrange
            var mandante = new Equipe(1, "Flamengo", "RJ");
            var visitante = new Equipe(2, "São Paulo", "SP");

            _equipeRepository.ObterPorIdAsync(1).Returns(mandante);
            _equipeRepository.ObterPorIdAsync(2).Returns(visitante);

            var partida = new Partida(0, visitante.Id, mandante.Id, 2, 1, DateTime.UtcNow.AddDays(3));

            // Act
            await _sut.AdicionarAsync(partida);

            // Assert
            await _partidaRepository.Received(1).AdicionarAsync(Arg.Is<Partida>(p =>
                p.EquipeMandanteId == partida.EquipeMandanteId &&
                p.EquipeVisitanteId == partida.EquipeVisitanteId &&
                p.GolsMandante == partida.GolsMandante &&
                p.GolsVisitante == partida.GolsVisitante
            ));
        }

        [Fact]
        public async Task AdicionarAsync_DeveLancarExcecao_SeMandanteNaoExistir()
        {
            // Arrange
            _equipeRepository.ObterPorIdAsync(1).Returns((Equipe?)null);
            var partida = new Partida(0, 2, 1, 2, 1, DateTime.UtcNow.AddDays(3));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.AdicionarAsync(partida));
        }

        [Fact]
        public async Task AdicionarAsync_DeveLancarExcecao_SeVisitanteNaoExistir()
        {
            // Arrange
            _equipeRepository.ObterPorIdAsync(1).Returns(new Equipe(1, "Flamengo", "RJ"));
            _equipeRepository.ObterPorIdAsync(2).Returns((Equipe?)null);
            var partida = new Partida(0, 2, 1, 2, 1, DateTime.UtcNow.AddDays(3));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.AdicionarAsync(partida));
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalse_SePartidaNaoExistir()
        {
            // Arrange
            _partidaRepository.ObterPorIdAsync(1).Returns((Partida?)null);
            var partida = new Partida(1, 2, 1, 3, 2, DateTime.UtcNow.AddDays(3));

            // Act
            var resultado = await _sut.AtualizarAsync(partida);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizar_QuandoObjetosValidos()
        {
            // Arrange
            var partidaExistente = new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2));
            var mandante = new Equipe(1, "Flamengo", "RJ");
            var visitante = new Equipe(2, "São Paulo", "SP");

            _partidaRepository.ObterPorIdAsync(1).Returns(partidaExistente);
            _equipeRepository.ObterPorIdAsync(1).Returns(mandante);
            _equipeRepository.ObterPorIdAsync(2).Returns(visitante);
            _partidaRepository.AtualizarAsync(Arg.Any<Partida>()).Returns(true);

            var partidaAtualizada = new Partida(1, visitante.Id, mandante.Id, 3, 1, DateTime.UtcNow.AddDays(3));

            // Act
            var resultado = await _sut.AtualizarAsync(partidaAtualizada);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveLancarExcecao_SeMandanteNaoExistir()
        {
            // Arrange
            var partidaExistente = new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2));
            _partidaRepository.ObterPorIdAsync(1).Returns(partidaExistente);
            _equipeRepository.ObterPorIdAsync(1).Returns((Equipe?)null);

            var partida = new Partida(1, 2, 1, 3, 1, DateTime.UtcNow.AddDays(3));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.AtualizarAsync(partida));
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarTrue()
        {
            // Arrange
            _partidaRepository.RemoverAsync(1).Returns(true);

            // Act
            var resultado = await _sut.RemoverAsync(1);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarFalse()
        {
            // Arrange
            _partidaRepository.RemoverAsync(1).Returns(false);

            // Act
            var resultado = await _sut.RemoverAsync(1);

            // Assert
            Assert.False(resultado);
        }
    }
}
