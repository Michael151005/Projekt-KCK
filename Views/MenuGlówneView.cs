public class MenuGlowneView
{
    public static void WyswietlMenu(List<OpcjaMenuGlownego> opcje)
    {
        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine("STACJA BENZYNOWA");
        Console.WriteLine("==============================");
        Console.WriteLine("Wybierz swoją rolę:");

        for (int i = 0; i < opcje.Count; i++)
        {
            string nazwa = opcje[i] switch
            {
                OpcjaMenuGlownego.Klient => "Klient",
                OpcjaMenuGlownego.Pracownik => "Pracownik stacji",
                OpcjaMenuGlownego.Kierownik => "Kierownik stacji",
                _ => opcje[i].ToString() 
            };

            Console.WriteLine($"{i + 1}) {nazwa}");
        }
        
        Console.WriteLine("0) Wyjście");
    }

    public static string PobierzWybor()
    {
        Console.Write("Twój wybór: ");
        return Console.ReadLine() ?? "0";
    }

    public static void PokazKomunikat(string komunikat)
    {
        Console.WriteLine(komunikat);
    }

    public static string PobierzHaslo()
    {
        Console.WriteLine();
        Console.Write("Podaj hasło dostępu: ");
        return InputHelper.PobierzUkrytyTekst();
    }
}
