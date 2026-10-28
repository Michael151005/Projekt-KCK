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
            Console.WriteLine("Produkt: "+ pozycja.Nazwa);
            Console.WriteLine("Ilość : "+ pozycja.Ilość+ " " + pozycja.Jednostka);
            Console.WriteLine("Pozycja jednostkowa:" +pozycja.CenaJednostkowa+ " zł");
            decimal wartosc = pozycja.Ilość * pozycja.CenaJednostkowa;
            Console.WriteLine("Wartość : "+ wartosc+ " zł");
        }
        Console.WriteLine("-----------------------");
        Console.WriteLine("Suma: "+ transakcja.Kwota+ " zł");
        Console.WriteLine("Metoda płatności: "+transakcja.MetodaPlatnosci);
        Console.WriteLine("=======================");
    }
}