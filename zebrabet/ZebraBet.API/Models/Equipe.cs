using System.ComponentModel.DataAnnotations;

namespace ZebraBet.API.Models;

public class Equipe
{
    public Equipe(
        int id,
        string nome,
        string siglaEstado)
    {
        Id = id;
        Nome = nome;
        SiglaEstado = siglaEstado;

        Validar();
    }
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string SiglaEstado { get; private set; }

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

        if (string.IsNullOrEmpty(SiglaEstado))
        {
            throw new ArgumentException("Sigla vazia");
        }
    }
}