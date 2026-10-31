using System.Collections.Generic;

public class MenuGlowneView
{
    public static string PobierzWyborMenuGlownego(List<OpcjaMenuGlownego> opcje)
    {
        string[] opcjeNapisy = new string[opcje.Count + 1];

        for (int i = 0; i < opcje.Count; i++)
        {
            opcjeNapisy[i] = opcje[i] switch
            {
                OpcjaMenuGlownego.Klient => "Zaloguj jako Klient",
                OpcjaMenuGlownego.Pracownik => "Zaloguj jako Pracownik stacji",
                OpcjaMenuGlownego.Kierownik => "Zaloguj jako Kierownik stacji",
                _ => opcje[i].ToString() 
            };
        }
        
        opcjeNapisy[opcje.Count] = "Wyjście z programu";

        int indeksWyboru = UIHelper.PokazInteraktywneMenu("WYBIERZ SWOJĄ ROLĘ", opcjeNapisy);

        if (indeksWyboru == opcje.Count)
        {
            return "0";
        }

        return (indeksWyboru + 1).ToString();
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
