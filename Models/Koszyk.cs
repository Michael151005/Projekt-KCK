using System;

public class Koszyk
{
    private List<PozycjaKoszyka> pozycje = new List<PozycjaKoszyka>();
    
    public List<PozycjaKoszyka> PobierzPozycje()
    {
        return pozycje;
    }

    public void DodajPozycje(PozycjaKoszyka nowaPozycja)
    {
        var istniejaca = pozycje.FirstOrDefault(p => p.Nazwa.Equals(nowaPozycja.Nazwa, StringComparison.OrdinalIgnoreCase));

        if (istniejaca != null)
        {
            istniejaca.Ilość += nowaPozycja.Ilość;
        }
        else 
        {
            pozycje.Add(nowaPozycja);
        }
    }

    public decimal ObliczWartoscKoszyka()
    {
        decimal suma = 0m;

        foreach (PozycjaKoszyka pozycja in pozycje)
        {
            suma += pozycja.Ilość * pozycja.CenaJednostkowa;
        }
        
        return Math.Round(suma, 2);
    }

    public void WyczyscKoszyk()
    {
        pozycje.Clear();
    }
}
