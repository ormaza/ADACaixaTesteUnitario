namespace ZebraBet.API.Tests.Models
{
    public class EquipeFutebolTests
    {
        [Fact]
        public void DadoEquipeFutebolValida_QuandoCriada_EntaoSucesso()
        {
            // Arrange & Act
            var sut = new API.Models.EquipeFutebol("Corinthians", "COR");

            // Assert
            Assert.Equal("Corinthians", sut.Nome);
            Assert.Equal("COR", sut.Sigla);
        }

        [Fact]
        public void DadoNomeComMenosDe3Caracteres_QuandoCriada_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.EquipeFutebol("AB", "ABC"));

            // Assert
            Assert.Equal("Nome deve possuir ao menos 3 caracteres", exception.Message);
        }

        [Fact]
        public void DadoNomeComMaisDe20Caracteres_QuandoCriada_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.EquipeFutebol("NomeMuitoLongoAcimaDeVinte", "ABC"));

            // Assert
            Assert.Equal("Nome deve possuir no máximo 20 caracteres", exception.Message);
        }

        [Fact]
        public void DadoSiglaVazia_QuandoCriada_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.EquipeFutebol("Corinthians", ""));

            // Assert
            Assert.Equal("Sigla vazia", exception.Message);
        }
    }
}