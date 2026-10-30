using System;
using System.Collections.Generic;

public class PracownikView
{
    public static void PokazOpcjeMenu(List<OpcjaPracownika> opcje)
    {
        Console.WriteLine();
        Console.WriteLine("===== PANEL PRACOWNIKA =====");

        for (int i = 0; i < opcje.Count; i++)
        {
            string nazwa = opcje[i] switch
            {
                OpcjaPracownika.StanMagazynuPaliw => "Stan magazynu paliw",
                OpcjaPracownika.StanMagazynuSklepu => "Stan magazynu sklepu",
                OpcjaPracownika.DostawaPaliwa => "Domów paliwa",
                OpcjaPracownika.DostawaProduktu => "Domów produkty do sklepu",
                OpcjaPracownika.ZmianaHasla => "Zmień hasło pracownika",
                _ => opcje[i].ToString()
            };

            Console.WriteLine((i + 1) + ") " + nazwa);
        }

        Console.WriteLine("0) Powrót do menu głównego");
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

    public static decimal PobierzIloscLitrow()
    {
        while (true)
        {
            Console.Write("Podaj ilość dostarczonych litrów: ");
            string wejscie = Console.ReadLine() ?? "";
            
            if (wejscie == "0") return 0;
            
            if (decimal.TryParse(wejscie, out decimal ilosc) && ilosc > 0)
            {
                return Math.Round(ilosc, 2);
            }

            Console.WriteLine("Podaj poprawną liczbę większa od zera!");
        }
    }
    
    public static int PobierzIloscSztuk(int dostepnaIlosc)
    {
        while (true)
        {
            Console.Write("Podaj ilość dostarczonych sztuk: ");
            string wejscie = Console.ReadLine() ?? "";
            
            if (wejscie == "0") return 0;
            
            if (int.TryParse(wejscie, out int ilosc) && ilosc > 0)
            {
                return ilosc;
            }

            Console.WriteLine("Podaj poprawną liczbę całkowitą większą od zera!");
        }
    }

    public static string PobierzNoweHaslo()
    {
        Console.Write("Podaj nowe hasło pracownika: ");
        return InputHelper.PobierzUkrytyTekst();
    }
}