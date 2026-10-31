using System;
using System.Collections.Generic;

public class PracownikController
{
    private static MagazynPaliw magazynPaliw = WspolneMagazyny.Paliwa;
    private static MagazynProduktow magazynProduktow = WspolneMagazyny.Produkty;

    private static List<OpcjaPracownika> opcje = new List<OpcjaPracownika>
    {
        OpcjaPracownika.StanMagazynuPaliw,
        OpcjaPracownika.StanMagazynuSklepu,
        OpcjaPracownika.DostawaPaliwa,
        OpcjaPracownika.DostawaProduktu,
        OpcjaPracownika.ZmianaHasla
    };

    public static void UruchomPanelPracownika()
    {
        while (true)
        {
            string wybor = PracownikView.PobierzWyborMenuPracownika(opcje);

            if (wybor == "0")
            {
                PracownikView.PokazKomunikat("Wylogowano z Panelu Pracownika...");
                break;
            }

            ObsluzWyborPracownika(wybor);
        }
    }

    public static void ObsluzWyborPracownika(string wybor)
    {
        if (!int.TryParse(wybor, out int numer) || numer < 1 || numer > opcje.Count)
        {
            PracownikView.PokazKomunikat("Nieprawidłowa opcja!");
            return;
        }

        OpcjaPracownika wybranaOpcja = opcje[numer - 1];

        switch (wybranaOpcja)
        {
            case OpcjaPracownika.StanMagazynuPaliw:
                ObslugaStanuMagazynuPaliw();
                break;
            
            case OpcjaPracownika.StanMagazynuSklepu:
                ObslugaStanuMagazynuSklepu();
                break;
            
            case OpcjaPracownika.DostawaPaliwa:
                ObslugaDostawyPaliwa();
                break;

            case OpcjaPracownika.DostawaProduktu:
                ObslugaDostawyProduktu();
                break;
            
            case OpcjaPracownika.ZmianaHasla:
                ObslugaZmianyHasla();
                break;
        }
    }


    private static void ObslugaStanuMagazynuPaliw()
    {
        Console.Clear();
        List<Paliwo> paliwa = magazynPaliw.PobierzPaliwa();
        KlientView.PokazPaliwa(paliwa);
        UIHelper.PoczekajNaKlawisz();
    }

    private static void ObslugaStanuMagazynuSklepu()
    {
        Console.Clear();
        List<Produkt> produkty = magazynProduktow.PobierzProdukty();
        SklepView.PokazProdukty(produkty);
        UIHelper.PoczekajNaKlawisz();
    }

    private static void ObslugaDostawyPaliwa()
    {
        List<Paliwo> listaPaliw = magazynPaliw.PobierzPaliwa();
        KlientView.PokazPaliwa(listaPaliw);
        string wyborPaliwa = KlientView.PobierzWyborPaliwa(listaPaliw.Count);

        if (wyborPaliwa == "0")
        {
            PracownikView.PokazKomunikat("Anulowano dostawę paliwa.");
            return;
        }

        int indeksPaliwa = int.Parse(wyborPaliwa) - 1;
        Paliwo wybranePaliwo = listaPaliw[indeksPaliwa];

        decimal iloscLitrow = PracownikView.PobierzIloscLitrow();
        if (iloscLitrow == 0) return;

        wybranePaliwo.DostepnaIloscPaliwa += iloscLitrow;
        magazynPaliw.ZapiszPaliwa();
        PracownikView.PokazKomunikat($"Dodano {iloscLitrow:F2} l do {wybranePaliwo.NazwaPaliwa}. Nowy stan: {wybranePaliwo.DostepnaIloscPaliwa:F2} l.");
    }

    private static void ObslugaDostawyProduktu()
    {
        List<Produkt> listaProduktow = magazynProduktow.PobierzProdukty();
        SklepView.PokazProdukty(listaProduktow);
        string wyborProduktu = SklepView.PobierzWyborProduktu();

        if (wyborProduktu == "0")
        {
            PracownikView.PokazKomunikat("Anulowano dostawę produktu.");
            return;
        }

        if (!int.TryParse(wyborProduktu, out int numerProduktu) || numerProduktu < 1 || numerProduktu > listaProduktow.Count)
        {
            PracownikView.PokazKomunikat("Nieprawidłowy wybór produktu!");
            return;
        }

        Produkt wybranyProdukt = listaProduktow[numerProduktu - 1];
        int iloscSztuk = PracownikView.PobierzIloscSztuk(wybranyProdukt.DostepnaIlosc);

        if (iloscSztuk == 0) return;

        wybranyProdukt.DostepnaIlosc += iloscSztuk;
        magazynProduktow.ZapiszProdukty();
        PracownikView.PokazKomunikat($"Dodano {iloscSztuk} szt. produktu {wybranyProdukt.Nazwa}. Nowy stan: {wybranyProdukt.DostepnaIlosc} szt.");
    }

    private static void ObslugaZmianyHasla()
    {
        string noweHaslo = PracownikView.PobierzNoweHaslo();
                    
        if (string.IsNullOrWhiteSpace(noweHaslo))
        {
            UIHelper.PokazBlad("Hasło nie może być puste!");
            return;
        }
                    
        AutoryzacjaService.ZmienHaslo("Pracownik", noweHaslo);
        UIHelper.PokazSukces("Hasło pracownika zostało pomyślnie zmienione!");
    }
}