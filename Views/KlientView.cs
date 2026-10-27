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
            Console.WriteLine(numer +") Paliwo: " + paliwo.NazwaPaliwa);
            Console.WriteLine("Cena za litr: " + paliwo.CenaZaLitr + " zł");
            Console.WriteLine("Dostępna ilość: " + paliwo.DostepnaIloscPaliwa + " l");
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
    public static decimal PobierzIlośćLitrów()
    {
        while(true)
        {
          Console.Write("Podaj liczbę litrów: ");
            string tekst = Console.ReadLine()?? "";
            decimal ilość;
            bool liczba = decimal.TryParse(tekst,out ilość);
            if(liczba &&ilość>0)
            {
                return ilość;
            }
            Console.WriteLine("Niepoprawna ilość! Spróbuj ponownie");  
        } 
    }
    public static string PotwierdzTankowanie()
    {
        while(true)
        {
            Console.WriteLine("Czy chcesz potwierdzić tankowanie ?");
            Console.WriteLine("1) Tak,potwierdzam");
            Console.WriteLine("0) Nie, anuluj");
            Console.Write("Twój wybór: ");
            string wybór = Console.ReadLine() ?? "";
            if(wybór =="1" || wybór =="0")
            {
                return wybór;
            }
            Console.WriteLine("Niepoprawny wybór ! Spróbuj ponownie");
        }
    }
    public static void PokazKoszyk(List<PozycjaKoszyka>pozycje,decimal suma)
    {
        Console.WriteLine();
        Console.WriteLine("======= TWÓJ KOSZYK =======");
        if(pozycje.Count ==0)
        {
            Console.WriteLine("Twój koszyk jest pusty");
            return;
        }
        foreach(PozycjaKoszyka pozycja in pozycje)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Produkt: " + pozycja.Nazwa);
            Console.WriteLine("Ilość: " + pozycja.Ilość + " " + pozycja.Jednostka);
            Console.WriteLine("Cena jednostkowa: " + pozycja.CenaJednostkowa + " zł");
            Console.WriteLine("Wartość: " + (pozycja.Ilość * pozycja.CenaJednostkowa) + " zł");
        }
        Console.WriteLine("---------------------------");
        Console.WriteLine("Łącznie do zapłaty: " + suma + " zł");
    }
}
