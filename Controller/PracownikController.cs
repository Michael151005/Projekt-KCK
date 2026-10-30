using System;
using System.Collections.Generic;

public class PracownikController
{
    private static MagazynPaliw magazynPaliw = new MagazynPaliw();
    private static MagazynProduktow magazynProduktow = new MagazynProduktow();

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
            PracownikView.PokazOpcjeMenu(opcje);
            string wybor = PracownikView.PobierzWybor();

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
                List<Paliwo> paliwa = magazynPaliw.PobierzPaliwa();
                KlientView.PokazPaliwa(paliwa);
                break;
            
            case OpcjaPracownika.StanMagazynuSklepu:
                List<Produkt> produkty = magazynProduktow.PobierzProdukty();
                SklepView.PokazProdukty(produkty);
                break;
            
            case OpcjaPracownika.DostawaPaliwa:
                List<Paliwo> listaPaliw = magazynPaliw.PobierzPaliwa();
                KlientView.PokazPaliwa(listaPaliw);
                string wyborPaliwa = KlientView.PobierzWyborPaliwa(listaPaliw.Count);

                if (wyborPaliwa == "0")
                {
                    PracownikView.PokazKomunikat("Anulowano dostawę paliwa.");
                    break;
                }

                int indeksPaliwa = int.Parse(wyborPaliwa) - 1;
                Paliwo wybranePaliwo = listaPaliw[indeksPaliwa];

                decimal iloscLitrow = PracownikView.PobierzIloscLitrow();
                if (iloscLitrow == 0) break;

                wybranePaliwo.DostepnaIloscPaliwa += iloscLitrow;
                magazynPaliw.ZapiszPaliwa();
                PracownikView.PokazKomunikat($"Dodano {iloscLitrow:F2} l do {wybranePaliwo.NazwaPaliwa}. Nowy stan: {wybranePaliwo.DostepnaIloscPaliwa:F2} l.");
                break;

            case OpcjaPracownika.DostawaProduktu:
                List<Produkt> listaProduktow = magazynProduktow.PobierzProdukty();
                SklepView.PokazProdukty(listaProduktow);
                string wyborProduktu = SklepView.PobierzWyborProduktu();

                if (wyborProduktu == "0")
                {
                    PracownikView.PokazKomunikat("Anulowano dostawę produktu.");
                }

                if (!int.TryParse(wyborProduktu, out int numerProduktu) || numerProduktu < 1 || numerProduktu > listaProduktow.Count)
                {
                    PracownikView.PokazKomunikat("Nieprawidłowy wybór produktu!");
                    break;
                }

                Produkt wybranyProdukt = listaProduktow[numerProduktu - 1];
                int iloscSztuk = PracownikView.PobierzIloscSztuk(wybranyProdukt.DostepnaIlosc);

                if (iloscSztuk == 0) break;

                wybranyProdukt.DostepnaIlosc += iloscSztuk;
                magazynProduktow.ZapiszProdukty();
                PracownikView.PokazKomunikat($"Dodano {iloscSztuk} szt. produktu {wybranyProdukt.Nazwa}. Nowy stan: {wybranyProdukt.DostepnaIlosc} szt.");
                break;
            
            case OpcjaPracownika.ZmianaHasla:
                    string noweHaslo = PracownikView.PobierzNoweHaslo();
                    
                    if (string.IsNullOrWhiteSpace(noweHaslo))
                    {
                        PracownikView.PokazKomunikat("Hasło nie może być puste!");
                        break;
                    }
                    
                    AutoryzacjaService.ZmienHaslo("Pracownik", noweHaslo);
                    PracownikView.PokazKomunikat("Hasło pracownika zostało pomyślnie zmienione!");
                    break;
        }
    }
}