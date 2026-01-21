namespace ZebraBet.API.Models;

public class Estado
{

    public Estado(
        string nome,
        string sigla)
    {
        Nome = nome;
        Sigla = sigla;

        Validar();
    }

    public string Nome { get; private set; }
    public string Sigla { get; private set; }

    private void Validar()
    {
        if (Nome.Length > 20)
        {
            throw new ArgumentException("Nome acima de 20 caracteres");
        }

        if (Sigla.Length != 2)
        {
            throw new ArgumentException("Sigla deve possuir 2 caracteres");
        }

        if (string.IsNullOrEmpty(Nome))
        {
            throw new ArgumentException("Nome vazio");
        }

        if (string.IsNullOrEmpty(Sigla))
        {
            throw new ArgumentException("Sigla vazio");
        }
    }
}
