public class PlatnośćController
{
    public static void UruchomPlatność(Koszyk koszyk,HistoriaTransakcji historia)
    {
        decimal kwota = koszyk.ObliczWartoscKoszyka();
        if(koszyk.PobierzPozycje().Count ==0)
        {
            PlatnoscView.PokazKomunikat("Koszyk jest pusty");
            return;
        }
        Platnosc platnosc = new Platnosc();
        platnosc.Kwota= kwota;
        platnosc.CzyOplacona = false;
        PlatnoscView.PokazMenuPlatnosci();
        string wybór = PlatnoscView.PobierzWybórPłatności();
        switch (wybór)
        {
            case "1":
                platnosc.MetodaPlatnosci = "Gotówka";
                break;

            case "2":
                platnosc.MetodaPlatnosci = "Karta";
                break;

            case "3":
                platnosc.MetodaPlatnosci = "BLIK";
                break;

            case "0":
                PlatnoscView.PokazKomunikat("0)Powrót do koszyka...");
                return;
        }
        bool wynik =PlatnoscService.WykonajPlatnosc(platnosc);
        if(wynik)
        {
            Transakcja transakcja = new Transakcja();
            transakcja.Kwota = platnosc.Kwota;
            transakcja.MetodaPlatnosci = platnosc.MetodaPlatnosci;
            transakcja.DataTransakcji = DateTime.Now;
            transakcja.Pozycje = new List<PozycjaKoszyka>();
            foreach(PozycjaKoszyka pozycja in koszyk.PobierzPozycje())
            {
                transakcja.Pozycje.Add(new PozycjaKoszyka
                {
                    Nazwa = pozycja.Nazwa,
                    Ilość = pozycja.Ilość,
                    CenaJednostkowa = pozycja.CenaJednostkowa,
                    Jednostka = pozycja.Jednostka
                });
            }
            historia.DodajTransakcje(transakcja);
            PlatnoscView.PokazKomunikat("Płatność zakończona pomyślnie!");
            PlatnoscView.PokazKomunikat("Zapłacono: "+platnosc.Kwota + " zł");
            PlatnoscView.PokazKomunikat("Metoda płatności: "+platnosc.MetodaPlatnosci);
        }
        else
        {
            PlatnoscView.PokazKomunikat("Nie udało się zrealizować płatnośći");
        }
    }
    
}