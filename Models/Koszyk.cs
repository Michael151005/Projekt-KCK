
public class Koszyk
{
    private List<PozycjaKoszyka> pozycje = new List<PozycjaKoszyka>();
    public List<PozycjaKoszyka> PobierzPozycje()
    {
        return pozycje;
    }
    public void DodajPozycje(PozycjaKoszyka pozycja)
    {
        pozycje.Add(pozycja);
    }
    public decimal ObliczWartoscKoszyka()
    {
        decimal suma = 0m;
        foreach (PozycjaKoszyka pozycja in pozycje)
        {
            suma += pozycja.Ilość * pozycja.CenaJednostkowa;
        }
        return suma;
    }
}
