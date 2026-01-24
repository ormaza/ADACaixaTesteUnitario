namespace ZebraBet.API.Models;

public class Estado
{

    public Estado(int id,
        string nome,
        string sigla)
    {
        Id = id;
        Nome = nome;
        Sigla = sigla;

        Validar();
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Sigla { get; private set; }

    private void Validar()
    {
        if (string.IsNullOrEmpty(Nome))
        {
            throw new ArgumentException("Nome não pode ser vazio");
        }

        if (string.IsNullOrEmpty(Sigla))
        {
            throw new ArgumentException("Sigla não pode ser vazia");
        }

        if (Nome.Length > 20)
        {
            throw new ArgumentException("Nome acima de 20 caracteres");
        }

        if (Sigla.Length != 2)
        {
            throw new ArgumentException("Sigla deve possuir 2 caracteres");
        }
    }
}
