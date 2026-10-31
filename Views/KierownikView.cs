public class KierownikView
{
    public static string PobierzWyborMenuKierownika(List<OpcjaKierownika> opcje)
    {
        string[] opcjeNapisy = new string[opcje.Count + 1];

        for (int i = 0; i < opcje.Count; i++)
        {
            opcjeNapisy[i] = opcje[i] switch
            {
                OpcjaKierownika.StanMagazynu => "Stan magazynu paliw i sklepu",
                OpcjaKierownika.ZmianaCenPaliw => "Zmiana cen paliw",
                OpcjaKierownika.ZmianaCenProduktow => "Zmiana cen produktów",
                OpcjaKierownika.DomawianieProduktow => "Domówianie produktów",
                OpcjaKierownika.RaportySprzedazy => "Raport sprzedaży",
                OpcjaKierownika.ZmianaHasla => "Zmień swoje hasło",
                _ => opcje[i].ToString()
            };
        }

        opcjeNapisy[opcje.Count] = "Wyloguj się";
        int indeks = UIHelper.PokazInteraktywneMenu("PANEL KIEROWNIKA", opcjeNapisy);

        if (indeks == opcje.Count) return "0";
        return (indeks + 1).ToString();
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
    
    public static void PokazPaliwaDoZmianyCeny(List<Paliwo> paliwa)
    {
        Console.WriteLine();
        Console.WriteLine("===== ZMIANA CEN PALIW =====");

        for (int i = 0; i < paliwa.Count; i++)
        {
            Console.WriteLine(
                (i + 1) + ") " +
                paliwa[i].NazwaPaliwa +
                $" - {paliwa[i].CenaZaLitr:F2} zł/l");
        }

        Console.WriteLine("0) Powrót");
    }

    public static string PobierzDane(string komunikat)
    {
        Console.Write(komunikat);
        return Console.ReadLine() ?? "";
    }

    public static void PokazProduktyDoZmianyCeny (List<Produkt> produkty)
    {
        Console.WriteLine();
        Console.WriteLine("===== ZMIANA CEN PRODUKTÓW =====");

        for (int i = 0; i < produkty.Count; i++)
        {
           Console.WriteLine((i+1) + ") " + produkty[i].Nazwa + $" - {produkty[i].Cena:F2} zł"); 
        }

        Console.WriteLine("0) Powrót");
    }

    public static void PokazProduktyDoDomowienia(List<Produkt> produkty)
    {
        Console.WriteLine();
        Console.WriteLine("===== DOMAWIANIE PRODUKTÓW =====");

        for (int i = 0; i < produkty.Count; i++)
        {
            Console.WriteLine((i+1) + ") " + produkty[i].Nazwa + " - stan " + produkty[i].DostepnaIlosc + " szt.");
        }
        
        Console.WriteLine("0) Powrót");
    }
    public static void PokazRaportSprzedazy(RaportSprzedazy raport)
    {
        Console.WriteLine();
        Console.WriteLine("===== RAPORT SPRZEDAŻY =====");
        Console.WriteLine("Liczba transakcji: " + raport.LiczbaTransakcji);
        Console.WriteLine($"Łączny przychód: {raport.LacznyPrzychod:F2} zł");
        Console.WriteLine("Sprzedane paliwo: " + raport.SprzedaneLitry + " l");
        Console.WriteLine("Sprzedane produkty: " + raport.SprzedaneSztuki + " szt.");
        Console.WriteLine();
        Console.WriteLine("--- SPRZEDAŻ PALIW ---");
        foreach (var paliwo in raport.SprzedanePaliwa)
        {
            Console.WriteLine(paliwo.Key + ": " + paliwo.Value + " l");
        }
        Console.WriteLine();
        Console.WriteLine("--- SPRZEDAŻ PRODUKTÓW ---");
        foreach (var produkt in raport.SprzedaneProdukty)
        {
            Console.WriteLine(produkt.Key + ": " + produkt.Value + " szt.");
        }
        Console.WriteLine("============================");
    }

    public static string PobierzNoweHaslo()
    {
        Console.Write("Podaj nowe hasło kierownika: ");
        return InputHelper.PobierzUkrytyTekst();
    }
}