public class HistoriaTransakcji
{
    private List<Transakcja> transakcje = new List<Transakcja>();

    public void DodajTransakcje(Transakcja transakcja)
    {
        transakcje.Add(transakcja);
        ZapiszParagonDoPliku(transakcja);
    }

    public List<Transakcja> PobierzTransakcje()
    {
        return transakcje;
    }

    private void ZapiszParagonDoPliku(Transakcja transakcja) 
    {
        string folderParagon = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Paragony");

        if (!Directory.Exists(folderParagon))
        {
            Directory.CreateDirectory(folderParagon);
        }

        string nazwaPliku = $"Paragon_{transakcja.DataTransakcji:yyyy-MM-dd_HH-mm-ss}.txt";
        string sciezkaCalkowita = Path.Combine(folderParagon, nazwaPliku);

        using (StreamWriter writer = new StreamWriter(sciezkaCalkowita))
        {
            writer.WriteLine("==============================");
            writer.WriteLine("       STACJA BENZYNOWA       ");
            writer.WriteLine("==============================");
            writer.WriteLine($"Data: {transakcja.DataTransakcji:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine($"Metoda płatności: {transakcja.MetodaPlatnosci}");
            writer.WriteLine("------------------------------");
            writer.WriteLine("POZYCJE:");

            foreach (PozycjaKoszyka pozycja in transakcja.Pozycje)
            {
                decimal wartosc = Math.Round(pozycja.Ilość * pozycja.CenaJednostkowa, 2);
                writer.WriteLine($"{pozycja.Nazwa} | {pozycja.Ilość} {pozycja.Jednostka} x {pozycja.CenaJednostkowa:F2} zl = {wartosc:F2} zl");
            }

            writer.WriteLine("------------------------------");
            writer.WriteLine($"Suma: {transakcja.Kwota:F2} zł");
            writer.WriteLine("==============================");
            writer.WriteLine("     Dziękujemy za zakupy!    ");
        }
    }
}