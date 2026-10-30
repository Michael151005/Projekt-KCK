public class KierownikController
{
    private static MagazynPaliw MagazynPaliw = WspolneMagazyny.Paliwa;
    private static MagazynProduktow magazynProduktow = WspolneMagazyny.Produkty;
    private static List<OpcjaKierownika> opcje = new List<OpcjaKierownika>
    {
        OpcjaKierownika.StanMagazynu,
        OpcjaKierownika.ZmianaCenPaliw,
        OpcjaKierownika.ZmianaCenProduktow,
        OpcjaKierownika.DomawianieProduktow,
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
                    List<Produkt> produktyDoZmiany = magazynProduktow.PobierzProdukty();
                    KierownikView.PokazProduktyDoZmianyCeny(produktyDoZmiany);
                    string wyborProduktu = KierownikView.PobierzDane("Wybierz produkt: ");
                    if (wyborProduktu == "0")
                    {
                        break;
                    }
                    if(!int.TryParse(wyborProduktu, out int numerProduktu) || numerProduktu < 1 || numerProduktu > produktyDoZmiany.Count)
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowy wybór produktu");
                        break;
                    }
                    Produkt wybranyProdukt = produktyDoZmiany[numerProduktu - 1];
                    string wpisanaCenaProduktu = KierownikView.PobierzDane("Podaj nową cenę produktu: ");
                    if(!decimal.TryParse(wpisanaCenaProduktu, out decimal nowaCenaProduktu))
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowy format ceny");
                        break;
                    }
                    bool zmienionoProdukt = KierownikService.ZmienCeneProduktu(wybranyProdukt, nowaCenaProduktu, magazynProduktow);
                    if(zmienionoProdukt)
                    {
                        KierownikView.PokazKomunikat("Cena produktu została zmieniona");
                    }
                    else
                    {
                        KierownikView.PokazKomunikat("Cena produktu musi być większa od zera");
                    }
                    break;

                case OpcjaKierownika.DomawianieProduktow:
                    KierownikView.PokazKomunikat("Wybrano domawianie produktów");
                    List<Produkt> produktyDoDomowienia = magazynProduktow.PobierzProdukty();
                    KierownikView.PokazProduktyDoDomowienia(produktyDoDomowienia);
                    string wybierzProduktDoDomowienmia = KierownikView.PobierzDane("Wybierz produkt: ");
                    if(wybierzProduktDoDomowienmia =="0")
                    {
                        break;
                    }
                    if(!int.TryParse(wybierzProduktDoDomowienmia,out int numerDomawianegoProduktu) || numerDomawianegoProduktu < 1 || numerDomawianegoProduktu > produktyDoDomowienia.Count)
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowy wybór produktu");
                        break;
                    }
                    Produkt produktDoDomowienia = produktyDoDomowienia[numerDomawianegoProduktu-1];
                    string WpisanaIlosc = KierownikView.PobierzDane("Ile sztuk chcesz domówić ? ");
                    if(!int.TryParse(WpisanaIlosc,out int iloscDoDomowienia))
                    {
                        KierownikView.PokazKomunikat("Podaj poprawną liczbę sztuk");
                        break;
                    }
                    bool domowiono = KierownikService.DomowProdukt(produktDoDomowienia,iloscDoDomowienia,magazynProduktow);
                    if(domowiono)
                    {
                        KierownikView.PokazKomunikat("Produkty zostały domówione do magazynu");
                    }
                    else
                    {
                        KierownikView.PokazKomunikat("Nieprawidłowa ilość produktów");
                    }
                    break;

                case OpcjaKierownika.RaportySprzedazy:
                    KierownikView.PokazKomunikat("Wybrano raporty sprzedaży");
                    HistoriaTransakcji historia = new HistoriaTransakcji();
                    List<Transakcja> transakcje = historia.PobierzTransakcje();
                    if (transakcje.Count == 0)
                    {
                        KierownikView.PokazKomunikat("Brak transakcji do wygenerowania raportu");
                        break;
                    }
                    RaportSprzedazy raport = RaportSprzedazyService.GenerujRaport(transakcje);
                    KierownikView.PokazRaportSprzedazy(raport);
                    break;
                    
                case OpcjaKierownika.ZmianaHasla:
                    string noweHaslo = PracownikView.PobierzNoweHaslo();
                    
                    if (string.IsNullOrWhiteSpace(noweHaslo))
                    {
                        PracownikView.PokazKomunikat("Hasło nie może być puste!");
                        break;
                    }
                    
                    AutoryzacjaService.ZmienHaslo("Kierownik", noweHaslo);
                    PracownikView.PokazKomunikat("Hasło kierownika zostało pomyślnie zmienione!");
                    break;
            }
        }
    }
}