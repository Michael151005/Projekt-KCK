public class PozycjaKoszyka
{
    public string Nazwa {get;set;} = "";
    public decimal Ilość{get;set;}
    public decimal CenaJednostkowa{get;set;}
    public string Jednostka {get;set;}= "";

    private List<PozycjaKoszyka> pozycje = new List<PozycjaKoszyka>();
    public List<PozycjaKoszyka> PobierzPozycje()
    {
        return pozycje;
    }
    public void DodajPozycje(PozycjaKoszyka pozycja)
    {
        pozycje.Add(pozycja);
    }
   
}
