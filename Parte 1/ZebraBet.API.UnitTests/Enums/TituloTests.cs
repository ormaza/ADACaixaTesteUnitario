namespace ZebraBet.API.Tests.Enums
{
    public class TituloTests
    {
        [Fact]
        public void DadoTituloInexistente_QuandoCriado_EntaoNaoEDefinido()
        {
            // Arrange & Act
            var sut = Enum.IsDefined(typeof(API.Enums.Titulo), 100);

            // Assert
            Assert.False(sut);
        }
    }
}