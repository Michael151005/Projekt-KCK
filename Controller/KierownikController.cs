public class KierownikController
{
    private static MagazynPaliw MagazynPaliw = WspolneMagazyny.Paliwa;
    private static MagazynProduktow magazynProduktow = WspolneMagazyny.Produkty;
    private static List<OpcjaKierownika> opcje = new List<OpcjaKierownika>
    {
        OpcjaKierownika.StanMagazynu,
        OpcjaKierownika.ZmianaCenPaliw,
        OpcjaKierownika.ZmianaCenProduktow,
        OpcjaKierownika.ZarzadzanieProduktami,
        OpcjaKierownika.RaportySprzedazy,
        OpcjaKierownika.ZmianaHasla  
    };
    
    public static void UruchomPanelKierownika()
    {
        while (true)
        {
            KierownikView.PokazMenu(opcje);
            string wybor = KierownikView.PobierzWybor();

            if (wybor == "0")
            {
                KlientView.PokazKomunikat("Wylogowano kierownika");
                return;
            }

            if (!int.TryParse(wybor,out int numer) || numer < 1 || numer > opcje.Count)
            {
                KierownikView.PokazKomunikat("Nieprawidłowa opcja !");
                continue;
            }

            OpcjaKierownika wybranaOpcja = opcje[numer - 1];

            switch(wybranaOpcja)
            {
                case OpcjaKierownika.StanMagazynu:
                    KierownikView.PokazKomunikat("Wybrano stan magazynu");
                    List<Paliwo> paliwa = MagazynPaliw.PobierzPaliwa();
                    List<Produkt> produkty = magazynProduktow.PobierzProdukty();
                    KierownikView.PobierzStanMagazynu(paliwa,produkty);
                    break;

                case OpcjaKierownika.ZmianaCenPaliw:
                    KierownikView.PokazKomunikat("Wybrano zmianę cen paliw");
                    List<Paliwo> paliwaDoZmiany = MagazynPaliw.PobierzPaliwa();
                    KierownikView.PokazPaliwaDoZmianyCeny(paliwaDoZmiany);
                    string wyborPaliwa = KierownikView.PobierzDane("Wybierz paliwo: ");
                    if (wyborPaliwa == "0")
                    {
                        break;
                    }
                    if(!int.TryParse(wyborPaliwa,out int numerPaliwa) || numerPaliwa < 1 || numerPaliwa > paliwaDoZmiany.Count)
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowy wybór paliwa!");
                        break;
                    }
                    Paliwo wybranePaliwo = paliwaDoZmiany[numerPaliwa - 1];
                    string wpisanaCena = KierownikView.PobierzDane("Podaj nową cenę za litr: ");
                    if(!decimal.TryParse(wpisanaCena,out decimal nowaCena))
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowy format ceny!");
                        break;
                    }
                    bool zmieniono = KierownikService.ZmienCenePaliwa(wybranePaliwo,nowaCena,MagazynPaliw);
                    if(zmieniono)
                    {
                        KierownikView.PokazKomunikat("Cena paliwa została zmieniona");
                    }
                    else
                    {
                        KierownikView.PokazKomunikat("Nie udało się zmienić ceny. Sprawdź cene lub zapis pliku");
                    }
                    break;

                case OpcjaKierownika.ZmianaCenProduktow:
                    KierownikView.PokazKomunikat("Wybrano zmianę cen produktów");
                    break;

                case OpcjaKierownika.ZarzadzanieProduktami:
                    KierownikView.PokazKomunikat("Wybrano zarządzanie produktami");
                    break;

                case OpcjaKierownika.RaportySprzedazy:
                    KierownikView.PokazKomunikat("Wybrano raporty sprzedaży");
                    break;
                    
                case OpcjaKierownika.ZmianaHasla:
                    KierownikView.PokazKomunikat("Wybrano zmianę hasła");
                    break;
            }
        }
    }
}