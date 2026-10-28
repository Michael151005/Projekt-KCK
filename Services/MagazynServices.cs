using System.Text.Json;
public class MagazynProduktow
{
    private List<Produkt> produkty;
    public MagazynProduktow()
    {
        string sciezka = Path.Combine(Directory.GetCurrentDirectory(),"Data","produkty.json");
        string json = File.ReadAllText(sciezka);
        produkty = JsonSerializer.Deserialize<List<Produkt>>(json)?? new List<Produkt>();
    }
    public List<Produkt> PobierzProdukty()
    {
        return produkty;
    }
    public void ZapiszProdukty()
    {
        string sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Data", "produkty.json");
        string json = JsonSerializer.Serialize(produkty, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(sciezka, json);
    }
}