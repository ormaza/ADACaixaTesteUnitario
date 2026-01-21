namespace ZebraBet.API.Models;

public class Aposta
{
    public Aposta(Partida partida,
                   Usuario usuario,
                   int palpiteGolsVisitante,
                   int palpiteGolsMandante,
                   DateTime dataAposta)
    {
        Partida = partida;
        Usuario = usuario;
        PalpiteGolsVisitante = palpiteGolsVisitante;
        PalpiteGolsMandante = palpiteGolsMandante;
        DataAposta = dataAposta;

        Validar();
    }
    public Partida Partida { get; private set; }
    public Usuario Usuario { get; private set; }
    public int PalpiteGolsVisitante { get; private set; }
    public int PalpiteGolsMandante { get; private set; }
    public DateTime DataAposta { get; private set; }

    private void Validar()
    {
        if(Partida == null)
        {
            throw new ArgumentException("Partida nula");
        }

        if(Usuario == null)
        {
            throw new ArgumentException("Usuario nulo");
        }

        if(PalpiteGolsMandante < 0)
        {
            throw new ArgumentException("Palpite gols mandante não pode ser negativo");
        }

        if(PalpiteGolsVisitante < 0)
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
