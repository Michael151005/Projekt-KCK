
using System.Collections;
using System.Text.Json;

public class HistoriaTransakcji
{
    private List<Transakcja> transakcje;
    private readonly string sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Data", "transakcje.json");
    public HistoriaTransakcji()
    {
        if (File.Exists(sciezka))
        {
            string json = File.ReadAllText(sciezka);

            transakcje = JsonSerializer.Deserialize<List<Transakcja>>(json)
                ?? new List<Transakcja>();
        }
        else
        {
            transakcje = new List<Transakcja>();
        }
    }
    public bool DodajTransakcje(Transakcja transakcja)
    {
        List<Transakcja> nowaHistoria = new List<Transakcja>(transakcje);
        nowaHistoria.Add(transakcja);

        try
        { 

            string json = JsonSerializer.Serialize(nowaHistoria, new JsonSerializerOptions { WriteIndented = true });
            string? folder = Path.GetDirectoryName(sciezka);

            if (folder != null)
            {
                Directory.CreateDirectory(folder);
            }

            string plikTymczasowy = sciezka + ".tmp";
            File.WriteAllText(plikTymczasowy, json);
            File.Move(plikTymczasowy, sciezka, true);

            transakcje.Add(transakcja);

            ZapiszParagonDoPliku(transakcja);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public List<Transakcja> PobierzTransakcje()
    {
        return transakcje;
    }
    private void ZapiszTransakcje()
    {
        string? folder = Path.GetDirectoryName(sciezka);

        if (folder != null)
        {
            Directory.CreateDirectory(folder);
        }
        string json = JsonSerializer.Serialize(transakcje,new JsonSerializerOptions{WriteIndented = true});
        File.WriteAllText(sciezka, json);
    }

    private void ZapiszParagonDoPliku(Transakcja transakcja)
    {
        string folderParagon = Path.Combine(Directory.GetCurrentDirectory(),"Data","Paragony");
        if (!Directory.Exists(folderParagon))
        {
            Directory.CreateDirectory(folderParagon);
        }
        string nazwaPliku =$"Paragon_{transakcja.DataTransakcji:yyyy-MM-dd_HH-mm-ss}.txt";

        string sciezkaCalkowita = Path.Combine(folderParagon,nazwaPliku);

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
                decimal wartosc = Math.Round(
                    pozycja.Ilość * pozycja.CenaJednostkowa, 2);

                writer.WriteLine(
                    $"{pozycja.Nazwa} | {pozycja.Ilość} {pozycja.Jednostka} x " +
                    $"{pozycja.CenaJednostkowa:F2} zl = {wartosc:F2} zł");
            }

            writer.WriteLine("------------------------------");
            writer.WriteLine($"Suma: {transakcja.Kwota:F2} zł");
            writer.WriteLine("==============================");
            writer.WriteLine("     Dziękujemy za zakupy!    ");
        }
    }
}
