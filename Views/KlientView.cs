
public class KlientView
{
    public static void MenuKlienta()
    {
        Console.WriteLine();
        Console.WriteLine("======= MENU KLIENTA =======");
        Console.WriteLine("1) Tankowanie");
        Console.WriteLine("2) Sklep");
        Console.WriteLine("3) Koszyk i platnosc");
        Console.WriteLine("4) Paragon");
        Console.WriteLine("0) Powrot do menu glownego");
    }
    public static string PobierzWybor()
    {
        while (true)
        {
            Console.Write("Twoj wybor: ");
            string? wybor = Console.ReadLine();

            if (wybor == null)
            {
                return "0";
            }

            if (wybor == "1" || wybor == "2" ||wybor == "3" || wybor == "4" ||wybor == "0")
            {
                return wybor;
            }

            PokazKomunikat("Niepoprawna opcja! Wybierz ponownie.");
        }
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
    public static string PobierzWyborPaliwa()
    {
        while (true)
        {
            Console.Write("Wybierz paliwo: ");
            string? wybor = Console.ReadLine();

            if (wybor == null)
            {
                return "0";
            }

            if (wybor == "1" || wybor == "2" || wybor == "3" || wybor == "4" || wybor == "0")
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
}
