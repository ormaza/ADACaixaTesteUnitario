namespace ZebraBet.API.Models;

public class Partida
{
    public Partida(int id,
                    int equipeMandanteId,
                    int equipeVisitanteId,
                    int golsVisitante, 
                    int golsMandante, 
                    DateTime dataPartida)
    {
        Id = id;
        EquipeVisitanteId = equipeVisitanteId;
        EquipeMandanteId = equipeMandanteId;
        GolsVisitante = golsVisitante;
        GolsMandante = golsMandante;
        DataPartida = dataPartida;

        Validar();
    }
    public int Id { get; private set; }
    public int EquipeVisitanteId { get; private set; }
    public int EquipeMandanteId { get; private set; }
    public int GolsVisitante { get; private set; }
    public int GolsMandante { get; private set; }
    public DateTime DataPartida { get; private set; }

    private void Validar()
    {
        if (EquipeVisitanteId == 0)
        {
            throw new ArgumentException("Equipe visitante vazia");
        }

        if (EquipeMandanteId == 0)
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