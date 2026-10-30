public class RaportSprzedazyService
{
    public static RaportSprzedazy GenerujRaport(List<Transakcja> transakcje)
    {
        RaportSprzedazy raport = new RaportSprzedazy();
        raport.LiczbaTransakcji = transakcje.Count;

        foreach (Transakcja transakcja in transakcje)
        {
            raport.lacznyPrzychod += transakcja.Kwota;
            foreach (PozycjaKoszyka pozycja in transakcja.Pozycje)
            {
                if (pozycja.Jednostka == "l")
                {
                    raport.SprzedaneLitry += pozycja.Ilość;
                    if (!raport.SprzedanePaliwa.ContainsKey(pozycja.Nazwa))
                    {
                        raport.SprzedanePaliwa[pozycja.Nazwa] = 0;
                    }
                    raport.SprzedanePaliwa[pozycja.Nazwa] += pozycja.Ilość;
                }
                else if (pozycja.Jednostka == "szt.")
                {
                    raport.SprzedaneSztuki += pozycja.Ilość;
                    if (!raport.SprzedaneProdukty.ContainsKey(pozycja.Nazwa))
                    {
                        raport.SprzedaneProdukty[pozycja.Nazwa] = 0;
                    }
                    raport.SprzedaneProdukty[pozycja.Nazwa] += pozycja.Ilość;
                }
            }
        }
        return raport;
    }
}
