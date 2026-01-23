namespace ZebraBet.API.Tests.Models
{
    public class UsuarioTests
    {
        [Fact]
        public void DadoUsuarioValido_QuandoCriado_EntaoSucesso()
        {
            // Arrange & Act
            var sut = new API.Models.Usuario(API.Enums.Titulo.Sr,
                                            "nomeTeste",
                                            "sobrenomeTeste",
                                             new DateTime(1990, 5, 20),
                                            "email@exemplo.com");

            // Assert
            Assert.Equal(API.Enums.Titulo.Sr, sut.Titulo);
            Assert.Equal("nomeTeste", sut.Nome);
            Assert.Equal("sobrenomeTeste", sut.Sobrenome);
            Assert.Equal(new DateTime(1990, 5, 20), sut.DataNascimento);
            Assert.Equal("email@exemplo.com", sut.Email);
        
        }
    }
}