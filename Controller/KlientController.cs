
public class KlientController
{
    private static MagazynPaliw magazyn = WspolneMagazyny.Paliwa;
    private static Koszyk koszyk = new Koszyk();
    private static HistoriaTransakcji historia = new HistoriaTransakcji();
    private static MagazynProduktow magazynProduktow = WspolneMagazyny.Produkty;
    private static string pinKarty = "0";

    private static List<OpcjaKlienta> opcje = new List<OpcjaKlienta>
    {
        OpcjaKlienta.Tankowanie,
        OpcjaKlienta.Sklep,
        OpcjaKlienta.Koszyk,
        OpcjaKlienta.Paragon
    };

    public static void UruchomPanelKlienta()
    {
        pinKarty = KlientView.PobierzPinPrzyStarcie();

        while (true)
        {
            string wybor = KlientView.PobierzWyborMenuKlienta(opcje);

            if (wybor == "0")
            {
                KlientView.PokazKomunikat("Powrót do menu głównego...");
                break;
            }
            
            ObsluzWyborKlienta(wybor);
        }
    }

    public static void ObsluzWyborKlienta(string wybor)
    {
        if (!int.TryParse(wybor, out int numer) || numer < 1 || numer > opcje.Count)
        {
            KlientView.PokazKomunikat("Nieprawidłowa opcja!");
            return;
        }

        OpcjaKlienta wybranaOpcja = opcje[numer - 1];

        switch (wybranaOpcja)
        {
            case OpcjaKlienta.Tankowanie:
                ObsluzTankowanie();
                break;

            case OpcjaKlienta.Sklep:
                ObsluzSklep();
                break;

            case OpcjaKlienta.Koszyk:
                ObsluzKoszyk();
                break;

            case OpcjaKlienta.Paragon:
                ObsluzParagon();
                break;
        }
    }

    private static void ObsluzTankowanie()
    {
        List<Paliwo> paliwa = magazyn.PobierzPaliwa();
        KlientView.PokazPaliwa(paliwa);
        string wyborPaliwa = KlientView.PobierzWyborPaliwa(paliwa.Count);

        if (wyborPaliwa == "0")
        {
            KlientView.PokazKomunikat("Tankowanie anulowane.");
            return;
        }

        int indeksPaliwa = int.Parse(wyborPaliwa) - 1;
        Paliwo wybranePaliwo = paliwa[indeksPaliwa];
        decimal iloscLitrow = KlientView.PobierzIloscLitrow();

        if (iloscLitrow == 0) 
        {
            KlientView.PokazKomunikat("Tankowanie anulowane.");
            return;
        }

        if (!TankowanieService.CzyMoznaZatankować(iloscLitrow, wybranePaliwo))
        {
            KlientView.PokazKomunikat("Nie można zatankować podanej ilości paliwa!");
            return;
        }

        decimal koszt = TankowanieService.ObliczKosztTankowania(iloscLitrow, wybranePaliwo.CenaZaLitr);
        KlientView.PokazKomunikat("Koszt tankowania: " + koszt + " zł.");
        string potwierdzenie = KlientView.PotwierdzTankowanie();
                
        if (potwierdzenie == "1")
        {
            bool zatankowano = TankowanieService.WykonajTankowanie(wybranePaliwo, iloscLitrow);

            if (zatankowano)
            {
                PozycjaKoszyka nowaPozycja = new PozycjaKoszyka();

                nowaPozycja.Nazwa = wybranePaliwo.NazwaPaliwa;
                nowaPozycja.Ilość = iloscLitrow;
                nowaPozycja.CenaJednostkowa = wybranePaliwo.CenaZaLitr;
                nowaPozycja.Jednostka = "l";

                koszyk.DodajPozycje(nowaPozycja);
                magazyn.ZapiszPaliwa();

                UIHelper.PokazSukces($"Tankowanie zakończono pomyślnie!\nDodano paliwo do koszyka.\nPozostało paliwa: {wybranePaliwo.DostepnaIloscPaliwa:F2} l.");
            }
            else
            {
                UIHelper.PokazBlad("Nie udało się zatankować paliwa!");
            }
        }
        else
        {
            UIHelper.PokazBlad("Tankowanie anulowane.");
        }      
    }

    private static void ObsluzSklep()
    {
        List <Produkt> produkty = magazynProduktow.PobierzProdukty();
        SklepView.PokazProdukty(produkty);
        string wyborProduktu = SklepView.PobierzWyborProduktu();

        if(wyborProduktu =="0")
        {
            return;
        }

        if (!int.TryParse(wyborProduktu, out int numerProduktu) || numerProduktu < 1 || numerProduktu > produkty.Count)
        {
            SklepView.PokazKomunikat("Nieprawidłowy wybór produktu!");
            return;
        }

        Produkt wybranyProdukt = produkty[numerProduktu - 1];
        int ilosc = SklepView.PobierzIlosc(wybranyProdukt.DostepnaIlosc);

        if (ilosc == 0)
        {
            UIHelper.PokazBlad("Anulowano dodanie produktu.");
            return;
        }

        decimal iloscWKoszyku = koszyk.PobierzPozycje().Where(p => p.Nazwa == wybranyProdukt.Nazwa && p.Jednostka == "szt.").Sum(p => p.Ilość);

        if (iloscWKoszyku + ilosc > wybranyProdukt.DostepnaIlosc)
        {
            UIHelper.PokazBlad("Brak wystarczającej ilości produktu!");
            return;
        }
                
        PozycjaKoszyka nowaPozycjaProduktu = new PozycjaKoszyka
        {
            Nazwa = wybranyProdukt.Nazwa,
            Ilość = ilosc,
            CenaJednostkowa = wybranyProdukt.Cena,
            Jednostka = "szt."
        };

        koszyk.DodajPozycje(nowaPozycjaProduktu);
        UIHelper.PokazSukces("Dodano produkt do koszyka!");
        return;
    }

    private static void ObsluzKoszyk()
    {
        List<PozycjaKoszyka> pozycje = koszyk.PobierzPozycje();

        if (pozycje.Count == 0)
        {
            UIHelper.PokazBlad("Koszyk jest pusty!");
            return;
        }

        Console.Clear();
        decimal suma = koszyk.ObliczWartoscKoszyka();
        KlientView.PokazKoszyk(pozycje, suma);
        UIHelper.PoczekajNaKlawisz();
        PlatnośćController.UruchomPlatność(koszyk, historia, ref pinKarty, magazynProduktow);
    }

    private static void ObsluzParagon()
    {
        List<Transakcja>transakcje = historia.PobierzTransakcje();

        if (transakcje.Count == 0)
        {
            UIHelper.PokazBlad("Nie ma jeszcze żadnych opłaconych transakcji!");
            return;
        }

        Transakcja ostatniaTransakcja = transakcje[transakcje.Count - 1];
        Console.Clear();
        ParagonView.PokazParagon(ostatniaTransakcja);
        UIHelper.PoczekajNaKlawisz();
    }
}