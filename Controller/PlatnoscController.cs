public class PlatnośćController
{
    private static readonly List<MetodaPlatnosci> metody = new()
    {
        MetodaPlatnosci.Gotowka,
        MetodaPlatnosci.Karta,
        MetodaPlatnosci.Blik
    };

    public static void UruchomPlatność(Koszyk koszyk, HistoriaTransakcji historia)
    {
        if (koszyk.PobierzPozycje().Count == 0)
        {
            PlatnoscView.PokazKomunikat("Koszyk jest pusty");
            return;
        }

        decimal kwota = koszyk.ObliczWartoscKoszyka();
        PlatnoscView.PokazMenuPlatnosci(metody);
        string wybor = PlatnoscView.PobierzWybórPłatności();

        if (wybor == "0")
        {
            PlatnoscView.PokazKomunikat("Powrót do koszyka...");
            return;
        }

        if (!int.TryParse(wybor, out int numer) || numer < 1 || numer > metody.Count)
        {
            PlatnoscView.PokazKomunikat("Niepoprawna metoda płatności!");
            return;
        }

        MetodaPlatnosci wybranaMetoda = metody[numer - 1];
        string nazwaMetody = wybranaMetoda switch
        {
            MetodaPlatnosci.Gotowka => "Gotówka",
            MetodaPlatnosci.Karta => "Karta",
            MetodaPlatnosci.Blik => "BLIK",
            _ => wybranaMetoda.ToString()
        };

        Platnosc platnosc = new Platnosc
        {
            Kwota = kwota,
            MetodaPlatnosci = nazwaMetody,
            CzyOplacona = false
        };

        bool wynik = PlatnoscService.WykonajPlatnosc(platnosc);
        if (wynik)
        {
            Transakcja transakcja = new Transakcja
            {
                Kwota = platnosc.Kwota,
                MetodaPlatnosci = platnosc.MetodaPlatnosci,
                DataTransakcji = DateTime.Now
            };

            foreach (PozycjaKoszyka pozycja in koszyk.PobierzPozycje())
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
            koszyk.WyczyscKoszyk();
            PlatnoscView.PokazKomunikat("Płatność zakończona pomyślnie!");
            PlatnoscView.PokazKomunikat("Zapłacono: " + platnosc.Kwota + " zł");
            PlatnoscView.PokazKomunikat("Metoda płatności: " + platnosc.MetodaPlatnosci);
        }
        else
        {
            PlatnoscView.PokazKomunikat("Nie udało się zrealizować płatności");
        }
    }
}
