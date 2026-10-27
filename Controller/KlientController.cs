
public class KlientController
{
    private static MagazynPaliw magazyn = new MagazynPaliw();
    private static Koszyk koszyk = new Koszyk();
    private static HistoriaTransakcji historia = new HistoriaTransakcji();

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
                string wyborPaliwa = KlientView.PobierzWyborPaliwa();
                if (wyborPaliwa == "0")
                {
                    KlientView.PokazKomunikat("Tankowanie anulowane");
                    break;
                }
                int indeksPaliwa = int.Parse(wyborPaliwa) - 1;
                Paliwo wybranePaliwo = paliwa[indeksPaliwa];
                decimal iloscLitrow = KlientView.PobierzIlośćLitrów();
                if (!TankowanieService.CzyMoznaZatankować(
                    iloscLitrow, wybranePaliwo))
                {
                    KlientView.PokazKomunikat(
                        "Nie można zatankować podanej ilości paliwa");
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
                KlientView.PokazKomunikat("Wybrano sklep");
                break;
            case OpcjaKlienta.Koszyk:
                KlientView.PokazKomunikat("Wybrano koszyk i płatność");
                List<PozycjaKoszyka> pozycje = koszyk.PobierzPozycje();
                decimal suma = koszyk.ObliczWartoscKoszyka();
                KlientView.PokazKoszyk(pozycje, suma);
                PlatnośćController.UruchomPlatność(koszyk, historia);
                break;
            case OpcjaKlienta.Paragon:
                KlientView.PokazKomunikat("Wybrano paragon");
                break;
        }
    }
}
