using System.ComponentModel.DataAnnotations;

namespace ZebraBet.API.Models;

public class Equipe
{
    public Equipe(
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
        if (Nome.Length < 3)
        {
            throw new ArgumentException("Nome deve possuir ao menos 3 caracteres");
        }

        if (Nome.Length > 20)
        {
            throw new ArgumentException("Nome deve possuir no máximo 20 caracteres");
        }

        if (string.IsNullOrEmpty(Sigla))
        {
            throw new ArgumentException("Sigla vazia");
        }
    }
}