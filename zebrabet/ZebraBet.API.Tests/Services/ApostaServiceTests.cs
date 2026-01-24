using NSubstitute;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;
using Xunit;

namespace ZebraBet.API.Tests.Services
{
    public class ApostaServiceTests
    {
        private readonly IApostaRepository _apostaRepo;
        private readonly IPartidaRepository _partidaRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ApostaService _sut;

        public ApostaServiceTests()
        {
            _apostaRepo = Substitute.For<IApostaRepository>();
            _partidaRepo = Substitute.For<IPartidaRepository>();
            _usuarioRepo = Substitute.For<IUsuarioRepository>();
            _sut = new ApostaService(_apostaRepo, _partidaRepo, _usuarioRepo);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarLista()
        {
            // Arrange
            _apostaRepo.ObterTodosAsync().Returns(new List<Aposta>
            {
                new Aposta(1, 1, 1, 2, 1, DateTime.UtcNow.AddDays(1))
            });

            // Act
            var result = await _sut.ObterTodosAsync();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarAposta()
        {
            // Arrange
            var aposta = new Aposta(1, 1, 1, 1, 0, DateTime.UtcNow.AddDays(1));
            _apostaRepo.ObterPorIdAsync(1).Returns(aposta);

            // Act
            var result = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_SeNaoExistir()
        {
            // Arrange
            _apostaRepo.ObterPorIdAsync(1).Returns((Aposta?)null);

            // Act
            var result = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AdicionarAsync_DeveCriarAposta()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr", "Ramon", "Valdez", new DateTime(1990, 1, 1), "madruga@email.com");
            var partida = new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2));

            _usuarioRepo.ObterPorIdAsync(1).Returns(usuario);
            _partidaRepo.ObterPorIdAsync(1).Returns(partida);

            var aposta = new Aposta(0, partida.Id, usuario.Id, 2, 1, DateTime.UtcNow.AddDays(2));

            // Act
            await _sut.AdicionarAsync(aposta);

            // Assert
            await _apostaRepo.Received(1).AdicionarAsync(Arg.Is<Aposta>(a =>
                a.PartidaId == aposta.PartidaId &&
                a.UsuarioId == aposta.UsuarioId &&
                a.GolsMandante == aposta.GolsMandante &&
                a.GolsVisitante == aposta.GolsVisitante
            ));
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarTrue_SeSucesso()
        {
            // Arrange
            var apostaExistente = new Aposta(1, 1, 1, 1, 1, DateTime.UtcNow.AddDays(1));
            var usuario = new Usuario(1, "Sr", "Ramon", "Valdez", new DateTime(1950, 1, 5), "madruga@email.com");
            var partida = new Partida(1, 1, 2, 0, 0, DateTime.UtcNow.AddDays(2));

            _apostaRepo.ObterPorIdAsync(1).Returns(apostaExistente);
            _usuarioRepo.ObterPorIdAsync(usuario.Id).Returns(usuario);
            _partidaRepo.ObterPorIdAsync(partida.Id).Returns(partida);
            _apostaRepo.AtualizarAsync(Arg.Any<Aposta>()).Returns(true);

            var apostaAtualizada = new Aposta(1, partida.Id, usuario.Id, 2, 0, DateTime.UtcNow.AddDays(2));

            // Act
            var result = await _sut.AtualizarAsync(apostaAtualizada);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalse_SeApostaNaoExistir()
        {
            // Arrange
            _apostaRepo.ObterPorIdAsync(1).Returns((Aposta?)null);
            var aposta = new Aposta(1, 1, 1, 2, 1, DateTime.UtcNow.AddDays(1));

            // Act
            var result = await _sut.AtualizarAsync(aposta);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarTrue()
        {
            // Arrange
            _apostaRepo.RemoverAsync(1).Returns(true);

            // Act
            var result = await _sut.RemoverAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarFalse()
        {
            // Arrange
            _apostaRepo.RemoverAsync(1).Returns(false);

            // Act
            var result = await _sut.RemoverAsync(1);

            // Assert
            Assert.False(result);
        }
    }
}
