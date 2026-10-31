public class PlatnoscView
{
    public static string PobierzWyborMenuPlatnosci(List<MetodaPlatnosci> metody)
    {
        string[] opcjeNapisy = new string[metody.Count + 1];

        for (int i = 0; i < metody.Count; i++)
        {
            opcjeNapisy[i] = metody[i] switch
            {
                MetodaPlatnosci.Gotowka => "Płatność Gotówką",
                MetodaPlatnosci.Karta => "Karta płatnicza",
                MetodaPlatnosci.Blik => "Płatność BLIK",
                _ => metody[i].ToString()
            };
        }

        opcjeNapisy[metody.Count] = "Anuluj płatność";
        int indeks = UIHelper.PokazInteraktywneMenu("WYBIERZ METODĘ PŁATNOŚCI", opcjeNapisy);

        if (indeks == metody.Count) return "0";
        return (indeks + 1).ToString();
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
