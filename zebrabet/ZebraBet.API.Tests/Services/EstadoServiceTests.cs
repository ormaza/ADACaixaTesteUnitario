using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using ZebraBet.API.Models;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;

namespace ZebraBet.API.Tests.Services
{
    public class EstadoServiceTests
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly EstadoService _sut;

        public EstadoServiceTests()
        {
            _estadoRepository = Substitute.For<IEstadoRepository>();
            _sut = new EstadoService(_estadoRepository);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarListaDeEstados()
        {
            // Arrange
            _estadoRepository.ObterTodosAsync()
                .Returns(new List<Estado>
                {
                    new Estado(1, "São Paulo", "SP"),
                    new Estado(2, "Rio de Janeiro", "RJ")
                });

            // Act
            var estados = await _sut.ObterTodosAsync();

            // Assert
            Assert.NotNull(estados);
            Assert.Equal(2, estados.Count);
            Assert.Equal("São Paulo", estados[0].Sigla);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarEstadoExistente()
        {
            // Arrange
            var estado = new Estado(1, "Minas Gerais", "MG");
            _estadoRepository.ObterPorIdAsync(1).Returns(estado);

            // Act
            var resultado = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("MG", resultado!.Sigla);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_SeNaoExistir()
        {
            // Arrange
            _estadoRepository.ObterPorIdAsync(1).Returns((Estado?)null);

            // Act
            var resultado = await _sut.ObterPorIdAsync(1);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task AdicionarAsync_DeveChamarRepositorio()
        {
            // Arrange
            var novoEstado = new Estado(3, "Bahia", "BA");

            // Act
            await _sut.AdicionarAsync(novoEstado);

            // Assert
            await _estadoRepository.Received(1).AdicionarAsync(novoEstado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarTrue_SeSucesso()
        {
            // Arrange
            var estado = new Estado(1, "Paran�", "PR");
            _estadoRepository.AtualizarAsync(estado).Returns(true);

            // Act
            var resultado = await _sut.AtualizarAsync(estado);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalseSeFalhar()
        {
            // Arrange
            var estado = new Estado(1, "Paran�", "PR");
            _estadoRepository.AtualizarAsync(estado).Returns(false);

            // Act
            var resultado = await _sut.AtualizarAsync(estado);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarTrue_SeSucesso()
        {
            // Arrange
            _estadoRepository.RemoverAsync(1).Returns(true);

            // Act
            var resultado = await _sut.RemoverAsync(1);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarFalse_SeNaoExistir()
        {
            // Arrange
            _estadoRepository.RemoverAsync(1).Returns(false);

            // Act
            var resultado = await _sut.RemoverAsync(1);

            // Assert
            Assert.False(resultado);
        }
    }
}
