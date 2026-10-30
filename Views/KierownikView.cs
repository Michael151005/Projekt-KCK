public class KierownikView
{
    public static void PokazMenu(List<OpcjaKierownika> opcje)
    {
        Console.WriteLine();
        Console.WriteLine("===== PANEL KIEROWNIKA =====");

        for (int i = 0; i < opcje.Count; i++)
        {
            string nazwa = opcje[i] switch
            {
                OpcjaKierownika.StanMagazynu => "Stan magazynu",
                OpcjaKierownika.ZmianaCenPaliw => "Zmiana cen paliw",
                OpcjaKierownika.ZmianaCenProduktow => "Zarządzanie cen produktów",
                OpcjaKierownika.ZarzadzanieProduktami => "Zarządzanie produktami",
                OpcjaKierownika.RaportySprzedazy => "Raporty sprzedaży",
                OpcjaKierownika.ZmianaHasla => "Zmiana hasła",
                _ => opcje[i].ToString()
            };

            Console.WriteLine((i + 1) + ") " + nazwa);
        }

        Console.WriteLine("0) Wyloguj");
    }

    public static string PobierzWybor()
    {
        Console.Write("Twój wybór: ");
        return Console.ReadLine() ?? "";
    }

    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }

    public static void PobierzStanMagazynu(List<Paliwo> paliwa, List<Produkt> produkty)
    {
        Console.WriteLine();
        Console.WriteLine("===== STAN MAGAZYNU =====");
        Console.WriteLine();
        Console.WriteLine("--- PALIWA ---");

        foreach (Paliwo paliwo in paliwa)
        {
            Console.WriteLine("Paliwo: " + paliwo.NazwaPaliwa);
            Console.WriteLine("Dostępna ilość: " + paliwo.DostepnaIloscPaliwa + " l");
            Console.WriteLine("-----------------------");
        }
        
        Console.WriteLine();
        Console.WriteLine("--- PRODUKTY SKLEPOWE ---");

        foreach (Produkt produkt in produkty)
        {
            Console.WriteLine("Produkt: " + produkt.Nazwa);
            Console.WriteLine("Dostępna ilość: " + produkt.DostepnaIlosc + " szt.");
            Console.WriteLine("-----------------------");
        }
    }
}