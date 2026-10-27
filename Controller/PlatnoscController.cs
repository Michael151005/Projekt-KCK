public class PlatnośćController
{
    public static void UruchomPlatność(Koszyk koszyk)
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