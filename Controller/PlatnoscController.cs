using System.IO;

public class PlatnośćController
{
    private static readonly List<MetodaPlatnosci> metody = new()
    {
        MetodaPlatnosci.Gotowka,
        MetodaPlatnosci.Karta,
        MetodaPlatnosci.Blik
    };

    public static void UruchomPlatność(Koszyk koszyk, HistoriaTransakcji historia, ref string pinKarty, MagazynProduktow magazynProduktow)
    {
        if (koszyk.PobierzPozycje().Count == 0)
        {
            PlatnoscView.PokazKomunikat("Koszyk jest pusty");
            return;
        }

        var zamowioneProdukty = koszyk.PobierzPozycje().Where(p => p.Jednostka == "szt.").GroupBy(p => p.Nazwa, StringComparer.OrdinalIgnoreCase).Select(grupa => new
        {
            Nazwa = grupa.Key,
            Ilosc = grupa.Sum(p => p.Ilość)
        }).ToList();

        foreach (var zamowienie in zamowioneProdukty)
        {
            Produkt? produkt = magazynProduktow.PobierzProdukty().FirstOrDefault(p => p.Nazwa.Equals(zamowienie.Nazwa, StringComparison.OrdinalIgnoreCase));

            if (produkt == null || zamowienie.Ilosc <= 0 || zamowienie.Ilosc != decimal.Truncate(zamowienie.Ilosc) || zamowienie.Ilosc > produkt.DostepnaIlosc)
            {
                PlatnoscView.PokazKomunikat("Produkt " + zamowienie.Nazwa + " jest niedostępny w wymaganej ilości!");
                return;
            }
        }

        decimal kwota = koszyk.ObliczWartoscKoszyka();
        PlatnoscView.PokazMenuPlatnosci(metody);
        string wybor = PlatnoscView.PobierzWyborPlatnosci();

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

        if (wybranaMetoda == MetodaPlatnosci.Blik)
        {
            Random random = new Random();
            int wygenerowanyKod = random.Next(100000, 1000000);

            string sciezkaBlik = Path.Combine(Directory.GetCurrentDirectory(), "Data", "blik.txt");
            string zawartoscPliku = $"=== TWÓJ TELEFON ===\nKOD BLIK: {wygenerowanyKod}\nWażny przez: 2 minuty\n====================";
            File.WriteAllText(sciezkaBlik, zawartoscPliku);

            PlatnoscView.PokazKomunikatBlikZTelefonu();

            string wpisanyKod = PlatnoscView.PobierzKodBlik();

            if (wpisanyKod == "0")
            {
                PlatnoscView.PokazKomunikat("Płatność Blik anulowana.");
                return;
            }

            if (wpisanyKod != wygenerowanyKod.ToString())
            {
                PlatnoscView.PokazKomunikat("Niepoprawny kod Blik! Transakcja odrzucona.");
                return;
            }

            PlatnoscView.PokazKomunikat("Kod Blik zaakceptowany. Przetwarzanie płatności...");
        }

        if (wybranaMetoda == MetodaPlatnosci.Karta)
        {
            if (pinKarty == "BRAK")
            {
                PlatnoscView.PokazKomunikat("Brak zarejestrowanej karty!");
                return;
            }

            if (pinKarty == "ODRZUCONA")
            {
                PlatnoscView.PokazKomunikat("Zablokowana karta!");
                return;
            }

            int proby = 3;
            while (proby > 0)
            {
                string wpisanyPin = KlientView.PobierzPinPrzyStarcie();

                if (wpisanyPin == "0")
                {
                    PlatnoscView.PokazKomunikat("Płatność kartą anulowano.");
                    return;
                }

                if (wpisanyPin == pinKarty)
                {
                    PlatnoscView.PokazKomunikat("PIN poprawny. Autoryzacja transakcji...");
                    break;
                }
                else
                {
                    proby--;

                    if (proby > 0)
                    {
                        PlatnoscView.PokazKomunikat($"Błędny PIN! Pozostałe próby: {proby}");
                    }
                    else
                    {
                        pinKarty = "ODRZUCONA";
                        PlatnoscView.PokazKomunikat("KARTA ZABLOKOWANA! Przekroczono limit prób.");
                        return;
                    }
                }
            }
        }

        if (wybranaMetoda == MetodaPlatnosci.Gotowka)
        {
            if (kwota > 15000m)
            {
                Console.WriteLine($"Kwota rachunku ({kwota} zł) przekracza maksymalny limit 15 000 zł!");
                Console.WriteLine("Wybierz inną metode płatności.");
                return;
            }
        }

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

            Dictionary<Produkt, int> poprzednieStany = new Dictionary<Produkt, int>();

            try
            {
                foreach (var zamowienie in zamowioneProdukty)
                {
                    Produkt produkt = magazynProduktow.PobierzProdukty().First(p => p.Nazwa.Equals( zamowienie.Nazwa, StringComparison.OrdinalIgnoreCase));
                    poprzednieStany[produkt] = produkt.DostepnaIlosc;
                    bool zakupUdany = SklepService.WykonajZakup(produkt, (int)zamowienie.Ilosc);

                    if (!zakupUdany)
                    {
                        throw new InvalidOperationException("Nie udało się zaktualizować magazynu.");
                    }
                }

                magazynProduktow.ZapiszProdukty();
                bool paragonZapisany = historia.DodajTransakcje(transakcja);
                koszyk.WyczyscKoszyk();
                PlatnoscView.PokazKomunikat("Płatność zakończona pomyślnie!");
                PlatnoscView.PokazKomunikat("Zapłacono: " + platnosc.Kwota + " zł");
                PlatnoscView.PokazKomunikat("Metoda płatności: " + platnosc.MetodaPlatnosci);

                if (!paragonZapisany)
                {
                    PlatnoscView.PokazKomunikat("Transakcja zapisana, ale nie udało się utworzyć paragonu TXT.");
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
            {
                foreach (var stan in poprzednieStany)
                {
                    stan.Key.DostepnaIlosc = stan.Value;
                }

                try
                {
                    magazynProduktow.ZapiszProdukty();
                }
                catch (IOException)
                {
                    PlatnoscView.PokazKomunikat("Nie udało się przywrócić pliku magazynu!");
                }
                catch (UnauthorizedAccessException)
                {
                    PlatnoscView.PokazKomunikat("Brak dostępu do pliku magazynu!");
                }

                PlatnoscView.PokazKomunikat("Nie udało się zakończyć zapisu transakcji.");
            }
        }
        else
        {
            PlatnoscView.PokazKomunikat("Nie udało się zrealizować płatności.");
        }
    }
}
