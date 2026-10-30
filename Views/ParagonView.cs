public class ParagonView
{
    public static void PokazParagon(Transakcja transakcja)
    {
        Console.WriteLine();
        Console.WriteLine("======= PARAGON =======");
        Console.WriteLine("Data: " + transakcja.DataTransakcji);

        foreach (PozycjaKoszyka pozycja in transakcja.Pozycje)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Produkt: " + pozycja.Nazwa);
            Console.WriteLine("Ilość: " + pozycja.Ilość + " " + pozycja.Jednostka);
            Console.WriteLine($"Cena jednostkowa: {pozycja.CenaJednostkowa:F2} zł");
            Console.WriteLine($"Wartość: {pozycja.Wartosc:F2} zł");
        }
        
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Suma: {transakcja.Kwota:F2} zł");
        Console.WriteLine("Metoda płatności: " + transakcja.MetodaPlatnosci);
        Console.WriteLine("=======================");
    }
}