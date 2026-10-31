using System;
using System.Collections.Generic;

public class PracownikView
{
    public static string PobierzWyborMenuPracownika(List<OpcjaPracownika> opcje)
    {
        string[] opcjeNapisy = new string[opcje.Count + 1];

        for (int i = 0; i < opcje.Count; i++)
        {
            opcjeNapisy[i] = opcje[i] switch
            {
                OpcjaPracownika.StanMagazynuPaliw => "Stan magazynu paliw",
                OpcjaPracownika.StanMagazynuSklepu => "Stan magazynu sklepu",
                OpcjaPracownika.DostawaPaliwa => "Zamów paliwo na stacje",
                OpcjaPracownika.DostawaProduktu => "Zamów produkty do sklepu",
                OpcjaPracownika.ZmianaHasla => "Zmień swoje hasło",
                _ => opcje[i].ToString()
            };
        }

        opcjeNapisy[opcje.Count] = "Wyloguj się";
        int indeks = UIHelper.PokazInteraktywneMenu("PANEL PRACOWNIKA", opcjeNapisy);

        if (indeks == opcje.Count) return "0";
        return (indeks + 1).ToString();
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

            Console.WriteLine("Podaj poprawną liczbę większą od zera!");
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