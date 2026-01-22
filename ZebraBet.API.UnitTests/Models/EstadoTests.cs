namespace ZebraBet.API.Tests.Models
{
    public class EstadoTests
    {
        [Fact]
        public void DadoEstadoValido_QuandoCriado_EntaoSucesso()
        {
            // Arrange & Act
            var sut = new API.Models.Estado("São Paulo", "SP");

            // Assert
            Assert.Equal("São Paulo", sut.Nome);
            Assert.Equal("SP", sut.Sigla);
        }

        [Fact]
        public void DadoNomeAcimaDe20Caracteres_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Estado("NomeMuitoLongoAcimaDeVinte", "SP"));

            // Assert
            Assert.Equal("Nome acima de 20 caracteres", exception.Message);
        }

        [Fact]
        public void DadoSiglaComTamanhoInvalido_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Estado("São Paulo", "SPL"));

            // Assert
            Assert.Equal("Sigla deve possuir 2 caracteres", exception.Message);
        }
    }
}