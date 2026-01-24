namespace ZebraBet.API.Models;

public class Partida
{
    public Partida(Equipe equipeVisitante, 
                    Equipe equipeMandante, 
                    int golsVisitante, 
                    int golsMandante, 
                    DateTime dataPartida)
    {
        EquipeVisitante = equipeVisitante;
        EquipeMandante = equipeMandante;
        GolsVisitante = golsVisitante;
        GolsMandante = golsMandante;
        DataPartida = dataPartida;

        Validar();
    }
    public Equipe EquipeVisitante { get; private set; }
    public Equipe EquipeMandante { get; private set; }
    public int GolsVisitante { get; private set; }
    public int GolsMandante { get; private set; }
    public DateTime DataPartida { get; private set; }

    private void Validar()
    {
        if (EquipeVisitante == null)
        {
            throw new ArgumentException("Equipe visitante vazia");
        }

        if (EquipeMandante == null)
        {
            throw new ArgumentException("Equipe mandante vazia");
        }

        if (GolsVisitante < 0)
        {
            throw new ArgumentException("Gols visitante não pode ser negativo");
        }

        if (GolsMandante < 0)
        {
            throw new ArgumentException("Gols mandante não pode ser negativo");
        }

        if(DataPartida.Date <= DateTime.Now.Date)
        {
            throw new ArgumentException("Data da partida deve ser maior que a data atual");
        }
    }
}