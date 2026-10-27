public class PlatnoscView
{
    public static void PokazMenuPlatnosci()
    {
        Console.WriteLine();
        Console.WriteLine("===== METODA PŁATNOŚCI =====");
        Console.WriteLine();
        Console.WriteLine("1) Gotówka");
        Console.WriteLine("2) Karta Płatnicza");
        Console.WriteLine("3) BLIK");
        Console.WriteLine("0 Powrót do koszyka");
        Console.WriteLine();
    }
    public static string PobierzWybórPłatności()
    {
        while (true)
        {
            Console.Write("Twoj wybor: ");
            string? wybor = Console.ReadLine();

            if (wybor == null)
            {
                return "0";
            }

            if (wybor == "1" || wybor == "2" ||wybor == "3" ||wybor == "0")
            {
                return wybor;
            }
            Console.WriteLine("Niepoprawna opcja! Wybierz ponownie.");
        }
    }
    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }
}