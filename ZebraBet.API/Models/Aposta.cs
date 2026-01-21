namespace ZebraBet.API.Models;

public class Aposta
{
    public Partida Partida { get; private set; }
    public Usuario Usuario { get; private set; }
    public int PalpiteGolsVisitante { get; private set; }
    public int PalpiteGolsMandante { get; private set; }
    public string DataAposta { get; private set; }
}
