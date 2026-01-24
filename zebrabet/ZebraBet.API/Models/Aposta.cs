namespace ZebraBet.API.Models;

public class Aposta
{
    public Aposta(int id,
                   int partidaId,
                   int usuarioId,
                   int golsVisitante,
                   int golsMandante,
                   DateTime dataAposta)
    {
        Id = id;
        PartidaId = partidaId;
        UsuarioId = usuarioId;
        GolsVisitante = golsVisitante;
        GolsMandante = golsMandante;
        DataAposta = dataAposta;

        Validar();
    }

    public int Id { get; private set; }
    public int PartidaId { get; private set; }
    public int UsuarioId { get; private set; }
    public int GolsVisitante { get; private set; }
    public int GolsMandante { get; private set; }
    public DateTime DataAposta { get; private set; }

    private void Validar()
    {
        if(GolsMandante < 0)
        {
            throw new ArgumentException("Palpite gols mandante não pode ser negativo");
        }

        if(GolsVisitante < 0)
        {
            throw new ArgumentException("Palpite gols visitante não pode ser negativo");
        }

        if(DataAposta.Date < DateTime.Now.Date)
        {
            throw new ArgumentException("Data da aposta não pode ser no passado");
        }

        if(DataAposta.Date > DateTime.Now.Date.AddDays(7))
        {
            throw new ArgumentException("Data da aposta não pode ser mais de uma semana no futuro");
        }
    }
}
