public class SklepView
{
    public static void PokazProdukty(List<Produkt> produkty)
    {
        Console.WriteLine();
        Console.WriteLine("===== SKLEP STACJI =====");
        for(int i=0;i<produkty.Count;i++)
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
    public static int PobierzIlosc()
    {
        while (true)
        {
            Console.Write("Podaj liczbę sztuk: ");
            if (int.TryParse(Console.ReadLine(), out int ilosc)
                && ilosc > 0)
            {
                return ilosc;
            }
            Console.WriteLine("Podaj poprawną liczbę sztuk!");
        }
    }
    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }
}