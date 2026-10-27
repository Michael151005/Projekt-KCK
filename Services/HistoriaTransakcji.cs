public class HistoriaTransakcji
{
    private List<Transakcja> transakcje = new List<Transakcja>();
    public void DodajTransakcje(Transakcja transakcja)
    {
        transakcje.Add(transakcja);
    }
    public List<Transakcja> PobierzTransakcje()
    {
        return transakcje;
    }
}