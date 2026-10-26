public class KlientController
{
    private static MagazynPaliw magazyn = new MagazynPaliw();
    private static Koszyk koszyk = new Koszyk();
    public static void UruchomPanelKlienta()
    {
        while (true)
        {
            KlientView.MenuKlienta();

            string wybor = KlientView.PobierzWybor();

            if (wybor == "0")
            {
                KlientView.PokazKomunikat(
                    "Powrot do menu glownego...");
                return;
            }
            ObsluzWyborKlienta(wybor);
        } 
    }

    public static void ObsluzWyborKlienta(string wybor)
    {
        switch (wybor)
        {
            case "1":
                KlientView.PokazKomunikat("Wybrano tankowanie");
                List<Paliwo> dostepnePaliwa= magazyn.PobierzPaliwa();
                KlientView.PokazPaliwa(dostepnePaliwa);
                string wyborPaliwa = KlientView.PobierzWyborPaliwa();
                if (wyborPaliwa == "0")
                {
                    break;
                }
                int numerPaliwa = int.Parse(wyborPaliwa);
                int indeksPaliwa = numerPaliwa - 1;
                Paliwo wybranePaliwo=dostepnePaliwa[indeksPaliwa];
                KlientView.PokazKomunikat("Wybrałeś paliwo: "+wybranePaliwo.NazwaPaliwa);
                decimal ilosclitrow = KlientView.PobierzIlośćLitrów();
                bool czyMoznaZatankowac = TankowanieService.CzyMoznaZatankować(ilosclitrow,wybranePaliwo);
                if(czyMoznaZatankowac)
                {
                    decimal koszt = TankowanieService.ObliczKosztTankowania(ilosclitrow,wybranePaliwo.CenaZaLitr);
                    KlientView.PokazKomunikat("Koszt tankowania: "+ koszt + " zł");
                }
                else
                {
                    KlientView.PokazKomunikat("Brak wystarczającej ilości paliwa!");
                    break;
                }
                string potwierdzenie = KlientView.PotwierdzTankowanie();
                if(potwierdzenie == "1")
                {
                   bool wykonano = TankowanieService.WykonajTankowanie(wybranePaliwo,ilosclitrow);
                   if(wykonano)
                    {
                        PozycjaKoszyka nowaPozycja = new PozycjaKoszyka();
                        nowaPozycja.Nazwa=wybranePaliwo.NazwaPaliwa;
                        nowaPozycja.Ilość = ilosclitrow;
                        nowaPozycja.CenaJednostkowa=wybranePaliwo.CenaZaLitr;
                        nowaPozycja.Jednostka = "l";
                        koszyk.DodajPozycje(nowaPozycja);
                        KlientView.PokazKomunikat("Tankowanie zakończono pomyślnie");
                        KlientView.PokazKomunikat("Dodano paliwo do koszyka");
                        KlientView.PokazKomunikat("Pozostało paliwa: " + wybranePaliwo.DostepnaIloscPaliwa +" l");
                    }
                    else
                    {
                        KlientView.PokazKomunikat("Nie udało się zatankować paliwa");
                    }
                }
                else
                {
                    KlientView.PokazKomunikat("Tankowanie anulowane");
                }
                break;
            case "2":
                KlientView.PokazKomunikat("Wybrano sklep");
                break;

            case "3":
                KlientView.PokazKomunikat("Wybrano koszyk i platnosc");
                List<PozycjaKoszyka> pozycje = koszyk.PobierzPozycje();
                decimal suma = koszyk.ObliczWartoscKoszyka();
                KlientView.PokazKoszyk(pozycje,suma);
                break;

            case "4":
                KlientView.PokazKomunikat("Wybrano paragon");
                break;
        }
    }
}
