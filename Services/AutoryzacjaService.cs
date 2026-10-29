using System;
using System.IO;
using System.Text;
using System.Text.Json;

public class AutoryzacjaService
{
    private class HaslaModel
    {
        public string HasloPracownikaBase64 { get; set; } = "";
        public string HasloKierownikaBase64 { get; set; } = "";
    }

    private static string sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Data", "hasla.json");

    public static void ZainicjujHasla()
    {
        if (!File.Exists(sciezka))
        {
            HaslaModel domyslne = new HaslaModel
            {
                HasloPracownikaBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("1111")),
                HasloKierownikaBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("9999"))
            };

            string json = JsonSerializer.Serialize(domyslne, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sciezka, json);
        }
    }

    public static bool WeryfikujHaslo(string rola, string wpisaneHaslo)
    {
        if (!File.Exists(sciezka))
        {
            ZainicjujHasla();
        }

        string json = File.ReadAllText(sciezka);
        HaslaModel hasla = JsonSerializer.Deserialize<HaslaModel>(json)!;

        string zakodowaneWpisane = Convert.ToBase64String(Encoding.UTF8.GetBytes(wpisaneHaslo));

        if (rola == "Pracownik") 
        {
            return zakodowaneWpisane == hasla.HasloPracownikaBase64;
        }
        else if (rola == "Kierownik") 
        {
            return zakodowaneWpisane == hasla.HasloKierownikaBase64;
        }

        return false;
    }

    public static void ZmienHaslo(string rola, string noweHaslo)
    {
        if (!File.Exists(sciezka))
        {
            ZainicjujHasla();
        }

        string json = File.ReadAllText(sciezka);
        HaslaModel hasla = JsonSerializer.Deserialize<HaslaModel>(json)!;

        string zakodowaneNowe = Convert.ToBase64String(Encoding.UTF8.GetBytes(noweHaslo));

        if (rola == "Pracownik") 
        {
            hasla.HasloPracownikaBase64 = zakodowaneNowe;
        }
        else if (rola == "Kierownik") 
        {
            hasla.HasloKierownikaBase64 = zakodowaneNowe;
        }

        string nowyJson = JsonSerializer.Serialize(hasla, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(sciezka, nowyJson);
    }
}