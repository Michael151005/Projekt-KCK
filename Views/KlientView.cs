public class KlientView
{
    public static void PokazMenuOpcji(List<OpcjaKlienta> opcje)
    {
        Console.WriteLine("===== MENU KLIENTA =====");

        for (int i = 0; i < opcje.Count; i++)
        {
            Console.WriteLine((i + 1) + ") " + opcje[i]);
        }

        Console.WriteLine("0) Powrót");
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

    public static void PokazPaliwa(List<Paliwo> paliwa)
    {
        int numer = 1;
        Console.WriteLine("======= DOSTĘPNE PALIWA ======");

        foreach (Paliwo paliwo in paliwa)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine(numer + ") Paliwo: " + paliwo.NazwaPaliwa);
            Console.WriteLine($"Cena za litr: {paliwo.CenaZaLitr:F2} zł");
            Console.WriteLine($"Dostępna ilość: {paliwo.DostepnaIloscPaliwa:F2} l");
            numer++;
        }

        Console.WriteLine("------------------------------");
        Console.WriteLine("0) Powrót");
        Console.WriteLine("------------------------------");
    }

    public static string PobierzWyborPaliwa(int liczbaPaliw)
    {
        while (true)
        {
            Console.Write("Wybierz paliwo: ");
            string? wybor = Console.ReadLine();

            if (wybor == null)
            {
                return "0";
            }

            if (wybor == "0")
            {
                return wybor;
            }

            if (int.TryParse(wybor, out int numer) && numer >= 1 && numer <= liczbaPaliw)
            {
                return wybor;
            }

            PokazKomunikat("Niepoprawna opcja! Wybierz ponownie.");
        }
    }

    public static decimal PobierzIloscLitrow()
    {
        while(true)
        {
            Console.Write("Podaj liczbę litrów: ");
            string tekst = Console.ReadLine()?? "";

            if (tekst == "0") return 0;

            if (decimal.TryParse(tekst, out decimal ilość) && ilość > 0)  
            {
                return Math.Round(ilość, 2);
            }
            else 
            {
                Console.WriteLine("Niepoprawna ilość! Podaj poprawną wartość liczbową (użyj przecinka dla ułamków).");
            }
        } 
    }

    public static string PotwierdzTankowanie()
    {
        while(true)
        {
            Console.WriteLine("Czy chcesz potwierdzić tankowanie?");
            Console.WriteLine("1) Tak, potwierdzam");
            Console.WriteLine("0) Nie, anuluj");
            Console.Write("Twój wybór: ");
            string wybór = Console.ReadLine() ?? "";

            if (wybór == "1" || wybór == "0")
            {
                return wybór;
            }

            Console.WriteLine("Niepoprawny wybór! Spróbuj ponownie.");
        }
    }

    public static void PokazKoszyk(List<PozycjaKoszyka>pozycje, decimal suma)
    {
        Console.WriteLine();
        Console.WriteLine("======= TWÓJ KOSZYK =======");

        if (pozycje.Count == 0)
        {
            Console.WriteLine("Twój koszyk jest pusty");
            return;
        }

        foreach (PozycjaKoszyka pozycja in pozycje)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Produkt: " + pozycja.Nazwa);
            Console.WriteLine("Ilość: " + pozycja.Ilość + " " + pozycja.Jednostka);
            Console.WriteLine($"Cena jednostkowa: {pozycja.CenaJednostkowa:F2} zł");
            Console.WriteLine($"Wartość: {pozycja.Wartosc:F2} zł");
        }

        Console.WriteLine("---------------------------");
        Console.WriteLine($"Łącznie do zapłaty: {suma:F2} zł");
    }

    public static string PobierzPinPrzyStarcie()
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write("Podaj 4-cyfrowy PIN do swojej karty płatniczej (wpisz 0, jeśli nie masz przy sobie karty): ");
            string pin = InputHelper.PobierzUkrytyTekst();

            if (pin == "0")
            {
                return "BRAK";
            }

            if (pin.Length == 4 && int.TryParse(pin, out _))
            {
                return pin;
            }

            Console.WriteLine("Niepoprawny PIN! PIN musi składać się z dokładnie 4 cyfr.");
        }
    }
}
