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
            string wybor = KierownikView.PobierzWyborMenuKierownika(opcje);

            if (wybor == "0")
            {
                KlientView.PokazKomunikat("Wylogowano kierownika");
                return;
            }

            if (!int.TryParse(wybor, out int numer) || numer < 1 || numer > opcje.Count)
            {
                KierownikView.PokazKomunikat("Nieprawidłowa opcja!");
                continue;
            }

            OpcjaKierownika wybranaOpcja = opcje[numer - 1];

            switch(wybranaOpcja)
            {
                case OpcjaKierownika.StanMagazynu:
                    ObsluzStanMagazynu();
                    break;

                case OpcjaKierownika.ZmianaCenPaliw:
                    ObslugaZmianCenPaliw();
                    break;

                case OpcjaKierownika.ZmianaCenProduktow:
                    ObslugaZmianCenProduktow();
                    break;

                case OpcjaKierownika.DomawianieProduktow:
                    ObslugaDomawianiaProduktow();
                    break;

                case OpcjaKierownika.RaportySprzedazy:
                    ObslugaRaportowSprzedazy();
                    break;
                    
                case OpcjaKierownika.ZmianaHasla:
                    ObslugaZmianyHasla();
                    break;
            }
        }
    }

    private static void ObsluzStanMagazynu()
    {
        Console.Clear();
        List<Paliwo> paliwa = MagazynPaliw.PobierzPaliwa();
        List<Produkt> produkty = magazynProduktow.PobierzProdukty();
        KierownikView.PobierzStanMagazynu(paliwa, produkty);
        UIHelper.PoczekajNaKlawisz();
    }

    private static void ObslugaZmianCenPaliw()
    {
        List<Paliwo> paliwaDoZmiany = MagazynPaliw.PobierzPaliwa();
        KierownikView.PokazPaliwaDoZmianyCeny(paliwaDoZmiany);
        string wyborPaliwa = KierownikView.PobierzDane("Wybierz paliwo: ");

        if (wyborPaliwa == "0")
        {
            return;
        }
                    
        if (!int.TryParse(wyborPaliwa, out int numerPaliwa) || numerPaliwa < 1 || numerPaliwa > paliwaDoZmiany.Count)
        {
            UIHelper.PokazBlad("Nieprawidłowy wybór paliwa!");
            return;
        }
                    
        Paliwo wybranePaliwo = paliwaDoZmiany[numerPaliwa - 1];
        string wpisanaCena = KierownikView.PobierzDane("Podaj nową cenę za litr: ");
                    
        if (!decimal.TryParse(wpisanaCena, out decimal nowaCena))
        {
            UIHelper.PokazBlad("Nieprawidłowy format ceny!");
            return;
        }
                    
        bool zmieniono = KierownikService.ZmienCenePaliwa(wybranePaliwo,nowaCena,MagazynPaliw);
                    
        if (zmieniono)
        {
            UIHelper.PokazSukces("Cena paliwa została zmieniona.");
        }
        else
        {
            UIHelper.PokazBlad("Nie udało się zmienić ceny! Sprawdź cene lub zapis pliku.");
        }
    }

    private static void ObslugaZmianCenProduktow()
    {
        // KierownikView.PokazKomunikat("Wybrano zmianę cen produktów.");
        List<Produkt> produktyDoZmiany = magazynProduktow.PobierzProdukty();
        KierownikView.PokazProduktyDoZmianyCeny(produktyDoZmiany);
        string wyborProduktu = KierownikView.PobierzDane("Wybierz produkt: ");
                    
        if (wyborProduktu == "0")
        {
            return;
        }
                    
        if (!int.TryParse(wyborProduktu, out int numerProduktu) || numerProduktu < 1 || numerProduktu > produktyDoZmiany.Count)
        {
           UIHelper.PokazBlad("Nieprawidłowy wybór produktu!");
            return;
        }
                    
        Produkt wybranyProdukt = produktyDoZmiany[numerProduktu - 1];
        string wpisanaCenaProduktu = KierownikView.PobierzDane("Podaj nową cenę produktu: ");
                    
        if (!decimal.TryParse(wpisanaCenaProduktu, out decimal nowaCenaProduktu))
        {
            UIHelper.PokazBlad("Nieprawidłowy format ceny!");
            return;
        }

        bool zmienionoProdukt = KierownikService.ZmienCeneProduktu(wybranyProdukt, nowaCenaProduktu, magazynProduktow);
        
        if (zmienionoProdukt)
        {
            UIHelper.PokazSukces("Cena produktu została zmieniona.");
        }
        else
        {
            UIHelper.PokazBlad("Cena produktu musi być większa od zera!");
        }
    }

    private static void ObslugaDomawianiaProduktow()
    {
        // KierownikView.PokazKomunikat("Wybrano domawianie produktów.");
        List<Produkt> produktyDoDomowienia = magazynProduktow.PobierzProdukty();
        KierownikView.PokazProduktyDoDomowienia(produktyDoDomowienia);
        string wybierzProduktDoDomowienia = KierownikView.PobierzDane("Wybierz produkt: ");
                    
        if (wybierzProduktDoDomowienia == "0")
        {
            return;
        }
                    
        if (!int.TryParse(wybierzProduktDoDomowienia, out int numerDomawianegoProduktu) || numerDomawianegoProduktu < 1 || numerDomawianegoProduktu > produktyDoDomowienia.Count)
        {
            KierownikView.PokazKomunikat("Nieprawidłowy wybór produktu!");
            return;
        }

        Produkt produktDoDomowienia = produktyDoDomowienia[numerDomawianegoProduktu-1];
        string WpisanaIlosc = KierownikView.PobierzDane("Ile sztuk chcesz domówić?");
                    
        if (!int.TryParse(WpisanaIlosc, out int iloscDoDomowienia))
        {
            KierownikView.PokazKomunikat("Podaj poprawną liczbę sztuk!");
            return;
        }
                    
        bool domowiono = KierownikService.DomowProdukt(produktDoDomowienia,iloscDoDomowienia,magazynProduktow);
        
        if (domowiono)
        {
            KierownikView.PokazKomunikat("Produkty zostały domówione do magazynu.");
        }
        else
        {
            KierownikView.PokazKomunikat("Nieprawidłowa ilość produktów!");
        }
    }

    private static void ObslugaRaportowSprzedazy()
    {
        KierownikView.PokazKomunikat("Wybrano raporty sprzedaży.");
        HistoriaTransakcji historia = new HistoriaTransakcji();
        List<Transakcja> transakcje = historia.PobierzTransakcje();
                    
        if (transakcje.Count == 0)
        {
            KierownikView.PokazKomunikat("Brak transakcji do wygenerowania raportu!");
            return;
        }
                    
        RaportSprzedazy raport = RaportSprzedazyService.GenerujRaport(transakcje);
        Console.Clear();
        KierownikView.PokazRaportSprzedazy(raport);
        UIHelper.PoczekajNaKlawisz();
    }

    private static void ObslugaZmianyHasla()
    {
        string noweHaslo = PracownikView.PobierzNoweHaslo();
                    
        if (string.IsNullOrWhiteSpace(noweHaslo))
        {
            UIHelper.PokazBlad("Hasło nie może być puste!");
            return;
        }
                    
        AutoryzacjaService.ZmienHaslo("Kierownik", noweHaslo);
        UIHelper.PokazSukces("Hasło kierownika zostało pomyślnie zmienione!");
    }
}