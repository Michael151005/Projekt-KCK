public class SklepView
{
    public static void PokazProdukty(List<Produkt> produkty)
    {
        Console.WriteLine();
        Console.WriteLine("===== SKLEP STACJI =====");

        for (int i = 0; i < produkty.Count; i++)
        {
            Produkt produkt = produkty[i];
            Console.WriteLine("-------------------------");
            Console.WriteLine((i + 1) + ") " + produkt.Nazwa);
            Console.WriteLine("Cena: " + produkt.Cena + " zł");
            Console.WriteLine("Dostępna ilość: " + produkt.DostepnaIlosc + " szt.");
        }

        Console.WriteLine("-------------------------");
        Console.WriteLine("0) Powrót");
    }
    
    public static string PobierzWyborProduktu()
    {   
        Console.Write("Wybierz produkt: ");
        return Console.ReadLine() ?? "";
    }     

    public static int PobierzIlosc(int dostepnaIlosc)
    {
        while (true)
        {
            Console.Write("Podaj liczbę sztuk: ");
            string wejscie = Console.ReadLine() ?? "";

            if (wejscie == "0") return 0;

            if (!int.TryParse(wejscie, out int ilosc))
            {
                Console.WriteLine("Podaj poprawną liczbę sztuk!");
                continue;
            }

            if (ilosc < 0)
            {
                Console.WriteLine("Liczba sztuk nie może być ujemna!");
                continue;
            }

            if (ilosc > dostepnaIlosc) 
            {
                Console.WriteLine($"Brak takiej ilości na magazynie! Dostępne {dostepnaIlosc} szt.");
                continue;
            }

            return ilosc;
        }
    }
    
    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }
}