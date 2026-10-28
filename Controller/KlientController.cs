
public class KlientController
{
    private static MagazynPaliw magazyn = new MagazynPaliw();
    private static Koszyk koszyk = new Koszyk();
    private static HistoriaTransakcji historia = new HistoriaTransakcji();
    private static MagazynProduktow magazynProduktow = new MagazynProduktow();
    private static List<OpcjaKlienta> opcje = new List<OpcjaKlienta>
    {
        OpcjaKlienta.Tankowanie,
        OpcjaKlienta.Sklep,
        OpcjaKlienta.Koszyk,
        OpcjaKlienta.Paragon
    };
    public static void UruchomPanelKlienta()
    {
        while (true)
        {
            KlientView.PokazMenuOpcji(opcje);
            string wybor = KlientView.PobierzWybor();
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
        if (!int.TryParse(wybor, out int numer) ||
            numer < 1 ||
            numer > opcje.Count)
        {
            KlientView.PokazKomunikat("Nieprawidłowa opcja!");
            return;
        }
        OpcjaKlienta wybranaOpcja = opcje[numer - 1];
        switch (wybranaOpcja)
        {
            case OpcjaKlienta.Tankowanie:
                KlientView.PokazKomunikat("Wybrano tankowanie");
                List<Paliwo> paliwa = magazyn.PobierzPaliwa();
                KlientView.PokazPaliwa(paliwa);
                string wyborPaliwa = KlientView.PobierzWyborPaliwa(paliwa.Count);
                if (wyborPaliwa == "0")
                {
                    KlientView.PokazKomunikat("Tankowanie anulowane");
                    break;
                }

                int indeksPaliwa = int.Parse(wyborPaliwa) - 1;
                Paliwo wybranePaliwo = paliwa[indeksPaliwa];
                decimal iloscLitrow = KlientView.PobierzIlośćLitrów(wybranePaliwo.DostepnaIloscPaliwa);

                if (iloscLitrow == 0) 
                {
                    KlientView.PokazKomunikat("Tankowanie anulowane.");
                    break;
                }

                if (!TankowanieService.CzyMoznaZatankować(iloscLitrow, wybranePaliwo))
                {
                    KlientView.PokazKomunikat("Nie można zatankować podanej ilości paliwa");
                    break;
                }

                decimal koszt = TankowanieService.ObliczKosztTankowania(iloscLitrow, wybranePaliwo.CenaZaLitr);
                KlientView.PokazKomunikat("Koszt tankowania: " + koszt + " zł");
                string potwierdzenie = KlientView.PotwierdzTankowanie();
                if (potwierdzenie == "1")
                {
                    bool zatankowano = TankowanieService.WykonajTankowanie(wybranePaliwo, iloscLitrow);
                    if (zatankowano)
                    {
                        PozycjaKoszyka nowaPozycja = new PozycjaKoszyka();

                        nowaPozycja.Nazwa = wybranePaliwo.NazwaPaliwa;
                        nowaPozycja.Ilość = iloscLitrow;
                        nowaPozycja.CenaJednostkowa =
                            wybranePaliwo.CenaZaLitr;
                        nowaPozycja.Jednostka = "l";

                        koszyk.DodajPozycje(nowaPozycja);

                        KlientView.PokazKomunikat(
                            "Tankowanie zakończono pomyślnie!");
                        KlientView.PokazKomunikat(
                            "Dodano paliwo do koszyka");
                        KlientView.PokazKomunikat(
                            "Pozostało paliwa: " +
                            wybranePaliwo.DostepnaIloscPaliwa + " l");
                    }
                    else
                    {
                        KlientView.PokazKomunikat(
                            "Nie udało się zatankować paliwa");
                    }
                }
                else
                {
                    KlientView.PokazKomunikat("Tankowanie anulowane");
                }
                break;
            case OpcjaKlienta.Sklep:
                List <Produkt> produkty = magazynProduktow.PobierzProdukty();
                SklepView.PokazProdukty(produkty);
                string wyborProduktu = SklepView.PobierzWyborProduktu();
                if(wyborProduktu =="0")
                {
                    break;
                }
                if (!int.TryParse(wyborProduktu, out int numerProduktu) || numer < 1 || numer > produkty.Count)
                {
                    SklepView.PokazKomunikat("Nieprawidłowy wybór produktu!");
                    break;
                }
                Produkt wybranyProdukt = produkty[numer - 1];
                int ilosc = SklepView.PobierzIlosc();
                if (!SklepService.CzyMoznaKupic(wybranyProdukt, ilosc))
                {
                    SklepView.PokazKomunikat("Brak wystarczającej ilości produktu!");
                    break;
                }
                bool zakupUdany = SklepService.WykonajZakup(wybranyProdukt, ilosc);
                if(zakupUdany)
                {
                    PozycjaKoszyka nowaPozycja = new PozycjaKoszyka();
                    nowaPozycja.Nazwa = wybranyProdukt.Nazwa;
                    nowaPozycja.Ilość = ilosc;
                    nowaPozycja.CenaJednostkowa = wybranyProdukt.Cena;
                    nowaPozycja.Jednostka = "szt.";
                    koszyk.DodajPozycje(nowaPozycja);
                    SklepView.PokazKomunikat("Produkt dodany do koszyka!");
                }
                else
                {
                    SklepView.PokazKomunikat("Nie udało się kupić produktu.");
                }
                break;
            case OpcjaKlienta.Koszyk:
                KlientView.PokazKomunikat("Wybrano koszyk i płatność");
                List<PozycjaKoszyka> pozycje = koszyk.PobierzPozycje();
                decimal suma = koszyk.ObliczWartoscKoszyka();
                KlientView.PokazKoszyk(pozycje, suma);
                PlatnośćController.UruchomPlatność(koszyk, historia);
                break;
            case OpcjaKlienta.Paragon:
                List<Transakcja>transakcje = historia.PobierzTransakcje();
                if(transakcje.Count ==0)
                {
                    KlientView.PokazKomunikat("Nie ma jeszcze adnych opłaconych transakcji");
                    break;
                }
                Transakcja ostatniaTransakcja= transakcje[transakcje.Count -1];
                ParagonView.PokazParagon(ostatniaTransakcja);
                break;
        }
    }
}
 