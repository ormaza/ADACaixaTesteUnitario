using ZebraBet.API.Models;

namespace ZebraBet.API.Tests.Models
{
    public class ApostaTests
    {
        [Fact]
        public void CriarAposta_ComDadosValidos_DeveCriarObjetoComSucesso()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);
            
            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            var palpiteGolsVisitante = 2;
            var palpiteGolsMandante = 3;
            var dataAposta = DateTime.Now.AddDays(1);

            // Act
            var aposta = new Aposta(partida,
                                    usuario,
                                    palpiteGolsVisitante,
                                    palpiteGolsMandante,
                                    dataAposta);

            // Assert
            Assert.Equal(partida, aposta.Partida);
            Assert.Equal(usuario, aposta.Usuario);
            Assert.Equal(palpiteGolsVisitante, aposta.PalpiteGolsVisitante);
            Assert.Equal(palpiteGolsMandante, aposta.PalpiteGolsMandante);
            Assert.Equal(dataAposta, aposta.DataAposta);
        }

        [Fact]
        public void CriarAposta_ComPartidaNula_DeveLancarArgumentException()
        {
            // Arrange
            Partida partida = null;
            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             2,
                                                                             3,
                                                                             DateTime.Now.AddDays(1)));
            Assert.Equal("Partida não pode ser nula", exception.Message);
        }

        [Fact]
        public void CriarAposta_ComUsuarioNulo_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);
            
            Usuario usuario = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             2,
                                                                             3,
                                                                             DateTime.Now.AddDays(1)));
            Assert.Equal("Usuario não pode ser nulo", exception.Message);
        }   

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        public void CriarAposta_ComPalpiteGolsVisitanteNegativo_DeveLancarArgumentException(int palpiteNegativo)
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);

            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             palpiteNegativo,
                                                                             3,
                                                                             DateTime.Now.AddDays(1)));
            Assert.Equal("Palpite gols visitante não pode ser negativo", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        public void CriarAposta_ComPalpiteGolsMandanteNegativo_DeveLancarArgumentException(int palpiteNegativo)
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);

            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             3,
                                                                             palpiteNegativo,
                                                                             DateTime.Now.AddDays(1)));
            Assert.Equal("Palpite gols mandante não pode ser negativo", exception.Message);
        }

        [Fact]
        public void CriarAposta_ComDataApostaNoPassado_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);

            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             3,
                                                                             3,
                                                                             DateTime.Now.AddDays(-1)));
            Assert.Equal("Data da aposta não pode ser no passado", exception.Message);
        }

        [Fact]
        public void CriarAposta_ComDataApostaMaisDeUmaSemanaNoFuturo_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);
            var partida = new Partida(equipeVisitante, equipeMandante, golsVisitante, golsMandante, dataPartida);

            var usuario = new Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Aposta(partida,
                                                                             usuario,
                                                                             3,
                                                                             3,
                                                                             DateTime.Now.AddDays(8)));
            Assert.Equal("Data da aposta não pode ser mais de uma semana no futuro", exception.Message);
        }
    }
}