public class Transakcja
{
    public List<PozycjaKoszyka> Pozycje { get; set; } = new List<PozycjaKoszyka>();
    public decimal Kwota { get; set; }
    public string MetodaPlatnosci { get; set; } = "";
    public DateTime DataTransakcji { get; set; }
}