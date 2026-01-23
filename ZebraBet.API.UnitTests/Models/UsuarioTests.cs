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

        [Fact]
        public void DadoNomeAcimaDe15Caracteres_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "NomeMuitoLongoAcimaDeQuinze",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplo.com"));
            // Assert
            Assert.Equal("Nome acima de 15 caracteres", exception.Message);
        }

        [Fact]
        public void DadoSobrenomeAcimaDe50Caracteres_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "SobrenomeMuitoLongoAcimaDeCinquentaCaracteresTestes",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplo.com"));
            // Assert
            Assert.Equal("Sobrenome acima de 50 caracteres", exception.Message);
        }

        [Fact]
        public void DadoDataNascimentoMenorQue18Anos_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        DateTime.Now.AddYears(-17),
                                                                                        "email@exemplo.com")); 
            // Assert
            Assert.Equal("Data de nascimento deve ser de pelo menos 18 anos atrás", exception.Message);
        }

        [Fact]
        public void DadoEmailIniciandoComArroba_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "@exemplo.com"));
            // Assert
            Assert.Equal("Email não pode iniciar com @", exception.Message);
        }

        [Fact]
        public void DadoEmailSemArroba_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "emailexemplo.com"));
            // Assert
            Assert.Equal("Email não contém @", exception.Message);
        }

        [Fact]
        public void DadoEmailComMaisDeUmPonto_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplo..com"));
            // Assert
            Assert.Equal("Email deve conter exatamente um ponto", exception.Message);
        }

        [Fact]
        public void DadoEmailComNenhumPonto_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplocom"));
            // Assert
            Assert.Equal("Email deve conter exatamente um ponto", exception.Message);
        }

        [Fact]
        public void DadoUsuarioComNomeVazio_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplo.com"));
            // Assert
            Assert.Equal("Nome vazio", exception.Message);
        }

        [Fact]
        public void DadoUsuarioComSobrenomeVazio_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        "email@exemplo.com"));
            // Assert
            Assert.Equal("Sobrenome vazio", exception.Message);
        }

        [Fact]
        public void DadoUsuarioComEmailVazio_QuandoCriado_EntaoArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new API.Models.Usuario(API.Enums.Titulo.Sr,
                                                                                        "nomeTeste",
                                                                                        "sobrenomeTeste",
                                                                                        new DateTime(1990, 5, 20),
                                                                                        ""));
            // Assert
            Assert.Equal("Email vazio", exception.Message);
        }
    }
}