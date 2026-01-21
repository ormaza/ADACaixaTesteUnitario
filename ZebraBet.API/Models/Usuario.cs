using ZebraBet.API.Enums;

namespace ZebraBet.API.Models;

public class Usuario
{
    public Usuario(
        Titulo titulo,
        string nome,
        string sobrenome,
        DateTime dataNascimento,
        string email)    
    {
        Titulo = titulo;
        Nome = nome;       
        Sobrenome = sobrenome;
        DataNascimento = dataNascimento;
        Email = email;

        Validar();
    }

    public Titulo Titulo { get; private set; }
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Email { get; private set; }

    private void Validar()
    {
        if (Nome.Length > 15)
        {
            throw new ArgumentException("Nome acima de 15 caracteres");
        }

        if (Sobrenome.Length > 50)
        {
            throw new ArgumentException("Sobrenome acima de 50 caracteres");
        }

        if (DataNascimento > DateTime.Now.AddYears(-18))
        {
            throw new ArgumentException("Data de nascimento deve ser de pelo menos 18 anos atrás");
        }

        if (Email.StartsWith("@"))
        {
            throw new ArgumentException("Email não pode iniciar com @");
        }

        if (!Email.Contains("@"))
        {
            throw new ArgumentException("Email não contém @");
        }

        if (Email.Count(c => c == '.') != 1)
        {
            throw new ArgumentException("Email deve conter exatamente um ponto");
        }

        if (string.IsNullOrEmpty(Nome))
        {
            throw new ArgumentException("Nome vazio");
        }

        if (string.IsNullOrEmpty(Sobrenome))
        {
            throw new ArgumentException("Sobrenome vazio");
        }

        if (string.IsNullOrEmpty(Email))
        {
            throw new ArgumentException("Email vazio");
        }
    }

    public string NomeCompleto()
    {
        if(Titulo == Titulo.Nenhum)
        {
            return $"{Nome} {Sobrenome}";
        }
        else
        {
            return $"{Titulo} {Nome} {Sobrenome}";
        }
    }
}
