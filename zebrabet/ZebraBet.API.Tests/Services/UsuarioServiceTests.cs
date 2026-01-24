using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;
using NSubstitute;
using Xunit;

namespace ZebraBet.API.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _usuarioRepository = Substitute.For<IUsuarioRepository>();
            _service = new UsuarioService(_usuarioRepository);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var lista = new List<Usuario>
            {
                new Usuario(1, "Sr", "Ramon", "Valdez", DateTime.Now.AddYears(-25), "madruguinha@email.com"),
                new Usuario(1, "Sra", "Florinda", "Meza", DateTime.Now.AddYears(-30), "flroinda@email.com")
            };
            _usuarioRepository.ObterTodosAsync().Returns(lista);

            // Act
            var resultado = await _service.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioSeExistir()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr", "Ramon", "Valdez", DateTime.Now.AddYears(-25), "madruguinha@email.com");
            _usuarioRepository.ObterPorIdAsync(1).Returns(usuario);

            // Act
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuario, resultado);
        }

        [Fact]
        public async Task Adicionar_DeveChamarRepositorio()
        {
            // Arrange
            var usuario = new Usuario(1, "Sr", "Ramon", "Valdez", DateTime.Now.AddYears(-25), "madruguinha@email.com");

            // Act
            await _service.AdicionarAsync(usuario);

            // Assert
            await _usuarioRepository.Received(1).AdicionarAsync(usuario);
        }

        [Fact]
        public async Task Remover_DeveChamarRepositorio()
        {
            // Arrange
            _usuarioRepository.RemoverAsync(1).Returns(true);

            // Act
            var resultado = await _service.RemoverAsync(1);

            // Assert
            Assert.True(resultado);
            await _usuarioRepository.Received(1).RemoverAsync(1);
        }
    }
}
