
public class MenuGlowneView
{
    public static void WyswietlMenu()
    {
        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine("STACJA BENZYNOWA");
        Console.WriteLine("==============================");
        Console.WriteLine("Wybierz swoja role:");
        Console.WriteLine("1) Klient");
        Console.WriteLine("2) Pracownik stacji");
        Console.WriteLine("3) Kierownik stacji");
        Console.WriteLine("0) Wyjscie");
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

            if (wybor == "1" || wybor == "2" ||
                wybor == "3" || wybor == "0")
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
}
