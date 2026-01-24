namespace ZebraBet.API.Models;

public class Partida
{
    public Partida(string equipeVisitante, 
                    string equipeMandante, 
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
    public string EquipeVisitante { get; private set; }
    public string EquipeMandante { get; private set; }
    public int GolsVisitante { get; private set; }
    public int GolsMandante { get; private set; }
    public DateTime DataPartida { get; private set; }

    private void Validar()
    {
        if (string.IsNullOrEmpty(EquipeVisitante))
        {
            throw new ArgumentException("Equipe visitante vazia");
        }

        if (string.IsNullOrEmpty(EquipeMandante))
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