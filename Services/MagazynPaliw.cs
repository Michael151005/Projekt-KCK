using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class MagazynPaliw
{
    private List<Paliwo> paliwa;
    
    public MagazynPaliw()
    {
        string sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Data", "paliwa.json");

        if (File.Exists(sciezka))
        {
            string json = File.ReadAllText(sciezka);
            paliwa = JsonSerializer.Deserialize<List<Paliwo>>(json) ?? new List<Paliwo>();
        }
        else
        {
            paliwa = new List<Paliwo>();
            Console.WriteLine("UWAGA: Nie znaleziono pliku Data/paliwa.json!");
        }
    }

    public List<Paliwo> PobierzPaliwa()
    {
        return paliwa;
    }

    public void ZapiszPaliwa()
    {
        string sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Data", "paliwa.json");
        string json = JsonSerializer.Serialize(paliwa, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(sciezka, json);
    }
}