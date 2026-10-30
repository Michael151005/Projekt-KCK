public class PlatnoscView
{
    public static void PokazMenuPlatnosci(List<MetodaPlatnosci> metody)
    {
        Console.WriteLine();
        Console.WriteLine("===== METODA PŁATNOŚCI =====");
        
        for (int i = 0; i < metody.Count; i++)
        {
            string nazwa = metody[i] switch
            {
                MetodaPlatnosci.Gotowka => "Gotówka",
                MetodaPlatnosci.Karta => "Karta płatnicza",
                MetodaPlatnosci.Blik => "BLIK",
                _ => metody[i].ToString()
            };

            Console.WriteLine($"{i + 1}) {nazwa}");
        }

        Console.WriteLine("0) Powrót do koszyka");
    }

    public static string PobierzWyborPlatnosci()
    {
        Console.Write("Twój wybór: ");
        return Console.ReadLine() ?? "0";
    }

    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }

    public static void PokazKomunikatBlikZTelefonu()
    {
        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine("[Telefon] Wygenerowano nowy kod BLIK!");
        Console.WriteLine("Otwórz plik 'Data/blik.txt', aby odczytać kod.");
        Console.WriteLine("==============================");
        Console.WriteLine();
    }

    public static string PobierzKodBlik()
    {
        Console.Write("Wpisz kod Blik: ");
        return Console.ReadLine() ?? "";
    }
}
