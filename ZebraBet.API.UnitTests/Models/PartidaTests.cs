using ZebraBet.API.Models;

namespace ZebraBet.API.Tests.Models
{
    public class PartidaTests
    {
        [Fact]
        public void CriarPartida_ComDadosValidos_DeveCriarObjetoComSucesso()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);

            // Act
            var partida = new Partida(equipeVisitante, 
                                    equipeMandante, 
                                    golsVisitante, 
                                    golsMandante, 
                                    dataPartida);

            // Assert
            Assert.Equal(equipeVisitante, partida.EquipeVisitante);
            Assert.Equal(equipeMandante, partida.EquipeMandante);
            Assert.Equal(golsVisitante, partida.GolsVisitante);
            Assert.Equal(golsMandante, partida.GolsMandante);
            Assert.Equal(dataPartida, partida.DataPartida);
        }

        [Fact]
        public void CriarPartida_ComEquipeVisitanteVazia_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Equipe visitante vazia", exception.Message);
        }

        [Fact]
        public void CriarPartida_ComEquipeMandanteVazia_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Equipe mandante vazia", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        public void CriarPartida_ComGolsVisitanteNegativos_DeveLancarArgumentException(int golsVisitante)
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Gols visitante não pode ser negativo", exception.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        public void CriarPartida_ComGolsMandanteNegativos_DeveLancarArgumentException(int golsMandante)
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var dataPartida = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Gols mandante não pode ser negativo", exception.Message);
        }

        [Fact]
        public void CriarPartida_ComDataPartidaNoPassado_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now.AddDays(-1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Data da partida deve ser maior que a data atual", exception.Message);
        }

        [Fact]
        public void CriarPartida_ComDataPartidaNoPresente_DeveLancarArgumentException()
        {
            // Arrange
            var equipeVisitante = "Time A";
            var equipeMandante = "Time B";
            var golsVisitante = 2;
            var golsMandante = 3;
            var dataPartida = DateTime.Now;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                new Partida(equipeVisitante, 
                            equipeMandante, 
                            golsVisitante, 
                            golsMandante, 
                            dataPartida));
            Assert.Equal("Data da partida deve ser maior que a data atual", exception.Message);
        }

    }
}